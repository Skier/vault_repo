<?

include_once("classes/AuthPage.php");
include_once("entities/LocationInfo.php");
include_once("entities/RentalListingRequest.php");

class OwnerSelectPhotoPage extends AuthPage
{

    var $fileName;
    var $description;

    function __construct() {
        parent::__construct("Finish", "OwnerSelectPhoto.php", "OwnerSelectPhoto.html");
        $this->addVariable("fileName");
        $this->addVariable("description");
    }

    function isPostBack() {
        return isset($_GET["submit"]) || isset($_GET["finish"]);
    }

    function check() {
        if (isset($_GET["finish"])) {
            return TRUE;
        }

        if ("" == trim($this->fileName)) {
            $this->addMessage("ERR_FILE_NAME_REQUIRED");
        }

        return !$this->hasMessages();
    }

    function process() {
        if (isset($_GET["finish"])) {
            $this->redirect("Owner.php");
        } else if (isset($_GET["submit"])) {
            // TODO: store uploaded file and create record in database
            /* $uploadfile = $uploaddir . basename($_FILES[$this->fileName]['name']);

            if (move_uploaded_file($_FILES[$this->fileName]['tmp_name'], $uploadfile)) {
                print "File is valid, and was successfully uploaded. ";
                print "Here's some more debugging info:\n";
                print_r($_FILES);
            } else {
                print "Possible file upload attack!  Here's some debugging info:\n";
                print "Possible file upload attack!  Дополнительная отладочная информация:\n";
                print_r($_FILES);
            } */
            $this->redirect($this->m_url);
        }
        exit;
    }

}

$selectPage = new OwnerSelectPhotoPage();
$selectPage->render();

?>
