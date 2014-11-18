using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class VerifoneWrapperTest
	{
		#region Data
		UOW _uow;
		string _storeCode;
		string _clerkID;			
		string _transNum; 
		decimal _localAmount;
		decimal _ldAmount;
		string _commPort;
		string _phNumber;
		int _accNumber;
	

		#endregion
		/*		Constructors		*/
		public VerifoneWrapperTest()
		{
			SetValues();
		}
        
		/*		Methods		*/
		public static void Main()
		{
			VerifoneWrapperTest test = new VerifoneWrapperTest();
			test.SubmitPendXact();
			test.SubmitMonthlyXact();
			test.SubmitNewXact();
			
		}
		[Test]
		public void SubmitNewXact()
		{
			_uow = new UOW();
			_transNum = new Random().Next(1, 1000000).ToString();
			_commPort = "New Order";
			try
			{
				IVerifoneResult result = VerifoneWrapper.SubmitNewXact(_uow, _storeCode, _clerkID, _transNum, _localAmount, _ldAmount, _commPort);
				Assertion.Assert(result.Id > 0);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occured Message: {0}, Stack Trace: {1}", ex.Message, ex.StackTrace); 
			}
			finally
			{
				_uow.close();
			}
		}
		[Test]
		public void SubmitMonthlyXact()
		{
			_uow = new UOW();
			_transNum = new Random().Next(1, 1000000).ToString();
			_commPort = "Monthly Pa";
			try
			{
				IVerifoneResult result = VerifoneWrapper.SubmitMonthlyXact(_uow, _storeCode, _clerkID, _transNum, _phNumber, _localAmount, _ldAmount, _commPort);
				Assertion.Assert(result.Id > 0);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occured Message: {0}, Stack Trace: {1}", ex.Message, ex.StackTrace); 
			}
			finally
			{
				_uow.close();
			}
		}
		[Test]
		public void SubmitPendXact()
		{
			_uow = new UOW();
			_transNum = new Random().Next(1, 1000000).ToString();
			_commPort = "New Paymen";			
			try
			{
				IVerifoneResult result = VerifoneWrapper.SubmitPendXact(_uow, _storeCode, _clerkID, _transNum, _localAmount, _ldAmount, _commPort, _accNumber);
				Assertion.Assert(result.Id > 0);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occured Message: {0}, Stack Trace: {1}", ex.Message, ex.StackTrace); 
			}
			finally
			{
				_uow.close();
			}
		}
		void SetValues()
		{
			
			_storeCode = "NCRW0443RW";
			_clerkID = "RW0443";			
			_localAmount = 139.99m;
			_ldAmount = 10.47m;
			_phNumber = "8036353892";
			//_accNumber = 30012319;
		}
	}
}