<?

include_once("classes/AuthPage.php");
include_once("entities/User.php");
//include_once("entities/CompanyUser.php");
include_once("entities/RentalListingRequest.php");

class CompanyUserPage extends AuthPage
{
    //var $companyUser;
    var $user;
    var $requests;

    function __construct() {
        parent::__construct("Company User Page", "CompanyUser.php", "CompanyUser.html");

        $this->user = new user();
        $this->user->getById($this->getUserId());

        //$this->companyUser = new CompanyUser();
        //$this->companyUser->getById($this->getUserId());
    }

    function isUserFound() {
        return /*((NULL != $this->companyUser->getUserId()) &&*/ (NULL != $this->user->getUserId())/*)*/;
    }

    function show() {
        $this->requests = RentalListingRequest::getByStatus("Pending");
        parent::show();
    }
}

$companyUserPage = new CompanyUserPage();
$companyUserPage->render();

?>
