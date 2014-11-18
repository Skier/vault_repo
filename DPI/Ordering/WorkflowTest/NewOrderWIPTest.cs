using System;
using System.IO;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;

using DPI.Interfaces;
using DPI.Components;
using DPI.ClientComp;
using DPI.Ordering;
using DPI.Services;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class NewOrderWipTest
	{
		WIP wip;
		IMap imap;
		string serFile = "NewOrderWip.dat";
		string 	zip = "75080"; 
			
		IILECInfo		selectedIlec;  
		IILECInfo[]		avaIlecs;
		IProdPrice		selectedBasicService;
		IProdPrice[]	topProducts;
		IProdPrice[]	prods;

		IUser			user;

		CustInfo	custInfo;
		IAddr2		servAddr;
		IAddr2		mailAddr;
		IOrderSum   sum;

		public NewOrderWipTest()
		{
			imap = IMapFactory.getIMap();
			avaIlecs = OrgSvc.GetILECsAtZip(imap, zip);  
			selectedIlec =  avaIlecs[0];
			
			topProducts = ProdSvc.GetTopProd(imap, selectedIlec, zip);
			selectedBasicService = topProducts[0]; 
			prods = ProdSvc.GetDependentProds(imap, selectedBasicService, selectedIlec, zip);
			
			custInfo = new CustInfo();
			custInfo.FirstName = "Peter";
			custInfo.LastName = "The Great";
			custInfo.Birthday = String.Format("{0:MM/dd/yyyy}", DateTime.Now.AddYears(-25));

			servAddr =  new CustAddress(AddressType.Service);
			servAddr.City = "Dallas";
			servAddr.State = "TX";
			servAddr.Street = "Main";
			servAddr.StreetType = "Drive";
			servAddr.Zipcode = "75080";
			servAddr.Unit = "5151";
			servAddr.StreetNum = "101";

			mailAddr = new CustAddress(AddressType.Mailing);
			mailAddr.City = "Irving";
			mailAddr.State = "TX";
			mailAddr.Street = "Blue Ridge";
			mailAddr.StreetType = "Cir";
			mailAddr.Zipcode = "75017";
			mailAddr.Unit = "A";
			mailAddr.StreetNum = "1316";
			sum = CustSvc.GetOrderSummary(imap, prods, zip, selectedIlec, DemandType.New.ToString(), OrderType.New);
			

		}
		public static void Main()
		{
			NewOrderWipTest t = new NewOrderWipTest();
	
			t.ClearImap();
			t.serWip();
			t.deserWip();
			t.serJustWip();
			t.deserJustWip();
			
		}
		[Test]
		public void serWip()
		{
			user = new User();
			wip = new NewOrderWip(user);

			wip["ServAddr"] = servAddr;
			wip["selectedIlec"] = selectedIlec;  
			wip["avaIlecs"] = avaIlecs;
			wip["selectedBasicService"] = selectedBasicService;
			wip["topProducts"] = topProducts;
			wip["prods"] = prods;
			
			wip["custInfo"] = custInfo;
			wip["servAddr"] = servAddr;
			wip["mailAddr"] = mailAddr;
			wip["ordersummary"] = sum;
			
			imap.add(wip);

			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream(serFile, FileMode.Create, FileAccess.Write, FileShare.None);
			fmt.Serialize(stream, imap);
			stream.Close();
		}
		[Test]
		public void deserWip()
		{
			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream(serFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			Assertion.Assert(stream.Length > 100);
	
			IMap imap = (IMap) fmt.Deserialize(stream);	
			stream.Close();

			WIP wip = (WIP)imap.find(WIP.IKeyS);
			Assertion.Assert(CheckAddr(servAddr, (IAddr)wip["ServAddr"]));
			Assertion.Assert(CheckAddr(mailAddr, (IAddr)wip["mailAddr"]));

			Assertion.Assert(CheckProd(topProducts, (IProdPrice[])wip["topProducts"]));
			Assertion.Assert(CheckProd(selectedBasicService, (IProdPrice)wip["selectedBasicService"]));
			Assertion.Assert(CheckProd(prods, (IProdPrice[])wip["prods"]));

			Assertion.Assert(CheckIlec(avaIlecs, (IILECInfo[])wip["avaIlecs"]));
			Assertion.Assert(CheckIlec(selectedIlec, (IILECInfo)wip["selectedIlec"]));
			Assertion.AssertNotNull(wip["ordersummary"]);
		}
		[Test]
		public void serJustWip()
		{
			wip = new NewOrderWip(user);
			imap = IMapFactory.getIMap();
			imap.add(wip);

			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream(serFile, FileMode.Create, FileAccess.Write, FileShare.None);
			fmt.Serialize(stream, imap);
			stream.Close();
		}
		[Test]
		public void deserJustWip()
		{
			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream(serFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			Console.WriteLine(stream.Length);

			IMap imap = (IMap) fmt.Deserialize(stream);
			WIP wip = (WIP)imap.find(WIP.IKeyS);
			stream.Close();
		}
		[Test]
		public void ClearImap()
		{
			wip = new NewOrderWip(user);

			wip["ServAddr"] = servAddr;
			wip["selectedIlec"] = selectedIlec;  
			wip["avaIlecs"] = avaIlecs;
			wip["selectedBasicService"] = selectedBasicService;
			wip["topProducts"] = topProducts;
			wip["prods"] = prods;
			
			wip["custInfo"] = custInfo;
			wip["servAddr"] = servAddr;
			wip["mailAddr"] = mailAddr;
			
			imap.add(wip);
			UOW uow = new UOW(imap);
			try
			{
				Product.getAll(uow);
				IFormatter fmt = new BinaryFormatter();
				Stream stream = new FileStream(serFile, FileMode.Create, FileAccess.Write, FileShare.None);
				fmt.Serialize(stream, imap);
				stream.Close();

				fmt = new BinaryFormatter();
				stream = new FileStream(serFile, FileMode.Open, FileAccess.Read, FileShare.Read);
				Assertion.Assert(stream.Length > 100);
				stream.Close();
				
				imap.ClearDomainObjs();
				fmt = new BinaryFormatter();
				stream = new FileStream(serFile, FileMode.Create, FileAccess.Write, FileShare.None);
				fmt.Serialize(stream, imap);
				stream.Close();

				fmt = new BinaryFormatter();
				stream = new FileStream(serFile, FileMode.Open, FileAccess.Read, FileShare.Read);
				Assertion.Assert(stream.Length > 100);
				stream.Close();				
				Assertion.Assert(imap.Count > 0);
			}
			finally
			{
				uow.close();
			}
		}
		bool CheckIlec(IILECInfo[] orig, IILECInfo[] rest)
		{
			if ((orig == null) && (rest == null))
				return true;

			if (orig == null)
				return false;

			if (rest == null)
				return false;

			if (orig.Length != rest.Length)
				return false;
			
			for (int i = 0; i < orig.Length; i++)
				if (!CheckIlec(orig[i], rest[i]))
					return false;

			return true;
		}
		bool CheckIlec(IILECInfo orig, IILECInfo rest)
		{
			if ((orig == null) && (rest == null))
				return true;

			if (orig == null)
				return false;

			if (rest == null)
				return false;

			if (orig.OrgId != rest.OrgId)
				return false;
			
			if (orig.ILECName != rest.ILECName)
				return false;
			
			if (orig.ILECCode != rest.ILECCode)
				return false;

			if (orig.IsDefault != rest.IsDefault)
				return false;

			return true;
		}
		bool CheckProd(IProdPrice[] orig, IProdPrice[] rest)
		{
			if ((orig == null) && (rest == null))
				return true;

			if (orig == null)
				return false;

			if (rest == null)
				return false;

			if (orig.Length != rest.Length)
				return false;
			
			for (int i = 0; i < orig.Length; i++)
				if (!CheckProd(orig[i], rest[i]))
					return false;

			return true;
		}
		bool CheckProd(IProdPrice orig, IProdPrice rest)
		{
			if ((orig == null) && (rest == null))
				return true;

			if (orig == null)
				return false;

			if (rest == null)
				return false;
		
			if (orig.ProdId != rest.ProdId)
				return false;

			if (orig.ProdName != rest.ProdName)
				return false;

			if (orig.ProdSelState != rest.ProdSelState)
				return false;

			if (orig.UnitPrice != rest.UnitPrice)
				return false;

			if (orig.Description != rest.Description)
				return false;

			if (orig.Locked != rest.Locked)
				return false;

			return true;
		}

		bool CheckAddr(IAddr orig, IAddr rest)
		{
			if ((orig == null) && (rest == null))
				return true;

			if (orig == null)
				return false;

			if (rest == null)
				return false;

			if (orig.City != rest.City)
                return false;

			if (orig.State != rest.State)
				return false;

			if (orig.Street != rest.Street)
				return false;

			if (orig.StreetNum != rest.StreetNum)
				return false;

			if (orig.StreetPrefix != rest.StreetPrefix)
				return false;

			if (orig.StreetSuffix != rest.StreetSuffix)
				return false;

			if (orig.StreetType != rest.StreetType)
				return false;

			if (orig.Unit != rest.Unit)
				return false;

			if (orig.Zipcode != rest.Zipcode)
				return false;

			return true;
		}
	}
}