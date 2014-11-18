<?

class ActivityInfo
{

    var $m_activityInfoId;
    var $m_activityName;
    var $m_language;
    var $m_activityId;

    function ActivityInfo() {
    }

    function fetch($row) {
        $this->setActivityInfoId($row["ACTIVITY_INFO_ID"]);
        $this->setActivityName($row["ACTIVITY_NAME"]);
        $this->setLanguage($row["LANGUAGE"]);
        $this->setActivityId($row["ACTIVITY_ID"]);
    }

    static function getAll($language) {
        $language = preprocess($language);
        $sql = "select * from VRBO_ACTIVITY_INFO ai "
            . " inner join VRBO_ACTIVITY a on a.ACTIVITY_ID = ai.ACTIVITY_ID "
            . " where ai.LANGUAGE = $language "
            . " order by ai.ACTIVITY_NAME";
        $res = execSql($sql);

        $result = array();
        while ($row = mysql_fetch_array($res)) {
            $item = new ActivityInfo();
            $item->fetch($row);
            $result[] = $item;
        }
        return $result;
    }

    static function getByActivityId($id, $language) {
        $language = preprocess($language);
        $sql = "select * from VRBO_ACTIVITY_INFO ai "
            . " where ai.LANGUAGE = $language "
            . " and ai.ACTIVITY_ID = $id"
            . " order by ai.ACTIVITY_NAME";
        $res = execSql($sql);

        $rowsNumber = mysql_num_rows($res);
        if (0 == $rowsNumber) {
            return null;
        } else if (1 < $rowsNumber) {
            die("Duplicate activity info found.");
        }

        $row = mysql_fetch_array($res);
        $item = new ActivityInfo();
        $item->fetch($row);
        return $item;
    }

    function setActivityInfoId($activityInfoId) {
        $this->m_activityInfoId = $activityInfoId;
    }

    function getActivityInfoId() {
        return $this->m_activityInfoId;
    }

    function setActivityName($activityName) {
        $this->m_activityName = trim($activityName);
    }

    function getActivityName() {
        return $this->m_activityName;
    }

    function setLanguage($language) {
        $this->m_language = trim($language);
    }

    function getLanguage() {
        return $this->m_language;
    }

    function setActivityId($activityId) {
        $this->m_activityId = $activityId;
    }

    function getActivityId() {
        return $this->m_activityId;
    }

}

?>
