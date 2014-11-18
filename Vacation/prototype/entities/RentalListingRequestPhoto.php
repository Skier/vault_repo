<?

class RentalListingRequestPhoto
{

    var $m_photoId;
    var $m_requestId;
    var $m_fileName;

    function RentalListingRequestPhoto() {
    }

    function fetch($row) {
        $this->setPhotoId($row["PHOTO_ID"]);
        $this->setRequestId($row["REQUEST_ID"]);
        $this->setFileName($row["FILE_NAME"]);
    }

    function store() {
        $requestId = intpreprocess($this->getRequestId());
        $fileName = preprocess($this->getFileName());

        if (is_null($this->getPhotoId())) {
            $sql = "insert into VRBO_RENTAL_LISTING_REQUEST_PHOTO (REQUEST_ID, FILE_NAME)"
                . " values ($requestId, $fileName)";
            execSql($sql);
            $this->setPhotoId(mysql_insert_id());
        } else {
            $photoId = $this->getPhotoId();
            $sql = "update VRBO_RENTAL_LISTING_REQUEST_PHOTO"
                . " set REQUEST_ID = $requestId, "
                . " FILE_NAME = $fileName "
                . " where PHOTO_ID = $photoId";
            execSql($sql);
        }
    }

    function setPhotoId($photoId) {
        $this->m_photoId = $photoId;
    }

    function getPhotoId() {
        return $this->m_photoId;
    }

    function setRequestId($requestId) {
        $this->m_requestId = $requestId;
    }

    function getRequestId() {
        return $this->m_requestId;
    }

    function setFileName($fileName) {
        $this->m_fileName = $fileName;
    }

    function getFileName() {
        return $this->m_fileName;
    }

}

?>
