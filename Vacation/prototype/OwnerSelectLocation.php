<?

include_once("classes/AuthPage.php");
include_once("entities/LocationInfo.php");

class OwnerSelectLocationPage extends AuthPage
{

    var $parentId = null;
    var $locations;
    var $locationDescription;

    function __construct() {
        parent::__construct("Select location", "OwnerSelectLocation.php", "OwnerSelectLocation.html");
        $this->addVariable("parentId");
        $this->addVariable("locationDescription");
    }

    function isPostBack() {
        return isset($_GET["parentId"]) || isset($_GET["submit"]) || isset($_GET["back"]);
    }

    function check() {
        if (isset($_GET["parentId"]) && !isset($_GET["submit"]) && !isset($_GET["back"])) {
            $this->locations = LocationInfo::getByParentId($this->parentId, $this->getLanguage());
            return (0 == count($this->locations));
        } else if (isset($_GET["submit"])) {
            if ("" == trim($this->locationDescription)) {
                $this->addMessage("ERR_LOCATION_DESCRIPTION_REQUIRED");
            }
            return (!$this->hasMessages());
        } else if (isset($_GET["back"])) {
            return TRUE;
        }

        return FALSE;
    }

    function show() {
        $this->locations = LocationInfo::getByParentId($this->parentId, $this->getLanguage());
        parent::show();
    }

    function process() {
        if (isset($_GET["back"])) {
            $this->redirect("Owner.php");
            exit;
        }

        if (!is_null($this->parentId) && ("" != trim($this->parentId))) {
            $_SESSION["location"] = $this->parentId;
        }
        $_SESSION["locationDescription"] = $this->locationDescription;
        $this->redirect("OwnerSelectBaseInfo.php");
        exit;
    }

}

$selectPage = new OwnerSelectLocationPage();
$selectPage->render();

?>
