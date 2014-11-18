<?

class CompanyUser extends User
{

    var $m_companyUserType;

    function CompanyUser() {
    }

    function setCompanyUserType($companyUserType) {
        $this->m_companyUserType = trim($companyUserType);
    }

    function getCompanyUserType() {
        return $this->m_companyUserType;
    }

}

?>
