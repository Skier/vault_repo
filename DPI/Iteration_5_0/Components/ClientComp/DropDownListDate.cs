using System;
using System.Collections;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ClientComp
{	
	public class DropDownListDate // should be DropDownListData
	{
	#region Methods
		// Days
		public static IDropDownListItem[] GetDays()
		{			
			return GetDays(false);
		}
		public static IDropDownListItem[] GetDays(bool empty)
		{			
			ArrayList ar = new ArrayList();
			
			if (empty)
				AddEmpty(ar);
			
			for (int i = 1; i <= 31; i++)
				ar.Add(new DropDownListItem(i.ToString(), i.ToString()));
			
			return Conv(ar);
		}

		// Months
		public static IDropDownListItem[] GetMonths()
		{
			//return  GetCCMonths(false);
			ArrayList ar = new ArrayList();			

			ar.Add(new DropDownListItem("JAN", "1"));
			ar.Add(new DropDownListItem("FEB", "2"));
			ar.Add(new DropDownListItem("MAR", "3"));
			ar.Add(new DropDownListItem("APR", "4"));
			ar.Add(new DropDownListItem("MAY", "5"));
			ar.Add(new DropDownListItem("JUN", "6"));
			ar.Add(new DropDownListItem("JUL", "7"));
			ar.Add(new DropDownListItem("AUG", "8"));
			ar.Add(new DropDownListItem("SEP", "9"));
			ar.Add(new DropDownListItem("OCT", "10"));
			ar.Add(new DropDownListItem("NOV", "11"));
			ar.Add(new DropDownListItem("DEC", "12"));

			return Conv(ar);
		}

		public static IDropDownListItem[] GetCCMonths(bool empty)
		{
			ArrayList ar = new ArrayList();			

			if (empty)
				AddEmpty(ar);

			ar.Add(new DropDownListItem("Jan",  "01"));
			ar.Add(new DropDownListItem("Feb", "02"));
			ar.Add(new DropDownListItem("Mar", "03"));
			ar.Add(new DropDownListItem("Apr", "04"));
			ar.Add(new DropDownListItem("May", "05"));
			ar.Add(new DropDownListItem("Jun", "06"));
			ar.Add(new DropDownListItem("Jul", "07"));
			ar.Add(new DropDownListItem("Aug", "08"));
			ar.Add(new DropDownListItem("Sep", "09"));
			ar.Add(new DropDownListItem("Oct", "10"));
			ar.Add(new DropDownListItem("Nov", "11"));
			ar.Add(new DropDownListItem("Dec", "12"));

			return Conv(ar);
		}
	
		public static IDropDownListItem[] GetMonthsLong()
		{
			ArrayList ar = new ArrayList();			
			ar.Add(new DropDownListItem("", ""));
			ar.Add(new DropDownListItem("JANUARY", "01"));
			ar.Add(new DropDownListItem("FEBRUARY", "02"));
			ar.Add(new DropDownListItem("MARCH", "03"));
			ar.Add(new DropDownListItem("APRIL", "04"));
			ar.Add(new DropDownListItem("MAY", "05"));
			ar.Add(new DropDownListItem("JUNE", "06"));
			ar.Add(new DropDownListItem("JULY", "07"));
			ar.Add(new DropDownListItem("AUGUST", "08"));
			ar.Add(new DropDownListItem("SEPTEMBER", "09"));
			ar.Add(new DropDownListItem("OCTOBER", "10"));
			ar.Add(new DropDownListItem("NOVEMBER", "11"));
			ar.Add(new DropDownListItem("DECEMBER", "12"));

			return Conv(ar);
		}

		// Years
		public static IDropDownListItem[] GetYears()
		{
			ArrayList ar = new ArrayList();			

			for (int i = 1999; i <= DateTime.Now.Year; i++)
				ar.Add(new DropDownListItem(i.ToString(), i.ToString()));

			return Conv(ar);
		}
		public static IDropDownListItem[] GetYears(int start, int count)
		{
			ArrayList ar = new ArrayList();			

			for (int i = start; i <= count + start; i++)
				ar.Add(new DropDownListItem(i.ToString(), i.ToString()));

			return Conv(ar);
		}
		public static IDropDownListItem[] GetYears(int start, int count, bool empty)
		{
			ArrayList ar = new ArrayList();

			if (empty)
				AddEmpty(ar);

			for (int i = start; i <= count + start; i++)
				ar.Add(new DropDownListItem(i.ToString(), i.ToString()));

			return Conv(ar);
		}

		public static IDropDownListItem[] GetBirthYears(bool empty, int age)
		{
			ArrayList ar = new ArrayList();			

			if (empty)
				AddEmpty(ar);

			for (int i = DateTime.Now.Year - age; i > 1900; i--)
				ar.Add(new DropDownListItem(i.ToString(), i.ToString()));

			return Conv(ar);
		}

		public static IDropDownListItem[] GetCCYears(bool empty)
		{

			ArrayList ar = new ArrayList();
			int	size = 15;
			int year = DateTime.Now.Year;	

			if (empty)
				ar.Add(new DropDownListItem("", ""));

			for (int i = 0; i < size; i++)
				ar.Add( new DropDownListItem((i + year).ToString(), (i + year).ToString()));

			return Conv(ar);
		}

		// States
		public static IDropDownListItem[] GetStatesShort(bool empty)
		{
			ArrayList ar = new ArrayList(54);

			if (empty)
				AddEmpty(ar);

			ar.Add(new DropDownListItem("New Jersey","NJ"));
			ar.Add(new DropDownListItem("New Mexico","NM"));
			ar.Add(new DropDownListItem("New York","NY"));
			ar.Add(new DropDownListItem("Colorado","CO"));
			ar.Add(new DropDownListItem("Connecticut","CT"));
			ar.Add(new DropDownListItem("Alabama","AL"));
			ar.Add(new DropDownListItem("Alaska","AK"));
			ar.Add(new DropDownListItem("California","CA"));
			ar.Add(new DropDownListItem("Delaware","DE"));
			ar.Add(new DropDownListItem("District of Columbia","DC"));
			ar.Add(new DropDownListItem("Florida","FL"));
			ar.Add(new DropDownListItem("Georgia","GA"));
			ar.Add(new DropDownListItem("Hawaii","HI"));
			ar.Add(new DropDownListItem("Idaho","ID"));
			ar.Add(new DropDownListItem("Puerto Rico","PR"));
			ar.Add(new DropDownListItem("Rhode Island","RI"));
			ar.Add(new DropDownListItem("South Carolina","SC"));
			ar.Add(new DropDownListItem("South Dakota","SD"));
			ar.Add(new DropDownListItem("Tennessee","TN"));
			ar.Add(new DropDownListItem("Texas","TX"));
			ar.Add(new DropDownListItem("Utah","UT"));
			ar.Add(new DropDownListItem("Arizona","AZ"));
			ar.Add(new DropDownListItem("Arkansas","AR"));
			ar.Add(new DropDownListItem("Vermont","VT"));
			ar.Add(new DropDownListItem("Virgin Islands","VI"));
			ar.Add(new DropDownListItem("Virginia","VA"));
			ar.Add(new DropDownListItem("Washington","WA"));
			ar.Add(new DropDownListItem("West Virginia","WV"));
			ar.Add(new DropDownListItem("Wisconsin","WI"));
			ar.Add(new DropDownListItem("Wyoming","WY"));
			ar.Add(new DropDownListItem("Illinois","IL"));
			ar.Add(new DropDownListItem("Indiana","IN"));
			ar.Add(new DropDownListItem("Iowa","IA"));
			ar.Add(new DropDownListItem("Kansas","KS"));
			ar.Add(new DropDownListItem("Kentucky","KY"));
			ar.Add(new DropDownListItem("Louisiana","LA"));
			ar.Add(new DropDownListItem("Maine","ME"));
			ar.Add(new DropDownListItem("Maryland","MD"));
			ar.Add(new DropDownListItem("Mississippi","MS"));
			ar.Add(new DropDownListItem("Missouri","MO"));
			ar.Add(new DropDownListItem("Montana","MT"));
			ar.Add(new DropDownListItem("Nebraska","NE"));
			ar.Add(new DropDownListItem("Nevada","NV"));
			ar.Add(new DropDownListItem("New Hampshire","NH"));
			ar.Add(new DropDownListItem("North Carolina","NC"));
			ar.Add(new DropDownListItem("North Dakota","ND"));
			ar.Add(new DropDownListItem("Ohio","OH"));
			ar.Add(new DropDownListItem("Oklahoma","OK"));
			ar.Add(new DropDownListItem("Oregon","OR"));
			ar.Add(new DropDownListItem("Pennsylvania","PA"));
			ar.Add(new DropDownListItem("Massachusetts","MA"));
			ar.Add(new DropDownListItem("Michigan","MI"));
			ar.Add(new DropDownListItem("Minnesota","MN"));


			return SortIt(ar);
		}		
	
		public static IDropDownListItem[] GetStatesShort(bool empty, bool isAll)
		{
			
			ArrayList ar = new ArrayList(54);
			
			if (empty)
				AddEmpty(ar);
			
			if (isAll)
				AddAll(ar);

			ar.Add(new DropDownListItem("New Jersey","NJ"));
			ar.Add(new DropDownListItem("New Mexico","NM"));
			ar.Add(new DropDownListItem("New York","NY"));
			ar.Add(new DropDownListItem("Colorado","CO"));
			ar.Add(new DropDownListItem("Connecticut","CT"));
			ar.Add(new DropDownListItem("Alabama","AL"));
			ar.Add(new DropDownListItem("Alaska","AK"));
			ar.Add(new DropDownListItem("California","CA"));
			ar.Add(new DropDownListItem("Delaware","DE"));
			ar.Add(new DropDownListItem("District of Columbia","DC"));
			ar.Add(new DropDownListItem("Florida","FL"));
			ar.Add(new DropDownListItem("Georgia","GA"));
			ar.Add(new DropDownListItem("Hawaii","HI"));
			ar.Add(new DropDownListItem("Idaho","ID"));
			ar.Add(new DropDownListItem("Puerto Rico","PR"));
			ar.Add(new DropDownListItem("Rhode Island","RI"));
			ar.Add(new DropDownListItem("South Carolina","SC"));
			ar.Add(new DropDownListItem("South Dakota","SD"));
			ar.Add(new DropDownListItem("Tennessee","TN"));
			ar.Add(new DropDownListItem("Texas","TX"));
			ar.Add(new DropDownListItem("Utah","UT"));
			ar.Add(new DropDownListItem("Arizona","AZ"));
			ar.Add(new DropDownListItem("Arkansas","AR"));
			ar.Add(new DropDownListItem("Vermont","VT"));
			ar.Add(new DropDownListItem("Virgin Islands","VI"));
			ar.Add(new DropDownListItem("Virginia","VA"));
			ar.Add(new DropDownListItem("Washington","WA"));
			ar.Add(new DropDownListItem("West Virginia","WV"));
			ar.Add(new DropDownListItem("Wisconsin","WI"));
			ar.Add(new DropDownListItem("Wyoming","WY"));
			ar.Add(new DropDownListItem("Illinois","IL"));
			ar.Add(new DropDownListItem("Indiana","IN"));
			ar.Add(new DropDownListItem("Iowa","IA"));
			ar.Add(new DropDownListItem("Kansas","KS"));
			ar.Add(new DropDownListItem("Kentucky","KY"));
			ar.Add(new DropDownListItem("Louisiana","LA"));
			ar.Add(new DropDownListItem("Maine","ME"));
			ar.Add(new DropDownListItem("Maryland","MD"));
			ar.Add(new DropDownListItem("Mississippi","MS"));
			ar.Add(new DropDownListItem("Missouri","MO"));
			ar.Add(new DropDownListItem("Montana","MT"));
			ar.Add(new DropDownListItem("Nebraska","NE"));
			ar.Add(new DropDownListItem("Nevada","NV"));
			ar.Add(new DropDownListItem("New Hampshire","NH"));
			ar.Add(new DropDownListItem("North Carolina","NC"));
			ar.Add(new DropDownListItem("North Dakota","ND"));
			ar.Add(new DropDownListItem("Ohio","OH"));
			ar.Add(new DropDownListItem("Oklahoma","OK"));
			ar.Add(new DropDownListItem("Oregon","OR"));
			ar.Add(new DropDownListItem("Pennsylvania","PA"));
			ar.Add(new DropDownListItem("Massachusetts","MA"));
			ar.Add(new DropDownListItem("Michigan","MI"));
			ar.Add(new DropDownListItem("Minnesota","MN"));


			return SortIt(ar);
				

		}


		//DMA
		public static IDropDownListItem[] GetDMA(bool empty, bool isAll)
		{
			
			ArrayList ar = new ArrayList(240);
			
			if (empty)
				AddEmpty(ar);
			
			if (isAll)
				AddAll(ar);

			ar.Add(new DropDownListItem("DALLAS-FT. WORTH","1"));
			ar.Add(new DropDownListItem("SHERMAN-ADA","2"));
			ar.Add(new DropDownListItem("COLUMBIA, SC","9"));
			ar.Add(new DropDownListItem("GREENVLL-SPART-ASHEVLL-AND","11"));
			ar.Add(new DropDownListItem("CHARLESTON, SC","13"));
			ar.Add(new DropDownListItem("FLORENCE-MYRTLE BEACH","38"));
			ar.Add(new DropDownListItem("BOWLING GREEN","3"));
			ar.Add(new DropDownListItem("NEW YORK","4"));
			ar.Add(new DropDownListItem("ALBANY-SCHENECTADY-TROY","5"));
			ar.Add(new DropDownListItem("ERIE","6"));
			ar.Add(new DropDownListItem("SPRINGFIELD-HOLYOKE","7"));
			ar.Add(new DropDownListItem("CORPUS CHRISTI","8"));
			ar.Add(new DropDownListItem("LITTLE ROCK-PINE BLUFF","10"));
			ar.Add(new DropDownListItem("SYRACUSE","12"));
			ar.Add(new DropDownListItem("MIAMI-FT. LAUDERDALE","15"));
			ar.Add(new DropDownListItem("HOUSTON","16"));
			ar.Add(new DropDownListItem("PHOENIX","17"));
			ar.Add(new DropDownListItem("CHARLOTTE","19"));
			ar.Add(new DropDownListItem("VICTORIA","21"));
			ar.Add(new DropDownListItem("UTICA","23"));
			ar.Add(new DropDownListItem("ELMIRA","25"));
			ar.Add(new DropDownListItem("BUFFALO","27"));
			ar.Add(new DropDownListItem("NASHVILLE","28"));
			ar.Add(new DropDownListItem("PADUCAH-C.GIRD-HARBG-MT VN","30"));
			ar.Add(new DropDownListItem("DAYTON","32"));
			ar.Add(new DropDownListItem("BINGHAMTON","33"));
			ar.Add(new DropDownListItem("TAMPA-ST. PETE (SARASOTA)","34"));
			ar.Add(new DropDownListItem("PORTLAND-AUBURN","35"));
			ar.Add(new DropDownListItem("ROANOKE-LYNCHBURG","37"));
			ar.Add(new DropDownListItem("WICHITA-HUTCHINSON PLUS","40"));
			ar.Add(new DropDownListItem("WASHINGTON, DC (HAGRSTWN)","42"));
			ar.Add(new DropDownListItem("HARRISONBURG","44"));
			ar.Add(new DropDownListItem("GREENVILLE-N.BERN-WASHNGTN","46"));
			ar.Add(new DropDownListItem("TRI-CITIES, TN-VA","48"));
			ar.Add(new DropDownListItem("TOPEKA","50"));
			ar.Add(new DropDownListItem("ABILENE-SWEETWATER","83"));
			ar.Add(new DropDownListItem("RICHMOND-PETERSBURG","54"));
			ar.Add(new DropDownListItem("WACO-TEMPLE-BRYAN","55"));
			ar.Add(new DropDownListItem("WILMINGTON","57"));
			ar.Add(new DropDownListItem("SHREVEPORT","59"));
			ar.Add(new DropDownListItem("MONTGOMERY (SELMA)","60"));
			ar.Add(new DropDownListItem("JACKSON, TN","62"));
			ar.Add(new DropDownListItem("LUBBOCK","63"));
			ar.Add(new DropDownListItem("EL PASO","18"));
			ar.Add(new DropDownListItem("BIRMINGHAM (ANN AND TUSC)","20"));
			ar.Add(new DropDownListItem("WATERTOWN","22"));
			ar.Add(new DropDownListItem("ROCHESTER, NY","24"));
			ar.Add(new DropDownListItem("BURLINGTON-PLATTSBURGH","26"));
			ar.Add(new DropDownListItem("KNOXVILLE","29"));
			ar.Add(new DropDownListItem("LEXINGTON","31"));
			ar.Add(new DropDownListItem("BANGOR","36"));
			ar.Add(new DropDownListItem("SAN ANTONIO","39"));
			ar.Add(new DropDownListItem("CHARLOTTESVILLE","41"));
			ar.Add(new DropDownListItem("MEMPHIS","43"));
			ar.Add(new DropDownListItem("GREENSBORO-H.POINT-W.SALEM","45"));
			ar.Add(new DropDownListItem("RALEIGH-DURHAM (FAYETVILLE)","47"));
			ar.Add(new DropDownListItem("FT. SMITH-FAY-SPRNGDL-RGRS","49"));
			ar.Add(new DropDownListItem("ORLANDO-DAYTONA BCH-MELBRN","56"));
			ar.Add(new DropDownListItem("PANAMA CITY","77"));
			ar.Add(new DropDownListItem("CHATTANOOGA","61"));
			ar.Add(new DropDownListItem("AMARILLO","64"));
			ar.Add(new DropDownListItem("JACKSONVILLE","65"));
			ar.Add(new DropDownListItem("NORFOLK-PORTSMTH-NEWPT NWS","76"));
			ar.Add(new DropDownListItem("TULSA","78"));
			ar.Add(new DropDownListItem("EVANSVILLE","80"));
			ar.Add(new DropDownListItem("AUSTIN","82"));
			ar.Add(new DropDownListItem("BOSTON (MANCHESTER)","85"));
			ar.Add(new DropDownListItem("HARRISBURG-LNCSTR-LEB-YORK","87"));
			ar.Add(new DropDownListItem("JOHNSTOWN-ALTOONA","89"));
			ar.Add(new DropDownListItem("MONROE-EL DORADO","92"));
			ar.Add(new DropDownListItem("PITTSBURGH","94"));
			ar.Add(new DropDownListItem("TUCSON (SIERRA VISTA)","96"));
			ar.Add(new DropDownListItem("YOUNGSTOWN","99"));
			ar.Add(new DropDownListItem("KANSAS CITY","100"));
			ar.Add(new DropDownListItem("GRAND JUNCTION-MONTROSE","101"));
			ar.Add(new DropDownListItem("EUGENE","103"));
			ar.Add(new DropDownListItem("MANKATO","105"));
			ar.Add(new DropDownListItem("TOLEDO","107"));
			ar.Add(new DropDownListItem("WAUSAU-RHINELANDER","109"));
			ar.Add(new DropDownListItem("ALBANY, GA","111"));
			ar.Add(new DropDownListItem("MINNEAPOLIS-ST. PAUL","113"));
			ar.Add(new DropDownListItem("COLUMBUS-TUPELO-WEST POINT","115"));
			ar.Add(new DropDownListItem("RAPID CITY","117"));
			ar.Add(new DropDownListItem("BALTIMORE","119"));
			ar.Add(new DropDownListItem("PRESQUE ISLE","121"));
			ar.Add(new DropDownListItem("BEND, OR","123"));
			ar.Add(new DropDownListItem("ALBUQUERQUE-SANTA FE","125"));
			ar.Add(new DropDownListItem("FAIRBANKS","127"));
			ar.Add(new DropDownListItem("BILLINGS","129"));
			ar.Add(new DropDownListItem("DES MOINES-AMES","131"));
			ar.Add(new DropDownListItem("GRAND RAPIDS-KALMZOO-B.CRK","133"));
			ar.Add(new DropDownListItem("LANSING","135"));
			ar.Add(new DropDownListItem("AUGUSTA","137"));
			ar.Add(new DropDownListItem("CHAMPAIGN&SPRNGFLD-DECATUR","139"));
			ar.Add(new DropDownListItem("LIMA","141"));
			ar.Add(new DropDownListItem("DENVER","143"));
			ar.Add(new DropDownListItem("GREEN BAY-APPLETON","145"));
			ar.Add(new DropDownListItem("SANTABARBRA-SANMAR-SANLUOB","147"));
			ar.Add(new DropDownListItem("BATON ROUGE","149"));
			ar.Add(new DropDownListItem("CLARKSBURG-WESTON","151"));
			ar.Add(new DropDownListItem("COLORADO SPRINGS-PUEBLO","153"));
			ar.Add(new DropDownListItem("IDAHO FALLS-POCATELLO","155"));
			ar.Add(new DropDownListItem("MISSOULA","157"));
			ar.Add(new DropDownListItem("WHEELING-STEUBENVILLE","159"));
			ar.Add(new DropDownListItem("TRAVERSE CITY-CADILLAC","161"));
			ar.Add(new DropDownListItem("MADISON","163"));
			ar.Add(new DropDownListItem("ANCHORAGE","165"));
			ar.Add(new DropDownListItem("OMAHA","167"));
			ar.Add(new DropDownListItem("SACRAMNTO-STKTON-MODESTO","169"));
			ar.Add(new DropDownListItem("EUREKA","171"));
			ar.Add(new DropDownListItem("SAN FRANCISCO-OAK-SAN JOSE","173"));
			ar.Add(new DropDownListItem("PALM SPRINGS","174"));
			ar.Add(new DropDownListItem("RENO","175"));
			ar.Add(new DropDownListItem("LAFAYETTE, LA","176"));
			ar.Add(new DropDownListItem("BOISE","177"));
			ar.Add(new DropDownListItem("ST. LOUIS","178"));
			ar.Add(new DropDownListItem("MEDFORD-KLAMATH FALLS","179"));
			ar.Add(new DropDownListItem("OTTUMWA-KIRKSVILLE","180"));
			ar.Add(new DropDownListItem("HATTIESBURG-LAUREL","181"));
			ar.Add(new DropDownListItem("ALPENA","182"));
			ar.Add(new DropDownListItem("NORTH PLATTE","183"));
			ar.Add(new DropDownListItem("COLUMBUS, GA","184"));
			ar.Add(new DropDownListItem("MACON","185"));
			ar.Add(new DropDownListItem("CHICO-REDDING","186"));
			ar.Add(new DropDownListItem("ST. JOSEPH","187"));
			ar.Add(new DropDownListItem("FLINT-SAGINAW-BAY CITY","188"));
			ar.Add(new DropDownListItem("INDIANAPOLIS","189"));
			ar.Add(new DropDownListItem("CASPER-RIVERTON","190"));
			ar.Add(new DropDownListItem("PROVIDENCE-NEW BEDFORD","191"));
			ar.Add(new DropDownListItem("CHICAGO","192"));
			ar.Add(new DropDownListItem("LOS ANGELES","193"));
			ar.Add(new DropDownListItem("BAKERSFIELD","194"));
			ar.Add(new DropDownListItem("LINCOLN & HASTINGS-KRNY","195"));
			ar.Add(new DropDownListItem("GREAT FALLS","196"));
			ar.Add(new DropDownListItem("SALISBURY","197"));
			ar.Add(new DropDownListItem("MARQUETTE","198"));
			ar.Add(new DropDownListItem("MONTEREY-SALINAS","199"));
			ar.Add(new DropDownListItem("MINOT-BISMARCK-DICKINSON","200"));
			ar.Add(new DropDownListItem("FT. WAYNE","201"));
			ar.Add(new DropDownListItem("YUMA-EL CENTRO","202"));
			ar.Add(new DropDownListItem("QUINCY-HANNIBAL-KEOKUK","203"));
			ar.Add(new DropDownListItem("WICHITA FALLS & LAWTON","204"));
			ar.Add(new DropDownListItem("GAINESVILLE","205"));
			ar.Add(new DropDownListItem("BLUEFIELD-BECKLEY-OAK HILL","206"));
			ar.Add(new DropDownListItem("SAVANNAH","207"));
			ar.Add(new DropDownListItem("TERRE HAUTE","208"));
			ar.Add(new DropDownListItem("SOUTH BEND-ELKHART","209"));
			ar.Add(new DropDownListItem("LAS VEGAS","210"));
			ar.Add(new DropDownListItem("ROCKFORD","211"));
			ar.Add(new DropDownListItem("MERIDIAN","212"));
			ar.Add(new DropDownListItem("COLUMBIA-JEFFERSON CITY","213"));
			ar.Add(new DropDownListItem("PEORIA-BLOOMINGTON","214"));
			ar.Add(new DropDownListItem("JONESBORO","215"));
			ar.Add(new DropDownListItem("ATLANTA","216"));
			ar.Add(new DropDownListItem("CHEYENNE-SCOTTSBLUF","217"));
			ar.Add(new DropDownListItem("FRESNO-VISALIA","218"));
			ar.Add(new DropDownListItem("LAFAYETTE, IN","219"));
			ar.Add(new DropDownListItem("ZZInactive","220"));
			ar.Add(new DropDownListItem("WEST PALM BEACH-FT. PIERCE","66"));
			ar.Add(new DropDownListItem("MOBILE-PENSACOLA (FT WALT)","67"));
			ar.Add(new DropDownListItem("FT. MYERS-NAPLES","68"));
			ar.Add(new DropDownListItem("TYLER-LONGVIEW(LFKN&NCGD)","69"));
			ar.Add(new DropDownListItem("HUNTSVILLE-DECATUR (FLOR)","70"));
			ar.Add(new DropDownListItem("LOUISVILLE","71"));
			ar.Add(new DropDownListItem("BEAUMONT-PORT ARTHUR","72"));
			ar.Add(new DropDownListItem("HARLINGEN-WSLCO-BRNSVL-MCA","73"));
			ar.Add(new DropDownListItem("OKLAHOMA CITY","79"));
			ar.Add(new DropDownListItem("CHARLESTON-HUNTINGTON","81"));
			ar.Add(new DropDownListItem("BILOXI-GULFPORT","84"));
			ar.Add(new DropDownListItem("GREENWOOD-GREENVILLE","86"));
			ar.Add(new DropDownListItem("JACKSON, MS","88"));
			ar.Add(new DropDownListItem("JOPLIN-PITTSBURG","91"));
			ar.Add(new DropDownListItem("ODESSA-MIDLAND","93"));
			ar.Add(new DropDownListItem("TALLAHASSEE-THOMASVILLE","95"));
			ar.Add(new DropDownListItem("WILKES BARRE-SCRANTON","98"));
			ar.Add(new DropDownListItem("SPRINGFIELD, MO","102"));
			ar.Add(new DropDownListItem("PORTLAND, OR","104"));
			ar.Add(new DropDownListItem("COLUMBUS, OH","106"));
			ar.Add(new DropDownListItem("ROCHESTR-MASON CITY-AUSTIN","108"));
			ar.Add(new DropDownListItem("GLENDIVE","110"));
			ar.Add(new DropDownListItem("SAN DIEGO","112"));
			ar.Add(new DropDownListItem("PHILADELPHIA","114"));
			ar.Add(new DropDownListItem("FARGO-VALLEY CITY","116"));
			ar.Add(new DropDownListItem("DULUTH-SUPERIOR","118"));
			ar.Add(new DropDownListItem("PARKERSBURG","120"));
			ar.Add(new DropDownListItem("SALT LAKE CITY","122"));
			ar.Add(new DropDownListItem("CEDAR RAPIDS-WATERLOO&DUBQ","124"));
			ar.Add(new DropDownListItem("HARTFORD & NEW HAVEN","126"));
			ar.Add(new DropDownListItem("ZANESVILLE","128"));
			ar.Add(new DropDownListItem("CINCINNATI","130"));
			ar.Add(new DropDownListItem("HONOLULU","132"));
			ar.Add(new DropDownListItem("TWIN FALLS","134"));
			ar.Add(new DropDownListItem("ALEXANDRIA, LA","136"));
			ar.Add(new DropDownListItem("YAKIMA-PASCO-RCHLND-KNNWCK","138"));
			ar.Add(new DropDownListItem("BUTTE-BOZEMAN","140"));
			ar.Add(new DropDownListItem("DETROIT","142"));
			ar.Add(new DropDownListItem("HELENA","144"));
			ar.Add(new DropDownListItem("LA CROSSE-EAU CLAIRE","146"));
			ar.Add(new DropDownListItem("SPOKANE","148"));
			ar.Add(new DropDownListItem("MILWAUKEE","150"));
			ar.Add(new DropDownListItem("LAKE CHARLES","152"));
			ar.Add(new DropDownListItem("JUNEAU","154"));
			ar.Add(new DropDownListItem("LAREDO","156"));
			ar.Add(new DropDownListItem("DOTHAN","158"));
			ar.Add(new DropDownListItem("SAN ANGELO","160"));
			ar.Add(new DropDownListItem("NEW ORLEANS","162"));
			ar.Add(new DropDownListItem("DAVENPORT-R.ISLAND-MOLINE","164"));
			ar.Add(new DropDownListItem("SIOUX FALLS(MITCHELL)","166"));
			ar.Add(new DropDownListItem("SIOUX CITY","168"));
			ar.Add(new DropDownListItem("SEATTLE-TACOMA","170"));
			ar.Add(new DropDownListItem("CLEVELAND","172"));
			ar.Add(new DropDownListItem("ANNISTON","222"));
			ar.Add(new DropDownListItem("CHARLESTON-HUNTINGTON","223"));
			ar.Add(new DropDownListItem("CHARLESTON-HUNTINGTON","224"));
			ar.Add(new DropDownListItem("CINCINNATI","225"));
			ar.Add(new DropDownListItem("FT. WAYNE","226"));
			ar.Add(new DropDownListItem("PARKERSBURG","227"));
			ar.Add(new DropDownListItem("TOLEDO","228"));
			ar.Add(new DropDownListItem("WHEELING-STEUBENVILLE","229"));
			ar.Add(new DropDownListItem("YOUNGSTOWN","230"));
			
			return SortIt(ar);
		}

		public static IDropDownListItem[] GetStreetDirections()
		{
			return GetStreetDirections(false);
		}

		public static IDropDownListItem[] GetStreetDirections(bool addEmpty)
		{
			ArrayList ar = new ArrayList();

			if ( addEmpty )
				AddEmpty(ar);

			ar.Add(new DropDownListItem("North", "N"));
			ar.Add(new DropDownListItem("South", "S"));
			ar.Add(new DropDownListItem("East", "E"));
			ar.Add(new DropDownListItem("West", "W"));

			return Conv(ar);
		}

	#endregion

	#region Implementation
	
		static IDropDownListItem[] SortIt(ArrayList ar)
		{
			IDropDownListItem[] itms = Conv(ar);
			Array.Sort(itms);
			return itms;
		}
		static void AddEmpty(ArrayList ar)
		{
			ar.Add(new DropDownListItem("", ""));
		}
		static void AddAll(ArrayList ar)
		{
			ar.Add(new DropDownListItem(" All", "%"));
		}
		static IDropDownListItem[] Conv(ArrayList ar)
		{
			DropDownListItem[] itms = new DropDownListItem[ar.Count];
			ar.CopyTo(itms);
			return itms;
		}

	#endregion
	}
}