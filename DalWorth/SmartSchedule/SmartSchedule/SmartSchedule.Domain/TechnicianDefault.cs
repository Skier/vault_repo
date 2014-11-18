using System;
  
namespace SmartSchedule.Domain
{
    public partial class TechnicianDefault
    {
        public TechnicianDefault(){ }

        #region ConvertTo

        public Technician ConvertTo(DateTime scheduleDate)
        {            
            return new Technician(
                0, m_iD, scheduleDate.Date, m_servmanId, m_name, m_hourlyRate, m_hourlyRate150to300, m_hourlyRateMore300, 
                m_displaySequence, m_companyId, m_depotAddress, m_depotLatitude, 
                m_depotLongitude, m_driveTimeMinutes, m_isContractor, m_maxVisitsCount, 
                m_maxNonExclusiveVisitsCount);
        }

        public static TechnicianDefault ConvertTo(Technician technician)
        {
            return new TechnicianDefault(
                technician.TechnicianDefaultId, technician.ServmanId, technician.Name, technician.HourlyRate, 
                technician.HourlyRate150to300, technician.HourlyRateMore300, 
                technician.DisplaySequence,
                technician.CompanyId, technician.DepotAddress, technician.DepotLatitude,
                technician.DepotLongitude, technician.DriveTimeMinutes, technician.IsContractor,
                technician.MaxVisitsCount, technician.MaxNonExclusiveVisitsCount, string.Empty);
        }

        #endregion
    }
}
      