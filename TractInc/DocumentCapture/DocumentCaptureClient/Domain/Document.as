package Domain
{
    import Domain.Codegen.*;
    import mx.collections.ArrayCollection;
        
    [Bindable]
    [RemoteClass(alias="TractInc.DocCapture.Domain.Document")]
    public dynamic class Document extends _Document
    {
        public var IsNew:Boolean;
            
        public function copy():Document{
            var result:Document = new Document();
            result.DocTypeId = DocTypeId;
            result.State = this.State;
            result.County = County;
            result.DocumentNo = DocumentNo;
            result.Vol = Vol;
            result.Pg = Pg;
            
            return result;
        }
    }
}
    