package truetract.plotter.utils
{
    import mx.utils.StringUtil;
    
    public class GeoBearing
    {

        public function GeoBearing(hDir:String = "E", vDir:String = "N", 
                                   dd:int = 0, mm:int = 0, ss:Number = 0)
        {
            hDirection = hDir;
            vDirection = vDir;
            Degree = dd;
            Minutes = mm;
            Seconds = ss;
        }

        public var Degree:int;
        public var Minutes:int;
        public var Seconds:Number;
        
        private var _vDirection:String; //Possible values : "N" and "S"
        public function get vDirection():String { return _vDirection; }
        public function set vDirection(value:String):void {
            switch (value.toUpperCase()) {
                
                case 'N':
                case 'NORTH':
                    _vDirection = "N";
                    break;
                    
                case 'S':
                case 'SOUTH':
                    _vDirection = "S";
                    break;
                    
                default:
                    throw new Error ("Vertical direction is invalid");
            }
        }

        private var _hDirection:String; //Possible values : "E" and "W"
        public function get hDirection():String { return _hDirection; }
        public function set hDirection(value:String):void
        {
            switch (value.toUpperCase())
            {
                case 'E':
                case 'EAST':
                    _hDirection = "E";
                    break;
                    
                case 'W':
                case 'WEST':
                    _hDirection = "W";
                    break;
                    
                default:
                    throw new Error ("Horisontal direction is invalid");
            }
        }

        public function get DecimalDegree():Number 
        {
			return Degree + (Minutes / 60) + (Seconds / 3600);
        }
        
        public function get Azimuth():Number
        {
			var result:Number;
			var absDegree:Number = DecimalDegree;
			
			switch (vDirection + hDirection){
			    
			    case "NW":
			        result = 360 - absDegree;
			        break;
			        
			    case "NE":
			        result = absDegree;
			        break;
			        
			    case "SE":
			        result = 90 + (90 - absDegree);
			        break;
			        
			    case "SW":
			        result = 270 - (90 - absDegree);
			        break;
			        
                default:
                    throw new Error("Invalid direction");
			}
			
			return result;
        }
        
        public function get Radian():Number
        {
            return Azimuth * (Math.PI/180);
        }
        
        /**
         * 
         * Parse bearing from various string representations.
         * Example: the string "12.22326, NE" means the Bearing N 12° 22' 32.6" E
         *
         * Possible formats :
         * group 1
         * 1. 12.2232, NE
         * 2. 12.2232-1 (Based on Quadrants 1:NE, 2:SE, 3:SW, 4:NW)
         * group 2
         * 3. N12.2232E
         * 4. N 12.2232 E
         * 5. North 12.2232 East
         * group 3
         * 6. N (Straight North and the on validation the curser will not auto move to the direction field on these types of entries)
         * 
         */        
        public static function Parse(s:String):GeoBearing
        {
            s = StringUtil.trim(s).toUpperCase();
            
            if (!s || s.length < 2) {
                //This may bearing in 6 format
                return CreateBySide(s);
            }
            
            var degreePosition:Number = NaN;
            var degreeendPosition:Number = NaN;
             
            for (var i:int = 0; i < s.length; i++) {
                if ( !isNaN(Number(s.charAt(i))) ) {
                    degreePosition = i;
                    break;
                }
            }
            
            if (isNaN(degreePosition)){
                throw new Error("Missing bearing degree.");
            }

            var bearing:GeoBearing = new GeoBearing();
            
            if (degreePosition == 0) {
                //specified bearing in format of group number 1.
                                
                degreeendPosition = s.indexOf(',', degreePosition);
                if (degreeendPosition != -1){
                    
                    //specified bearing in format number 1.
                                    
                    parseDegreeValue(s.substring(0, degreeendPosition), bearing);
                    
                    var direction:String = StringUtil.trim(s.substring(degreeendPosition + 1)).toUpperCase();

                    if (direction.length < 2) throw new Error("Bearing Direction is not provided.");

                    bearing.vDirection = direction.charAt(0);
                    bearing.hDirection = direction.charAt(1);
                    
                } else {
                    degreeendPosition = s.indexOf('-', degreePosition);
                    
                    if ( degreeendPosition == -1 ) throw new Error ("Bearing Direction is not provided.");
                    
                    //specified bearing in format number 2.
                    
                    parseDegreeValue(s.substring(0, degreeendPosition), bearing);
                    
                    var quadrant:Number = Number( StringUtil.trim( s.substring(degreeendPosition + 1) ) );
                    
                    if ( isNaN(quadrant) ) throw new Error ("Specified Quadrant is invalid.");
                    
                    if (quadrant == 1) { bearing.vDirection = "N"; bearing.hDirection = "E" }
                    else if (quadrant == 2) { bearing.vDirection = "S"; bearing.hDirection = "E" }
                    else if (quadrant == 3) { bearing.vDirection = "S"; bearing.hDirection = "W" }
                    else if (quadrant == 4) { bearing.vDirection = "N"; bearing.hDirection = "W" }
                    else throw new Error ("Specified Quadrant is invalid.");
                }
                
            } else {
                //specified bearing in format of group number 2.
                // 3. N12.2232E
                // 4. N 12.2232 E
                // 5. North 12.2232 East
                bearing.vDirection = s.substring(0, degreePosition);
                
                for (i = degreePosition + 1; i < s.length; i++){
                    var char:String = s.charAt(i);
                    if ( isNaN( Number(char) ) && char != "." ){
                        degreeendPosition = i;
                        break;
                    }
                }
                
                if ( isNaN(degreeendPosition) ) throw new Error ("Horisontal Direction is not provided.");

                parseDegreeValue(s.substring(degreePosition, degreeendPosition), bearing);
                
                bearing.hDirection = s.substring(degreeendPosition);
            }
            

            return bearing;
        }        

        //Parse string to bearing components in format [DD.mmsstt]
        //where DD - degrees, mm - minutes, ss - seconds and tt - decimal seconds
        //Each parsed value has being filled to corresponded bearing property
        private static function parseDegreeValue(s:String, bearing:GeoBearing):void
        {
            s = StringUtil.trim(s);
            
            var ddSep:int = s.indexOf(".");
            
            var degree:Number = 0;
            var minutes:Number = 0;
            var seconds:Number = 0;
            var decimalSeconds:Number = 0;
            
            degree = Number( s.substring(0, (ddSep == -1 ? s.length : ddSep) ) );
            if (isNaN(degree)) throw new Error("Degree is not a number");                

            s = s.substr(ddSep == -1 ? s.length : ddSep + 1);
            
            function getFirstSymbols(ln:int):String
            {
                var result:String = "";
                
                if (s.length != 0)
                {
                    if (s.length <= ln)
                    {
                        result = s;
                        s = "";
                    } 
                    else 
                    {
                        result = s.substring(0, ln);
                        s = s.substring(ln);
                    }
                }

                return result;
            }

            minutes = Number(getFirstSymbols(2));
            if (isNaN(minutes)) throw new Error("Minutes are not a number");

            seconds = Number(getFirstSymbols(2));
            if (isNaN(seconds)) throw new Error("Seconds are not a number");

            decimalSeconds = Number(getFirstSymbols(2));
            if (isNaN(decimalSeconds)) throw new Error("Milliseconds are not a number");

            bearing.Degree = degree;
            bearing.Minutes = int(minutes);
            bearing.Seconds = seconds + (decimalSeconds * 0.1);
        }

        public function toString():String
        {
            var minutes:String = Minutes.toString();
            if (minutes.length == 1) minutes = "0" + minutes;

            var seconds:String = Seconds.toFixed(2);
            if (seconds.substring(0, seconds.indexOf(".")).length == 1) seconds = "0" + seconds;
            
            return vDirection + " " + Degree.toString() + "\u00b0" + minutes + "\"" + seconds + "' " + hDirection;
        }

        public function toInputString():String
        {
            var minutes:String = Minutes.toString();
            if (minutes.length == 1) minutes = "0" + minutes;

            var seconds:String = Seconds.toFixed(2);
            if (seconds.substring(0, seconds.indexOf(".")).length == 1) seconds = "0" + seconds;

            seconds = seconds.replace(".", "");

            if (Seconds == 0) {
                seconds = "";
                
                if (Minutes == 0) {
                    minutes = "";
                } else {
                    //remove zero ending in seconds
                    while (Number (minutes.charAt(minutes.length - 1)) == 0) { 
                        minutes = minutes.substr(0, minutes.length - 2);
                    }
                }
            } else {
                //remove zero ending in seconds
                var s:String = seconds.charAt(seconds.length - 1);
                
                while (Number (seconds.charAt(seconds.length - 1)) == 0) { 
                    seconds = seconds.substr(0, seconds.length - 1);
                }
            }

            var degree:String = Degree.toString();
            degree += (minutes + seconds).length > 0 ? "." + minutes + seconds : "";
            
            return vDirection + " " + degree + " " + hDirection;
        }
        
        public static function CreateByAzimuth(azimuth:Number):GeoBearing {
            if (isNaN(azimuth))
                throw new Error("Azimuth is not a number");

            var result:GeoBearing;

            while (azimuth < 0){
                azimuth += 360;
            }
            
            while (azimuth > 360){
                azimuth -= 360;
            }

            var degree:int = int(azimuth);
            var minutes:Number = (azimuth - degree) * 60;
            var seconds:Number = (minutes - int(minutes)) * 60;
            
			if (azimuth <= 90){
			    
			    result = new GeoBearing("E", "N", degree, int(minutes), seconds);
			    
			} else if (azimuth <= 180) {
			    
			    if (azimuth > degree) {
		            result = new GeoBearing("E", "S", 179 - degree, 59 - int(minutes), 60 - seconds);
			    } else {
		            result = new GeoBearing("E", "S", 180 - degree, int(minutes), seconds);
			    }
			    
			} else if (azimuth <= 270){
                
		        result = new GeoBearing("W", "S", degree - 180.0, int(minutes), seconds);
		        
            } else if (azimuth <= 360) {

			    if (azimuth > degree) {
		            result = new GeoBearing("W", "N", 359 - degree, 59 - int(minutes), 60 - seconds);
			    } else {
		            result = new GeoBearing("W", "N", 360 - degree, int(minutes), seconds);
			    }
		        
            }
            
            return result;
        }

        public static function CreateBySide(worldSide:String):GeoBearing {
            var bearing:GeoBearing;
            
            switch (worldSide.toUpperCase()) {
                case "N":
                    bearing = new GeoBearing();
                    break;
                    
                case "E":
                    bearing = new GeoBearing("E", "N", 90);
                    break;
                    
                case "S":
                    bearing = new GeoBearing("E", "S");
                    break;
                    
                case "W":
                    bearing = new GeoBearing("W", "S", 90);
                    break;
                    
                default:
                    throw new Error("Direction is invalid");
            }
            
            return bearing;
        }
    }
}
