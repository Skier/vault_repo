using System;
using System.Configuration;
using System.Runtime.Remoting;

using DPI.Components;
using DPI.Interfaces;

namespace DPI.Components
{
	public class EzTaxProxy
	{
		ITaxService bTax;
		string ezTaxUri;

		public EzTaxProxy()
		{
			this.ezTaxUri = ConfigurationSettings.AppSettings["DpiEzTaxUri"];
		}
		
		public ITaxService GetTaxProxy()
		{
			try
			{
				if (bTax != null)
					return bTax;

				bTax = (ITaxService)Activator.GetObject(typeof(ITaxService), ezTaxUri);
			}
			catch (Exception ex)
			{
				bTax = null;
				throw new ApplicationException("Error Creating Tax Proxy. Error Message: " + ex.Message);
			}
			return bTax;
		}

	}
}