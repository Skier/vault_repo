package truetract.utils
{
	import mx.utils.StringUtil;
	
	public class Angle
	{
		public static const MASK_D:String = "D";
		public static const MASK_DM:String = "DM";
		public static const MASK_DMS:String = "DMS";
		
		public var degree:Number = 0;
		public var minutes:Number = 0;
		public var seconds:Number = 0;
		
		public var originalMask:String;

		public function Angle(dd:Number = 0, mm:Number = 0, ss:Number = 0)
		{
			degree = dd;
			minutes = mm;
			seconds = ss;
		}
		
		public static function Parse(s:String):Angle 
		{
			s = StringUtil.trim(s).toUpperCase();
			
			if (s.length == 0) {
				return new Angle();
			}
			
			s = s.replace("\u00b0", ":");
			s = s.replace("'", ":");
			s = s.replace("\"", ":");

			var result:Angle;
			
			var original:String = Angle.MASK_D;
				
			if (s.indexOf(":") != -1) {
				// try to parse as dd:mm:ss.sss
				
				var dd:Number = 0;
				var mm:Number = 0;
				var ss:Number = 0;
				
				var tokens:Array = s.split(':');
				
				if (tokens.length > 0) {
					if (isNaN(Number(tokens[0]))) {
						throw new Error("Error parsing degree");
					} else {
						dd = Number(tokens[0]);
						if (dd.toString().length > dd.toFixed(8).length) {
							throw new Error("Dergees value is too long");
						}
					}
				}
				
				if (tokens.length > 1) {
					if (isNaN(Number(tokens[1]))) {
						throw new Error("Error parsing minutes");
					} else {
						mm = Number(tokens[1]);
						if (String(tokens[1]).length > 0) {
							if (mm.toString().length > mm.toFixed(8).length) {
								throw new Error("Minutes value is too long");
							}
							if (dd > int(dd)) {
								throw new Error("Degree must be integer");
							}
							original = Angle.MASK_DM;
						}
					}
				}
				if (mm >= 60) {
					throw new Error("Minutes can not be greater or equal 60");
				}
				
				if (tokens.length > 2) {
					if (isNaN(Number(tokens[2]))) {
						throw new Error("Error parsing seconds");
					} else {
						ss = Number(tokens[2]);
						if (String(tokens[2]).length > 0) {
							if (ss.toString().length > ss.toFixed(8).length) {
								throw new Error("Seconds value is too long");
							}
							if (mm > int(mm)) {
								throw new Error("Minutes must be integer");
							}
							original = Angle.MASK_DMS;
						}
					}
				}
				if (ss >= 60) {
					throw new Error("Seconds can not be greater or equal 60");
				}
				
				switch (original) {
					
					case Angle.MASK_D :
						result = new Angle(dd);
						break;
					
					case Angle.MASK_DM :
						result = new Angle(int(dd), mm);
						break;
					
					case Angle.MASK_DMS :
						result = new Angle(int(dd), int(mm), ss);
						break;
					
				}

			} else {
				// try to parse as decimal degree

				if (isNaN(Number(s))) {
					throw new Error("Error parsing decimal degree");
				} else {
					result = new Angle(Number(s));
				}
			}
			
			result.originalMask = original;
			return result;
			
		}
		
		public function get value():Number 
		{
			return degree + (minutes/60) + (seconds/3600);
		}
		
		public function toString():String
		{
			var result:String = "";

			var min:Number;
			var sec:Number;
			
			var ddS:String;
			var mmS:String;
			var ssS:String;
			
			if (originalMask == Angle.MASK_DM) {

				min = (degree - int(degree)) * 60 + minutes;
				if (min >= 60) {
					degree += 1;
					min -=60;
				}
				
				ddS = int(degree).toString();
				
				if (min.toString().length < min.toFixed(8).length) {
					mmS = min < 10 ? "0" + min.toString() : min.toString();
				} else {
					mmS = min < 10 ? "0" + min.toFixed(8) : min.toFixed(8);
					while (mmS.charAt(mmS.length-1) == "0") {
						mmS = mmS.substr(0, mmS.length-1);
					}
					if (mmS.charAt(mmS.length-1) == ".") {
						mmS += "0";
					}
				}
				
				result = ddS + "\u00b0" + mmS + "'";

			} else if (originalMask == Angle.MASK_DMS) {

				min = (degree - int(degree)) * 60 + minutes;
				if (min >= 60) {
					degree += 1;
					min -=60;
				}
				
				sec = (min - int(min)) * 60 + seconds;
				if (sec >= 60) {
					min += 1;
					sec -=60;
				}

				ddS = int(degree).toString();

				mmS = min < 10 ? "0" + int(min).toString() : int(min).toString();

				if (sec.toString().length < sec.toFixed(8).length) {
					ssS = sec < 10 ? "0" + sec.toString() : sec.toString();
				} else {
					ssS = sec < 10 ? "0" + sec.toFixed(8) : sec.toFixed(8);
					while (ssS.charAt(ssS.length-1) == "0") {
						ssS = ssS.substr(0, ssS.length-1);
					}
					if (ssS.charAt(ssS.length-1) == ".") {
						ssS += "0";
					}
				}
				
				result = ddS + "\u00b0" + mmS + "'" + ssS + "\"";

			} else {

				if (degree.toString().length < degree.toFixed(8).length) {
					result = degree.toString();
				} else {
					result = degree.toFixed(8);
				}

			}
			
			return result;
		}
				
		public function toDbString():String
		{
			var result:String = toString();
			
			result = result.replace("\u00b0", ":");
			result = result.replace("'", ":");
			result = result.replace("\"", "");
			
			return result;
		}
				
	}
}