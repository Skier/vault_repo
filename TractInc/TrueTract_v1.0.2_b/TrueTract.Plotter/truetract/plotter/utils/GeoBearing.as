package truetract.plotter.utils
{
    import mx.utils.StringUtil;
    
    public class GeoBearing
    {

        public function GeoBearing(hDir:String = "E", vDir:String = "N", a:Angle = null)
        {
            hDirection = hDir;
            vDirection = vDir;
            
            if (a) {
	            angle = a;
            } else {
            	angle = new Angle();
            }
        }

		private var angle:Angle;

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
			return angle.value;
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
        	if (!isNaN(Number(s))) {
        		var result:GeoBearing = CreateByAzimuth(Number(s));
        		result.angle.originalMask = Angle.MASK_DMS;

        		return result;
        	}
        	
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
                    if ( isNaN( Number(char) ) && char != "." && char != "\u00b0" && char != "'" && char != "\"" && char != ":"){
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
        	var angle:Angle = Angle.Parse(s);

			if (angle.value > 90 || angle.value < 0) {
                throw new Error ("Angle should be between 0 and 90 degrees.");
			}

			bearing.angle = angle;
        }

        public function toString():String
        {
 			return vDirection + " " + angle.toString() + " " + hDirection;
            
        }

        public function toDbString():String
        {
 			return vDirection + " " + angle.toDbString() + " " + hDirection;
            
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
			
			var angle:Angle = new Angle(azimuth);
			
			if (azimuth <= 90){
			    
			    result = new GeoBearing("E", "N", angle);
			    
			} else if (azimuth <= 180) {
			    
			    angle.degree = (180 - angle.degree);
	            result = new GeoBearing("E", "S", angle);
			    
			} else if (azimuth <= 270){

			    angle.degree = (angle.degree - 180);
		        result = new GeoBearing("W", "S", angle);
		        
            } else if (azimuth <= 360) {

			    angle.degree = (360 - angle.degree);
	            result = new GeoBearing("W", "N", angle);
		        
            }
			
			result.angle.originalMask = Angle.MASK_D;
			            
            return result;
        }

        public static function CreateBySide(worldSide:String):GeoBearing {
            var bearing:GeoBearing;
            
            switch (worldSide.toUpperCase()) {
                case "N":
                    bearing = new GeoBearing();
                    break;
                    
                case "E":
                    bearing = new GeoBearing("E", "N", new Angle(90));
                    break;
                    
                case "S":
                    bearing = new GeoBearing("E", "S");
                    break;
                    
                case "W":
                    bearing = new GeoBearing("W", "S", new Angle(90));
                    break;
                    
                default:
                    throw new Error("Direction is invalid");
            }
            
            bearing.angle.originalMask = Angle.MASK_D;

            return bearing;
        }
    }
}
