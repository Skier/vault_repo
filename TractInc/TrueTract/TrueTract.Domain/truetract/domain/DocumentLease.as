package truetract.domain
{
    
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.DocumentLeaseInfo")]
public class DocumentLease 
{
    public var DocLeaseId:int;
    public var DocId:int;
    public var LCN:String = null;
    public var Term:int = 0;
    public var Royalty:Number = 0.0;
    public var EffectiveDate:Date;
    public var Acreage:Number = 0.0;
    public var AliasGrantee:String = null;
    public var ExpirationDate:Date;
    public var HBP:Boolean = false;

    public function clone():DocumentLease
    {
        var clone:DocumentLease = new DocumentLease();
        
        clone.DocLeaseId = DocLeaseId;
        clone.DocId = DocId;
        clone.LCN = LCN;
        clone.Term = Term;
        clone.Royalty = Royalty;
        clone.EffectiveDate = EffectiveDate;
        clone.Acreage = Acreage;
        clone.AliasGrantee = AliasGrantee;
        clone.ExpirationDate = ExpirationDate;
        clone.HBP = HBP;

        return clone;
    }

    public function toString():String {
        return "DocumentLease: DocLeaseId=" + DocLeaseId +
            ", DocId=" + DocId +
            ", LCN=" + LCN +
            ", Term=" + Term +
            ", Royalty=" + Royalty +
            ", EffectiveDate=" + EffectiveDate +
            ", Acreage=" + Acreage +
            ", AliasGrantee=" + AliasGrantee +
            ", ExpirationDate=" + ExpirationDate +
            ", HBP=" + HBP;
    }

}
}