<?

include_once("AbstractPage.php");
include_once("entities/Language.php");

class Page extends AbstractPage
{

    function __construct($title, $url, $html) {
        parent::__construct($title, $url, $html);
    }

    function showHeadLine() {
        echo("<div class=\"languages\">");
        $languages = Language::getAll();
        for ($index = 0; $index < count($languages); $index ++) {
            $language = $languages[$index];

            echo("<a href=\"" . $this->m_url . "?language=".
                    $language->getLanguage() . "\">" . $language->getLanguageName() . "</a>");

            if ($index + 1 < count($languages)) {
                echo(" :: ");
            }
        }
        echo("</div>");
        echo("<hr />");
    }

}

?>
