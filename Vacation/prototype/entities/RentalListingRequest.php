<?

class RentalListingRequest
{

    var $m_requestId;
    var $m_userId;
    var $m_locationId;
    var $m_status;
    var $m_dateCreated;
    var $m_rentalName;
    var $m_rentalAddress;
    var $m_rentalDescription;
    var $m_bedroomsNumber;
    var $m_bedroomsAdditional;
    var $m_bathsNumber;
    var $m_sleepingPlacesNumber;
    var $m_isPetFriendly;
    var $m_isNoSmoking;
    var $m_amenities;
    var $m_activities;
    var $m_ratesDescription;
    var $m_locationDescription;

    function RentalListingRequest() {
    }

    function fetch($row) {
        $this->setRequestId($row["REQUEST_ID"]);
        $this->setUserId($row["USER_ID"]);
        $this->setLocationId($row["LOCATION_ID"]);
        $this->setStatus($row["REQUEST_STATUS"]);
        $this->setDateCreated($row["DATE_CREATED"]);
        $this->setRentalName($row["RENTAL_NAME"]);
        $this->setRentalAddress($row["RENTAL_ADDRESS"]);
        $this->setRentalDescription($row["RENTAL_DESCRIPTION"]);
        $this->setBedroomsNumber($row["BEDROOMS_NUMBER"]);
        $this->setBedroomsAdditional($row["BEDROOMS_ADDITIONAL"]);
        $this->setBathsNumber($row["BATHS_NUMBER"]);
        $this->setSleepingPlacesNumber($row["SLEEPING_PLACES_NUMBER"]);
        $this->setIsPetFriendly($row["IS_PET_FRIENDLY"]);
        $this->setIsNoSmoking($row["IS_NO_SMOKING"]);
        $this->setAmenities($row["AMENITIES"]);
        $this->setActivities($row["ACTIVITIES"]);
        $this->setRatesDescription($row["RATES_DESCRIPTION"]);
        $this->setLocationDescription($row["LOCATION_DESCRIPTION"]);
    }

    static function getByUserId($userId) {
        $sql = "select * from VRBO_RENTAL_LISTING_REQUEST where USER_ID = $userId";
        $res = execSql($sql);

        $result = array();
        while($row = mysql_fetch_array($res)) {
            $item = new RentalListingRequest();
            $item->fetch($row);
            $result[] = $item;
        }
        return $result;
    }

    static function getById($id) {
        $sql = "select * from VRBO_RENTAL_LISTING_REQUEST where REQUEST_ID = $id";
        $res = execSql($sql);
        $rowsNumber = mysql_num_rows($res);
        if (0 == $rowsNumber) {
            return null;
        } else if (1 < $rowsNumber) {
            die("Duplicate rental listing request found.");
        }

        $row = mysql_fetch_array($res);
        $item = new RentalListingRequest();
        $item->fetch($row);
        return $item;
    }

    static function getByLocationId($locationId) {
        $sql = "select * from VRBO_RENTAL_LISTING_REQUEST where LOCATION_ID = $locationId";
        $res = execSql($sql);
        $result = array();

        while($row = mysql_fetch_array($res)) {
            $item = new RentalListingRequest();
            $item->fetch($row);
            $result[] = $item;
        }
        return $result;
    }

    static function getByStatus($status) {
        $sql = "select * from VRBO_RENTAL_LISTING_REQUEST where REQUEST_STATUS = '$status'";
        $res = execSql($sql);
        $result = array();

        while($row = mysql_fetch_array($res)) {
            $item = new RentalListingRequest();
            $item->fetch($row);
            $result[] = $item;
        }
        return $result;
    }

    function store() {
        $userId = intpreprocess($this->getUserId());
        $locationId = intpreprocess($this->getLocationId());
        $status = preprocess($this->getStatus());
        $rentalName = preprocess($this->getRentalName());
        $rentalAddress = preprocess($this->getRentalAddress());
        $rentalDescription = preprocess($this->getRentalDescription());
        $bedroomsNumber = intpreprocess($this->getBedroomsNumber());
        $bedroomsAdditional = preprocess($this->getBedroomsAdditional());
        $bathsNumber = intpreprocess($this->getBathsNumber());
        $sleepingPlacesNumber = intpreprocess($this->getSleepingPlacesNumber());
        $isPetFriendly = boolpreprocess($this->getIsPetFriendly());
        $isNoSmoking = boolpreprocess($this->getIsNoSmoking());
        $amenities = preprocess($this->getAmenities());
        $activities = preprocess($this->getActivities());
        $ratesDescription = preprocess($this->getRatesDescription());
        $locationDescription = preprocess($this->getLocationDescription());

        if (is_null($this->getRequestId())) {
            $sql = "insert into VRBO_RENTAL_LISTING_REQUEST (USER_ID, LOCATION_ID, REQUEST_STATUS, "
                . "DATE_CREATED, RENTAL_NAME, RENTAL_ADDRESS, RENTAL_DESCRIPTION, BEDROOMS_NUMBER, "
                . "BEDROOMS_ADDITIONAL, BATHS_NUMBER, SLEEPING_PLACES_NUMBER, IS_PET_FRIENDLY, "
                . "IS_NO_SMOKING, AMENITIES, ACTIVITIES, RATES_DESCRIPTION, LOCATION_DESCRIPTION)"
                . "values ($userId, $locationId, $status, now(), $rentalName, "
                . "$rentalAddress, $rentalDescription, $bedroomsNumber, $bedroomsAdditional, "
                . "$bathsNumber, $sleepingPlacesNumber, $isPetFriendly, $isNoSmoking, "
                . "$amenities, $activities, $ratesDescription, $locationDescription)";
            execSql($sql);
            $this->setRequestId(mysql_insert_id());
        } else {
            $requestId = $this->getRequestId();
            $sql = "update VRBO_RENTAL_LISTING_REQUEST"
                . " set LOCATION_ID = $locationId,"
                . " REQUEST_STATUS = $status,"
                . " RENTAL_NAME = $rentalName,"
                . " RENTAL_ADDRESS = $rentalAddress,"
                . " RENTAL_DESCRIPTION = $rentalDescription,"
                . " BEDROOMS_NUMBER = $bedroomsNumber,"
                . " BEDROOMS_ADDITIONAL = $bedroomsAdditional,"
                . " BATHS_NUMBER = $bathsNumber,"
                . " SLEEPING_PLACES_NUMBER = $sleepingPlacesNumber,"
                . " IS_PET_FRIENDLY = $isPetFriendly,"
                . " IS_NO_SMOKING = $isNoSmoking,"
                . " AMENITIES = $amenities,"
                . " ACTIVITIES = $activities,"
                . " RATES_DESCRIPTION = $ratesDescription,"
                . " LOCATION_DESCRIPTION = $locationDescription"
                . " where REQUEST_ID = $requestId";
            execSql($sql);
        }
    }

    function setRequestId($requestId) {
        $this->m_requestId = $requestId;
    }

    function getRequestId() {
        return $this->m_requestId;
    }

    function setUserId($userId) {
        $this->m_userId = $userId;
    }

    function getUserId() {
        return $this->m_userId;
    }

    function setLocationId($locationId) {
        $this->m_locationId = $locationId;
    }

    function getLocationId() {
        return $this->m_locationId;
    }

    function setStatus($status) {
        $this->m_status = trim($status);
    }

    function getStatus() {
        return $this->m_status;
    }

    function setDateCreated($dateCreated) {
        $this->m_dateCreated = $dateCreated;
    }

    function getDateCreated() {
        return $this->m_dateCreated;
    }

    function setRentalName($rentalName) {
        $this->m_rentalName = trim($rentalName);
    }

    function getRentalName() {
        return $this->m_rentalName;
    }

    function setRentalAddress($rentalAddress) {
        $this->m_rentalAddress = trim($rentalAddress);
    }

    function getRentalAddress() {
        return $this->m_rentalAddress;
    }

    function setRentalDescription($rentalDescription) {
        $this->m_rentalDescription = trim($rentalDescription);
    }

    function getRentalDescription() {
        return $this->m_rentalDescription;
    }

    function setBedroomsNumber($bedroomsNumber) {
        $this->m_bedroomsNumber = $bedroomsNumber;
    }

    function getBedroomsNumber() {
        return $this->m_bedroomsNumber;
    }

    function setBedroomsAdditional($bedroomsAdditional) {
        $this->m_bedroomsAdditional = trim($bedroomsAdditional);
    }

    function getBedroomsAdditional() {
        return $this->m_bedroomsAdditional;
    }

    function setBathsNumber($bathsNumber) {
        $this->m_bathsNumber = $bathsNumber;
    }

    function getBathsNumber() {
        return $this->m_bathsNumber;
    }

    function setSleepingPlacesNumber($sleepingPlacesNumber) {
        $this->m_sleepingPlacesNumber = $sleepingPlacesNumber;
    }

    function getSleepingPlacesNumber() {
        return $this->m_sleepingPlacesNumber;
    }

    function setIsPetFriendly($isPetFriendly) {
        $this->m_isPetFriendly = $isPetFriendly;
    }

    function getIsPetFriendly() {
        return $this->m_isPetFriendly;
    }

    function setIsNoSmoking($isNoSmoking) {
        $this->m_isNoSmoking = $isNoSmoking;
    }

    function getIsNoSmoking() {
        return $this->m_isNoSmoking;
    }

    function setAmenities($amenities) {
        $this->m_amenities = trim($amenities);
    }

    function getAmenities() {
        return $this->m_amenities;
    }

    function setActivities($activities) {
        $this->m_activities = trim($activities);
    }

    function getActivities() {
        return $this->m_activities;
    }

    function setRatesDescription($ratesDescription) {
        $this->m_ratesDescription = $ratesDescription;
    }

    function getRatesDescription() {
        return $this->m_ratesDescription;
    }

    function setLocationDescription($locationDescription) {
        $this->m_locationDescription = $locationDescription;
    }

    function getLocationDescription() {
        return $this->m_locationDescription;
    }

}

?>
