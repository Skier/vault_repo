<?

include_once("include/connect.php");
include_once("RentalListingRequestPhoto.php");

class RentalListingRequestPhotoInfo extends RentalListingRequestPhoto
{

    var $m_photoInfoId;
    var $m_description;
    var $m_language;

    function RentalListingRequestPhotoInfo() {
    }

    function fetch($row) {
        $this->setPhotoInfoId($row["PHOTO_INFO_ID"]);
        $this->setDescription($row["PHOTO_DESCRIPTION"]);
        $this->setLanguage($row["LANGUAGE"]);
        parent::fetch($row);
    }

    function getByPhotoId($photoId, $language) {
        $language = preprocess($language);
        $sql = "select pi.PHOTO_INFO_ID, pi.PHOTO_DESCRIPTION, pi.LANGUAGE,"
            . " p.PHOTO_ID, p.REQUEST_ID, p.FILE_NAME"
            . " from VRBO_RENTAL_LISTING_REQUEST_PHOTO_INFO pi"
            . " inner join VRBO_RENTAL_LISTING_REQUEST_PHOTO p"
            . " on p.PHOTO_ID = pi.PHOTO_ID"
            . " where pi.PHOTO_ID = $photoId and pi.LANGUAGE = $language";
        $res = execSql($sql);
        $rowsNumber = mysql_num_rows($res);
        if (0 == $rowsNumber) {
            return null;
        } else if (1 < $rowsNumber) {
            die("Duplicate photo info found.");
        }

        $row = mysql_fetch_array($res);
        $this->fetch($row);
    }

    function store() {
        parent::store();
        $description = preprocess($this->getDescription());
        $language = preprocess($this->getLanguage());
        $photoId = intpreprocess($this->getPhotoId());

        if (is_null($this->getPhotoInfoId())) {
            $sql = "insert into VRBO_RENTAL_LISTING_REQUEST_PHOTO_INFO ("
                . " PHOTO_DESCRIPTION, LANGUAGE, PHOTO_ID)"
                . " values ($description, $language, $photoId)";
            execSql($sql);
            $this->setPhotoInfoId(mysql_insert_id());
        } else {
            $photoInfoId = $this->getPhotoInfoId();
            $sql = "update VRBO_RENTAL_LISTING_REQUEST_PHOTO_INFO"
                . " set PHOTO_DESCRIPTION = $description,"
                . " LANGUAGE = $language,"
                . " PHOTO_ID = $photoId"
                . " where PHOTO_INFO_ID = $photoInfoId";
            execSql($sql);
        }
    }

    function setPhotoInfoId($photoInfoId) {
        $this->m_photoInfoId = $photoInfoId;
    }

    function getPhotoInfoId() {
        return $this->m_photoInfoId;
    }

    function setDescription($description) {
        $this->m_description = $description;
    }

    function getDescription() {
        return $this->m_description;
    }

    function setLanguage($language) {
        $this->m_language = $language;
    }

    function getLanguage() {
        return $this->m_language;
    }

}

?>
