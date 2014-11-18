<?

include_once("classes/AuthPage.php");
include_once("entities/Owner.php");

class OwnerEditPage extends AuthPage
{
    var $firstName;
    var $lastName;
    var $phoneNumber;
    var $mobilePhoneNumber;
    var $address;
    var $comments;

    function isPostBack() {
        return isset($_GET["submit"]);
    }

    function __construct() {
        parent::__construct("OwnerEditPage", "OwnerEdit.php", "OwnerEdit.html");

        $this->addVariable("firstName");
        $this->addVariable("lastName");
        $this->addVariable("phoneNumber");
        $this->addVariable("mobilePhoneNumber");
        $this->addVariable("address");
        $this->addVariable("comments");
    }


    function show() {
        if ((!$this->isPostBack()) && (!isset($_GET["language"]))) {
            $owner = new Owner();
            $owner->getById($this->getUserId());

            $this->firstName = $owner->getFirstName();
            $this->lastName = $owner->getLastName();
            $this->phoneNumber = $owner->getPhoneNumber();
            $this->mobilePhoneNumber = $owner->getMobilePhoneNumber();
            $this->address = $owner->getAddress();
            $this->comments = $owner->getComments();
        }
        parent::show();
    }

    function isEmpty($str) {
        return ("" == trim($this->firstName));
    }

    function isOverflow($str, $size) {
        return ($size < strlen($str));
    }

    function check() {
        if ($this->isEmpty($this->firstName)) {
            $this->addMessage("ERR_FIRST_NAME_REQUIRED");
        } elseif ($this->isOverflow($this->firstName, 30)) {
            $this->addMessage("ERR_FIRST_NAME_OVERFLOW");
        }

        if ($this->isEmpty($this->lastName)) {
            $this->addMessage("ERR_LAST_NAME_REQUIRED");
        } elseif ($this->isOverflow($this->lastName, 30)) {
            $this->addMessage("ERR_LAST_NAME_OVERFLOW");
        }
        
        if ("" == trim($this->phoneNumber)) {
            $this->addMessage("ERR_PHONE_NUMBER_REQUIRED");
        } elseif (!ereg("([0-9]{1})-([0-9]{3,5})-([0-9]{2,3})-([0-9]{2,3})", $this->phoneNumber)) {
            $this->addMessage("ERR_PHONE_NUMBER_INVALID");
        }
        
        if ("" == trim($this->address)) {
            $this->addMessage("ERR_ADDRESS_REQUIRED");
        } elseif ($this->isOverflow($this->address, 200)) {
            $this->addMessage("ERR_LAST_NAME_OVERFLOW");
        }

        if ($this->isOverflow($this->comments, 200)) {
            $this->addMessage("ERR_LAST_NAME_OVERFLOW");
        }
        
        return !$this->hasMessages();
    }

    function process() {
        $owner = new Owner();
        $owner->getById($this->getUserId());
        $owner->setFirstName($this->firstName);
        $owner->setLastName($this->lastName);
        $owner->setPhoneNumber($this->phoneNumber);
        $owner->setMobilePhoneNumber($this->mobilePhoneNumber);
        $owner->setAddress($this->address);
        $owner->setComments($this->comments);
        $owner->store();
        $this->redirect("Owner.php");
        exit;
    }
}

$ownerEditPage = new OwnerEditPage();
$ownerEditPage->render();

?>
