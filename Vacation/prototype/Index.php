<?

include_once("classes/AbstractPage.php");

class IndexPage extends AbstractPage
{

    function __construct() {
        parent::__construct("Index", "Index.php", "Index.html");
        $this->m_additionalMetainfo = "<meta http-equiv=\"refresh\" content=\"2, url=" . $this->m_url . "?language=EN\">";
    }

    function isPostBack() {
        return isset($_GET["language"]);
    }

    function process() {
        $_SESSION["language"] = $_GET["language"];
        $this->redirect("Login.php");
    }

}

$indexPage = new IndexPage();
$indexPage->render();

?>
