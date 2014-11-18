using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TractInc.Expense.Domain;

namespace TractInc.Expense.Service
{
public class ExpenseService
{
    private readonly SyncLogDataMapper synclogMapper = new SyncLogDataMapper();
    private readonly BillItemDataMapper billitemMapper = new BillItemDataMapper();

    // returns AssetId based on username and password
    public int Login(string username, string password) {
        // to do: fix it
        return 3;
    }

    public string GenerateDeviceId() {
        return Guid.NewGuid().ToString();
    }

    public List<BillItem> SyncBillItems(string deviceId, int assetId, BillItem[] remoteItems) {
        DateTime newTimestamp = GenerateNewSyncStamp(deviceId, assetId);
        DateTime lastTimestamp = GetPrevSyncStamp(deviceId, assetId, newTimestamp);

        List<BillItem> result = billitemMapper.findChangedBillItems(assetId, lastTimestamp);

        foreach (BillItem item in remoteItems) {
            if ( BillItem.NEW == item.Status ) {
                item.Status = BillItem.SUBMITTED;
                billitemMapper.create(item);
            } else if ( BillItem.CORRECTED == item.Status ) {
                item.Status = BillItem.SUBMITTED;
                billitemMapper.update(item);
            } else {
                throw new Exception("Incorrect billitem status \"" + item.Status + "\"");
            }
        } 
        return result;
    }

    protected DateTime GenerateNewSyncStamp(string deviceId, int assetId) {
        SyncLog log = new SyncLog();
        log.DeviceId = deviceId;
        log.AssetId = assetId;
        synclogMapper.create(log);
        return synclogMapper.findMaxTimeStamp(deviceId, assetId, DateTime.MaxValue);    
    }

    protected DateTime GetPrevSyncStamp(string deviceId, int assetId, DateTime lastStamp) {
        return synclogMapper.findMaxTimeStamp(deviceId, assetId, lastStamp);    
    }

}
}
