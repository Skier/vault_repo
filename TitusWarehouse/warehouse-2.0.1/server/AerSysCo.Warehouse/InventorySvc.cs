using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AerSysCo.Common;
using AerSysCo.Entity;

namespace AerSysCo.Warehouse
{
public class InventorySvc : TraceableSvc
{
    public static Inventory FindByItemWarehouse(SqlTransaction tran, int itemId, int warehouseId) {
        Logger.ASSERT(0 != itemId);
        Logger.ASSERT(0 != warehouseId);

        List<Inventory> result = Select(tran, itemId, warehouseId);
        if ( 0 == result.Count ) {
            return null;
        } 
        Logger.ASSERT(1 <= result.Count);
        return result[0];
    }

    public static Inventory Update(SqlTransaction tran, Inventory inv) {
        Logger.ASSERT(null != inv);
        string sql = 
            " update Inventory "
           +"    set Qty = @Qty, "
           +"        MacPac_Inventory_id = @MacPac_Inventory_id, "
           +"        LastUpdateDate = @LastUpdateDate "
           +"  where InventoryId = @InventoryId ";
        inv.lastUpdateDate = DateTime.Now;

        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@Qty", inv.qty));
        parms.Add(new SqlParameter("@LastUpdateDate", inv.lastUpdateDate));
        parms.Add(new SqlParameter("@InventoryId", inv.inventoryId));
        parms.Add(new SqlParameter("@MacPac_Inventory_id", inv.MacPac_Inventory_id));
        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        return inv;
    }

    public static Inventory Insert(SqlTransaction tran, Inventory inv) {
        Logger.ASSERT(null != inv);
        string sql = 
            " insert into Inventory (WarehouseId, ItemId, Qty, DateCreated, CreatedByUser, LastUpdateDate, MacPac_Inventory_id ) "
           +" values (@WarehouseId, @ItemId, @Qty, @DateCreated, @CreatedByUser, @LastUpdateDate, @MacPac_Inventory_id) ";

        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@WarehouseId", inv.warehouseId ));
        parms.Add(new SqlParameter("@ItemId", inv.itemId ));
        parms.Add(new SqlParameter("@Qty", inv.qty ));
        parms.Add(new SqlParameter("@DateCreated", inv.dateCreated ));
        parms.Add(new SqlParameter("@CreatedByUser", inv.createdByUser ));
        parms.Add(new SqlParameter("@LastUpdateDate", inv.lastUpdateDate ));
        parms.Add(new SqlParameter("@MacPac_Inventory_id", inv.MacPac_Inventory_id));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        inv.inventoryId = SQLHelper.GetIdentity(tran);
        return inv;

    }

    public static Inventory FromReader(SqlDataReader rdr) {
        Inventory inv = new Inventory(); 
        inv.itemId = rdr.GetInt32(rdr.GetOrdinal("ItemId"));
        inv.inventoryId = rdr.GetInt32(rdr.GetOrdinal("InventoryId"));
        inv.qty = rdr.GetInt32(rdr.GetOrdinal("Qty"));
        inv.warehouseId = rdr.GetInt32(rdr.GetOrdinal("WarehouseId"));
        TraceableSvc.FromReader(rdr, inv);
        return inv;
    }    

    private static List<Inventory> Select(SqlTransaction tran, int itemId, int warehouseId) {
        string sql = 
            "select InventoryId, WarehouseId, ItemId, Qty, DateCreated, CreatedByUser, LastUpdateDate "
           +"  from Inventory " 
           +" where 1=1 "; 
        List<SqlParameter> parms = new List<SqlParameter>();
        if ( 0 != itemId ) {
            sql += " and ItemId = @ItemId ";
            parms.Add(new SqlParameter("@ItemId", itemId));
        }
        if ( 0 != warehouseId ) {
            sql += " and  WarehouseId = @WarehouseId ";
            parms.Add(new SqlParameter("@WarehouseId", warehouseId));
        }
        List<Inventory> result = new List<Inventory>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
             while( rdr.Read() ) {
                 result.Add(FromReader(rdr));
             }
        }
        return result;
    }
};
};