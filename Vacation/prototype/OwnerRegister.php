<?

include_once("classes/AuthPage.php");
include_once("entities/User.php");
include_once("entities/Owner.php");

class OwnerRegisterPage extends Page
{
    var $login;
    var $password;
    var $passwordAgain;
    var $email;
    var $firstName;
    var $lastName;
    var $phoneNumber;
    var $mobilePhoneNumber;
    var $address1;
    var $address2;
    var $city;
    var $stateProvince;
    var $region;
    var $country;
    var $postalCode;
    var $comments;

    /* This method is used to determine whether form is submitted. Should return TRUE or FALSE. */
    function isPostBack() {
        return isset($_GET["submit"]);
    }

    function __construct() {
        parent::__construct("OwnerRegisterPage", "OwnerRegister.php", "OwnerRegister.html");

        $this->addVariable("login");
        $this->addVariable("password");
        $this->addVariable("passwordAgain");
        $this->addVariable("email");
        $this->addVariable("firstName");
        $this->addVariable("lastName");
        $this->addVariable("phoneNumber");
        $this->addVariable("mobilePhoneNumber");
        $this->addVariable("address1");
        $this->addVariable("address2");
        $this->addVariable("city");
        $this->addVariable("stateProvince");
        $this->addVariable("region");
        $this->addVariable("country");
        $this->addVariable("postalCode");
        $this->addVariable("comments");
    }

    function check() {
        if ("" == trim($this->login)) {
            $this->addMessage("ERR_LOGIN_REQUIRED");
        } else {
            $user = new User();
            $user->getByLogin($this->login);
            if (!is_null($user->getUserId())) {
                $this->addMessage("ERR_LOGIN_ALREADY_BUSY");
            }
        }

        if ("" == trim($this->password)) {
            $this->addMessage("ERR_PASSWORD_REQUIRED");
        }

        if ("" == trim($this->passwordAgain)) {
            $this->addMessage("ERR_PASSWORD_AGAIN_REQUIRED");
        } else if (("" != trim($this->password)) && (trim($this->password) != trim($this->passwordAgain))) {
            $this->addMessage("ERR_PASSWORDS_NOT_AGREE");
        }

        if ("" == trim($this->firstName)) {
            $this->addMessage("ERR_FIRST_NAME_REQUIRED");
        }

        if ("" == trim($this->lastName)) {
            $this->addMessage("ERR_LAST_NAME_REQUIRED");
        }
        
        if ("" == trim($this->phoneNumber)) {
            $this->addMessage("ERR_PHONE_NUMBER_REQUIRED");
        }
        
        if ("" == trim($this->address1)) {
            $this->addMessage("ERR_ADDRESS1_REQUIRED");
        }
        
        if ("" == trim($this->city)) {
            $this->addMessage("ERR_CITY_REQUIRED");
        }
        
        if ("" == trim($this->stateProvince)) {
            $this->addMessage("ERR_STATE_PROVINCE_REQUIRED");
        }
        
        if ("" == trim($this->country)) {
            $this->addMessage("ERR_COUNTRY_REQUIRED");
        }
        
        if ("" == trim($this->postalCode)) {
            $this->addMessage("ERR_POSTAL_CODE_REQUIRED");
        }

        return !$this->hasMessages();
    }

    function process() {
        $user = new User();
        $user->setLogin($this->login);
        $user->setLogin($this->login);
        $user->setPassword($this->password);
        $user->setEmailAddress($this->address1);

        $user->setUserType("Owner");
        $user->setStatus("Pending");

        $user->store();
        $_SESSION["userId"] = $user->getUserId();

        $owner = new Owner();
        $owner->setUserId($user->getUserId());
        $owner->setFirstName($this->firstName);
        $owner->setLastName($this->lastName);
        $owner->setPhoneNumber($this->phoneNumber);
        $owner->setMobilePhoneNumber($this->mobilePhoneNumber);
        $owner->setAddress1($this->address1);
        $owner->setAddress2($this->address2);
        $owner->setCity($this->city);
        $owner->setStateProvince($this->stateProvince);
        $owner->setRegion($this->region);
        $owner->setCountry($this->country);
        $owner->setPostalCode($this->postalCode);
        $owner->setComments($this->comments);
        $owner->store();

        $this->redirect("OwnerSelectLocation.php");
        exit;
    }
}

$ownerRegisterPage = new OwnerRegisterPage();
$ownerRegisterPage->render();

?>
