using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using WatiN.Core;
using Dalworth.Server.SDK;
using Dalworth.Server.Domain;
using Dalworth.Server.Data;
using Dalworth.Server.Domain.Package;

namespace Dalworth.PpcTesterWin
{
    class GoogleTestThread
    {
        private TestBatch m_batch = null;
        private List<AdgroupKeywordPackage> m_testKeywords;
        private int m_keywordsProcessed = 0;
        private Campaign m_testCampaign;
        private bool m_processSuccess = true;
        private Thread m_googleTestThread;
       
        public GoogleTestThread(Campaign testCampaign, List<AdgroupKeywordPackage> keywords)
        {
            m_testCampaign = testCampaign;
            m_testKeywords = keywords;
        }

        public void Start()
        {
            m_googleTestThread = new Thread(new ThreadStart(StartGoogleCompaign));

            m_googleTestThread.SetApartmentState(ApartmentState.STA);
            m_googleTestThread.Start();
        }


        private void StartGoogleCompaign()
        {
            if (m_batch == null )
            {
                m_batch = new TestBatch() 
                {
                    DateRun = DateTime.Now,
                    CampaignId  = m_testCampaign.Id,
                    SearchEngineId = 1
               };

               TestBatch.Insert(m_batch);
            }

            ProcessGoogleCompaign(m_testKeywords, m_batch, m_testCampaign.TestDomain);

        }



        private void ProcessGoogleCompaign(List<AdgroupKeywordPackage> keywords, TestBatch testBatch, string domain)
        {
            m_processSuccess = true;
            using (var browser = new IE("https://adwords.google.com/select/AdTargetingPreviewTool"))
            {
                WatiN.Core.Button submit = (browser).Frames[1].Button(Find.ByName("btnG"));

                //browser.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.Minimize);

                TextField queryTextField;

                Random random = new Random();

                for (int i = m_keywordsProcessed; i < m_testKeywords.Count; i++,  m_keywordsProcessed++)
                {
                    AdgroupKeywordPackage keyword = m_testKeywords[i];

                    int sleepTime = random.Next(2000, 20000);

                    Thread.Sleep(sleepTime);

                    queryTextField = (browser).Frames[1].TextField(Find.ByName("q"));

                    // Google sorry page.  Need to enter catcha.
                    if (!queryTextField.Exists)
                    {
                        m_processSuccess = false;
                        return;
                    }

                    queryTextField.TypeText(keyword.Keyword.KeywordString);
                    submit.Click();

                    ProcessGoogleResult(keyword, testBatch, browser, domain);
                }

                browser.Close();
                m_processSuccess = true;
            }
        }

        private void ProcessGoogleResult(AdgroupKeywordPackage keyword, TestBatch batch, IE browser, string customerUrl)
        {
            Div divFirstThreeResults = browser.Frames[1].Div("tads");

            TestResult testResult = new TestResult() 
            { 
                TestBatchId = batch.Id, 
                DateRun = DateTime.Now, 
                SearchEngineKeywordId = keyword.AdgroupKeyword.SearchEngineKeywordId,
                SearchEngineId = 1
            };

            TestResult.Insert(testResult);

            if (divFirstThreeResults != null)
            {

                Element ad;

                ad = divFirstThreeResults.Element(WatiN.Core.Find.ByClass("taf"));

                if (ad.Exists)
                {
                    TestResultDetail detail = ParseGoogleTopTestResultDetail(ad);
                    if (detail != null)
                    {
                        detail.TestResultId = testResult.Id;
                        detail.AdPosition = 1;
                        if (detail.DisplayUrl.ToLower().Contains(customerUrl))
                        {
                            testResult.AdPosition = detail.AdPosition;
                            TestResult.Update(testResult);
                        }
                        TestResultDetail.Insert(detail);
                    }
                }


                ad = divFirstThreeResults.Element(WatiN.Core.Find.ByClass("tam"));
                if (ad.Exists)
                {
                    TestResultDetail detail = ParseGoogleTopTestResultDetail(ad);
                    if (detail != null)
                    {
                        detail.TestResultId = testResult.Id;
                        detail.AdPosition = 2;
                        if (detail.DisplayUrl.ToLower().Contains(customerUrl))
                        {
                            testResult.AdPosition = detail.AdPosition;
                            TestResult.Update(testResult);
                        }
                        TestResultDetail.Insert(detail);
                    }
                }



                ad = divFirstThreeResults.Element(WatiN.Core.Find.ByClass("tal"));
                if (ad.Exists)
                {
                    TestResultDetail detail = ParseGoogleTopTestResultDetail(ad);
                    if (detail != null)
                    {
                        detail.TestResultId = testResult.Id;
                        detail.AdPosition = 3;
                        if (detail.DisplayUrl.ToLower().Contains(customerUrl))
                        {
                            testResult.AdPosition = detail.AdPosition;
                            TestResult.Update(testResult);
                        }
                        TestResultDetail.Insert(detail);
                    }
                }



                ad = divFirstThreeResults.Element(WatiN.Core.Find.ByClass("tas"));
                if (ad.Exists)
                {
                    TestResultDetail detail = ParseGoogleTopTestResultDetail(ad);
                    if (detail != null)
                    {
                        detail.TestResultId = testResult.Id;
                        detail.AdPosition = 1;
                        if (detail.DisplayUrl.ToLower().Contains(customerUrl))
                        {
                            testResult.AdPosition = detail.AdPosition;
                            TestResult.Update(testResult);
                        }
                        TestResultDetail.Insert(detail);
                    }
                }
            }

            Div divSideResults = browser.Frames[1].Div("cnt");

            if (divSideResults.Exists)
            {
                ElementContainer<WatiN.Core.Element> ol = (ElementContainer<WatiN.Core.Element>)
                    divSideResults.Element(WatiN.Core.Find.ByClass("nobr"));

                if (ol.Exists)
                {

                    int i = 4;
                    Element nextSibling = ol.Elements[0];

                    while (true)
                    {
                        if (nextSibling == null)
                            break;

                        TestResultDetail detail = ParseGoogleSideTestResultDetail(nextSibling);
                        detail.TestResultId = testResult.Id;
                        detail.AdPosition = i++;
                        if (detail.DisplayUrl.ToLower().Contains(customerUrl))
                        {
                            testResult.AdPosition = detail.AdPosition;
                            TestResult.Update(testResult);
                        }
                        TestResultDetail.Insert(detail);
                        nextSibling = nextSibling.NextSibling;
                    }
                }
            }
        }

        private static TestResultDetail ParseGoogleTopTestResultDetail(Element ad)
        {
            TestResultDetail testResultDetail = null;

            if (ad.Text == null)
                return testResultDetail;

            string[] lines = ad.Text.Split('\n');


            if (lines.Length > 0 && lines[0] != null)
            {
                testResultDetail = new TestResultDetail();
                testResultDetail.Headline = lines[0].Trim();
            }

            if (lines.Length > 1 && lines[1] != null)
            {
                int firstWordIdx = lines[1].IndexOf(' ');

                testResultDetail.DisplayUrl = lines[1].Substring(0, firstWordIdx).Trim();
                testResultDetail.DescriptionLine1 = lines[1].Substring(firstWordIdx).Trim();
            }

            return testResultDetail;
        }

        private static TestResultDetail ParseGoogleSideTestResultDetail(Element ad)
        {
            TestResultDetail detail = new TestResultDetail();

            string[] lines = ad.Text.Split('\n');

            detail.Headline = lines[0].Trim();
            detail.DescriptionLine1 = lines[1].Trim();
            detail.DescriptionLine2 = lines[2].Trim();
            detail.DisplayUrl = lines[4].Trim();

            return detail;
        }


    }
}
