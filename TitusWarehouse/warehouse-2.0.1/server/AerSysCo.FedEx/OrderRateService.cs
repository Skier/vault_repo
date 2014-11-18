using System;
using System.Data.SqlClient;
using AerSysCo.Entity;
using AerSysCo.CustomerCenter;
using System.Web.Services.Protocols;
using AerSysCo.FedEx.WebReference;
using AerSysCo.Common;
using AerSysCo.Warehouse;
using log4net;


namespace AerSysCo.FedEx {

public class OrderRateService {

    public static void Rate(SqlTransaction tran, ShoppingCartShipment shipment, ShoppingCart cart) {
            // Build a RateRequest object
            RateRequest request = new RateRequest();
            //
            request.WebAuthenticationDetail = new WebAuthenticationDetail();
            request.WebAuthenticationDetail.UserCredential = new WebAuthenticationCredential();
            request.WebAuthenticationDetail.UserCredential.Key = ConfigurationManager.AppSettings[cart.customer.brandName+"_fedex_key"]; // Replace "XXX" with the Key
            request.WebAuthenticationDetail.UserCredential.Password = ConfigurationManager.AppSettings[cart.customer.brandName+"_fedex_password"]; // Replace "XXX" with the Password
            //
            request.ClientDetail = new ClientDetail();
            request.ClientDetail.AccountNumber = ConfigurationManager.AppSettings[cart.customer.brandName+"_fedex_account"]; // Replace "XXX" with clients account number
            request.ClientDetail.MeterNumber = ConfigurationManager.AppSettings[cart.customer.brandName+"_fedex_meter"]; // Replace "XXX" with clients meter number
            //
            request.TransactionDetail = new TransactionDetail();
            request.TransactionDetail.CustomerTransactionId = shipment.shoppingCartShipmentId.ToString(); // This is a reference field for the customer.  Any value can be used and will be provided in the response.
            //
            request.Version = new VersionId(); // WSDL version information, value is automatically set from wsdl            
            // 
            request.ReturnTransitAndCommit = true;
            request.ReturnTransitAndCommitSpecified = true;
            request.CarrierCodes = new CarrierCodeType[2];
            // Insert the Carriers you would like to see the rates for
            request.CarrierCodes[0] = CarrierCodeType.FDXE;
            request.CarrierCodes[1] = CarrierCodeType.FDXG;
            request.RequestedShipment = new RequestedShipment();
            request.RequestedShipment.Shipper = new Party();   // Origin information
            request.RequestedShipment.Shipper.Address = new AerSysCo.FedEx.WebReference.Address();
            request.RequestedShipment.Shipper.Address.StreetLines = new string[2] { shipment.warehouse.address1, shipment.warehouse.address2 };
            request.RequestedShipment.Shipper.Address.City = shipment.warehouse.city;
            request.RequestedShipment.Shipper.Address.StateOrProvinceCode = shipment.warehouse.state;
            request.RequestedShipment.Shipper.Address.PostalCode = shipment.warehouse.zip;
            request.RequestedShipment.Shipper.Address.CountryCode = shipment.warehouse.country;
            //

            request.RequestedShipment.Recipient = new Party(); // Destination Information
            request.RequestedShipment.Recipient.Address = new AerSysCo.FedEx.WebReference.Address();
            request.RequestedShipment.Recipient.Address.StreetLines = new string[2] { cart.shippingAddress.address1, cart.shippingAddress.address2};
            request.RequestedShipment.Recipient.Address.City = cart.shippingAddress.city;
            request.RequestedShipment.Recipient.Address.StateOrProvinceCode = cart.shippingAddress.state;
            request.RequestedShipment.Recipient.Address.PostalCode = cart.shippingAddress.zip;
            request.RequestedShipment.Recipient.Address.CountryCode = cart.shippingAddress.country;
            //
            request.RequestedShipment.ShippingChargesPayment = new Payment(); // Payment Information
            request.RequestedShipment.ShippingChargesPayment.PaymentType = PaymentType.SENDER; // Payment options are RECIPIENT, SENDER, THIRD_PARTY
            request.RequestedShipment.ShippingChargesPayment.PaymentTypeSpecified = true;
            request.RequestedShipment.ShippingChargesPayment.Payor = new Payor();
            request.RequestedShipment.ShippingChargesPayment.Payor.AccountNumber = "XXX"; // Replace "XXX" with clients account number
            request.RequestedShipment.DropoffType = DropoffType.REGULAR_PICKUP; //Drop off types are BUSINESS_SERVICE_CENTER, DROP_BOX, REGULAR_PICKUP, REQUEST_COURIER, STATION

            request.RequestedShipment.TotalInsuredValue = new Money();
            request.RequestedShipment.TotalInsuredValue.Amount = 0;
            request.RequestedShipment.TotalInsuredValue.Currency = "USD";
            request.RequestedShipment.ShipTimestamp = DateTime.Now; // Shipping date and time
            request.RequestedShipment.ShipTimestampSpecified = false;
            request.RequestedShipment.RateRequestTypes = new RateRequestType[2];
            request.RequestedShipment.RateRequestTypes[0] = RateRequestType.ACCOUNT;
//            request.RequestedShipment.RateRequestTypes[1] = RateRequestType.LIST;

            request.RequestedShipment.TotalWeight = new Weight();
            request.RequestedShipment.TotalWeight.Value = CalculateBruttoShippingWeight(tran, shipment);
            request.RequestedShipment.TotalWeight.Units = WeightUnits.LB;

            request.RequestedShipment.PackageCount = CalculatePackCount(tran, shipment).ToString();
            request.RequestedShipment.PackageDetail = RequestedPackageDetailType.PACKAGE_SUMMARY;

            //
            RateService rateService = new RateService(); // Initialize the service
            try {
                RateReply reply = rateService.getRates(request); // Service call

                if ( reply.HighestSeverity == NotificationSeverityType.SUCCESS 
                    || reply.HighestSeverity == NotificationSeverityType.NOTE 
                    || reply.HighestSeverity == NotificationSeverityType.WARNING ) 
                {
                    foreach (RateReplyDetail rateDetail in reply.RateReplyDetails) {
                        Logger.GetAppLogger().Debug("FedEx ServiceType: " + rateDetail.ServiceType+"----------------------------------");
                        foreach (RatedShipmentDetail shipmentDetail in rateDetail.RatedShipmentDetails) {
                            Logger.GetAppLogger().Debug("....................................................................");
                            Logger.GetAppLogger().Debug("RateType = " + shipmentDetail.ShipmentRateDetail.RateType);
                            Logger.GetAppLogger().Debug("Total Billing Weight = " + shipmentDetail.ShipmentRateDetail.TotalBillingWeight.Value);
                            Logger.GetAppLogger().Debug("Total Base Charge = " + shipmentDetail.ShipmentRateDetail.TotalBaseCharge.Amount);
                            Logger.GetAppLogger().Debug("Total Discount = " + shipmentDetail.ShipmentRateDetail.TotalFreightDiscounts.Amount);
                            Logger.GetAppLogger().Debug("Total Surcharges = " + shipmentDetail.ShipmentRateDetail.TotalSurcharges.Amount);
                            Logger.GetAppLogger().Debug("Net Charge = " + shipmentDetail.ShipmentRateDetail.TotalNetCharge.Amount);
                            Logger.GetAppLogger().Debug("....................................................................");
                        }
                        Logger.GetAppLogger().Debug("---------------------------------------------------------------------");
                    }
                } else {
                    Logger.GetAppLogger().Error(reply.Notifications[0].Message);
                    throw new ApplicationException(reply.Notifications[0].Message);
                }
            } catch (SoapException e) {
                Logger.GetAppLogger().Error(e.Detail.InnerText, e);
                throw new SystemException(e.Detail.InnerText);
            } catch (Exception e) {
                Logger.GetAppLogger().Error(e.Message, e);
                throw new SystemException(e.Message);
            }
    }

    private static int CalculateBoxCount(SqlTransaction tran, ShoppingCartShipment shipment) {
        double boxCount = 0;
        foreach (ShoppingCartDetail detail in shipment.details) {
            if ( null == detail.modelItem ) {
                detail.modelItem = ModelItemSvc.FindById(tran, detail.modelItemId);
                Logger.ASSERT(null == detail.modelItem.item);
            }
            double count = (detail.qtyOrdered / detail.modelItem.item.qtyIncrement);
            boxCount += count;
        }
        return Decimal.ToInt32(Decimal.Ceiling(Convert.ToDecimal(boxCount)));
    }
    private static int CalculateNettoShippingWeight(SqlTransaction tran, ShoppingCartShipment shipment) {
        double result = 0;
        foreach (ShoppingCartDetail detail in shipment.details) {
            if ( null == detail.modelItem ) {
                detail.modelItem = ModelItemSvc.FindById(tran, detail.modelItemId);
                Logger.ASSERT(null == detail.modelItem.item);
            }
            double count = (detail.qtyOrdered / detail.modelItem.item.qtyIncrement);
            double weight = Convert.ToDouble(detail.modelItem.item.weight * count);
            result += weight;
        }
        return Decimal.ToInt32(Decimal.Ceiling(Convert.ToDecimal(result)));
    }

    private static int CalculatePackCount(SqlTransaction tran, ShoppingCartShipment shipment) {
        double result = CalculateNettoShippingWeight(tran, shipment);
        double boxCount = CalculateBoxCount(tran, shipment);
        if ( boxCount > 10.0 || result > ShippingPriceSvc.BASE_WEIGHT )  {
           if ( ShippingPriceSvc.BASE_WEIGHT > result ) {
               return 1;
           } else {
               return Decimal.ToInt32(Decimal.Ceiling(Convert.ToDecimal(result/500.0)));
           }
        }
        return 1;
    }
    private static int CalculateBruttoShippingWeight(SqlTransaction tran, ShoppingCartShipment shipment) {
        double result = CalculateNettoShippingWeight(tran, shipment);
        double boxCount = CalculateBoxCount(tran, shipment);
        if ( boxCount > 10.0 || result > ShippingPriceSvc.BASE_WEIGHT )  {
           if ( ShippingPriceSvc.BASE_WEIGHT > result ) {
               result += 44.5;
           } else {
               result += Convert.ToDouble(Decimal.Ceiling(Convert.ToDecimal((result/500.0))))*44.5;
           }
        }
        return Decimal.ToInt32(Decimal.Ceiling(Convert.ToDecimal(result)));
    }

} 

};