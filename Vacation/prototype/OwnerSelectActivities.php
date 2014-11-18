<?

include_once("classes/AuthPage.php");
include_once("entities/LocationInfo.php");
include_once("entities/RentalListingRequest.php");
include_once("entities/ActivityInfo.php");

class OwnerSelectActivitiesPage extends AuthPage
{

    var $activities;

    function __construct() {
        parent::__construct("Enter rental's info", "OwnerSelectActivities.php", "OwnerSelectActivities.html");
    }

    function isPostBack() {
        return isset($_GET["submit"]);
    }

    function process() {
        $activities = ActivityInfo::getAll($this->getLanguage());
        $activitiesStr = '';
        for ($index = 0; $index < count($activities); $index ++) {
            $activity = $activities[$index];
            if (isset($_GET["ac" . $activity->getActivityId()])) {
                $activitiesStr .= $activity->getActivityId() . ",";
            }
        }

        $request = RentalListingRequest::getById($_SESSION["requestId"]);
        $request->setActivities($activitiesStr);
        $request->setStatus("ActivitiesSaved");
        $request->store();

        $this->redirect("OwnerSelectAdditional.php");
        exit;
    }

    function show() {
        $this->activities = ActivityInfo::getAll($this->getLanguage());
        parent::show();
    }

}

$selectPage = new OwnerSelectActivitiesPage();
$selectPage->render();

?>
