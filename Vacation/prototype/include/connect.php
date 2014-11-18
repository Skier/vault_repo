<?

include_once("config/config.php");

function execSql($sql) {
    $db_server = DB_SERVER;
    $db_user = DB_USER;
    $db_passwd = DB_PASSWD;
    $link = mysql_connect($db_server, $db_user, $db_passwd );
    $db = mysql_select_db(DATABASE_NAME) or die(mysql_error());
    mysql_query("set names cp1251");
    $res = mysql_query($sql) or die(mysql_error());
    return $res;
}

function preprocess($line) {
    $line = trim($line);
    if (is_null($line) || ("" == $line)) {
        return "null";
    } else {
        return "'" . $line . "'";
    }
}

function intpreprocess($line) {
    $line = trim($line);
    if (is_null($line) || ("" == $line)) {
        return "null";
    } else {
        return $line;
    }
}

function boolpreprocess($line) {
    $line = trim($line);
    if (is_null($line) || !$line) {
        return "0";
    } else {
        return "1";
    }
}

function isInteger($value) {
    return ereg("[0-9]*", $value);
}

?>
