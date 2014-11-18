<?

include_once("classes/AuthPage.php");
include_once("entities/LocationInfo.php");
include_once("entities/RentalListingRequest.php");
include_once("entities/AmenityInfo.php");

class OwnerSelectAmenitiesPage extends AuthPage
{

    var $amenities;

    function __construct() {
        parent::__construct("Enter rental's info", "OwnerSelectAmenities.php", "OwnerSelectAmenities.html");
    }

    function isPostBack() {
        return isset($_GET["submit"]);
    }

    function process() {
        $amenities = AmenityInfo::getAll($this->getLanguage());
        $amenitiesStr = '';
        for ($index = 0; $index < count($amenities); $index ++) {
            $amenity = $amenities[$index];
            if (isset($_GET["am" . $amenity->getAmenityId()])) {
                $amenitiesStr .= $amenity->getAmenityId() . ",";
            }
        }

        $request = RentalListingRequest::getById($_SESSION["requestId"]);
        $request->setAmenities($amenitiesStr);
        $request->setStatus("AmenitiesSaved");
        $request->store();

        $this->redirect("OwnerSelectActivities.php");
        exit;
    }

    function show() {
        $this->amenities = AmenityInfo::getAll($this->getLanguage());
        parent::show();
    }

}

$selectPage = new OwnerSelectAmenitiesPage();
$selectPage->render();

?>
