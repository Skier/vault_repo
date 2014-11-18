using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server.Servman.Domain.intermediate
{
    public class Address
    {
        #region AreaId

        private string m_areaId;
        public string AreaId
        {
            get { return (m_areaId ?? string.Empty).ToUpper(); }
            set { m_areaId = value; }
        }

        #endregion

        #region Block

        private string m_block;
        public string Block
        {
            get { return (m_block ?? string.Empty).ToUpper(); }
            set { m_block = value; }
        }

        #endregion

        #region Prefix

        private string m_prefix;
        public string Prefix
        {
            get { return (m_prefix ?? string.Empty).ToUpper(); }
            set { m_prefix = value; }
        }

        #endregion

        #region Street

        private string m_street;
        public string Street
        {
            get { return (m_street ?? string.Empty).ToUpper(); }
            set { m_street = value; }
        }

        #endregion

        #region Suffux

        private string m_suffux;
        public string Suffux
        {
            get { return (m_suffux ?? string.Empty).ToUpper(); }
            set { m_suffux = value; }
        }

        #endregion

        #region Unit

        private string m_unit;
        public string Unit
        {
            get { return (m_unit ?? string.Empty).ToUpper(); }
            set { m_unit = value; }
        }

        #endregion

        #region Address2

        private string m_address2;
        public string Address2
        {
            get { return (m_address2 ?? string.Empty).ToUpper(); }
            set { m_address2 = value; }
        }

        #endregion

        #region City

        private string m_city;
        public string City
        {
            get { return (m_city ?? string.Empty).ToUpper(); }
            set { m_city = value; }
        }

        #endregion

        #region State

        private string m_state;
        public string State
        {
            get { return (m_state ?? string.Empty).ToUpper(); }
            set { m_state = value; }
        }

        #endregion

        #region Zip

        private string m_zip;
        public string Zip
        {
            get { return (m_zip ?? string.Empty).ToUpper(); }
            set { m_zip = value; }
        }

        #endregion

        #region Zip4

        private string m_zip4;
        public string Zip4
        {
            get { return (m_zip4 ?? string.Empty).ToUpper(); }
            set { m_zip4 = value; }
        }

        #endregion

        #region Grid

        private string m_grid;
        public string Grid
        {
            get { return (m_grid ?? string.Empty).ToUpper(); }
            set { m_grid = value; }
        }

        #endregion

        #region Page

        private string m_page;
        public string Page
        {
            get
            {
                string result = m_page ?? string.Empty;
                if (result == string.Empty)
                    return "9999";
                return result.ToUpper();
            }
            set { m_page = value; }
        }

        #endregion
    }
}
