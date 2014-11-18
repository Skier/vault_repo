<?

include_once("include/connect.php");

class User
{

    var $m_userId;
    var $m_login;
    var $m_password;
    var $m_userType;
    var $m_dateCreated;
    var $m_status;
    var $m_emailAddress;

    function User() {
    }

    function fetch($row) {
        $this->setUserId($row["USER_ID"]);
        $this->setLogin($row["USER_LOGIN"]);
        $this->setPassword($row["USER_PASSWORD"]);
        $this->setUserType($row["USER_TYPE"]);
        $this->setDateCreated($row["DATE_CREATED"]);
        $this->setStatus($row["USER_STATUS"]);
        $this->setEmailAddress($row["EMAIL_ADDRESS"]);
    }

    function getByLogin($userLogin) {
        addslashes($userLogin);
        $sql = "select * from VRBO_USER where USER_LOGIN = '$userLogin'";
        $res = execSql($sql);
        $rowsNumber = mysql_num_rows($res);
        if (0 == $rowsNumber) {
            return;
        } else if (1 < $rowsNumber) {
            die("Duplicate login found.");
        }

        $row = mysql_fetch_array($res);
        $this->fetch($row);
    }

    function getById($userId) {
        $sql = "select * from VRBO_USER where USER_ID = $userId";
        $res = execSql($sql);
        $rowsNumber = mysql_num_rows($res);
        if (0 == $rowsNumber) {
            return;
        } else if (1 < $rowsNumber) {
            die("Duplicate id found.");
        }

        $row = mysql_fetch_array($res);
        $this->fetch($row);
    }

    function store() {
        $login = preprocess($this->getLogin());
        $password = preprocess($this->getPassword());
        $userType = preprocess($this->getUserType());
        $status = preprocess($this->getStatus());
        $emailAddress = preprocess($this->getEmailAddress());

        if (is_null($this->getUserId())) {
            $sql = "insert into VRBO_USER (USER_LOGIN, USER_PASSWORD, USER_TYPE,"
                . " DATE_CREATED, USER_STATUS, EMAIL_ADDRESS)"
                . " values ($login, $password, $userType,"
                . " now(), $status, $emailAddress)";
            execSql($sql);
            $this->setUserId(mysql_insert_id());
        } else {
            $userId = $this->getUserId();
            $sql = "update VRBO_USER"
                . " set USER_LOGIN = $login, "
                . " USER_PASSWORD = $password, "
                . " USER_TYPE = $userType, "
                . " USER_STATUS = $status, "
                . " EMAIL_ADDRESS = $emailAddress"
                . " where USER_ID = $userId";
            execSql($sql);
        }
    }

    function setUserId($userId) {
        $this->m_userId = $userId;
    }

    function getUserId() {
        return $this->m_userId;
    }

    function setLogin($login) {
        $this->m_login = trim($login);
    }

    function getLogin() {
        return $this->m_login;
    }

    function setPassword($password) {
        $this->m_password = trim($password);
    }

    function getPassword() {
        return $this->m_password;
    }

    function setUserType($userType) {
        $this->m_userType = trim($userType);
    }

    function getUserType() {
        return $this->m_userType;
    }

    function setDateCreated($dateCreated) {
        $this->m_dateCreated = $dateCreated;
    }

    function getDateCreated() {
        return $this->m_dateCreated;
    }

    function setStatus($status) {
        $this->m_status = trim($status);
    }

    function getStatus() {
        return $this->m_status;
    }

    function setEmailAddress($emailAddress) {
        $this->m_emailAddress = trim($emailAddress);
    }

    function getEmailAddress() {
        return $this->m_emailAddress;
    }

}

?>
