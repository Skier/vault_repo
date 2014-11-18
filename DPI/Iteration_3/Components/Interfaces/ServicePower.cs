using System;
 
namespace DPI.Interfaces.ServicePower
{
	public interface IAppointmentSearchResponse
	{

		string MFGID				{ get; }
		string SvcrName				{ get; }
		string ServiceCenterID		{ get; }
		string SvcrType				{ get; }
		string SvcrAddress			{ get; }
		string SvcrPhone			{ get; }
		string SvcrPostcodeLevel1	{ get; }
		string SvcrPostcodeLevel2	{ get; }
		string SvcrPostcodeLevel3	{ get; }
		string SvcrPostcode			{ get; }
		string SvcrOEMAuthFlag		{ get; }
		string SecretSauce			{ get; }
	}
}