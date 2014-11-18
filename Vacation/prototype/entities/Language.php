<?

include_once("include/connect.php");

class Language
{

    var $m_language;
    var $m_languageName;

    function Language() {
    }

    static function getAll() {
        $sql = "select * from VRBO_LANGUAGE";
        $res = execSql($sql);
        $rowsNumber = mysql_num_rows($res);

        $result = array();
        $counter = 0;
        while($row = mysql_fetch_array($res)) {
            $item = new Language();
            $item->setLanguage($row["LANGUAGE"]);
            $item->setLanguageName($row["LANGUAGE_NAME"]);
            $result[$counter] = $item;
            $counter ++;
        }
        return $result;
    }

    static function getByCode($code) {
        $code = preprocess($code);
        $sql = "select * from VRBO_LANGUAGE where LANGUAGE = $code";
        $res = execSql($sql);
        $rowsNumber = mysql_num_rows($res);
        if (0 == $rowsNumber) {
            return null;
        } else if (1 < $rowsNumber) {
            die("Duplicate language found.");
        }

        $row = mysql_fetch_array($res);
        $item = new Language();
        $item->setLanguage($row["LANGUAGE"]);
        $item->setLanguageName($row["LANGUAGE_NAME"]);
        return $item;
    }

    function setLanguage($language) {
        $this->m_language = trim($language);
    }

    function getLanguage() {
        return $this->m_language;
    }

    function setLanguageName($languageName) {
        $this->m_languageName = trim($languageName);
    }

    function getLanguageName() {
        return $this->m_languageName;
    }

}

?>