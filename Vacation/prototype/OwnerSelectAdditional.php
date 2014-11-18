<?

include_once("classes/AuthPage.php");
include_once("entities/LocationInfo.php");
include_once("entities/RentalListingRequest.php");

class OwnerSelectAdditionalPage extends AuthPage
{

    var $rentalDescription;
    var $ratesDescription;

    function __construct() {
        parent::__construct("Enter rental's info", "OwnerSelectAdditional.php", "OwnerSelectAdditional.html");
        $this->addVariable("rentalDescription");
        $this->addVariable("ratesDescription");
    }

    function isPostBack() {
        return isset($_GET["submit"]);
    }

    function check() {
        if ("" == trim($this->rentalDescription)) {
            $this->addMessage("ERR_RENTAL_DESCRIPTION_REQUIRED");
        }

        if ("" == trim($this->ratesDescription)) {
            $this->addMessage("ERR_RATES_DESCRIPTION_REQUIRED");
        }

        return !$this->hasMessages();
    }

    function process() {
        $request = RentalListingRequest::getById($_SESSION["requestId"]);
        $request->setRentalDescription($this->rentalDescription);
        $request->setRatesDescription($this->ratesDescription);
        $request->setStatus("LongDescriptionSaved");
        $request->store();

        $this->redirect("OwnerSelectPhoto.php");
        exit;
    }

}

$selectPage = new OwnerSelectAdditionalPage();
$selectPage->render();

?>
