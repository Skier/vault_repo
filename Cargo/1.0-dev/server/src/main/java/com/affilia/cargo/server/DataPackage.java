/**
 * $Id: DataPackage.java 280 2007-06-20 09:06:07Z moritur $
 */

package com.affilia.cargo.server;

import java.util.Date;
import java.util.Calendar;
import java.sql.Timestamp;
import java.util.StringTokenizer;
import com.affilia.cargo.data.CargoDAO;

public abstract class DataPackage
{
    private final static String NORTH ="N";
    private final static String SOUTH ="S";
    private final static String EAST ="E";
    private final static String WEST ="W";
    
    
    protected StringTokenizer parser = null;
    protected Integer networkId = null;
    protected String itemId = null;
    protected Timestamp timestamp = null;
    protected Double latitude = null;
    protected Double longitude = null;

    public DataPackage(Integer networkId, String data) {
        this.networkId = networkId;
        parser = new StringTokenizer(data, ";");
    }

    public abstract void storeTo(CargoDAO dao);

    public void parse() {
        String packageType = parser.nextToken();
        itemId = parser.nextToken();

        timestamp = parse(parser.nextToken());

        latitude = new Double(parser.nextToken());
        latitude = DataPackage.converGeograficalCoordinateToDecimal(latitude);

        String northSouth = parser.nextToken();
        if ( DataPackage.SOUTH.equals(northSouth) ) {
            latitude = new Double(-1*latitude.doubleValue());
        }
        

        longitude = new Double(parser.nextToken());
        longitude = DataPackage.converGeograficalCoordinateToDecimal(longitude);

        String eastWest = parser.nextToken();
        if ( DataPackage.WEST.equals(eastWest) ) {
            longitude = new Double(-1*longitude.doubleValue());
        }
        
        
    }

    protected Timestamp parse(String ts) {
        StringTokenizer st = new StringTokenizer(ts, ".");
        String hhmmss = st.nextToken();
        String nano = st.nextToken();
//        System.out.println("hhmmss=[" + hhmmss + "] nano=" + nano);
        String hh = hhmmss.substring(0, 2);
        String mm = hhmmss.substring(2, 4);
        String ss = hhmmss.substring(4, 6);
    
        Date today = Calendar.getInstance().getTime();
        return new Timestamp(today.getYear(), 
            today.getMonth(),
            today.getDate(),
            new Integer(hh).intValue(),
            new Integer(mm).intValue(),
            new Integer(ss).intValue(),
            new Integer(nano).intValue());
    }

    
    public static double converGeograficalCoordinateToDecimal(double gCoordinate) {

        gCoordinate = gCoordinate / 100.0;
        long grad = Math.round(gCoordinate);
        
        gCoordinate = ( gCoordinate - grad ) * 100.0;
        long minute = Math.round(gCoordinate);
    
        gCoordinate = ( gCoordinate - minute ) * 100.0;
        long sec = Math.round(gCoordinate);
        
        double remainder = ( gCoordinate - sec ) / 10000.0;
        
        return grad + minute / 60.0 + sec / 3600.0 + remainder;
        
    }
    
}
