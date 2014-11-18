
package com.dalworth.leadCentral.domain
{
    import Intuit.Sb.Cdm.vo.Customer;
    import Intuit.Sb.Cdm.vo.Invoice;
    
    import com.dalworth.leadCentral.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.QbInvoice")]
    public class QbInvoice extends _QbInvoice
    {
        public var RelatedIdsInvoice:Invoice;
        public var RelatedIdsCustomer:Customer;

        public var MatchLevel:int;
        public var IsMatched:Boolean;

        public function QbInvoice()
        {
        }
    }
}
      