using System;
using System.Collections.Generic;
using Intuit.Platform.Client.Core;
using Intuit.Sb.Cdm;
using Intuit.Sb.Cdm.QB;

namespace IdsTest
{
    class Program
    {
        private const string IdSplitter = ":";

        private static string AppToken;
        private static string UserLogin;
        private static string UserPassword;
        private static string RealmId;
        private static string AppDbId;

        static void Main(string[] args)
        {
            if (args.Length < 5)
            {
                Console.WriteLine(@"Usage: IdsTest.exe <appToken> <login> <password> <realmId> <appDbId>");
                Console.ReadLine();
                return;
            }

            AppToken = args[0];
            UserLogin = args[1];
            UserPassword = args[2];
            RealmId = args[3];
            AppDbId = args[4];

            while (true)
            {
                Console.Write(@"IDS test>");
                var command = Console.ReadLine();

                if (string.IsNullOrEmpty(command))
                    continue;

                if (command.Trim().ToLower().CompareTo("exit") == 0 || command.Trim().ToLower().CompareTo("quit") == 0)
                    return;
                try
                {
                    RunCommand(command);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void RunCommand(string command)
        {
            command = command.Trim().ToLower();

            switch (command)
            {
                case "help":
                    ShowHelp();
                    return;
                case "show job":
                    ShowAllJobs();
                    return;
                case "show customertype":
                    ShowAllCustomerTypes();
                    return;
            }

            if (command.StartsWith("show job") && command.Split(' ').Length > 2)
            {
                var idStr = command.Split(' ')[2];
                ShowJob(idStr);
            }
            else if (command.StartsWith("show customertype") && command.Split(' ').Length > 2)
            {
                var idStr = command.Split(' ')[2];
                ShowCustomerType(idStr);
            }
            else if (command.StartsWith("update job") && command.Split(' ').Length > 3)
            {
                var jobIdStr = command.Split(' ')[2];
                var customerTypeIdStr = command.Split(' ')[3];
                UpdateJobCustomerType(jobIdStr, customerTypeIdStr);
            }
            else
            {
                Console.WriteLine(@"Incorrect command");
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine(@"====================================================");
            Console.WriteLine(@"available commands:");
            Console.WriteLine(@"'show job' - load and show all jobs with today's last changes");
            Console.WriteLine(@"'show customertype' - load and show all customer types");
            Console.WriteLine(@"'show job <id_str>' - load and show job with id = <id_str>");
            Console.WriteLine(@"'show customertype <id_str>' - load and show customer type with id = <id_str>");
            Console.WriteLine(@"'update job <job_id_str> <type_id_str>' - update job with id <job_id_str> to customer type <type_id_str>");
            Console.WriteLine(@"where <id_str> - IdType with format: '<DOMAIN>:<VALUE>'");
            Console.WriteLine(@"====================================================");
        }

        private static void UpdateJobCustomerType(string jobIdStr, string customerTypeIdStr)
        {
            var job = GetJob(ParseIdStr(jobIdStr));
            var customerType = GetCustomerType(ParseIdStr(customerTypeIdStr));
            var updatedJob = UpdateJob(job, customerType);
            ShowJobInfo(updatedJob);
        }

        private static void ShowCustomerType(string idStr)
        {
            var customerType = GetCustomerType(ParseIdStr(idStr));
            ShowCustomerTypeInfo(customerType);
        }

        private static void ShowCustomerTypeInfo(CustomerType customerType)
        {
            if (customerType == null)
            {
                Console.WriteLine(string.Format(
                    "CustomerType Info: [NULL]"));
            }
            else
            {
                Console.WriteLine(string.Format(
                    "CustomerType Info: [Id={0}, Name={1}]",
                    GetIdStr(customerType.Id), customerType.Name));
            }
        }

        private static void ShowJob(string idStr)
        {
            var job = GetJob(ParseIdStr(idStr));
            ShowJobInfo(job);
        }

        private static void ShowJobInfo(Job job)
        {
            if (job == null)
            {
                Console.WriteLine(string.Format(
                    "Job Info: [NULL]"));
            }
            else
            {
                Console.WriteLine(string.Format(
                    "Job Info: [Id={0}, Name={1}, CustomerTypeId={2}, CustomerTypeName={3}]",
                    GetIdStr(job.Id), job.Name, GetIdStr(job.CustomerTypeId), job.CustomerTypeName));
            }
        }

        private static void ShowAllCustomerTypes()
        {
            var customerTypes = GetCustomerTypes();
            foreach (var customerType in customerTypes)
            {
                ShowCustomerTypeInfo(customerType);
            }
        }

        private static void ShowAllJobs()
        {
            var jobs = GetLastDayJobs();
            foreach (var job in jobs)
            {
                ShowJobInfo(job);
            }
        }

        private static PlatformSessionContext GetPlatformSessionContext()
        {
            var context = new PlatformSessionContext
            {
                AppToken = AppToken,
                Host = PlatformHost.WorkPlaceSecure,
                RequestAuthorizer = null,
                UserID = UserLogin,
                Password = UserPassword,
                AppDbId = AppDbId
            };

            context.ForceAuthentication();
            return context;
        }

        private static IEnumerable<CustomerType> GetCustomerTypes()
        {
            var service = new CustomerTypeService();
            return service.FindAll(GetPlatformSessionContext(), RealmId);
        }

        private static IEnumerable<Job> GetLastDayJobs()
        {
            var jobQuery = new JobQuery { CDCAsOf = DateTime.Today, CDCAsOfSpecified = true };
            var service = new JobService();
            return service.GetJobs(GetPlatformSessionContext(), RealmId, jobQuery);
        }

        private static Job UpdateJob(Job job, CustomerType customerType)
        {
            var service = new JobService();
            job.CustomerTypeId = customerType.Id;
            job.CustomerTypeName = customerType.Name;
            return service.UpdateJob(GetPlatformSessionContext(), RealmId, job);
        }

        private static Job GetJob(IdType jobId)
        {
            if (jobId == null)
                return null;
            var service = new JobService();
            return service.FindById(GetPlatformSessionContext(), RealmId, jobId);
        }

        private static CustomerType GetCustomerType(IdType customerTypeId)
        {
            if (customerTypeId == null)
                return null;
            var service = new CustomerTypeService();
            return service.FindById(GetPlatformSessionContext(), RealmId, customerTypeId);
        }

        private static string GetIdStr(IdType qbId)
        {
            if (qbId == null)
                return "";
            return Enum.GetName(typeof(idDomainEnum), qbId.idDomain) + IdSplitter + qbId.Value;
        }

        private static IdType ParseIdStr(string idStr)
        {
            var result = new IdType();
            var arr = idStr.Split(IdSplitter.ToCharArray());
            if (arr.Length > 1)
            {
                result.idDomain = (idDomainEnum)Enum.Parse(typeof(idDomainEnum), arr[0], true);
                result.Value = arr[1];
                return result;
            }
            return null;
        }
    }
}
