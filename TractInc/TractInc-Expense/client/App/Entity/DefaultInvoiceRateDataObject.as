package App.Entity
{
	import common.TypesRegistry;
	
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.DefaultInvoiceRateDataObject")]
	public class DefaultInvoiceRateDataObject
	{
		
        public var DefaultInvoiceRateId:int;

        public var ClientId:int;

        public var InvoiceItemTypeId:int;

        public var InvoiceRate:Number;
        
        public var Dummy:int;

        public function get InvoiceItemTypeName():String {
        	return TypesRegistry.instance.getInvoiceItemTypeById(InvoiceItemTypeId).Name;
        }

        public function get IsPresetRate():Boolean {
        	return TypesRegistry.instance.getInvoiceItemTypeById(InvoiceItemTypeId).IsPresetRate;
        }

	}
	
}
