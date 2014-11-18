using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server.Servman.Domain.intermediate
{
    public class OrderExport
    {
        #region H_order

        private h_order m_h_order;
        public h_order H_order
        {
            get { return m_h_order; }
            set { m_h_order = value; }
        }

        #endregion

        #region M_alt_ad

        private m_alt_ad m_m_alt_ad;
        public m_alt_ad M_alt_ad
        {
            get { return m_m_alt_ad; }
            set { m_m_alt_ad = value; }
        }

        #endregion

        #region Hdeflood

        private hdeflood m_hdeflood;
        public hdeflood Hdeflood
        {
            get { return m_hdeflood; }
            set { m_hdeflood = value; }
        }

        #endregion

        #region Ddeflood

        private ddeflood m_ddeflood;
        public ddeflood Ddeflood
        {
            get { return m_ddeflood; }
            set { m_ddeflood = value; }
        }

        #endregion

        #region Disp_que

        private disp_que m_disp_que;
        public disp_que Disp_que
        {
            get { return m_disp_que; }
            set { m_disp_que = value; }
        }

        #endregion

    }
}
