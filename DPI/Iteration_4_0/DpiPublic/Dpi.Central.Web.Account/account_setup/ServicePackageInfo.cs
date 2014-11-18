using System;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.AccountSetup
{
	public class ServicePackageInfo
	{
		public ServicePackageInfo(){}
	    
        #region PackageName

        private string m_packageName;
        public string PackageName {
            get { return m_packageName; }
            set { m_packageName = value; }
        }

        #endregion

        #region Price
        
        private decimal m_price;
        public decimal Price {
            get { return m_price; }
            set { m_price = value; }	        
        }

        #endregion

        #region Features

        private string[] m_features;
        public string[] Features {
            get { return m_features; }
            set { m_features = value; }	        
        }

        #endregion

        #region Package

        private IProdPrice m_package;
        public IProdPrice Package {
            get { return m_package; }
            set { m_package = value; }
        }

        #endregion
	}
}
