using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WatiN.Core;
using System.Threading;

using Dalworth.Server.SDK;
using Dalworth.Server.Domain;



namespace Dalworth.PpcTester
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            Configuration.RemoveConfigReadOnlyAttribute();

            Configuration.LoadGlobalConfiguration();

            //ImportGoogleCompaign(2, @"C:\dev\dalworth\Docs\DalworthCarpetAdwords\DalworthCarpetCleaning+Dalworth+2010-04-27.csv");

            
            //StartGoogleCompaing(12, "dalworthrugcleaning.com");
            //StartGoogleCompaign(20, "dalworth.com");
            RestartGoogleCompaing(20, 13, "dalworth.com");

            //ImportKeywords(445, @"C:\dev\dalworth\Docs\DalworthCarpetAdwords\TestList.csv");


            Console.WriteLine("DONE!!!!!");
            System.Console.ReadLine();
        }


        private static bool StartGoogleCompaign(int compaingId, string domain)
        {
            List<Keyword> keywords = Keyword.FindByCompaign(compaingId, null);

            TestBatch batch = new TestBatch() { DateRun = DateTime.Now };

            TestBatch.Insert(batch);

            return ProcessGoogleCompaign(keywords, batch, domain);
        }

        private static bool RestartGoogleCompaing(int compaignId, int testBatchId, string domain)
        {
            TestBatch batch = TestBatch.FindByPrimaryKey(testBatchId);
            List<Keyword> keywords = Keyword.FindByCompaignUnprocessed(compaignId, testBatchId, null);
            return ProcessGoogleCompaign(keywords, batch, domain);
        }

        private static bool ProcessGoogleCompaign(List<Keyword> keywords, TestBatch testBatch, string domain)
        {
             using (var browser = new IE("https://adwords.google.com/select/AdTargetingPreviewTool"))
            {
                Button submit = (browser).Frames[1].Button(Find.ByName("btnG"));

                //browser.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.Minimize);

                TextField queryTextField;

                Random random = new Random();

                foreach (Keyword keyword in keywords)
                {

                    int sleepTime = random.Next(2000, 4000);
                    Console.Write("* --");
                    Thread.Sleep(sleepTime);

                    Console.WriteLine(keyword.KeywordString);

                    queryTextField = (browser).Frames[1].TextField(Find.ByName("q"));

                    if (!queryTextField.Exists)
                    {
                        Console.WriteLine("Google Sorry Happend!!!");
                        return false;
                    }

                    queryTextField.TypeText(keyword.KeywordString);
                    submit.Click();

                    TestResult.ProcessGoogleResult(keyword, testBatch, browser, domain);
                }

                browser.Close();
                return true;

            }
        }

        private static void ImportGoogleCompaign(int companyId, string fileName)
        {
            const int COMPAIGN = 0;
            const int COMPAING_DAILY_BUDGET = 1;
            const int ADGROUP = 7;
            const int KEYWORD = 13;
            const int KEYWORD_TYPE = 15;
            const int HEADLINE = 18;
            const int DESCRIPTION_LINE_1 = 19;
            const int DESCRIPTION_LINE_2 = 20;
            const int DISPLAY_URL = 21;
            const int COMPAING_STATUS = 23;
            const int ADGROUP_STATUS = 24;
            const int KEYWORD_STATUS = 26;

            Compaign compaign = null;
            Adgroup adgroup = null;
            Advertisement advertisement = null;
            AdgroupKeyword adgroupKeyWord = null;
            Keyword keyword = null;

            using (StreamReader sr = new StreamReader(fileName))
            {
                String row;
                bool isFirstLine = true;
                while ((row = sr.ReadLine()) != null)
                {
                    string[] columns = row.Split('\t');

                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }



                    if (columns[COMPAIGN].Length > 0 && columns[COMPAING_DAILY_BUDGET].Length > 0 && columns[COMPAING_STATUS].Length > 0
                        && columns[COMPAING_STATUS] == "Active")
                    {
                        compaign = new Compaign() { CompanyId = companyId, Name = columns[COMPAIGN], SearchEngineId = 1 };
                        Compaign.Insert(compaign);
                        adgroup = null;
                        advertisement = null;
                        adgroupKeyWord = null;
                        keyword = null;
                        continue;
                    }

                    if (columns[ADGROUP].Length > 0 && columns[KEYWORD].Length == 0 && columns[HEADLINE].Length == 0
                        && columns[COMPAING_STATUS].Length > 0 &&
                        columns[ADGROUP_STATUS].Length > 0
                        && columns[COMPAING_STATUS] == "Active" && columns[ADGROUP_STATUS] == "Active")
                    {
                        adgroup = new Adgroup() { CompaingId = compaign.Id, Name = columns[ADGROUP] };
                        Adgroup.Insert(adgroup);
                        advertisement = null;
                        adgroupKeyWord = null;
                        keyword = null;
                        continue;
                    }

                    if (columns[COMPAIGN].Length > 0 && columns[ADGROUP].Length > 0 && columns[HEADLINE].Length > 0 &&
                        columns[DESCRIPTION_LINE_1].Length > 0 && columns[DESCRIPTION_LINE_2].Length > 0 &&
                        columns[DISPLAY_URL].Length > 0 && columns[COMPAING_STATUS].Length > 0 &&
                        columns[ADGROUP_STATUS].Length > 0
                        && columns[COMPAING_STATUS] == "Active" && columns[ADGROUP_STATUS] == "Active")
                    {
                        advertisement = new Advertisement()
                        {
                            AdgroupId = adgroup.Id,
                            Headline = columns[HEADLINE],
                            DescriptionLine1 = columns[DESCRIPTION_LINE_1],
                            DescriptionLine2 = columns[DESCRIPTION_LINE_2],
                            DisplayURL = columns[DISPLAY_URL]
                        };

                        Advertisement.Insert(advertisement);
                        adgroupKeyWord = null;
                        keyword = null;
                        continue;
                    }


                    if (columns[KEYWORD].Length > 0 && columns[KEYWORD_TYPE].Length > 0 && !columns[KEYWORD_TYPE].Contains("Negative")
                        && !columns[KEYWORD_TYPE].Contains("Paused")
                        && columns[COMPAING_STATUS] == "Active" && columns[ADGROUP_STATUS] == "Active"
                        && columns[KEYWORD_STATUS] == "Active")
                    {

                        keyword = new Keyword() { KeywordString = columns[KEYWORD] };
                        Keyword.InsertIfNotFound(keyword, null);

                        adgroupKeyWord = new AdgroupKeyword() { AdgroupId = adgroup.Id, KeywordId = keyword.Id };

                        AdgroupKeyword.InsertIfNotFound(adgroupKeyWord, null);

                        continue;
                    }
                }
            }
        }

        private static void ImportKeywords(int adgroupId, string fileName)
        {
            String row;
            using (StreamReader sr = new StreamReader(fileName))
            {
                while ((row = sr.ReadLine()) != null)
                {
                    Keyword keyword = new Keyword() { KeywordString = row };
                    Keyword.InsertIfNotFound(keyword, null);

                    AdgroupKeyword adgroupKeyWord = new AdgroupKeyword() { AdgroupId = adgroupId, KeywordId = keyword.Id };

                    AdgroupKeyword.InsertIfNotFound(adgroupKeyWord, null);

                    continue;
                }
            }
        }
    }
}
