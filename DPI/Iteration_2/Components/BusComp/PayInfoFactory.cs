//using System;
//
//using DPI.Interfaces;
// 
//namespace DPI.Components
//{
//	public class PayInfoFactory
//	{
//		public static PayInfo GetPayInfo(string pClass)
//		{
//			PayInfoClass payClass = Conv(pClass);
//			switch(payClass)
//			{
//				case PayInfoClass.PayInfo :
//					return new PayInfo(); 
//
//				case PayInfoClass.PayInfoLocal :
//					return new PayInfoLocal();
//			}
//		}
//		public static PayInfo GetPayInfo(UOW uow, string pClass)
//		{
//			PayInfoClass payClass = Conv(pClass);
//			switch(payClass)
//			{
//				case PayInfoClass.PayInfo :
//					return new PayInfo(uow); 
//
//				case PayInfoClass.PayInfoLocal :
//					return new PayInfoLocal(uow);
//			}
//		}
//		static PayInfoClass Conv(string payClass)
//		{
//			if (payClass == PayInfoClass.PayInfo.ToString())
//				return PayInfoClass.PayInfo;
//
//			if (payClass == PayInfoClass.PayInfoLocal.ToString())
//				return PayInfoClass.PayInfoLocal;
//
//			throw new ArgumentException("Unknow PayInfo class: " + payClass);
//		}
//	}
//}