<?

include_once("classes/AuthPage.php");
include_once("entities/LocationInfo.php");
include_once("entities/RentalListingRequest.php");

class OwnerSelectBaseInfoPage extends AuthPage
{

    var $rentalName;
    var $rentalAddress;
    var $bedroomsNumber;
    var $additionalSleeping;
    var $bathsNumber;
    var $sleepingPlacesNumber;
    var $isPetFriendly;
    var $isNoSmoking;

    function __construct() {
        parent::__construct("Enter rental's info", "OwnerSelectBaseInfo.php", "OwnerSelectBaseInfo.html");
        $this->addVariable("rentalName");
        $this->addVariable("rentalAddress");
        $this->addVariable("bedroomsNumber");
        $this->addVariable("additionalSleeping");
        $this->addVariable("bathsNumber");
        $this->addVariable("sleepingPlacesNumber");
        $this->addVariable("isPetFriendly");
        $this->addVariable("isNoSmoking");
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
        $request = new RentalListingRequest();
        $request->setUserId($this->getUserId());
        if (isset($_SESSION["location"])) {
            $request->setLocationId($_SESSION["location"]);
        }
        $request->setStatus("BasicInfoSaved");
        $request->setRentalName($this->rentalName);
        $request->setRentalAddress($this->rentalAddress);
        $request->setBedroomsNumber($this->bedroomsNumber);
        $request->setBedroomsAdditional($this->additionalSleeping);
        $request->setBathsNumber($this->bathsNumber);
        $request->setSleepingPlacesNumber($this->sleepingPlacesNumber);
        $request->setIsPetFriendly($this->isPetFriendly);
        $request->setIsNoSmoking($this->isNoSmoking);
        if (isset($_SESSION["locationDescription"])) {
            $request->setLocationDescription($_SESSION["locationDescription"]);
        }
        $request->store();
        $_SESSION["requestId"] = $request->getRequestId();

        $this->redirect("OwnerSelectAmenities.php");
        exit;
    }

}

$selectPage = new OwnerSelectBaseInfoPage();
$selectPage->render();

?>
