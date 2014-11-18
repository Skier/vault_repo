<?

include_once("Page.php");

class AuthPage extends Page
{

    function __construct($title, $url, $html) {
        parent::__construct($title, $url, $html);
    }

    function getUserId() {
        return $_SESSION["userId"];
    }

    function innerRender() {
        if (isset($_SESSION["userId"])) {
            parent::innerRender();
        } else {
            $this->redirect("Login.php");
            exit;
        }
    }

}

?>
