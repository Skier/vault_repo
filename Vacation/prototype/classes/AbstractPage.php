<?

session_start();

class AbstractPage
{

    var $m_title;
    var $m_url;
    var $m_html;
    var $m_additionalMetainfo;
    var $m_variables = array();

    function __construct($title, $url, $html) {
        $this->m_title = $title;
        $this->m_url = $url;
        $this->m_html = $html;
    }

    function addVariable($name) {
        $this->m_variables[] = $name;
    }

    function setSessionVariables() {
        $_SESSION["variables"] = array();
        for ($index = 0; $index < count($this->m_variables); $index ++) {
            $_SESSION["variables"][] = eval("return \$this->" . $this->m_variables[$index] . ";");
        }
    }

    function getSessionVariables() {
        for ($index = 0; $index < count($this->m_variables); $index ++) {
            if (isset($_SESSION["variables"])) {
                eval("\$this->" . $this->m_variables[$index] . " = \$_SESSION[\"variables\"][" . $index . "];");
            } else {
                eval("\$this->" . $this->m_variables[$index] . " = \"\";");
            }
        }
    }

    function getSubmitVariables() {
        for ($index = 0; $index < count($this->m_variables); $index ++) {
            if (isset($_GET[$this->m_variables[$index]])) {
                $val = $_GET[$this->m_variables[$index]];
            } else {
                $val = null;
            }
            eval("\$this->" . $this->m_variables[$index] . " = \$val;");
        }
    }

    function preRender() {
        echo("<html>");
        echo("<head>");
        echo("<title>" . $this->m_title . "</title>");
        echo("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1251\" />");
        if (!is_null($this->m_additionalMetainfo)) {
            echo($this->m_additionalMetainfo);
        }
        echo("<link rel=\"stylesheet\" type=\"text/css\" href=\"styles/main.css\" />");
        echo("</head>");
        echo("<body>");

        $this->showHeadLine();

        if (isset($_SESSION["messages"])) {
            $this->showMessages($_SESSION["messages"]);
        }
    }

    function postRender() {
        echo("</body>");
        echo("</html>");
    }

    function showHeadLine() {
    }

    function show() {
        include_once($this->m_html);
    }

    function check() {
        return TRUE;
    }

    function isPostBack() {
        return FALSE;
    }

    function process() {
    }

    function render() {
        $this->loadLanguage();
        if ($this->isPostBack()) {
            unset($_SESSION["messages"]);
            $this->getSubmitVariables();

            if ($this->check()) {
                unset($_SESSION["variables"]);
                $this->process();
            } else {
                $this->setSessionVariables();
                $this->innerRender();
            }
        } else {
            if (!isset($_GET["language"])) {
                unset($_SESSION["variables"]);
                unset($_SESSION["messages"]);
            }
            $this->getSessionVariables();
            $this->innerRender();
        }
    }

    function innerRender() {
        $this->preRender();
        $this->show();
        $this->postRender();
    }

    function redirect($url) {
        header("Location: $url?" . session_name() . "=" . session_id());
    }

    function showMessages($messages) {
        if (isset($messages)) {
            if (0 < count($messages)) {
                echo("<div class=\"message\">");
                for ($index = 0; $index < count($messages); $index ++) {
                    echo(constant($messages[$index]) . "<br />");
                }
                echo("</div>");
            }
        }
    }

    function loadLanguage() {
        if (isset($_GET["language"])) {
            $language = $_GET["language"];
        } else if (isset($_SESSION["language"])) {
            $language = $_SESSION["language"];
        } else {
            $language = "EN";
        }
        $_SESSION["language"] = $language;

        include("include/messages/$language/variables.php");
    }

    function getLanguage() {
        return $_SESSION["language"];
    }

    function hasMessages() {
        return isset($_SESSION["messages"]);
    }

    function addMessage($message) {
        if (!$this->hasMessages()) {
            $_SESSION["messages"] = array();
        }
        $_SESSION["messages"][] = $message;
    }

}

?>
