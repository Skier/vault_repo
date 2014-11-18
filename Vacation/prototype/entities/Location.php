<?

class Location
{

    var $m_locationId;
    var $m_parentId;

    function Location() {
    }

    function fetch($row) {
        $this->setLocationId($row["LOCATION_ID"]);
        $this->setParentId($row["PARENT_LOCATION_ID"]);
    }

    function setLocationId($locationId) {
        $this->m_locationId = $locationId;
    }

    function getLocationId() {
        return $this->m_locationId;
    }

    function setParentId($parentId) {
        $this->m_parentId = $parentId;
    }

    function getParentId() {
        return $this->m_parentId;
    }

}

?>
