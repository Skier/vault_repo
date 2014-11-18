<?

include_once("classes/Page.php");
include_once("entities/User.php");

class LoginPage extends Page
{

    var $login;
    var $password;

    var $userPagePath;

    function __construct() {
        parent::__construct("Login", "Login.php", "Login.html");
        $this->addVariable("login");
        $this->addVariable("password");
    }

    /* This method is used to determine whether form is submitted. Should return TRUE or FALSE. */
    function isPostBack() {
        return isset($_GET["submit"]);
    }

    /* This method is used to validate entered data. Should return TRUE or FALSE. */
    function check() {
        $user = new User();
        $user->getByLogin($this->login);

        if (is_null($user->getUserId())) {
            $this->addMessage("ERR_INVALID_LOGIN");
        }
        if ($user->getPassword() != $this->password) {
            $this->addMessage("ERR_INVALID_PASSWORD");
        }
        if ($user->getStatus() == "Inactive") {
            $this->addMessage("ERR_USER_INACTIVE");
        }
        if (!$this->hasMessages()) {
            $this->checkUserType($user->getUserType());
            $_SESSION["userId"] = $user->getUserId();
            return TRUE;
        }

        return FALSE;
    }

    function checkUserType($userType) {
        switch ($userType) {
            case "CompanyUser":
                    $this->userPagePath = "CompanyUser.php";
                break;
            case "Owner":
                    $this->userPagePath = "Owner.php";
                break;
            default:
                // Error
                exit(2);
        }
    }

    /* This method is called when form is valid. It can do some actions like
        working with database, redirect to another page etc. */
    function process() {
        if (NULL != $this->userPagePath) {
            $this->redirect($this->userPagePath);
            exit;
        } else {
            // Error
            exit(1);
        }
    }

}

/* There defined class should be instantiated and rendered. */
$loginPage = new LoginPage();
$loginPage->render();

?>
