using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Services.Protocols;
using AerSysCo.Common;
using AerSysCo.Entity;
using AerSysCo.CustomerCenter;
using AirSysco.Packaging;
using AerSysCo.Warehouse.WebReference;



namespace AerSysCo.Warehouse
{
public class ShippingPriceSvc
{
    public const int BASE_WEIGHT = 150;
    class PriceNotFound : Exception  {
    }

    public static ShipmentShippingOptions CalculateShipment(SqlTransaction tran, ShoppingCart cart, ShoppingCartShipment shipment) {
        ShipmentShippingOptions result = new ShipmentShippingOptions();

            // Build a RateRequest object
            AerSysCo.Entity.Warehouse warehouse = WarehouseSvc.FindById(tran, shipment.warehouseId);
            result.shoppingCartShipmentId = shipment.shoppingCartShipmentId;
            Logger.ASSERT(null != ConfigurationManager.AppSettings["lift_gate_price"]);
            Logger.ASSERT(!"".Equals(ConfigurationManager.AppSettings["lift_gate_price"]));
            try {
                result.liftGatePrice = Int32.Parse(ConfigurationManager.AppSettings["lift_gate_price"]); //125;
            } catch (Exception ex) {
                Logger.GetSysLogger().Fatal(ex.Message, ex);
                throw new SystemException("Configuration Error. Please notify support");
            }

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
            request.RequestedShipment.Shipper.Address = new AerSysCo.Warehouse.WebReference.Address();
            request.RequestedShipment.Shipper.Address.StreetLines = new string[2] { warehouse.address1, null == warehouse.address2?"":warehouse.address2};
            request.RequestedShipment.Shipper.Address.City = warehouse.city;
            request.RequestedShipment.Shipper.Address.StateOrProvinceCode = warehouse.state;
            request.RequestedShipment.Shipper.Address.PostalCode = warehouse.zip;
            request.RequestedShipment.Shipper.Address.CountryCode = null == warehouse.country || "".Equals(warehouse.country)?"US":warehouse.country;
            //

            request.RequestedShipment.Recipient = new Party(); // Destination Information
            request.RequestedShipment.Recipient.Address = new AerSysCo.Warehouse.WebReference.Address();
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
            request.RequestedShipment.ShippingChargesPayment.Payor.AccountNumber = ConfigurationManager.AppSettings[cart.customer.brandName+"_fedex_account"]; // Replace "XXX" with clients account number
            request.RequestedShipment.DropoffType = DropoffType.REGULAR_PICKUP; //Drop off types are BUSINESS_SERVICE_CENTER, DROP_BOX, REGULAR_PICKUP, REQUEST_COURIER, STATION

            request.RequestedShipment.TotalInsuredValue = new Money();
            request.RequestedShipment.TotalInsuredValue.Amount = 0;
            request.RequestedShipment.TotalInsuredValue.Currency = "USD";
            request.RequestedShipment.ShipTimestamp = DateTime.Now; // Shipping date and time
            request.RequestedShipment.ShipTimestampSpecified = false;
            request.RequestedShipment.RateRequestTypes = new RateRequestType[1];
            request.RequestedShipment.RateRequestTypes[0] = RateRequestType.ACCOUNT;

            Logger.GetAppLogger().Debug("Before Packaging");
            PackageSet[] packages = PackageService.Package(SQLHelper.CONN_STRING, MakePackageRequest(shipment), shipment.shoppingCartShipmentId);
            if ( Logger.GetAppLogger().IsDebugEnabled ) {
                List<SqlParameter> parms = new List<SqlParameter>();
                parms.Add(new SqlParameter("@ShipmentId", shipment.shoppingCartShipmentId));
                SQLHelper.ExecuteNonQuery(tran, CommandType.Text, "delete from FedexRates where ShipmentId = @ShipmentId", parms.ToArray());
                SQLHelper.ExecuteNonQuery(tran, CommandType.Text, "update ShoppingCartShipment set ShippingMultiplier = null where ShoppingCartShipmentId = @ShipmentId", parms.ToArray());

                String message = "Pack count: "+packages.Length;
                foreach( PackageSet ps in packages ) {
                    message += String.Format(" {0} ({1},{2},{3}) ",ps.weightLB, ps.lengthIN, ps.heightIN, ps.widthIN);
                }
                Logger.GetAppLogger().Debug(message);
            }
            Logger.GetAppLogger().Debug("After Packaging");

            request.RequestedShipment.PackageCount = packages.Length.ToString();
            request.RequestedShipment.PackageDetail = RequestedPackageDetailType.INDIVIDUAL_PACKAGES;
            request.RequestedShipment.RequestedPackages = new RequestedPackage[packages.Length];

            for ( int i = 0; i<packages.Length; i++ ) {
                request.RequestedShipment.RequestedPackages[i] = new RequestedPackage();
                request.RequestedShipment.RequestedPackages[i].SequenceNumber = (i+1).ToString();

                request.RequestedShipment.RequestedPackages[i].Weight = new Weight(); // package weight
                request.RequestedShipment.RequestedPackages[i].Weight.Units = WeightUnits.LB;
                request.RequestedShipment.RequestedPackages[i].Weight.Value = packages[i].weightLB;
                //
                if ( 0 != packages[i].lengthIN && 0 != packages[i].widthIN && 0 != packages[i].heightIN ) {
                    request.RequestedShipment.RequestedPackages[i].Dimensions = new Dimensions(); // package dimensions
                    request.RequestedShipment.RequestedPackages[i].Dimensions.Length = packages[i].lengthIN.ToString();
                    request.RequestedShipment.RequestedPackages[i].Dimensions.Width = packages[i].widthIN.ToString();
                    request.RequestedShipment.RequestedPackages[i].Dimensions.Height = packages[i].heightIN.ToString();
                    request.RequestedShipment.RequestedPackages[i].Dimensions.Units = LinearUnits.IN;
                }
            }


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
                        ShippingType t = ShippingTypeSvc.FromFedEx(tran, rateDetail.ServiceType);
                        if ( null != t && ("CA" != cart.shippingAddress.country || t.shippingTypeId == 5 || t.shippingTypeId == 6 )) {
                            ShippingOption option = new ShippingOption();
                            option.shippingTypeId = t.shippingTypeId;
                            option.shippigType = t;
                            option.isApplicable = true;
                            result.options.Add(option);
                            foreach (RatedShipmentDetail shipmentDetail in rateDetail.RatedShipmentDetails) {
                                if ( shipmentDetail.ShipmentRateDetail.RateType == ReturnedRateType.PAYOR_ACCOUNT ) {
                                    option.cost += shipmentDetail.ShipmentRateDetail.TotalNetCharge.Amount;
                                    Logger.GetAppLogger().Debug("..............  USED   ....................");
                                    if ( Logger.GetAppLogger().IsDebugEnabled ) {
                                        List<SqlParameter> parms = new List<SqlParameter>();

                                        parms.Add(new SqlParameter("@ShipmentId", shipment.shoppingCartShipmentId));
                                        parms.Add(new SqlParameter("@FedexServiceType",rateDetail.ServiceType.ToString()));
                                        parms.Add(new SqlParameter("@FedexRateType",shipmentDetail.ShipmentRateDetail.RateType.ToString()));
                                        parms.Add(new SqlParameter("@FedexBaseCharge",shipmentDetail.ShipmentRateDetail.TotalBaseCharge.Amount));
                                        parms.Add(new SqlParameter("@FedexDiscount",shipmentDetail.ShipmentRateDetail.TotalFreightDiscounts.Amount));
                                        parms.Add(new SqlParameter("@FedexSurcharges",shipmentDetail.ShipmentRateDetail.TotalSurcharges.Amount));
                                        parms.Add(new SqlParameter("@FedexNetCharge",shipmentDetail.ShipmentRateDetail.TotalNetCharge.Amount));

                                        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, 
                                             " insert into FedexRates ( ShipmentId, FedexServiceType, FedexRateType,  "
                                            +"        FedexBaseCharge, FedexDiscount, FedexSurcharges, FedexNetCharge)"
                                            +" values ( @ShipmentId, @FedexServiceType, @FedexRateType,  "
                                            +"        @FedexBaseCharge, @FedexDiscount, @FedexSurcharges, @FedexNetCharge)", 
                                            parms.ToArray());
                                    }
                                } else {
                                    Logger.GetAppLogger().Debug(".............. UN USED ....................");
                                }
                                if ( Logger.GetAppLogger().IsDebugEnabled ) {
                                    Logger.GetAppLogger().Debug("RateType = " + shipmentDetail.ShipmentRateDetail.RateType);
                                    Logger.GetAppLogger().Debug("Total Billing Weight = " + shipmentDetail.ShipmentRateDetail.TotalBillingWeight.Value);
                                    Logger.GetAppLogger().Debug("Total Base Charge = " + shipmentDetail.ShipmentRateDetail.TotalBaseCharge.Amount);
                                    Logger.GetAppLogger().Debug("Total Discount = " + shipmentDetail.ShipmentRateDetail.TotalFreightDiscounts.Amount);
                                    Logger.GetAppLogger().Debug("Total Surcharges = " + shipmentDetail.ShipmentRateDetail.TotalSurcharges.Amount);
                                    Logger.GetAppLogger().Debug("Net Charge = " + shipmentDetail.ShipmentRateDetail.TotalNetCharge.Amount);
                                    Logger.GetAppLogger().Debug("....................................................................");

                                }
                            }
                            double mult = GetShippingMultiplier(tran, cart.shippingAddress.country, t.shippingTypeId, cart.brandId, cart.total);

                            option.cost *= new Decimal(mult);

                            if ( Logger.GetAppLogger().IsDebugEnabled ) {
                                Logger.GetAppLogger().DebugFormat("Multiplier is {0} cost is {1}, brandId {2}, shippingTypeID {3}, baseCost {4}, country {5}", 
                                        mult, option.cost * new Decimal(mult), cart.brandId, t.shippingTypeId, option.cost, cart.shippingAddress.country);

                                List<SqlParameter> parms = new List<SqlParameter>();
                                parms.Add(new SqlParameter("@ShipmentId", shipment.shoppingCartShipmentId));
                                parms.Add(new SqlParameter("@mult", mult));

                                SQLHelper.ExecuteNonQuery(tran, CommandType.Text, "update ShoppingCartShipment set ShippingMultiplier = @mult where ShoppingCartShipmentId = @ShipmentId", parms.ToArray());

                            }

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
            } catch ( ApplicationException ) {
                throw;
            } catch (Exception e) {
                Logger.GetAppLogger().Error(e.Message, e);
                throw new SystemException(e.Message);
            }
        return result;
    }

    private static int CalculateBoxCount(SqlTransaction tran, ShoppingCartShipment shipment, bool doLog) {
        double boxCount = 0;

if ( doLog ) Logger.GetAppLogger().Debug("vvvvv BOXES CALCULATION vvvvv ");

        foreach (ShoppingCartDetail detail in shipment.details) {
            if ( null == detail.modelItem ) {
                detail.modelItem = ModelItemSvc.FindById(tran, detail.modelItemId);
                Logger.ASSERT(null == detail.modelItem.item);
            }
            double count = (detail.qtyOrdered / detail.modelItem.item.qtyIncrement);

if ( doLog ) Logger.GetAppLogger().DebugFormat("SKU {0} count=ordered/qtyIncrement {1}={2}/{3}", 
                  detail.modelItem.item.sku, count, detail.qtyOrdered, detail.modelItem.item.qtyIncrement);

            boxCount += count;
        }
if ( doLog ) Logger.GetAppLogger().Debug("^^^^^ BOXES CALCULATION ^^^^^");
        return Decimal.ToInt32(Decimal.Ceiling(Convert.ToDecimal(boxCount)));
    }

    private static int CalculateNettoShippingWeight(SqlTransaction tran, ShoppingCartShipment shipment, bool doLog) {
        double result = 0;
if ( doLog ) Logger.GetAppLogger().Debug("NETTO weight calculation");

        foreach (ShoppingCartDetail detail in shipment.details) {
            if ( null == detail.modelItem ) {
                detail.modelItem = ModelItemSvc.FindById(tran, detail.modelItemId);
                Logger.ASSERT(null == detail.modelItem.item);
            }
            double count = (detail.qtyOrdered / detail.modelItem.item.qtyIncrement);
if ( doLog ) Logger.GetAppLogger().DebugFormat("SKU {0} count = ordered/increment {1}={2}/{3};  ", detail.modelItem.item.sku, count, detail.qtyOrdered, detail.modelItem.item.qtyIncrement);

            double weight = Convert.ToDouble(detail.modelItem.item.weight * count);
if ( doLog ) Logger.GetAppLogger().DebugFormat("SKU {0} weight = count* itemWeight {1}={2}*{3};  ", detail.modelItem.item.sku, weight, count, detail.modelItem.item.weight);

            result += weight;
        }
        return Decimal.ToInt32(Decimal.Ceiling(Convert.ToDecimal(result)));
    }

    private static int CalculatePackCount(SqlTransaction tran, ShoppingCartShipment shipment) {
        double result = CalculateNettoShippingWeight(tran, shipment, false);
        double boxCount = CalculateBoxCount(tran, shipment, false);
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
Logger.GetAppLogger().Debug("vvvvv Weight Calculation vvvvv");
        double result = CalculateNettoShippingWeight(tran, shipment, true);
Logger.GetAppLogger().DebugFormat("NETTO weight is {0}", result);

        double boxCount = CalculateBoxCount(tran, shipment, true);
Logger.GetAppLogger().DebugFormat("BOX count is {0}", boxCount);

        if ( boxCount > 10.0 || result > ShippingPriceSvc.BASE_WEIGHT )  {
           if ( ShippingPriceSvc.BASE_WEIGHT > result ) {
Logger.GetAppLogger().DebugFormat("Netto weight {0} < 150, Brutto weight {1}={2}+44.5", result, result+44.5, result );
               result += 44.5;
           } else {
Logger.GetAppLogger().DebugFormat("Netto weight {0} > 150 and boxCount {1}>10 , Brutto weight {2}=({3}/500)*44.5", 
    result, 
    boxCount, 
    result+Convert.ToDouble(Decimal.Ceiling(Convert.ToDecimal((result/500.0))))*44.5, 
    result );
               result += Convert.ToDouble(Decimal.Ceiling(Convert.ToDecimal((result/500.0))))*44.5;
           }
        }
Logger.GetAppLogger().Debug("^^^^^ Weight Calculation ^^^^^");
        return Decimal.ToInt32(Decimal.Ceiling(Convert.ToDecimal(result)));
    }

    private static double GetShippingMultiplier(SqlTransaction tran, String country, int shippingTypeId, 
                                                int brandId, decimal cost) 
    {
        string sql = "select Multiplier " 
                   + "  from ShippingMultiplier "
                   + " where BrandId = @brandId "
                   + "   and ShippingTypeId = @ShippingTypeId "
                   + "   and Country = @Country "
                   + "   and CostGreateThan < @cost " 
                   + "    and @cost <= CostLessOrEqualThan";
       SqlParameter[] parms = new SqlParameter[4];
       parms[0] = new SqlParameter("@brandId", brandId);
       parms[1] = new SqlParameter("@ShippingTypeId", shippingTypeId);
       parms[2] = new SqlParameter("@cost", cost);
       parms[3] = new SqlParameter("@Country", country);
       List<ShippingType> result = new List<ShippingType>();
       using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms) ) {
           if( rdr.Read() ) {
               return rdr.GetDouble(rdr.GetOrdinal("Multiplier"));
           } else {
               return 1;
           }
       }
    }

    private static PackageRequest[] MakePackageRequest(ShoppingCartShipment shipment) {
        List<PackageRequest> result = new List<PackageRequest>();
        foreach( ShoppingCartDetail detail in shipment.details ) {
            PackageRequest request = new PackageRequest();
            request.SKU = detail.sku;
            request.ItemCount = detail.qtyOrdered;
            result.Add(request);
        }
        return result.ToArray();
    }

#if FALSE

    public static ShipmentShippingOptions CalculateShipment(SqlTransaction tran, ShoppingCart cart, ShoppingCartShipment shippment) {
        ShipmentShippingOptions result = new ShipmentShippingOptions();
        result.shoppingCartShipmentId = shippment.shoppingCartShipmentId;
        result.liftGatePrice = 125;
        result.options = new List<ShippingOption>();

        int weight = CalculateShippingWeight(tran, shippment);
        Logger.GetAppLogger().Debug("weight "+weight);
        Brand brand = BrandSvc.FindById(tran, cart.brandId);
        int distance = CalculateShippingDistance(tran, shippment, cart.shippingAddress, brand);
        Logger.GetAppLogger().Debug("distance "+distance);
        List<ShippingType> types = ShippingTypeSvc.GetAll(tran);

        foreach (ShippingType t in types ) {
            ShippingOption opt = new ShippingOption();
            opt.shippingTypeId = t.shippingTypeId;
            opt.shippigType = t;
            try {
                try {
                    Decimal price = GetPrice(tran, weight, distance, t.shippingTypeId);
                    opt.cost = price*weight;
                } catch (PriceNotFound) {
                    Decimal price = GetPrice(tran, BASE_WEIGHT, distance, t.shippingTypeId);
                    opt.cost = price*weight;
                }
                if ( 1 == t.shippingTypeId ) {
                    opt.cost = opt.cost * 1.5m;
                } else {
                    opt.cost = opt.cost * 1.2m;
                }
                opt.isApplicable = true;
            } catch (PriceNotFound) {
                string msg = string.Format("Delivery price not found for weight {0} distance {1} shipping type {2}, shopping cartid {3} ", 
                                        weight, distance, t.shippingCode, cart.shoppingCartId);
                Logger.GetAppLogger().Warn(msg);
                opt.isApplicable = false;
                opt.cost = 0;
            }
            result.options.Add(opt);
        }
        return result;
    }

    private static int CalculateShippingWeight(SqlTransaction tran, ShoppingCartShipment shipment) {
        double result = 0;
        double boxCount = 0;
        foreach (ShoppingCartDetail detail in shipment.details) {
            if ( null == detail.modelItem ) {
                detail.modelItem = ModelItemSvc.FindById(tran, detail.modelItemId);
                Logger.ASSERT(null == detail.modelItem.item);
            }
            double count = (detail.qtyOrdered / detail.modelItem.item.qtyIncrement);
            double weight = Convert.ToDouble(detail.modelItem.item.weight * count);
            result += weight;
            boxCount += count;
        }
        if ( boxCount > 10.0 || result > BASE_WEIGHT )  {
           if ( BASE_WEIGHT > result ) {
               result += 44.5;
           } else {
               result += Convert.ToDouble(Decimal.Ceiling(Convert.ToDecimal((result/500.0))))*44.5;
           }
        }
        return Decimal.ToInt32(Decimal.Ceiling(Convert.ToDecimal(result)));

    }

    private static int CalculateShippingDistance(SqlTransaction tran, ShoppingCartShipment shipment, Address destination, Brand brand) {
        if ( null == shipment.warehouse ) {
            shipment.warehouse = WarehouseSvc.FindById(tran, shipment.warehouseId);
        }
        CustomerCenterService svc = new CustomerCenterService(brand.brandName, brand.code, brand.imageURLprefix);
        return svc.CalculateDistance(shipment.warehouse.zip, destination.zip);
    }

    private static decimal GetPrice(SqlTransaction tran, int weight, int distnace, int shippingTypeId) {
        string sql = 
            " select sp.Price "
           +"   from ShippingPrice sp "
           +"        inner join ShippingZones sz on sp.ZoneId = sz.ZoneId "
           +"  where @distance >= sz.StartDistance and @distance <= sz.EndDistance "
           +"    and @weight >= sp.StartWeight and @weight <= sp.EndWeight "
           +"    and ShippingTypeId = @ShippingTypeId " ;
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@ShippingTypeId", shippingTypeId));
        parms.Add(new SqlParameter("@distance", distnace)); 
        parms.Add(new SqlParameter("@weight", weight));

        Decimal result;
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
            if( rdr.Read() ) {
                result = rdr.GetDecimal(rdr.GetOrdinal("Price"));
            } else {
                throw new PriceNotFound();
            }
        }
        return result;
    }

#endif 
}
}
