using System;
using System.Collections;
using System.Text.RegularExpressions;
using DPI.Interfaces;

namespace DPI.Components
{
    /// <summary>
    /// Implements <see cref="IDeliveryAddressLineParser"/>IDeliveryAddressLineParser</see> 
    /// interface for parsing a delivery address line in US street address format.
    /// 
    /// <DeliveryAddressLine>::=
    ///       <PrimaryAddressNumber>
    ///      [<Separator><Predirectional>]
    ///       <Separator><StreetName>
    ///      [<Separator><Suffix>]
    ///      [<Separator><Postdirectinal>]
    ///      [<Separator><SecondaryAddressIdentifier>]
    ///      [<Separator><SecondaryAddressRange>]
    ///      
    /// <Separator>::=' ' | ','
    /// <PrimaryAddressNumber>::=<ASES>
    /// <Predirectional>::={Defined}
    /// <StreetName>::=<ASES>[<Separator><ASES>]
    /// <Suffix>::={Defined}
    /// <Postdirectinal>::={Defined}
    /// <SecondaryAddressIdentifier>::={Defined}
    /// <SecondaryAddressRange>::=<ASES>
    /// <ASES>::={Any symbols EXCLUDING space}
    /// </summary>
    public class StreetAddressParser : IDeliveryAddressLineParser
    {
        #region Constants

        private const string INVALIDE_FORMAT_MSG = "Address format is invalid: address must contain at least primary address number and street name.";

        #endregion

        #region Static Members

        private static Hashtable _suffixDictionary;
        private static Hashtable _directionalDictionary;
        private static Hashtable _secondaryAddressIdentifierDictionary;

        static StreetAddressParser()
        {
            CreateSuffixDictionary();
            CreateDirectionalDictionary();
            CreateSecondaryAddressIdentifierDictionary();
        }

        private static void CreateSuffixDictionary()
        {
            _suffixDictionary = new Hashtable();

            _suffixDictionary.Add("ALLEE", "ALY");
            _suffixDictionary.Add("ALLY", "ALY");
            _suffixDictionary.Add("ALY", "ALY");
            _suffixDictionary.Add("ANEX", "ANX");
            _suffixDictionary.Add("ANNEX", "ANX");
            _suffixDictionary.Add("ANNX", "ANX");
            _suffixDictionary.Add("ANX", "ANX");
            _suffixDictionary.Add("ARC", "ARC");
            _suffixDictionary.Add("ARCADE", "ARC");
            _suffixDictionary.Add("AV", "AVN");
            _suffixDictionary.Add("AVE", "AVN");
            _suffixDictionary.Add("AVEN", "AVN");
            _suffixDictionary.Add("AVENU", "AVN");
            _suffixDictionary.Add("AVENUE", "AVN");
            _suffixDictionary.Add("AVN", "AVN");
            _suffixDictionary.Add("AVNUE", "AVN");
            _suffixDictionary.Add("BAYOO", "BYU");
            _suffixDictionary.Add("BAYOU", "BYU");
            _suffixDictionary.Add("BYU", "BYU");
            _suffixDictionary.Add("BCH", "BCH");
            _suffixDictionary.Add("BEACH", "BCH");
            _suffixDictionary.Add("BEND", "BND");
            _suffixDictionary.Add("BND", "BND");
            _suffixDictionary.Add("BLF", "BLF");
            _suffixDictionary.Add("BLUF", "BLF");
            _suffixDictionary.Add("BLUFF", "BLF");
            _suffixDictionary.Add("BLUFFS", "BLFS");
            _suffixDictionary.Add("BLFS", "BLFS");
            _suffixDictionary.Add("BOT", "BOT");
            _suffixDictionary.Add("BTM", "BTM");
            _suffixDictionary.Add("BOTTM", "BTM");
            _suffixDictionary.Add("BOTTOM", "BTM");
            _suffixDictionary.Add("BLVD", "BLVD");
            _suffixDictionary.Add("BOUL", "BLVD");
            _suffixDictionary.Add("BOULEVARD", "BLVD");
            _suffixDictionary.Add("BOULV", "BLVD");
            _suffixDictionary.Add("BR", "BR");
            _suffixDictionary.Add("BRNCH", "BR");
            _suffixDictionary.Add("BRANCH", "BR");
            _suffixDictionary.Add("BRDGE", "BRG");
            _suffixDictionary.Add("BRG", "BRG");
            _suffixDictionary.Add("BRIDGE", "BRG");
            _suffixDictionary.Add("BRK", "BRK");
            _suffixDictionary.Add("BROOK", "BRK");
            _suffixDictionary.Add("BROOKS", "BRK");
            _suffixDictionary.Add("BURG", "BURG");
            _suffixDictionary.Add("BURGS", "BURG");
            _suffixDictionary.Add("BYP", "BYP");
            _suffixDictionary.Add("BYPA", "BYP");
            _suffixDictionary.Add("BYPAS", "BYP");
            _suffixDictionary.Add("BYPASS", "BYP");
            _suffixDictionary.Add("BYPS", "BYP");
            _suffixDictionary.Add("CAMP", "CMP");
            _suffixDictionary.Add("CP", "CMP");
            _suffixDictionary.Add("CMP", "CMP");
            _suffixDictionary.Add("CANYN", "CNYN");
            _suffixDictionary.Add("CANYON", "CNYN");
            _suffixDictionary.Add("CNYN", "CNYN");
            _suffixDictionary.Add("CAPE", "CAPE");
            _suffixDictionary.Add("CPE", "CPE");
            _suffixDictionary.Add("CAUSEWAY", "CSWY");
            _suffixDictionary.Add("CAUSWAY", "CSWY");
            _suffixDictionary.Add("CSWY", "CSWY");
            _suffixDictionary.Add("CEN", "CEN");
            _suffixDictionary.Add("CENT", "CEN");
            _suffixDictionary.Add("CENTER", "CEN");
            _suffixDictionary.Add("CENTR", "CEN");
            _suffixDictionary.Add("CENTRE", "CEN");
            _suffixDictionary.Add("CNTER", "CEN");
            _suffixDictionary.Add("CNTR", "CEN");
            _suffixDictionary.Add("CTR", "CEN");
            _suffixDictionary.Add("CENTERS", "CENS");
            _suffixDictionary.Add("CENS", "CENS");
            _suffixDictionary.Add("CIR", "CIR");
            _suffixDictionary.Add("CIRC", "CIR");
            _suffixDictionary.Add("CIRCL", "CIR");
            _suffixDictionary.Add("CIRCLE", "CIR");
            _suffixDictionary.Add("CRCL", "CIR");
            _suffixDictionary.Add("CRCLE", "CIR");
            _suffixDictionary.Add("CIRCLES", "CIRS");
            _suffixDictionary.Add("CIRS", "CIRS");
            _suffixDictionary.Add("CLF", "CLF");
            _suffixDictionary.Add("CLIFF", "CLF");
            _suffixDictionary.Add("CLFS", "CLFS");
            _suffixDictionary.Add("CLIFFS", "CLFS");
            _suffixDictionary.Add("CLB", "CLB");
            _suffixDictionary.Add("CLUB", "CLB");
            _suffixDictionary.Add("COMMON", "COMN");
            _suffixDictionary.Add("COMMONS", "COMN");
            _suffixDictionary.Add("COMN", "COMN");
            _suffixDictionary.Add("COR", "COR");
            _suffixDictionary.Add("CORNER", "COR");
            _suffixDictionary.Add("CORNERS", "COR");
            _suffixDictionary.Add("CORS", "CORS");
            _suffixDictionary.Add("COURSE", "CORS");
            _suffixDictionary.Add("CRSE", "CORS");
            _suffixDictionary.Add("COURT", "CT");
            _suffixDictionary.Add("CT", "CT");
            _suffixDictionary.Add("COURTS", "CT");
            _suffixDictionary.Add("CTS", "CT");
            _suffixDictionary.Add("COVE", "CV");
            _suffixDictionary.Add("CV", "CV");
            _suffixDictionary.Add("COVES", "CV");
            _suffixDictionary.Add("CREEK", "CRK");
            _suffixDictionary.Add("CRK", "CRK");
            _suffixDictionary.Add("CRESCENT", "CRES");
            _suffixDictionary.Add("CRES", "CRES");
            _suffixDictionary.Add("CRSENT", "CRES");
            _suffixDictionary.Add("CRSNT", "CRES");
            _suffixDictionary.Add("CREST", "CRES");
            _suffixDictionary.Add("CROSSING", "XING");
            _suffixDictionary.Add("CRSSNG", "XING");
            _suffixDictionary.Add("XING", "XING");
            _suffixDictionary.Add("CROSSROAD", "XRD");
            _suffixDictionary.Add("CROSSROADS", "XRD");
            _suffixDictionary.Add("XRD", "XRD");
            _suffixDictionary.Add("CURVE", "CURV");
            _suffixDictionary.Add("DALE", "DL");
            _suffixDictionary.Add("DL", "DL");
            _suffixDictionary.Add("DAM", "DM");
            _suffixDictionary.Add("DM", "DM");
            _suffixDictionary.Add("DIV", "DV");
            _suffixDictionary.Add("DIVIDE", "DV");
            _suffixDictionary.Add("DV", "DV");
            _suffixDictionary.Add("DVD", "DV");
            _suffixDictionary.Add("DR", "DR");
            _suffixDictionary.Add("DRIV", "DR");
            _suffixDictionary.Add("DRIVE", "DR");
            _suffixDictionary.Add("DRV", "DR");
            _suffixDictionary.Add("DRIVES", "DRS");
            _suffixDictionary.Add("DRS", "DRS");
            _suffixDictionary.Add("EST", "EST");
            _suffixDictionary.Add("ESTATE", "EST");
            _suffixDictionary.Add("ESTATES", "EST");
            _suffixDictionary.Add("ESTS", "ESTS");
            _suffixDictionary.Add("EXP", "EXPY");
            _suffixDictionary.Add("EXPR", "EXPY");
            _suffixDictionary.Add("EXPRESS", "EXPY");
            _suffixDictionary.Add("EXPRESSWAY", "EXPY");
            _suffixDictionary.Add("EXPW", "EXPY");
            _suffixDictionary.Add("EXPY", "EXPY");
            _suffixDictionary.Add("EXT", "EXT");
            _suffixDictionary.Add("EXTENSION", "EXT");
            _suffixDictionary.Add("EXTN", "EXT");
            _suffixDictionary.Add("EXTNSN", "EXT");
            _suffixDictionary.Add("EXTS", "EXT");
            _suffixDictionary.Add("FALL", "FALL");
            _suffixDictionary.Add("FALLS", "FLS");
            _suffixDictionary.Add("FLS", "FLS");
            _suffixDictionary.Add("FERRY", "FRY");
            _suffixDictionary.Add("FRRY", "FRY");
            _suffixDictionary.Add("FRY", "FRY");
            _suffixDictionary.Add("FIELD", "FLD");
            _suffixDictionary.Add("FLD", "FLD");
            _suffixDictionary.Add("FIELDS", "FLDS");
            _suffixDictionary.Add("FLDS", "FLDS");
            _suffixDictionary.Add("FLAT", "FLT");
            _suffixDictionary.Add("FLT", "FLT");
            _suffixDictionary.Add("FLATS", "FLTS");
            _suffixDictionary.Add("FLTS", "FLTS");
            _suffixDictionary.Add("FORD", "FRD");
            _suffixDictionary.Add("FRD", "FRD");
            _suffixDictionary.Add("FORDS", "FRDS");
            _suffixDictionary.Add("FRDS", "FRDS");
            _suffixDictionary.Add("FOREST", "FRST");
            _suffixDictionary.Add("FORESTS", "FRST");
            _suffixDictionary.Add("FRST", "FRST");
            _suffixDictionary.Add("FORG", "FRG");
            _suffixDictionary.Add("FORGE", "FRG");
            _suffixDictionary.Add("FRG", "FRG");
            _suffixDictionary.Add("FORGES", "FRGS");
            _suffixDictionary.Add("FRGS", "FRGS");
            _suffixDictionary.Add("FORK", "FRK");
            _suffixDictionary.Add("FRK", "FRK");
            _suffixDictionary.Add("FORKS", "FRKS");
            _suffixDictionary.Add("FRKS", "FRKS");
            _suffixDictionary.Add("FORT", "FRT");
            _suffixDictionary.Add("FRT", "FRT");
            _suffixDictionary.Add("FT", "FRT");
            _suffixDictionary.Add("FREEWAY", "FWY");
            _suffixDictionary.Add("FREEWY", "FWY");
            _suffixDictionary.Add("FRWAY", "FWY");
            _suffixDictionary.Add("FRWY", "FWY");
            _suffixDictionary.Add("FWY", "FWY");
            _suffixDictionary.Add("GARDEN", "GRDN");
            _suffixDictionary.Add("GARDN", "GRDN");
            _suffixDictionary.Add("GRDEN", "GRDN");
            _suffixDictionary.Add("GRDN", "GRDN");
            _suffixDictionary.Add("GARDENS", "GDNS");
            _suffixDictionary.Add("GDNS", "GDNS");
            _suffixDictionary.Add("GRDNS", "GDNS");
            _suffixDictionary.Add("GATEWAY", "GTWY");
            _suffixDictionary.Add("GATEWY", "GTWY");
            _suffixDictionary.Add("GATWAY", "GTWY");
            _suffixDictionary.Add("GTWAY", "GTWY");
            _suffixDictionary.Add("GTWY", "GTWY");
            _suffixDictionary.Add("GLEN", "GLN");
            _suffixDictionary.Add("GLN", "GLN");
            _suffixDictionary.Add("GLENS", "GLNS");
            _suffixDictionary.Add("GLNS", "GLNS");
            _suffixDictionary.Add("GREEN", "GRN");
            _suffixDictionary.Add("GRN", "GRN");
            _suffixDictionary.Add("GREENS", "GRNS");
            _suffixDictionary.Add("GROV", "GRV");
            _suffixDictionary.Add("GROVE", "GRV");
            _suffixDictionary.Add("GRV", "GRV");
            _suffixDictionary.Add("GROVES", "GRVS");
            _suffixDictionary.Add("GRVS", "GRVS");
            _suffixDictionary.Add("HARB", "HBR");
            _suffixDictionary.Add("HARBOR", "HBR");
            _suffixDictionary.Add("HARBR", "HBR");
            _suffixDictionary.Add("HBR", "HBR");
            _suffixDictionary.Add("HRBOR", "HBR");
            _suffixDictionary.Add("HARBORS", "HBRS");
            _suffixDictionary.Add("HAVEN", "HVN");
            _suffixDictionary.Add("HVN", "HVN");
            _suffixDictionary.Add("HT", "HT");
            _suffixDictionary.Add("HTS", "HTS");
            _suffixDictionary.Add("HIGHWAY", "HWY");
            _suffixDictionary.Add("HIGHWY", "HWY");
            _suffixDictionary.Add("HIWAY", "HWY");
            _suffixDictionary.Add("HIWY", "HWY");
            _suffixDictionary.Add("HWAY", "HWY");
            _suffixDictionary.Add("HWY", "HWY");
            _suffixDictionary.Add("HILL", "HL");
            _suffixDictionary.Add("HL", "HL");
            _suffixDictionary.Add("HILLS", "HLS");
            _suffixDictionary.Add("HLS", "HLS");
            _suffixDictionary.Add("HLLW", "HOLW");
            _suffixDictionary.Add("HOLLOW", "HOLW");
            _suffixDictionary.Add("HOLLOWS", "HOLW");
            _suffixDictionary.Add("HOLW", "HOLW");
            _suffixDictionary.Add("HOLWS", "HOLW");
            _suffixDictionary.Add("INLT", "INLT");
            _suffixDictionary.Add("IS", "IS");
            _suffixDictionary.Add("ISLAND", "IS");
            _suffixDictionary.Add("ISLND", "IS");
            _suffixDictionary.Add("ISLANDS", "ISS");
            _suffixDictionary.Add("ISLNDS", "ISS");
            _suffixDictionary.Add("ISS", "ISS");
            _suffixDictionary.Add("ISLE", "ISLE");
            _suffixDictionary.Add("ISLES", "ISLE");
            _suffixDictionary.Add("JCT", "JCT");
            _suffixDictionary.Add("JCTION", "JCT");
            _suffixDictionary.Add("JCTN", "JCT");
            _suffixDictionary.Add("JUNCTION", "JCT");
            _suffixDictionary.Add("JUNCTN", "JCT");
            _suffixDictionary.Add("JUNCTON", "JCT");
            _suffixDictionary.Add("JCTNS", "JCTS");
            _suffixDictionary.Add("JCTS", "JCTS");
            _suffixDictionary.Add("JUNCTIONS", "JCTS");
            _suffixDictionary.Add("KEY", "KY");
            _suffixDictionary.Add("KY", "KY");
            _suffixDictionary.Add("KEYS", "KYS");
            _suffixDictionary.Add("KYS", "KYS");
            _suffixDictionary.Add("KNL", "KNL");
            _suffixDictionary.Add("KNOL", "KNL");
            _suffixDictionary.Add("KNOLL", "KNL");
            _suffixDictionary.Add("KNLS", "KNLS");
            _suffixDictionary.Add("KNOLLS", "KNLS");
            _suffixDictionary.Add("LK", "LK");
            _suffixDictionary.Add("LAKE", "LK");
            _suffixDictionary.Add("LKS", "LKS");
            _suffixDictionary.Add("LAKES", "LKS");
            _suffixDictionary.Add("LAND", "LAND");
            _suffixDictionary.Add("LANDING", "LNDG");
            _suffixDictionary.Add("LNDG", "LNDG");
            _suffixDictionary.Add("LNDNG", "LNDG");
            _suffixDictionary.Add("LANE", "LN");
            _suffixDictionary.Add("LN", "LN");
            _suffixDictionary.Add("LGT", "LGT");
            _suffixDictionary.Add("LIGHT", "LGT");
            _suffixDictionary.Add("LIGHTS", "LGTS");
            _suffixDictionary.Add("LGTS", "LGTS");
            _suffixDictionary.Add("LF", "LF");
            _suffixDictionary.Add("LOAF", "LF");
            _suffixDictionary.Add("LCK", "LCK");
            _suffixDictionary.Add("LOCK", "LCK");
            _suffixDictionary.Add("LCKS", "LCKS");
            _suffixDictionary.Add("LOCKS", "LCKS");
            _suffixDictionary.Add("LDG", "LDG");
            _suffixDictionary.Add("LDGE", "LDG");
            _suffixDictionary.Add("LODG", "LDG");
            _suffixDictionary.Add("LODGE", "LDG");
            _suffixDictionary.Add("LOOP", "LOOP");
            _suffixDictionary.Add("LOOPS", "LOOP");
            _suffixDictionary.Add("MALL", "MALL");
            _suffixDictionary.Add("MNR", "MNR");
            _suffixDictionary.Add("MANOR", "MNR");
            _suffixDictionary.Add("MANORS", "MNRS");
            _suffixDictionary.Add("MNRS", "MNRS");
            _suffixDictionary.Add("MEADOW", "MDW");
            _suffixDictionary.Add("MDW", "MDW");
            _suffixDictionary.Add("MDWS", "MDWS");
            _suffixDictionary.Add("MEADOWS", "MDWS");
            _suffixDictionary.Add("MEDOWS", "MDWS");
            _suffixDictionary.Add("MEWS", "MEWS");
            _suffixDictionary.Add("MILL", "MILL");
            _suffixDictionary.Add("MILLS", "MILL");
            _suffixDictionary.Add("MISSN", "MSSN");
            _suffixDictionary.Add("MSSN", "MSSN");
            _suffixDictionary.Add("MOTORWAY", "MTWY");
            _suffixDictionary.Add("MTWY", "MTWY");
            _suffixDictionary.Add("MNT", "MT");
            _suffixDictionary.Add("MT", "MT");
            _suffixDictionary.Add("MOUNT", "MT");
            _suffixDictionary.Add("MNTAIN", "MTN");
            _suffixDictionary.Add("MNTN", "MTN");
            _suffixDictionary.Add("MOUNTAIN", "MTN");
            _suffixDictionary.Add("MOUNTIN", "MTN");
            _suffixDictionary.Add("MTIN", "MTN");
            _suffixDictionary.Add("MTN", "MTN");
            _suffixDictionary.Add("MNTNS", "MTNS");
            _suffixDictionary.Add("MTNS", "MTNS");
            _suffixDictionary.Add("MOUNTAINS", "MTNS");
            _suffixDictionary.Add("NCK", "NCK");
            _suffixDictionary.Add("NECK", "NCK");
            _suffixDictionary.Add("ORCH", "ORCH");
            _suffixDictionary.Add("ORCHARD", "ORCH");
            _suffixDictionary.Add("ORCHRD", "ORCH");
            _suffixDictionary.Add("OVAL", "OVL");
            _suffixDictionary.Add("OVL", "OVL");
            _suffixDictionary.Add("OVERPASS", "OPAS");
            _suffixDictionary.Add("OPAS", "OPAS");
            _suffixDictionary.Add("PARK", "PRK");
            _suffixDictionary.Add("PRK", "PRK");
            _suffixDictionary.Add("PARKS", "PRKS");
            _suffixDictionary.Add("PRKS", "PRKS");
            _suffixDictionary.Add("PARKWAY", "PKWY");
            _suffixDictionary.Add("PARKWY", "PKWY");
            _suffixDictionary.Add("PKWAY", "PKWY");
            _suffixDictionary.Add("PKWY", "PKWY");
            _suffixDictionary.Add("PKY", "PKWY");
            _suffixDictionary.Add("PARKWAYS", "PKWY");
            _suffixDictionary.Add("PKWYS", "PKWY");
            _suffixDictionary.Add("PASS", "PASS");
            _suffixDictionary.Add("PASSAGE", "PASS");
            _suffixDictionary.Add("PATH", "PATH");
            _suffixDictionary.Add("PATHS", "PATH");
            _suffixDictionary.Add("PIKE", "PIKE");
            _suffixDictionary.Add("PIKES", "PIKE");
            _suffixDictionary.Add("PINE", "PINE");
            _suffixDictionary.Add("PINES", "PINE");
            _suffixDictionary.Add("PNES", "PNES");
            _suffixDictionary.Add("PL", "PLN");
            _suffixDictionary.Add("PLAIN", "PLN");
            _suffixDictionary.Add("PLN", "PLN");
            _suffixDictionary.Add("PLAINS", "PLNS");
            _suffixDictionary.Add("PLNS", "PLNS");
            _suffixDictionary.Add("PLAZA", "PLZ");
            _suffixDictionary.Add("PLZ", "PLZ");
            _suffixDictionary.Add("PLZA", "PLZ");
            _suffixDictionary.Add("POINT", "PT");
            _suffixDictionary.Add("PT", "PT");
            _suffixDictionary.Add("POINTS", "PTS");
            _suffixDictionary.Add("PTS", "PTS");
            _suffixDictionary.Add("PORT", "PRT");
            _suffixDictionary.Add("PRT", "PRT");
            _suffixDictionary.Add("PORTS", "PRTS");
            _suffixDictionary.Add("PRTS", "PRTS");
            _suffixDictionary.Add("PR", "PR");
            _suffixDictionary.Add("PRAIRIE", "PR");
            _suffixDictionary.Add("PRR", "PR");
            _suffixDictionary.Add("RAD", "RAD");
            _suffixDictionary.Add("RADIAL", "RAD");
            _suffixDictionary.Add("RADIEL", "RAD");
            _suffixDictionary.Add("RADL", "RAD");
            _suffixDictionary.Add("RAMP", "RAMP");
            _suffixDictionary.Add("RANCH", "RNCH");
            _suffixDictionary.Add("RANCHES", "RNCH");
            _suffixDictionary.Add("RNCH", "RNCH");
            _suffixDictionary.Add("RNCHS", "RNCH");
            _suffixDictionary.Add("RAPID", "RPD");
            _suffixDictionary.Add("RPD", "RPD");
            _suffixDictionary.Add("RAPIDS", "RPDS");
            _suffixDictionary.Add("RPDS", "RPDS");
            _suffixDictionary.Add("REST", "RST");
            _suffixDictionary.Add("RST", "RST");
            _suffixDictionary.Add("RDG", "RDG");
            _suffixDictionary.Add("RDGE", "RDG");
            _suffixDictionary.Add("RIDGE", "RDG");
            _suffixDictionary.Add("RDGS", "RDGS");
            _suffixDictionary.Add("RIDGES", "RDGS");
            _suffixDictionary.Add("RIV", "RIV");
            _suffixDictionary.Add("RIVER", "RIV");
            _suffixDictionary.Add("RVR", "RIV");
            _suffixDictionary.Add("RIVR", "RIV");
            _suffixDictionary.Add("RD", "RD");
            _suffixDictionary.Add("ROAD", "RD");
            _suffixDictionary.Add("ROADS", "RDS");
            _suffixDictionary.Add("RDS", "RDS");
            _suffixDictionary.Add("ROUTE", "RTE");
            _suffixDictionary.Add("RTE", "RTE");
            _suffixDictionary.Add("ROW", "ROW");
            _suffixDictionary.Add("RUE", "RUE");
            _suffixDictionary.Add("RUN", "RUN");
            _suffixDictionary.Add("SHL", "SHL");
            _suffixDictionary.Add("SHOAL", "SHL");
            _suffixDictionary.Add("SHLS", "SHLS");
            _suffixDictionary.Add("SHOALS", "SHLS");
            _suffixDictionary.Add("SHOAR", "SHR");
            _suffixDictionary.Add("SHORE", "SHR");
            _suffixDictionary.Add("SHR", "SHR");
            _suffixDictionary.Add("SHOARS", "SHRS");
            _suffixDictionary.Add("SHORES", "SHRS");
            _suffixDictionary.Add("SHRS", "SHRS");
            _suffixDictionary.Add("SKYWAY", "SKWY");
            _suffixDictionary.Add("SKWY", "SKWY");
            _suffixDictionary.Add("SPG", "SPG");
            _suffixDictionary.Add("SPNG", "SPG");
            _suffixDictionary.Add("SPRING", "SPG");
            _suffixDictionary.Add("SPRNG", "SPG");
            _suffixDictionary.Add("SPGS", "SPGS");
            _suffixDictionary.Add("SPNGS", "SPGS");
            _suffixDictionary.Add("SPRINGS", "SPGS");
            _suffixDictionary.Add("SPRNGS", "SPGS");
            _suffixDictionary.Add("SPUR", "SPUR");
            _suffixDictionary.Add("SPURS", "SPUR");
            _suffixDictionary.Add("SQ", "SQ");
            _suffixDictionary.Add("SQR", "SQ");
            _suffixDictionary.Add("SQRE", "SQ");
            _suffixDictionary.Add("SQU", "SQ");
            _suffixDictionary.Add("SQUARE", "SQ");
            _suffixDictionary.Add("SQRS", "SQS");
            _suffixDictionary.Add("SQUARES", "SQS");
            _suffixDictionary.Add("SQS", "SQS");
            _suffixDictionary.Add("STA", "STA");
            _suffixDictionary.Add("STATION", "STA");
            _suffixDictionary.Add("STATN", "STA");
            _suffixDictionary.Add("STN", "STA");
            _suffixDictionary.Add("STRA", "STRA");
            _suffixDictionary.Add("STRAV", "STRA");
            _suffixDictionary.Add("STRAVEN", "STRA");
            _suffixDictionary.Add("STRAVENUE", "STRA");
            _suffixDictionary.Add("STRAVN", "STRA");
            _suffixDictionary.Add("STRVN", "STRA");
            _suffixDictionary.Add("STRVNUE", "STRA");
            _suffixDictionary.Add("STREAM", "STRM");
            _suffixDictionary.Add("STREME", "STRM");
            _suffixDictionary.Add("STREET", "ST");
            _suffixDictionary.Add("STRM", "STRM");
            _suffixDictionary.Add("STRT", "ST");
            _suffixDictionary.Add("ST", "ST");
            _suffixDictionary.Add("STR", "ST");
            _suffixDictionary.Add("STREETS", "ST");
            _suffixDictionary.Add("SMT", "SMT");
            _suffixDictionary.Add("SUMIT", "SMT");
            _suffixDictionary.Add("SUMITT", "SMT");
            _suffixDictionary.Add("SUMMIT", "SMT");
            _suffixDictionary.Add("TER", "TER");
            _suffixDictionary.Add("TERR", "TER");
            _suffixDictionary.Add("TERRACE", "TER");
            _suffixDictionary.Add("THROUGHWAY", "THWY");
            _suffixDictionary.Add("THWY", "TRWY");
            _suffixDictionary.Add("TRACE", "TRCE");
            _suffixDictionary.Add("TRACES", "TRCE");
            _suffixDictionary.Add("TRCE", "TRCE");
            _suffixDictionary.Add("TRACK", "TRK");
            _suffixDictionary.Add("TRACKS", "TRKS");
            _suffixDictionary.Add("TRAK", "TRK");
            _suffixDictionary.Add("TRK", "TRK");
            _suffixDictionary.Add("TRKS", "TRKS");
            _suffixDictionary.Add("TRAFFICWAY", "TRWY");
            _suffixDictionary.Add("TRWY", "TRWY");
            _suffixDictionary.Add("TRAIL", "TRL");
            _suffixDictionary.Add("TRAILS", "TRLS");
            _suffixDictionary.Add("TRL", "TRL");
            _suffixDictionary.Add("TRLS", "TRLS");
            _suffixDictionary.Add("TRAILER", "TRLR");
            _suffixDictionary.Add("TRLR", "TRLR");
            _suffixDictionary.Add("TRLRS", "TRLR");
            _suffixDictionary.Add("TUNEL", "TUNL");
            _suffixDictionary.Add("TUNL", "TUNL");
            _suffixDictionary.Add("TUNLS", "TUNL");
            _suffixDictionary.Add("TUNNEL", "TUNL");
            _suffixDictionary.Add("TUNNELS", "TUNL");
            _suffixDictionary.Add("TUNNL", "TUNL");
            _suffixDictionary.Add("TRNPK", "TPKE");
            _suffixDictionary.Add("TPKE", "TPKE");
            _suffixDictionary.Add("TURNPIKE", "TPKE");
            _suffixDictionary.Add("TURNPK", "TPKE");
            _suffixDictionary.Add("UNDERPASS", "UPAS");
            _suffixDictionary.Add("UPAS", "UPAS");
            _suffixDictionary.Add("UN", "UN");
            _suffixDictionary.Add("UNION", "UN");
            _suffixDictionary.Add("UNIONS", "UNS");
            _suffixDictionary.Add("UNS", "UNS");
            _suffixDictionary.Add("VALLEY", "VLY");
            _suffixDictionary.Add("VALLY", "VLY");
            _suffixDictionary.Add("VLLY", "VLY");
            _suffixDictionary.Add("VLY", "VLY");
            _suffixDictionary.Add("VALLEYS", "VLYS");
            _suffixDictionary.Add("VLYS", "VLYS");
            _suffixDictionary.Add("VDCT", "VDCT");
            _suffixDictionary.Add("VIA", "VIA");
            _suffixDictionary.Add("VIADCT", "VIA");
            _suffixDictionary.Add("VIADUCT", "VIA");
            _suffixDictionary.Add("VIEW", "VW");
            _suffixDictionary.Add("VW", "VW");
            _suffixDictionary.Add("VIEWS", "VWS");
            _suffixDictionary.Add("VWS", "VWS");
            _suffixDictionary.Add("VILL", "VLG");
            _suffixDictionary.Add("VILLAG", "VLG");
            _suffixDictionary.Add("VILLAGE", "VLG");
            _suffixDictionary.Add("VILLG", "VLG");
            _suffixDictionary.Add("VILLIAGE", "VLG");
            _suffixDictionary.Add("VLG", "VLG");
            _suffixDictionary.Add("VILLAGES", "VLGS");
            _suffixDictionary.Add("VLGS", "VLGS");
            _suffixDictionary.Add("VILLE", "VL");
            _suffixDictionary.Add("VL", "VL");
            _suffixDictionary.Add("VIS", "VIS");
            _suffixDictionary.Add("VIST", "VIS");
            _suffixDictionary.Add("VISTA", "VIS");
            _suffixDictionary.Add("VST", "VIS");
            _suffixDictionary.Add("VSTA", "VIS");
            _suffixDictionary.Add("WALK", "WALK");
            _suffixDictionary.Add("WALKS", "WALK");
            _suffixDictionary.Add("WALL", "WALL");
            _suffixDictionary.Add("WY", "WAY");
            _suffixDictionary.Add("WAY", "WAY");
            _suffixDictionary.Add("WAYS", "WAYS");
            _suffixDictionary.Add("WELL", "WELL");
            _suffixDictionary.Add("WELLS", "WLS");
            _suffixDictionary.Add("WLS", "WLS");
        }

        private static void CreateDirectionalDictionary()
        {
            _directionalDictionary = new Hashtable();

            _directionalDictionary.Add("N", "N");
            _directionalDictionary.Add("S", "S");
            _directionalDictionary.Add("E", "E");
            _directionalDictionary.Add("W", "W");
            _directionalDictionary.Add("NE", "NE");
            _directionalDictionary.Add("NW", "NW");
            _directionalDictionary.Add("SE", "SE");
            _directionalDictionary.Add("SW", "SW");
            _directionalDictionary.Add("NORTH", "N");
            _directionalDictionary.Add("SOUTH", "S");
            _directionalDictionary.Add("EAST", "E");
            _directionalDictionary.Add("WEST", "W");
            _directionalDictionary.Add("NORTHEAST", "NE");
            _directionalDictionary.Add("NORTHWEST", "NW");
            _directionalDictionary.Add("SOUTHEAST", "SE");
            _directionalDictionary.Add("SOUTHWEST", "SW");
        }

        private static void CreateSecondaryAddressIdentifierDictionary()
        {
            _secondaryAddressIdentifierDictionary = new Hashtable();

            // Requires secondary range number to follow

            _secondaryAddressIdentifierDictionary.Add("APARTMENT", true);
            _secondaryAddressIdentifierDictionary.Add("BASEMENT", true);
            _secondaryAddressIdentifierDictionary.Add("BUILDING", true); 
            _secondaryAddressIdentifierDictionary.Add("DEPARTMENT", true);
            _secondaryAddressIdentifierDictionary.Add("FLOOR", true); 
            _secondaryAddressIdentifierDictionary.Add("FRONT", true);
            _secondaryAddressIdentifierDictionary.Add("HANGAR", true);
            _secondaryAddressIdentifierDictionary.Add("LOBBY", true);
            _secondaryAddressIdentifierDictionary.Add("LOT", true);
            _secondaryAddressIdentifierDictionary.Add("LOWER", true);
            _secondaryAddressIdentifierDictionary.Add("OFFICE", true);
            _secondaryAddressIdentifierDictionary.Add("PENTHOUSE", true);
            _secondaryAddressIdentifierDictionary.Add("PIER", true);
            _secondaryAddressIdentifierDictionary.Add("ROOM", true);
            _secondaryAddressIdentifierDictionary.Add("SLIP", true);
            _secondaryAddressIdentifierDictionary.Add("SPACE", true);
            _secondaryAddressIdentifierDictionary.Add("STOP", true);
            _secondaryAddressIdentifierDictionary.Add("SUITE", true);
            _secondaryAddressIdentifierDictionary.Add("TRAILER", true);
            _secondaryAddressIdentifierDictionary.Add("UNIT", true);
            _secondaryAddressIdentifierDictionary.Add("UPPER", true);
            _secondaryAddressIdentifierDictionary.Add("APT", true);
            _secondaryAddressIdentifierDictionary.Add("BLDG", true);
            _secondaryAddressIdentifierDictionary.Add("DEPT", true);
            _secondaryAddressIdentifierDictionary.Add("FL", true);
            _secondaryAddressIdentifierDictionary.Add("HNGR", true);
            _secondaryAddressIdentifierDictionary.Add("RM", true);
            _secondaryAddressIdentifierDictionary.Add("SPC", true);
            _secondaryAddressIdentifierDictionary.Add("STE", true);
            _secondaryAddressIdentifierDictionary.Add("TRLR", true);
            _secondaryAddressIdentifierDictionary.Add("UPPR", true);

            // Does not require secondary range number to follow

            _secondaryAddressIdentifierDictionary.Add("BSMT", false);
            _secondaryAddressIdentifierDictionary.Add("FRNT", false);
            _secondaryAddressIdentifierDictionary.Add("LBBY", false);
            _secondaryAddressIdentifierDictionary.Add("LOWR", false);
            _secondaryAddressIdentifierDictionary.Add("OFC", false);
            _secondaryAddressIdentifierDictionary.Add("PH", false);
            _secondaryAddressIdentifierDictionary.Add("REAR", false);
            _secondaryAddressIdentifierDictionary.Add("SIDE", false);
        }

        private static bool IsPrimaryAddressNumber(string lexeme)
        {
            return Regex.IsMatch(lexeme, @"^\d+\w*");
        }

        private static bool IsSuffix(string lexeme)
        {
            return _suffixDictionary.Contains(lexeme.ToUpper());
        }

        private static string GetShortSuffix(string lexeme)
        {
            if (!IsSuffix(lexeme)) {
                throw new ApplicationException(lexeme + " is not a suffix.");
            }

            return ((string)_suffixDictionary[lexeme.ToUpper()]).ToUpper();
        }

        private static bool IsSecondaryAddressIdentifier(string lexeme)
        {
            return _secondaryAddressIdentifierDictionary.Contains(lexeme.ToUpper());
        }

        private static string GetShortSecondaryAddressIdentifier(string lexeme)
        {
            if (!IsSecondaryAddressIdentifier(lexeme)) {
                throw new ApplicationException(lexeme + " is not a secondary address identifier.");
            }

            return lexeme.ToUpper();
        }

        private static bool IsSecondaryAddressRange(string lexeme)
        {
            return Regex.IsMatch(lexeme, @"^\d+\w*");
        }

        private static bool IsDirectional(string lexeme)
        {
            return _directionalDictionary.Contains(lexeme.ToUpper());
        }

        private static string GetShortDirectional(string lexeme) 
        {
            if (!IsDirectional(lexeme)) {
                throw new ApplicationException(lexeme + " is not a directional.");
            }

            return ((string)_directionalDictionary[lexeme.ToUpper()]).ToUpper();
        }

        #endregion

        #region Fields

        private string _primaryAddressNumber;
        private string _predirectional;
        private string _streetName;
        private string _suffix;
        private string _postdirectional;
        private string _secondaryAddressIdentifier;
        private string _secondaryAddressRange;

        private int _currentPosition;
        private string[] _addressLexeme;

        private bool _parsed;

        #endregion

        #region Constructors

        internal StreetAddressParser()
        {
            Reset();
        }

        #endregion

        #region IDeliveryAddressLineParser implementation

        public void Parse(string deliveryAddressLine)
        {
            Reset();

            if (deliveryAddressLine == null) {
                throw new ArgumentNullException("deliveryAddressLine");
            }

            if (deliveryAddressLine.Trim() == string.Empty) {
                throw new ArgumentException("Delivery address line can not be empty.");
            }

            RemoveSeparators(ref deliveryAddressLine);

            _addressLexeme = deliveryAddressLine.Split(' ');
            if (_addressLexeme.Length < 2) {
                throw new DeliveryAddressLineParserException(INVALIDE_FORMAT_MSG);
            }

            _currentPosition = _addressLexeme.Length - 1;

            DoParse();

            if (_primaryAddressNumber == string.Empty || _streetName == string.Empty) {
                Reset();
                throw new DeliveryAddressLineParserException(INVALIDE_FORMAT_MSG);
            }

            _parsed = true;
        }

        #endregion

        #region Private Methods

        private void Reset()
        {
            _parsed = false;

            _addressLexeme = new string[0];
            _currentPosition = -1;

            _primaryAddressNumber = string.Empty;
            _predirectional = string.Empty;
            _streetName = string.Empty;
            _suffix = string.Empty;
            _postdirectional = string.Empty;
            _secondaryAddressIdentifier = string.Empty;
            _secondaryAddressRange = string.Empty;
        }

        private void DoParse()
        {
            ParseSecondaryAddressRange();

            if (AnalysisBoundaryAttainment(ref _secondaryAddressRange)) {
                return;
            }

            ParseSecondaryAddressIdentifier();

            if (AnalysisBoundaryAttainment(ref _secondaryAddressIdentifier)) {
                return;
            }

            ParsePostdirectional();

            if (AnalysisBoundaryAttainment(ref _postdirectional)) {
                return;
            }

            ParseSuffix();

            if (AnalysisBoundaryAttainment(ref _suffix)) {
                return;
            }

            ParseStreetName(true);

            if (AnalysisBoundaryAttainment(ref _streetName)) {
                return;
            }

            ParsePredirectional();
            ParsePrimaryAddressNumber();

            _primaryAddressNumber = _primaryAddressNumber.Trim();
            _predirectional = _predirectional.Trim();
            _streetName = _streetName.Trim();
            _suffix = _suffix.Trim();
            _postdirectional = _postdirectional.Trim();
            _secondaryAddressIdentifier = _secondaryAddressIdentifier.Trim();
            _secondaryAddressRange = _secondaryAddressRange.Trim();
        }

        private int DecreaseCurrentPosition()
        {
            return --_currentPosition;
        }

        private bool AnalysisBoundaryAttainment(ref string lastLexeme)
        {
            if (_currentPosition == 0) {
                ParsePrimaryAddressNumber();
                if (!Object.ReferenceEquals(_streetName, lastLexeme)) {
                    _streetName = OriginalLastLexeme;
                    lastLexeme = string.Empty;
                }
            }

            return _currentPosition == 0;
        }

        private void ParseSecondaryAddressRange()
        {
            if (IsSecondaryAddressRange(CurrentLexeme)) {
                _secondaryAddressRange = CurrentLexeme;
                DecreaseCurrentPosition();
            }
        }

        private void ParseSecondaryAddressIdentifier()
        {
            if (IsSecondaryAddressIdentifier(CurrentLexeme)) {
                _secondaryAddressIdentifier = GetShortSecondaryAddressIdentifier(CurrentLexeme);
                DecreaseCurrentPosition();
            }
        }

        private void ParsePostdirectional()
        {
            if (IsDirectional(CurrentLexeme)) {
                _postdirectional = GetShortDirectional(CurrentLexeme);
                DecreaseCurrentPosition();
            }
        }

        private void ParseSuffix()
        {
            if (IsSuffix(CurrentLexeme)) {
                _suffix = GetShortSuffix(CurrentLexeme);
                DecreaseCurrentPosition();
            }
        }

        private void ParseStreetName(bool honourDirectional)
        {
            while ((!honourDirectional || !IsDirectional(CurrentLexeme)) && _currentPosition > 1) {
                _streetName = CurrentLexeme + " " + _streetName;
                DecreaseCurrentPosition();
            }

            _streetName = _streetName.TrimEnd(' ');
        }

        private void ParsePredirectional()
        {
            if (_streetName == string.Empty || _currentPosition > 1) {
                ParseStreetName(false);
            }

            if (IsDirectional(CurrentLexeme)) {
                if (_streetName == string.Empty && _currentPosition == 1) {
                    _streetName = CurrentLexeme + " " + _streetName;
                } else {
                    _predirectional = GetShortDirectional(CurrentLexeme);
                }

                DecreaseCurrentPosition();
            } else if (_currentPosition == 1) {
                _streetName = CurrentLexeme + " " + _streetName;
                DecreaseCurrentPosition();
            }
        }

        private void ParsePrimaryAddressNumber()
        {
            if (_currentPosition != 0) {
                throw new ApplicationException("The current parsing position must be 0.");
            }

            if (IsPrimaryAddressNumber(CurrentLexeme)) {
                _primaryAddressNumber = CurrentLexeme;
            }
        }

        private void RemoveSeparators(ref string deliveryAddressLine)
        {
            ReplaceWithSpace(",", ref deliveryAddressLine);
            ReplaceWithSpace("  ", ref deliveryAddressLine);

            deliveryAddressLine = deliveryAddressLine.Trim();

            if (deliveryAddressLine == string.Empty) {
                throw new DeliveryAddressLineParserException(INVALIDE_FORMAT_MSG);
            }
        }

        private void ReplaceWithSpace(string v, ref string s)
        {
            while (s.IndexOf(v) != -1) {
                s = s.Replace(v, " ");
            }
        }

        private void AssertParsed()
        {
            if (!_parsed) {
                throw new InvalidOperationException("No delivery address line has been parsed.");
            }
        }

        #endregion

        #region Properties

        public string PrimaryAddressNumber
        {
            get
            {
                AssertParsed();
                return _primaryAddressNumber;
            }
        }

        public string Predirectional
        {
            get
            {
                AssertParsed();
                return _predirectional;
            }
        }

        public string StreetName
        {
            get
            {
                AssertParsed();
                return _streetName;
            }
        }

        public string Suffix
        {
            get
            {
                AssertParsed();
                return _suffix;
            }
        }

        public string Postdirectinal
        {
            get
            {
                AssertParsed();
                return _postdirectional;
            }
        }

        public string SecondaryAddressIdentifier
        {
            get
            {
                AssertParsed();
                return _secondaryAddressIdentifier;
            }
        }

        public string SecondaryAddressRange
        {
            get
            {
                AssertParsed();
                return _secondaryAddressRange;
            }
        }

        private string CurrentLexeme
        {
            get { return _addressLexeme[_currentPosition]; }
        }

        private string OriginalLastLexeme 
        {
            get { return _addressLexeme[_currentPosition + 1]; }
        }

        #endregion
    }
}