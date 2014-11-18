<?

include_once("include/connect.php");

class RentalRate
{

    var $m_rentalRateId;
    var $m_rentalId;
    var $m_startDate;
    var $m_endDate;
    var $m_rentalRate;

    function RentalRate() {
    }

    function fetch($row) {
        $this->setRentalRateId($row["RENTAL_RATE_ID"]);
        $this->setRentalId($row["RENTAL_ID"]);
        $this->setStartDate($row["START_DATE"]);
        $this->setEndDate($row["END_DATE"]);
        $this->setRentalRate($row["RENTAL_RATE"]);
    }

    function getByRentalId($rentalId) {
        $sql = "select RENTAL_RATE_ID, RENTAL_ID, START_DATE, END_DATE, RENTAL_RATE"
            . "   from VRBO_RENTAL_RATE"
            . "  where RENTAL_ID = $rentalId";
        $res = execSql($sql);
        $rowsNumber = mysql_num_rows($res);

        $result = array();
        while ($row = mysql_fetch_array($res)) {
            $item = new RentalRate();
            $item->fetch($row);
            $result[] = $item;
        }
        return $result;
    }

    function store() {
        $rentalId = intpreprocess($this->getRentalId());
        $startDate = preprocess($this->getStartDate());
        $endDate = preprocess($this->getEndDate());
        $rentalRate = intpreprocess($this->getRentalRate());

        if (is_null($this->getRentalRateId())) {
            $sql = "insert into VRBO_RENTAL_RATE (RENTAL_ID, START_DATE, END_DATE, RENTAL_RATE)"
                . " values ($rentalId, $startDate, $endDate, $rentalRate)";
            execSql($sql);
            $this->setRentalRateId(mysql_insert_id());
        } else {
            $rentalRateId = intpreprocess($this->getRentalRateId());
            $sql = "update VRBO_RENTAL_RATE"
                . " set RENTAL_ID = $rentalId, "
                . " START_DATE = $startDate, "
                . " END_DATE = $endDate, "
                . " RENTAL_RATE = $rentalRate"
                . " where RENTAL_RATE_ID = $rentalRateId";
            execSql($sql);
        }
    }

    function setRentalRateId($rentalRateId) {
        $this->m_rentalRateId = $rentalRateId;
    }

    function getRentalRateId() {
        return $this->m_rentalRateId;
    }

    function setRentalId($rentalId) {
        $this->m_rentalId = $rentalId;
    }

    function getRentalId() {
        return $this->m_rentalId;
    }

    function setStartDate($startDate) {
        $this->m_startDate = $startDate;
    }

    function getStartDate() {
        return $this->m_startDate;
    }

    function setEndDate($endDate) {
        $this->m_endDate = $endDate;
    }

    function getEndDate() {
        return $this->m_endDate;
    }

    function setRentalRate($rentalRate) {
        $this->m_rentalRate = $rentalRate;
    }

    function getRentalRate() {
        return $this->m_rentalRate;
    }

}

?>
