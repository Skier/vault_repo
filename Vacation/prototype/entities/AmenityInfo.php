<?

class AmenityInfo
{

    var $m_amenityInfoId;
    var $m_amenityName;
    var $m_language;
    var $m_amenityId;

    function AmenityInfo() {
    }

    function fetch($row) {
        $this->setAmenityInfoId($row["AMENITY_INFO_ID"]);
        $this->setAmenityName($row["AMENITY_NAME"]);
        $this->setLanguage($row["LANGUAGE"]);
        $this->setAmenityId($row["AMENITY_ID"]);
    }

    static function getAll($language) {
        $language = preprocess($language);
        $sql = "select * from VRBO_AMENITY_INFO ai "
            . " inner join VRBO_AMENITY a on a.AMENITY_ID = ai.AMENITY_ID "
            . " where ai.LANGUAGE = $language "
            . " order by ai.AMENITY_NAME";
        $res = execSql($sql);

        $result = array();
        while ($row = mysql_fetch_array($res)) {
            $item = new AmenityInfo();
            $item->fetch($row);
            $result[] = $item;
        }
        return $result;
    }

    static function getByAmenityId($id, $language) {
        $language = preprocess($language);
        $sql = "select * from VRBO_AMENITY_INFO ai "
            . " where ai.LANGUAGE = $language "
            . " and ai.AMENITY_ID = $id"
            . " order by ai.AMENITY_NAME";
        $res = execSql($sql);

        $rowsNumber = mysql_num_rows($res);
        if (0 == $rowsNumber) {
            return null;
        } else if (1 < $rowsNumber) {
            die("Duplicate amenity info found.");
        }

        $row = mysql_fetch_array($res);
        $item = new AmenityInfo();
        $item->fetch($row);
        return $item;
    }

    function setAmenityInfoId($amenityInfoId) {
        $this->m_amenityInfoId = $amenityInfoId;
    }

    function getAmenityInfoId() {
        return $this->m_amenityInfoId;
    }

    function setAmenityName($amenityName) {
        $this->m_amenityName = trim($amenityName);
    }

    function getAmenityName() {
        return $this->m_amenityName;
    }

    function setLanguage($language) {
        $this->m_language = trim($language);
    }

    function getLanguage() {
        return $this->m_language;
    }

    function setAmenityId($amenityId) {
        $this->m_amenityId = $amenityId;
    }

    function getAmenityId() {
        return $this->m_amenityId;
    }

}

?>
