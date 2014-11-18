package TractInc.SDK.utils
{

public class DateHelper
{
    public static function isNullDate(dt:Date):Boolean
    {
        if ( null != dt ) {
            if ( dt.day == 1 && dt.month == 0 && dt.fullYear == 1 ) {
                return true;
            } else {
                return false;
            }
        } else {
            return true;
        }
    }
}

}