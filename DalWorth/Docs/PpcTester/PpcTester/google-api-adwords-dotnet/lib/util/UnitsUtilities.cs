// Copyright 2010, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Author: api.anash@gmail.com (Anash P. Oommen)

using com.google.api.adwords.v13;
using com.google.api.adwords.v200909;

using System;
using System.Collections;
using System.Collections.Generic;

namespace com.google.api.adwords.lib.util {
  /// <summary>
  /// Defines utility functions for the client library that use InfoService.
  /// </summary>
  public class UnitsUtilities {
    private class AdWordsAccount {
      internal string email = "";
      internal bool isManager = false;
      internal long units = 0;
      internal bool visited = false;
      internal List<AdWordsAccount> parents = new List<AdWordsAccount>();
      internal List<AdWordsAccount> children = new List<AdWordsAccount>();
    }
    /// <summary>
    /// Gets the quota usage of an account in units, broken down by method name.
    /// </summary>
    /// <param name="user">The AdWordsUser object for which the quota usage should be
    /// retrieved.</param>
    /// <param name="startDate">Start date for the date range for which results are
    /// to be retrieved.</param>
    /// <param name="endDate">End date for the date range for which results are
    /// to be retrieved.</param>
    /// <returns>A list of MethodQuotaUsage objects, with one entry for each method.</returns>
    public static List<MethodQuotaUsage> GetMethodQuotaUsage(AdWordsUser user, DateTime startDate,
        DateTime endDate) {
      List<MethodQuotaUsage> methodQuotaUsage = new List<MethodQuotaUsage>();
      SortedList<string, List<string>> serviceToMethodsMap = GetAllMethods();
      InfoService service = (InfoService) user.GetService(AdWordsService.v200909.InfoService);

      foreach (string serviceName in serviceToMethodsMap.Keys) {
        List<string> methods = serviceToMethodsMap[serviceName];

        foreach (string methodName in methods) {
          InfoSelector selector = new InfoSelector();
          selector.apiUsageTypeSpecified = true;
          selector.apiUsageType = ApiUsageType.UNIT_COUNT;
          selector.dateRange = new DateRange();
          selector.dateRange.min = startDate.ToString("YYYYMMDD");
          selector.dateRange.max = endDate.ToString("YYYYMMDD");
          selector.serviceName = serviceName;
          if (methodName.Contains(".")) {
            string[] splits = methodName.Split('.');
            selector.methodName = splits[0];
            selector.operatorSpecified = true;
            selector.@operator = (Operator) Enum.Parse(typeof(Operator), splits[1]);
          } else {
            selector.methodName = methodName;
          }

          MethodQuotaUsage temp = new MethodQuotaUsage();
          temp.methodName = methodName;
          temp.serviceName = serviceName;
          temp.units = service.get(selector).cost;
          methodQuotaUsage.Add(temp);
        }
      }
      return methodQuotaUsage;
    }

    /// <summary>
    /// Gets the quota usage for client accounts.
    /// </summary>
    /// <param name="user">The AdWordsUser object for which the quota usage
    /// should be retrieved.</param>
    /// <param name="startDate">Start date for the date range for which
    /// results are to be retrieved.</param>
    /// <param name="endDate">End date for the date range for which results are
    /// to be retrieved.</param>
    /// <param name="clientUsage">A map, with key as client name, and API
    /// usage as value.</param>
    /// <param name="totalUnits">The total units that could be accounted for.
    /// </param>
    /// <param name="diffUnits">The amount of units that could not be accounted
    /// for. This difference can be attributed to accounts for which API calls
    /// were made using this developer token, but the accounts themselves are
    /// not linked under the main MCC.</param>
    public static void GetClientQuotaUsage(AdWordsUser user, DateTime startDate, DateTime endDate,
        out SortedList<string, long> clientUsage, out long totalUnits, out long diffUnits) {
      AccountService accountService =
          (AccountService) user.GetService(AdWordsService.v13.AccountService);
      AdWordsAccount rootUser = new AdWordsAccount();
      rootUser.email = accountService.emailValue.Value[0];
      rootUser.isManager = true;
      Hashtable allUsers = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
      BuildUserGraph(accountService, rootUser, allUsers);

      InfoService infoService = (InfoService) user.GetService(AdWordsService.v200909.InfoService);
      FetchUnitUsagesRecursively(infoService, rootUser, startDate, endDate);

      clientUsage = new SortedList<string, long>();
      foreach (string email in allUsers.Keys) {
        clientUsage[email] = GetUnits((AdWordsAccount) allUsers[email], allUsers);
      }

      InfoSelector selector = new InfoSelector();
      selector.apiUsageTypeSpecified = true;
      selector.apiUsageType = ApiUsageType.UNIT_COUNT;
      selector.dateRange = new DateRange();
      selector.dateRange.min = startDate.ToString("YYYYMMDD");
      selector.dateRange.max = endDate.ToString("YYYYMMDD");

      totalUnits = infoService.get(selector).cost;
      diffUnits = totalUnits - clientUsage[rootUser.email];
      return;
    }

    /// <summary>
    /// Gets a list of all methods supported by AdWords API v13.
    /// </summary>
    /// <returns>A sorted map, with key as service name and value as an array of
    /// method names.</returns>
    public static SortedList<string, List<string>> GetAllMethods() {
      List<ApiMethod> methods = DataUtilities.GetAllMethods();
      SortedList<string, List<string>> serviceToMethodsMap = new SortedList<string, List<string>>();

      foreach (ApiMethod method in methods) {
        if (!serviceToMethodsMap.ContainsKey(method.serviceName)) {
          serviceToMethodsMap.Add(method.serviceName, new List<string>());
        }
        serviceToMethodsMap[method.serviceName].Add(method.methodName);
      }
      return serviceToMethodsMap;
    }

    private static long GetUnits(AdWordsAccount account, Hashtable allUsers) {
      foreach (AdWordsAccount user in allUsers.Values) {
        user.visited = false;
      }
      return RollupUnits(account);
    }

    private static long RollupUnits(AdWordsAccount account) {
      long retVal = 0;
      if (account.visited == false) {
        retVal = account.units;
        if (account.isManager == true) {
          foreach (AdWordsAccount child in account.children) {
            retVal += RollupUnits(child);
          }
        }
        account.visited = true;
      }
      return retVal;
    }

    private static AdWordsAccount BuildUserGraph(AccountService accountService,
        AdWordsAccount account, Hashtable allUsers) {
      clientEmail oldClientEmail = accountService.clientEmailValue;

      accountService.clientEmailValue = new clientEmail();
      accountService.clientEmailValue.Value = new string[] {account.email};

      if (allUsers.ContainsKey(account.email) == false) {
        allUsers.Add(account.email, account);
      }

      ClientAccountInfo[] clients = accountService.getClientAccountInfos();

      if (clients != null) {
        for (int i = 0; i < clients.Length - 1; i++) {
          for (int j = i + 1; j < clients.Length; j++) {
            if (clients[i].isCustomerManager == false && clients[j].isCustomerManager == true) {
              ClientAccountInfo temp = clients[i];
              clients[i] = clients[j];
              clients[j] = temp;
            }
          }
        }
        foreach (ClientAccountInfo client in clients) {
          AdWordsAccount child = null;
          if (allUsers.ContainsKey(client.emailAddress) == true) {
            child = (AdWordsAccount) allUsers[client.emailAddress];
          } else {
            child = new AdWordsAccount();
            child.email = client.emailAddress;
            child.isManager = client.isCustomerManager;
          }
          child.parents.Add(account);
          account.children.Add(child);

          BuildUserGraph(accountService, child, allUsers);
        }
      }
      accountService.clientEmailValue = oldClientEmail;
      return account;
    }

    private static void FetchUnitUsagesRecursively(InfoService infoService, AdWordsAccount account,
        DateTime startDate, DateTime endDate) {
      string oldClientEmail = infoService.RequestHeader.clientEmail;
      startDate = new DateTime(2009, 1, 1);
      infoService.RequestHeader.clientEmail = account.email;

      InfoSelector selector = new InfoSelector();
      selector.apiUsageTypeSpecified = true;
      selector.apiUsageType = ApiUsageType.UNIT_COUNT_FOR_CLIENTS;
      selector.dateRange = new DateRange();
      selector.dateRange.min = startDate.ToString("yyyyMMdd");
      selector.dateRange.max = endDate.ToString("yyyyMMdd");

      ApiUsageInfo usageInfo = null;
      try {
        usageInfo = infoService.get(selector);
      } catch (Exception ex){
        string temp = ex.StackTrace;
      }
      foreach (AdWordsAccount child in account.children) {
        if (usageInfo.apiUsageRecords != null) {
          foreach (ApiUsageRecord usageRecord in usageInfo.apiUsageRecords) {
            if (child.email == usageRecord.clientEmail) {
              child.units = usageRecord.cost;
              break;
            }
          }
        }

        if (child.isManager) {
          FetchUnitUsagesRecursively(infoService, child, startDate, endDate);
        }
      }
      infoService.RequestHeader.clientEmail = oldClientEmail;
    }

    private static string[] GetAllUsersFromGraph(AdWordsAccount rootUser) {
      List<string> allUsers = new List<string>();
      allUsers.Add(rootUser.email);

      foreach (AdWordsAccount child in rootUser.children) {
        allUsers.AddRange(GetAllUsersFromGraph(child));
      }
      return allUsers.ToArray();
    }
  }
}
