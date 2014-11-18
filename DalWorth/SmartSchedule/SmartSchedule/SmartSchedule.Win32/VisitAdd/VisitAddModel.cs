using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Domain.Sync;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;
using System.Linq;

namespace SmartSchedule.Win32.VisitAdd
{
    public class VisitAddModel : IModel
    {
        #region Request

        private RecommendationRequest m_request;
        public RecommendationRequest Request
        {
            get { return m_request; }
            set { m_request = value; }
        }

        #endregion

        #region ResponseItems

        private BindingList<RecommendationResponseItem> m_responseItems;
        public BindingList<RecommendationResponseItem> ResponseItems
        {
            get { return m_responseItems; }
        }

        #endregion

        #region Init

        public void Init()        
        {
            while (true)
            {
                List<RecommendationResponseItem> responseItems
                    = RecommendationResponseItem.FindResponse(m_request);
                if (responseItems.Count != 65)
                {
                    Connection.DeleteInstance(ConnectionKeyEnum.Servman);
                    Thread.Sleep(500);
                    continue;                    
                }
                    
                m_responseItems = new BindingList<RecommendationResponseItem>(responseItems);
                RecommendationResponseItem.DeleteResponse(m_request);
                break;
            }            
        }

        #endregion
    }
}
