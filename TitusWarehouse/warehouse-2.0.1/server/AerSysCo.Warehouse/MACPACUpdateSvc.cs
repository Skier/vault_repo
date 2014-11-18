using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using AerSysCo.Common;
using AerSysCo.Entity;


namespace AerSysCo.Warehouse
{
public class MACPACInventorySvc
{
    public const string NEW="NEW";
    public const string SUCCESS="SUCCESS";
    public const string FAIL="FAIL";

#if FALSE
    public static void ProcessUpdate(SqlTransaction tran) {
        List<Item> items = ItemSvc.GetAll(tran);
        List<Entity.Warehouse> warehouses = WarehouseSvc.GetAll(tran);
        foreach ( Item item in items ) {
            foreach (Entity.Warehouse warehouse in warehouses ) {
                MACPACUpdate update = MACPACUpdateSvc.GetLastUpdate(tran, item.sku, warehouse.warehouseCode);
                if ( null != update && DateTime.MinValue == update.dateProcessed ) {
                    DateTime now = DateTime.Now;
                    Inventory inv = InventorySvc.FindByItemWarehouse(tran, item.itemId, warehouse.warehouseId);
                    if ( null != inv ) {
                        inv.qty = update.qtyAllocated;
                        inv.lastUpdateDate = DateTime.Now;
                        InventorySvc.Update(tran, inv);
                    } else {
                        inv = new Inventory();
                        inv.itemId = item.itemId;
                        inv.warehouseId = warehouse.warehouseId;
                        inv.qty = update.qtyAllocated;
                        inv.createdByUser = "Inventory Update Process";
                        inv.lastUpdateDate = now;
                        inv.dateCreated = now;
                        inv = InventorySvc.Insert(tran, inv);
                    }
                    InventoryChangeLog log = new InventoryChangeLog();
                    log.inventoryId = inv.inventoryId;
                    log.macPacUpdateId = update.macPacUpdateId;
                    log.changeDate = now;
                    log.qty = update.qtyAllocated;
                    log.balance = 0;
                    InventoryChangeLogSvc.Insert(tran, log);

                    update.dateProcessed = now;
                    MACPACUpdateSvc.Update(tran, update);
                }
            }
        }
    }

    public static MACPACUpdate Update(SqlTransaction tran, MACPACUpdate update) {
        Logger.ASSERT(null != tran);
        Logger.ASSERT(null != update);
        update.lastUpdateDate = DateTime.Now;
        string sql = " update MACPACUpdate  "
                   + "    set DateProcessed = @DateProcessed, "
                   + "        LastUpdateDate = @LastUpdateDate "
                   + "  where MacPacUpdateId = @MacPacUpdateId ";
        List<SqlParameter> parms = new List<SqlParameter>();
        if ( DateTime.MinValue == update.dateProcessed ) {
            parms.Add(new SqlParameter("@DateProcessed", DBNull.Value));
        } else {
            parms.Add(new SqlParameter("@DateProcessed", update.dateProcessed));
        }
        parms.Add(new SqlParameter("@LastUpdateDate", update.lastUpdateDate));
        parms.Add(new SqlParameter("@MacPacUpdateId", update.macPacUpdateId));
        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        return update;
    }

    private static MACPACUpdate GetLastUpdate(SqlTransaction tran, string sku, string warehouse) {
        Logger.ASSERT(null != sku);
        Logger.ASSERT(null != warehouse);
        Logger.ASSERT(null != tran);

        string sql = 
             " select top 1 MacPacUpdateId, WarehouseCode, BrandName, Model, Configuration, SKU, QtyOnDemand, "
            +"        QtyAllocated, DateProcessed, CreatedByUser, LastUpdateDate, DateCreated "
            +"   from MACPACUpdate "
            +"  where SKU = @SKU and WarehouseCode = @WarehouseCode "
            +"  order by MacPacUpdateId desc ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@SKU", sku));
        parms.Add(new SqlParameter("@WarehouseCode", warehouse));
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray() ) ) {
            return FromReader(rdr);
        } 
    }

    private static MACPACUpdate FromReader(SqlDataReader rdr) {
        MACPACUpdate macpac = new MACPACUpdate();
        macpac.macPacUpdateId = rdr.GetInt32(rdr.GetOrdinal("MacPacUpdateId"));
        macpac.warehouseCode = rdr.GetString(rdr.GetOrdinal("WarehouseCode"));
        macpac.brandName = rdr.GetString(rdr.GetOrdinal("BrandName"));
        macpac.model = rdr.GetString(rdr.GetOrdinal("Model"));
        macpac.configuration = rdr.GetString(rdr.GetOrdinal("Configuration"));
        macpac.sku = rdr.GetString(rdr.GetOrdinal("SKU"));
        macpac.qtyOnDemand = rdr.GetInt32(rdr.GetOrdinal("QtyOnDemand"));
        macpac.qtyAllocated = rdr.GetInt32(rdr.GetOrdinal("QtyAllocated"));
        macpac.dateProcessed = rdr.IsDBNull(rdr.GetOrdinal("DateProcessed")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("DateProcessed"));
        TraceableSvc.FromReader(rdr, macpac);
        return macpac;
    }
#endif

public static List<MACPACInventory> GetNew(SqlTransaction tran) {
    return Select(tran, MACPACInventorySvc.NEW);
}

public static List<MACPACMultiplier> GetMultipliers(SqlTransaction tran, string brand, string customerId) {
    string sql = " select mm.part, mh.customer_id, mm.marketing_program, mm.multiplier "
                +"   from macpac_multiplier_header mh  "
                +"        inner join MACPAC_Multiplier mm on mh.marketing_program = mm.marketing_program "
                +"                                        and mh.brand = mm.brand_code "
                +"  where mh.brand = @Brand and mh.customer_id = @CustomerId ";

    List<SqlParameter> parms = new List<SqlParameter>();
    parms.Add(new SqlParameter("@Brand", brand));
    parms.Add(new SqlParameter("@CustomerId", customerId));
    List<MACPACMultiplier> result = new List<MACPACMultiplier>();

    using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray())) {
        while ( rdr.Read() ) {
            MACPACMultiplier mult = new MACPACMultiplier();
            mult.brand = brand;
            mult.sku = rdr.GetString(rdr.GetOrdinal("part"));
            mult.marketingProgram = rdr.GetString(rdr.GetOrdinal("marketing_program"));
            mult.customerid = rdr.GetString(rdr.GetOrdinal("customer_id"));
            mult.multiplier = rdr.GetDecimal(rdr.GetOrdinal("multiplier"));
            result.Add(mult);
        }
    }
    return result;
}

public static void MarkSuccess(SqlTransaction tran, int macPacInventoryId) {
    Logger.ASSERT(0 != macPacInventoryId);

    string sql = 
        "update MacPac_Inventory "
       +"   set ProcessedStatus = @Status "
       +" where MacPac_Inventory_id = @Id ";

    List<SqlParameter> parms = new List<SqlParameter>();
    parms.Add( new SqlParameter("@Status", MACPACInventorySvc.SUCCESS) );
    parms.Add( new SqlParameter("@Id", macPacInventoryId) );       
    SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
}

public static void MarkFail(SqlTransaction tran, int macPacInventoryId, string reason) {
    Logger.ASSERT(0 != macPacInventoryId);

    string sql = 
        "update MacPac_Inventory "
       +"   set ProcessedStatus = @Status, "
       +"       FailReason = @Reason "
       +" where MacPac_Inventory_id = @Id ";

    List<SqlParameter> parms = new List<SqlParameter>();
    parms.Add( new SqlParameter("@Status", MACPACInventorySvc.FAIL) );
    parms.Add( new SqlParameter("@Id", macPacInventoryId) );       
    parms.Add( new SqlParameter("@Reason", reason) );       
    SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
}

private static List<MACPACInventory> Select(SqlTransaction tran, string status) {
    string sql = 
        " select MacPac_Inventory_id, Plant, Brand, Part, PartDesc, AltDesc, Model, "
        +"       Configuration, ContainerCode, Height, Depth, Width, ContainerWeight, " 
        +"       partweight, qtypercontainer, BasePrice, OnHand, Allocated, PartStatus, "
        +"       MacPacTimeStamp, ImportTimeStamp, ProcessedStatus, FailReason "
        +"  from MacPac_Inventory  "
        +" where 1=1  "; 

    List<SqlParameter> parms = new List<SqlParameter>();
    if ( null != status ) {
        if ( MACPACInventorySvc.NEW.Equals(status) ) {
            sql += " and  ProcessedStatus is null ";
        } else {
            sql += " and  ProcessedStatus = @Status ";
            parms.Add(new SqlParameter("@Status", status));
        }
    }
    List<MACPACInventory> result = new List<MACPACInventory>();
    using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray())) {
        while ( rdr.Read() ) {
            result.Add(FromReader(rdr));
        }
    }

    return result;

}

private static MACPACInventory FromReader(SqlDataReader rdr) {
    MACPACInventory result = new MACPACInventory();

    result.macPac_Inventory_id = rdr.GetInt32(rdr.GetOrdinal("MacPac_Inventory_id"));
    result.plant = rdr.GetString(rdr.GetOrdinal("Plant"));
    result.brand = rdr.GetString(rdr.GetOrdinal("Brand"));
    result.part = rdr.GetString(rdr.GetOrdinal("Part"));
    result.partDesc = rdr.GetString(rdr.GetOrdinal("PartDesc"));
    result.altDesc = rdr.GetString(rdr.GetOrdinal("AltDesc"));
    result.model = rdr.GetString(rdr.GetOrdinal("Model"));
    result.configuration = rdr.GetString(rdr.GetOrdinal("Configuration"));
    result.containerCode = rdr.GetString(rdr.GetOrdinal("ContainerCode"));
    result.height = rdr.GetDecimal(rdr.GetOrdinal("Height"));
    result.depth = rdr.GetDecimal(rdr.GetOrdinal("Depth"));
    result.width = rdr.GetDecimal(rdr.GetOrdinal("Width"));
    result.containerWeight = rdr.GetDecimal(rdr.GetOrdinal("ContainerWeight"));
    result.partweight = rdr.GetDecimal(rdr.GetOrdinal("partweight"));
    result.qtypercontainer = rdr.GetDecimal(rdr.GetOrdinal("qtypercontainer"));
    result.basePrice = rdr.GetDecimal(rdr.GetOrdinal("BasePrice"));
    result.onHand = rdr.GetInt32(rdr.GetOrdinal("OnHand"));
    result.allocated = rdr.GetInt32(rdr.GetOrdinal("Allocated"));
    result.partStatus = rdr.GetString(rdr.GetOrdinal("PartStatus"));
    result.macPacTimeStamp = rdr.GetDateTime(rdr.GetOrdinal("MacPacTimeStamp"));
    result.importTimeStamp = rdr.GetDateTime(rdr.GetOrdinal("ImportTimeStamp"));
    result.processedStatus = rdr.IsDBNull(rdr.GetOrdinal("ProcessedStatus"))? null : rdr.GetString(rdr.GetOrdinal("ProcessedStatus"));
    result.failReason = rdr.IsDBNull(rdr.GetOrdinal("FailReason"))? null : rdr.GetString(rdr.GetOrdinal("FailReason"));

    return result;
}

}
}
