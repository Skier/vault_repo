using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class DictionariesDataObject
    {

        public List<BillStatusDataObject> BillStatuses;

        public List<BillItemStatusDataObject> BillItemStatuses;

        public List<InvoiceStatusDataObject> InvoiceStatuses;

        public List<InvoiceItemStatusDataObject> InvoiceItemStatuses;

        public List<BillItemTypeDataObject> BillItemTypes;

        public List<InvoiceItemTypeDataObject> InvoiceItemTypes;

        public List<AssetTypeDataObject> AssetTypes;

        public List<ClientDataObject> Clients;

        public List<AFEStatusDataObject> AFEStatuses;

        public List<ProjectStatusDataObject> ProjectStatuses;

    }

}
