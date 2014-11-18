<?

class EventLog
{

    var $m_eventId;
    var $m_userId;
    var $m_eventType;
    var $m_eventText;
    var $m_dateCreated;

    function EventLog($eventId, $userId, $eventType, $eventText, $dateCreated) {
        $this->setEventId($eventId);
        $this->setUserId($userId);
        $this->setEventType($eventType);
        $this->setEventText($eventText);
        $this->setDateCreated($dateCreated);
    }

    function setEventId($eventId) {
        $this->m_eventId = $eventId;
    }

    function getEventId() {
        return $this->m_eventId;
    }

    function setUserId($userId) {
        $this->m_userId = $userId;
    }

    function getUserId() {
        return $this->m_userId;
    }

    function setEventType($eventType) {
        $this->m_eventType = trim($eventType);
    }

    function getEventType() {
        return $this->m_eventType;
    }

    function setEventText($eventText) {
        $this->m_eventText = trim($eventText);
    }

    function getEventText() {
        return $this->m_eventText;
    }

    function setDateCreated($dateCreated) {
        $this->m_dateCreated = $dateCreated;
    }

    function getDateCreated() {
        return $this->m_dateCreated;
    }

}

?>
