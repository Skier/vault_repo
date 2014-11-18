<?

class Owner
{

    var $m_userId;
    var $m_phoneNumber;
    var $m_mobilePhoneNumber;
    var $m_firstName;
    var $m_middleName;
    var $m_lastName;
    var $m_address;
    var $m_comments;

    var $m_stored = FALSE;

    function Owner() {
    }

    function fetch($row) {
        $this->setUserId($row["USER_ID"]);
        $this->setPhoneNumber($row["PHONE"]);
        $this->setMobilePhoneNumber($row["MOBILE_PHONE"]);
        $this->setFirstName($row["FIRST_NAME"]);
        $this->setMiddleName($row["MIDDLE_NAME"]);
        $this->setLastName($row["LAST_NAME"]);
        $this->setAddress($row["ADDRESS"]);
        $this->setComments($row["COMMENTS"]);
    }

    function getById($userId) {
        $sql = "select USER_ID, PHONE, MOBILE_PHONE, FIRST_NAME, MIDDLE_NAME, LAST_NAME,"
            . " ADDRESS, COMMENTS"
            . " from VRBO_OWNER where USER_ID = $userId";
        $res = execSql($sql);
        $rowsNumber = mysql_num_rows($res);
        if (0 == $rowsNumber) {
            return;
        } else if (1 < $rowsNumber) {
            die("Duplicate user found.");
        }

        $row = mysql_fetch_array($res);
        $this->fetch($row);
        $this->m_stored = TRUE;
    }

    function store() {
        $userId = intpreprocess($this->getUserId());
        $phone = preprocess($this->getPhoneNumber());
        $mobilePhone = preprocess($this->getMobilePhoneNumber());
        $firstName = preprocess($this->getFirstName());
        $middleName = preprocess($this->getMiddleName());
        $lastName = preprocess($this->getLastName());
        $address = preprocess($this->getAddress());
        $comments = preprocess($this->getComments());

        if (!$this->m_stored) {
            $sql = "insert into VRBO_OWNER (USER_ID, PHONE, MOBILE_PHONE, FIRST_NAME,"
                . " MIDDLE_NAME, LAST_NAME, ADDRESS, COMMENTS)"
                . " values ($userId, $phone, $mobilePhone, $firstName,"
                . " $middleName, $lastName, $address, $comments)";
            execSql($sql);
            $this->m_stored = TRUE;
        } else {
            $sql = "update VRBO_OWNER"
                . " set PHONE = $phone,"
                . " MOBILE_PHONE = $mobilePhone,"
                . " FIRST_NAME = $firstName,"
                . " MIDDLE_NAME = $middleName,"
                . " LAST_NAME = $lastName,"
                . " ADDRESS = $address,"
                . " COMMENTS = $comments"
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

    function setPhoneNumber($phoneNumber) {
        $this->m_phoneNumber = trim($phoneNumber);
    }

    function getPhoneNumber() {
        return $this->m_phoneNumber;
    }

    function setMobilePhoneNumber($mobilePhoneNumber) {
        $this->m_mobilePhoneNumber = trim($mobilePhoneNumber);
    }

    function getMobilePhoneNumber() {
        return $this->m_mobilePhoneNumber;
    }

    function setFirstName($firstName) {
        $this->m_firstName = trim($firstName);
    }

    function getFirstName() {
        return $this->m_firstName;
    }

    function setMiddleName($middleName) {
        $this->m_middleName = trim($middleName);
    }

    function getMiddleName() {
        return $this->m_middleName;
    }

    function setLastName($lastName) {
        $this->m_lastName = trim($lastName);
    }

    function getLastName() {
        return $this->m_lastName;
    }

    function setAddress($address) {
        $this->m_address = trim($address);
    }

    function getAddress() {
        return $this->m_address;
    }

    function setComments($comments) {
        $this->m_comments = trim($comments);
    }

    function getComments() {
        return $this->m_comments;
    }

}

?>
