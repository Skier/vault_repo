<?

include_once("classes/AuthPage.php");
include_once("entities/User.php");
include_once("entities/Owner.php");

class OwnerPage extends AuthPage
{

    var $owner;

    function __construct() {
        parent::__construct("OwnerPage", "Owner.php", "Owner.html");
    }

    function show() {
        $this->owner = new Owner();
        $this->owner->getById($this->getUserId());
        parent::show();
    }

    function isOwnerFound() {
        return (NULL != $this->owner->getUserId());
    }

}

$ownerPage = new OwnerPage();
$ownerPage->render();

?>
