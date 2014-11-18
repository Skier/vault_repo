using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class PageDTOFactory
	{
		public static ICustomerPageDTO GetCustomerPageDTO()
		{
			return new CustomerPageDTO();
		}

		public static IBillPageDTO GetBillPageDTO()
		{
			return new BillPageDTO();
		}

		public static IAddressPageDTO GetAddressPageDTO()
		{
			AddressPage addressPage = new AddressPage();
			addressPage.MailAddress = new AddrDTO();
			
			return addressPage;
		}

		public static IAddr GetAddrDTO()
		{
			return new AddrDTO();
		}

		public static IPaymentLogPageDTO GetPaymentLogPageDTO()
		{
			return new PaymentLogPageDTO();
		}

	}
}