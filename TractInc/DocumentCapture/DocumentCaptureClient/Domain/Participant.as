package Domain
{
    import Domain.Codegen.*;
    import mx.collections.ArrayCollection;

    [Bindable]
    [RemoteClass(alias="TractInc.DocCapture.Domain.Participant")]
    public dynamic class Participant extends _Participant
    {

        public var isNew:Boolean;
        
        public var isSeller:Boolean;
        
        public function Participant():void {
            AsNamed = "";
            EntityName = "";
            FirstName = "";
            MiddleName = "";
            LastName = "";
            ContactPosition = "";
            PhoneHome = "";
            PhoneOffice = "";
            PhoneCell = "";
            PhoneAlt = "";
            TAXID = "";
            SSN = "";
        }
    }
}
    