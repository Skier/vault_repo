using System;

using DPI.Interfaces;
using DPI.Components;
using DPI.Services;
 
namespace DPI.Components
{
	public class SvcFactory : ISvcFactory
	{
		static SvcFactory fact;

		SvcFactory()
		{
			fact = this;
			WQSpinner.RegSvcFactory(this);
    	}
		public static void Start()
		{
			if (fact == null)
				new SvcFactory();		
		}
		public ISvcProvider GetProvider(string provider)
		{	
			if (provider == null)
				throw new ApplicationException("Svc Provider is missing");
			
			switch (provider.Trim().ToLower())
			{
				case "pinsvc" :
					return new PinSvc();
				
				default :
					throw new ArgumentException("Uknown Pin Product web services provider: " + provider);
			}
		}
	}
}