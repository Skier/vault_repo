package com.dalworth.leadCentral.phone.trackingPhone
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class UsState
	{
		public var name:String;
		public var code:String;
		public var codes:ArrayCollection;
		
		public function UsState()
		{
			codes = new ArrayCollection();
		}
		
		public static function getStates():ArrayCollection
		{
			var result:ArrayCollection = new ArrayCollection();
				result.addItem(getState("Alaska" , "AK", [["907","0"]]));
				result.addItem(getState("Alabama" , "AL", [["205","1"],["251","1"],["256","1"],["334","1"]]));
				result.addItem(getState("Arizona" , "AZ", [["445","0"],["480","1"],["520","1"],["602","0"],["623","0"],["928","1"]]));
				result.addItem(getState("Arkansas" , "AR", [["479","1"],["501","1"],["870","1"]]));
				result.addItem(getState("California" , "CA", [["209","1"],["213","1"],["310","1"],["323","1"],["408","1"],["415","1"],["424","1"],["442","0"],["510","1"],["530","1"],["559","1"],["562","1"],["619","1"],["626","1"],["650","1"],["657","1"],["661","1"],["707","1"],["714","1"],["747","0"],["760","1"],["805","1"],["818","1"],["831","1"],["858","1"],["909","1"],["916","1"],["925","1"],["949","1"],["951","1"]]));
				result.addItem(getState("Colorado" , "CO", [["303","1"],["719","1"],["720","1"],["970","1"]]));
				result.addItem(getState("Connecticut" , "CT", [["203","1"],["475","0"],["860","1"],["959","0"]]));
				result.addItem(getState("Delaware" , "DE", [["302","1"]]));
				result.addItem(getState("District Of Columbia" , "DC", [["202","1"]]));
				result.addItem(getState("Florida" , "FL", [["239","1"],["305","1"],["321","1"],["352","1"],["386","1"],["407","1"],["561","1"],["689","0"],["727","1"],["754","1"],["772","1"],["786","1"],["813","1"],["850","1"],["863","1"],["904","1"],["941","1"],["954","1"]]));
				result.addItem(getState("Georgia" , "GA", [["229","1"],["404","1"],["470","0"],["478","1"],["678","1"],["706","1"],["762","1"],["770","0"],["912","1"]]));
				result.addItem(getState("Hawaii" , "HI", [["808","0"]]));
				result.addItem(getState("Idaho" , "ID", [["208","1"]]));
				result.addItem(getState("Illinois" , "IL", [["217","1"],["224","1"],["302","1"],["309","1"],["312","1"],["331","1"],["618","1"],["630","1"],["708","1"],["773","1"],["779","1"],["815","1"],["847","1"],["872","0"]]));
				result.addItem(getState("Indiana" , "IN", [["219","1"],["260","1"],["317","1"],["574","1"],["765","1"],["812","1"]]));
				result.addItem(getState("Iowa" , "IA", [["319","1"],["515","1"],["563","1"],["641","0"],["712","1"]]));
				result.addItem(getState("Kansas" , "KS", [["316","1"],["620","1"],["785","1"],["913","1"]]));
				result.addItem(getState("Kentucky" , "KY", [["270","1"],["502","1"],["606","1"],["859","1"]]));
				result.addItem(getState("Louisiana" , "LA", [["225","1"],["318","1"],["337","1"],["504","1"],["985","1"]]));
				result.addItem(getState("Maine" , "ME", [["207","1"]]));
				result.addItem(getState("Maryland" , "MD", [["240","1"],["301","1"],["410","1"],["443","1"]]));
				result.addItem(getState("Massachusetts" , "MA", [["339","1"],["351","0"],["413","1"],["508","1"],["617","1"],["774","1"],["781","1"],["857","1"],["978","1"]]));
				result.addItem(getState("Michigan" , "MI", [["231","1"],["248","1"],["269","1"],["313","1"],["517","1"],["586","1"],["616","1"],["734","1"],["810","1"],["906","1"],["947","0"],["989","1"]]));
				result.addItem(getState("Minnesota" , "MN", [["218","1"],["320","1"],["507","1"],["612","1"],["651","1"],["763","1"],["952","1"]]));
				result.addItem(getState("Mississippi" , "MS", [["228","1"],["601","1"],["662","1"],["769","1"]]));
				result.addItem(getState("Missouri" , "MO", [["314","1"],["417","1"],["573","1"],["636","1"],["660","1"],["816","1"]]));
				result.addItem(getState("Montana" , "MT", [["406","1"]]));
				result.addItem(getState("Nebraska" , "NE", [["308","1"],["402","1"],["605","1"]]));
				result.addItem(getState("Nevada" , "NV", [["702","1"],["775","1"]]));
				result.addItem(getState("New Hampshire" , "NH", [["603","1"]]));
				result.addItem(getState("New Jersey" , "NJ", [["201","1"],["226","1"],["254","1"],["276","1"],["289","1"],["306","0"],["316","1"],["506","0"],["551","1"],["604","1"],["609","1"],["647","1"],["705","1"],["709","1"],["732","1"],["848","1"],["856","1"],["862","1"],["908","1"],["973","1"]]));
				result.addItem(getState("New Mexico" , "NM", [["505","1"],["575","1"]]));
				result.addItem(getState("New York" , "NY", [["212","0"],["315","1"],["347","1"],["516","1"],["518","1"],["585","1"],["607","1"],["631","1"],["646","1"],["712","1"],["716","1"],["718","1"],["845","1"],["914","1"],["917","1"]]));
				result.addItem(getState("North Carolina" , "NC", [["252","1"],["336","1"],["704","1"],["828","1"],["910","1"],["919","1"],["980","1"],["984","0"]]));
				result.addItem(getState("North Dakota" , "ND", [["701","1"]]));
				result.addItem(getState("Ohio" , "OH", [["216","1"],["234","1"],["283","0"],["330","1"],["380","0"],["419","1"],["440","1"],["513","1"],["567","1"],["614","1"],["740","1"],["937","1"]]));
				result.addItem(getState("Oklahoma" , "OK", [["405","1"],["580","1"],["918","1"]]));
				result.addItem(getState("Oregon" , "OR", [["503","1"],["541","1"],["971","1"]]));
				result.addItem(getState("Pennsylvania" , "PA", [["215","1"],["267","1"],["412","1"],["484","1"],["570","1"],["610","1"],["717","1"],["724","1"],["814","1"],["878","0"]]));
				result.addItem(getState("Rhode Island" , "RI", [["401","1"]]));
				result.addItem(getState("South Carolina" , "SC", [["803","1"],["843","1"],["864","1"]]));
				result.addItem(getState("South Dakota" , "SD", [["605","1"]]));
				result.addItem(getState("Tennessee" , "TN", [["423","1"],["615","1"],["731","1"],["865","1"],["901","1"],["931","1"]]));
				result.addItem(getState("Texas" , "TX", [["210","1"],["214","1"],["254","1"],["281","1"],["325","1"],["361","1"],["409","1"],["430","0"],["432","1"],["469","1"],["512","1"],["682","1"],["713","0"],["806","1"],["817","1"],["830","1"],["832","1"],["903","1"],["915","1"],["936","1"],["940","1"],["956","1"],["972","1"],["979","1"]]));
				result.addItem(getState("Utah" , "UT", [["385","1"],["435","1"],["801","1"]]));
				result.addItem(getState("Vermont" , "VT", [["802","1"]]));
				result.addItem(getState("Virginia" , "VA", [["276","1"],["434","1"],["540","1"],["571","1"],["703","1"],["757","1"],["804","1"]]));
				result.addItem(getState("Washington" , "WA", [["206","1"],["253","1"],["360","1"],["425","1"],["509","1"],["564","0"]]));
				result.addItem(getState("West Virginia" , "WV", [["304","1"],["681","0"]]));
				result.addItem(getState("Wisconsin" , "WI", [["262","1"],["414","1"],["608","1"],["715","1"],["920","1"]]));
				result.addItem(getState("Wyoming" , "WY", [["307","1"]]));
			return result;
		}
		
		private static function getState(name:String, code:String, codes:Array):UsState 
		{
			var result:UsState = new UsState();
				result.name = name;
				result.code = code;
				for each (var obj:Array in codes) 
				{
					if (obj[1] == "1")
					result.codes.addItem(obj[0]);
				}
			return result;
		}

	}
}