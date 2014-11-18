package truetract.domain
{
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.DateRange")]
public class DateRange
{
    public var dateFrom:Date;
    public var dateTo:Date;
    
    public static function create(dateFrom:Date, dateTo:Date):DateRange
    {
        var result:DateRange = new DateRange();
        result.dateFrom = dateFrom;
        result.dateTo = dateTo;
        
        return result;
    }
    
    public function inRange(date:Date):Boolean
    {
        return date >= dateFrom && date <= dateTo;
    }
}
}