<?

include_once("classes/AuthPage.php");
//include_once("entities/LocationInfo.php");
include_once("entities/RentalListingRequest.php");
include_once("entities/AmenityInfo.php");
include_once("entities/ActivityInfo.php");

class EditPendingRentalPage extends AuthPage
{
    // Fields
    var $rentalName;
    var $rentalAddress;
    var $bedroomsNumber;
    var $additionalSleeping;
    var $bathsNumber;
    var $sleepingPlacesNumber;
    var $isPetFriendly;
    var $amenities;
    var $activities;
    var $description;
    //  var $imagesList;  

    // global
    var $rentalId;

    function __construct() {
        parent::__construct("Edit pending rental's info", "EditPendingRental.php", "EditPendingRental.html");

        $this->addVariable("rentalName");
        $this->addVariable("rentalAddress");
        $this->addVariable("bedroomsNumber");
        $this->addVariable("additionalSleeping");
        $this->addVariable("bathsNumber");
        $this->addVariable("sleepingPlacesNumber");
        $this->addVariable("isPetFriendly");
        $this->addVariable("isNoSmoking");
        $this->addVariable("amenities");
        $this->addVariable("activities");
        $this->addVariable("description");

        if (isset($_GET["rentalId"])) {
            $this->rentalId = $_GET["rentalId"];
            $_SESSION["rentalId"] = $_GET["rentalId"];
        } elseif (isset($_SESSION["rentalId"])) {
            $this->rentalId = $_SESSION["rentalId"];
        }
    }

    function isPostBack() {
        return isset($_GET["submit"]);
    }

    function check() {
        if ("" == trim($this->rentalName)) {
            $this->addMessage("ERR_RENTAL_NAME_REQUIRED");
        }

        if ("" == trim($this->rentalAddress)) {
            $this->addMessage("ERR_RENTAL_ADDRESS_REQUIRED");
        }

        if ("" == trim($this->bedroomsNumber)) {
            $this->addMessage("ERR_BEDROOMS_NUMBER_REQUIRED");
        }

        if ("" == trim($this->bathsNumber)) {
            $this->addMessage("ERR_BATHS_NUMBER_REQUIRED");
        }

        if ("" == trim($this->sleepingPlacesNumber)) {
            $this->addMessage("ERR_SLEEPING_PLACES_NUMBER_REQUIRED");
        }

        return !$this->hasMessages();
    }

    function process() {
        $request = RentalListingRequest::getById($this->rentalId);
        //$request->setUserId($this->getUserId());
        //$request->setLocationId($_SESSION["location"]);
        $request->setStatus("Initiated");
        $request->setRentalName($this->rentalName);
        $request->setRentalAddress($this->rentalAddress);
        $request->setBedroomsNumber($this->bedroomsNumber);
        $request->setBedroomsAdditional($this->additionalSleeping);
        $request->setBathsNumber($this->bathsNumber);
        $request->setSleepingPlacesNumber($this->sleepingPlacesNumber);
        $request->setIsPetFriendly($this->isPetFriendly);
        $request->setIsNoSmoking($this->isNoSmoking);
        //$request->setAmenities($this->amenities);
        //$this->setAmenities($this->amenities);
        //$request->setAmenities($this->setAmenities($this->amenities));
        //$request->setActivities($this->activities);        
        //$this->setActivities($this->activities);
        //$request->setActivities($this->setActivities($this->activities));        
        $request->setRatesDescription($this->description);
        $request->store();
        $_SESSION["requestId"] = $request->getRequestId();
        
        $this->redirect("CompanyUser.php");
        exit;
    }

    function setAmenities($names) {
        $items = AmenitiesInfo::getAll($this->getLanguage());
        $ids = NULL;

        for ($i = 0; $i < count($items); $index ++) {
            $item = $items[$i];

            if (strstr($names, $item->getAmenitiesName())) {
                $ids .= $item->getAmenitiesId() . ",";
            }
        }
        return $ids;
    }

    function getAmenities($ids) {
        $items = NULL;

        while ($p = strpos($ids, ',')) {
            $item = AmenityInfo::getByAmenityId(substr($ids, 0, $p), $this->getLanguage())->getAmenityName();

            if (NULL == $items) {
                $items = $item;
            } else {
                $items .= ", " . $item;
            }
            $ids = substr($ids, 1 + $p);
        }

        return $items;
    }

    function setActivities($names) {
        $items = ActivityInfo::getAll($this->getLanguage());
        $ids = NULL;

        for ($i = 0; $i < count($items); $index ++) {
            $item = $items[$i];

            if (strstr($names, $item->getActivityName())) {
                $ids .= $item->getActivityId() . ",";
            }
        }
        return $ids;
    }

    function getActivities($ids) {
        $items = NULL;

        while ($p = strpos($ids, ',')) {
            $item = ActivityInfo::getByActivityId(substr($ids, 0, $p), $this->getLanguage())->getActivityName();

            if (NULL == $items) {
                $items = $item;
            } else {
                $items .= ", " . $item;
            }
            $ids = substr($ids, 1 + $p);
        }
        return $items;
    }

    function show() {
        if ((!$this->isPostBack()) && (!isset($_GET["language"]))) {
            $request = RentalListingRequest::getById($this->rentalId);

            if (NULL != $request->getUserId()) {
                $this->rentalName = $request->getRentalName();
                $this->rentalAddress = $request->getRentalAddress();
                $this->bedroomsNumber = $request->getBedroomsNumber();
                $this->additionalSleeping = $request->getBedroomsAdditional();
                $this->bathsNumber = $request->getBathsNumber();
                $this->sleepingPlacesNumber = $request->getSleepingPlacesNumber();
                $this->isPetFriendly = $request->getIsPetFriendly();
                $this->isNoSmoking = $request->getIsNoSmoking();
                //$this->amenities = $request->getAmenities();
                $this->amenities = $this->getAmenities($request->getAmenities());
                //$this->activities = $request->getActivities();
                $this->activities = $this->getActivities($request->getActivities());
                $this->description = $request->getRatesDescription();
            }
        }

        parent::show();
    }
}

$editPage = new EditPendingRentalPage();
$editPage->render();

?>
