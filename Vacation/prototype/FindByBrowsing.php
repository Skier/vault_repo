<?

include_once("classes/Page.php");
include_once("entities/LocationInfo.php");

class FindByBrowsingPage extends Page
{
    var $parentId = null;
    var $locations;

    /* This method is used to determine whether form is submitted. Should return TRUE or FALSE. */
    function isPostBack() {
        return isset($_GET["parentId"]);
    }

    function __construct() {
        parent::__construct("Find By Browsing", "FindByBrowsing.php", "FindByBrowsing.html");
        $this->addVariable("parentId");
    }

    function check() {
        if ("" == $this->parentId) {
            $this->parentId = null;
        }

        $this->locations = LocationInfo::getByParentId($this->parentId, $this->getLanguage());
        
        return (0 == count($this->locations));
    }

    function show() {
        $this->locations = LocationInfo::getByParentId($this->parentId, $this->getLanguage());

        parent::show();
    }

    function process() {
        $_SESSION["location"] = $this->parentId;
        $this->redirect("ShowBrowsed.php");
        exit;
    }
}

$findByBrowsingPage = new FindByBrowsingPage();
$findByBrowsingPage->render();

?>
