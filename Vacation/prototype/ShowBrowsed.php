<?

include_once("classes/Page.php");
include_once("entities/LocationInfo.php");
include_once("entities/RentalListingRequest.php");

class ShowBrowsedPage extends Page
{
    var $requests;

    /* This method is used to determine whether form is submitted. Should return TRUE or FALSE. */
    function isPostBack() {
        return isset($_GET["submit"]);
    }

    function __construct() {
        parent::__construct("Show Browsed", "ShowBrowsed.php", "ShowBrowsed.html");
    }

    function check() {
        return FALSE;
    }

    function show() {
        $this->requests = RentalListingRequest::getByLocationId($_SESSION["location"]);
        parent::show();
    }
}

$showBrowsedPage = new ShowBrowsedPage();
$showBrowsedPage->render();

?>
