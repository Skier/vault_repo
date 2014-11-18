package Service
{
	
    import mx.rpc.remoting.RemoteObject;
    import mx.controls.Alert;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    
    import Domain.*;
    import mx.collections.ArrayCollection;      
    

    public class ServerSyncService
    {
      private var remoteObject:RemoteObject;

      private var _isDashboardDirtyOK:Function;
      private var _isDashboardDirtyFailed:Function;
      
      private var _findDispatcherByNameOK:Function;
      private var _findDispatcherByNameFailed:Function;
      
      private var _findCustomersByTemplateOK:Function;
      private var _findCustomersByTemplateFailed:Function;
      
      private var _findJobsAndTicketsByCustomerOK:Function;
      private var _findJobsAndTicketsByCustomerFailed:Function;
      
      private var _findAllTechniciansAndWorkOK:Function;
      private var _findAllTechniciansAndWorkFailed:Function;
      
      private var _getUnassignedTechniciansOk:Function;
      private var _getUnassignedTechniciansFailed:Function;

      private var _getUnassignedVansOk:Function;
      private var _getUnassignedVansFailed:Function;

      private var _getPendingTicketsOk:Function;
      private var _getPendingTicketsFailed:Function;

      private var _getEquipmentEstimateOk:Function;
      private var _getEquipmentEstimateFailed:Function;

      private var _createWorkOk:Function;
      private var _createWorkFailed:Function;

      private var _assignTicketExecutionOk:Function;
      private var _assignTicketExecutionFailed:Function;
      
      
      public function ServerSyncService()
      {
        remoteObject  = new RemoteObject("GenericDestination");
        remoteObject.source = "Dalworth.WebService.Web.ServerSyncService";
        
        remoteObject.FindDispatcherByName.addEventListener("result",FindDispatcherByNameHandler);
        remoteObject.FindCustomersByTemplate.addEventListener("result",FindCustomersByTemplateHandler);
        remoteObject.FindJobsAndTicketsByCustomer.addEventListener("result",FindJobsAndTicketsByCustomerHandler);
        remoteObject.FindAllTechniciansAndWork.addEventListener("result",FindAllTechniciansAndWorkHandler);
        remoteObject.IsDashboardDirty.addEventListener("result",IsDashboardDirtyHandler);
        
        
        
        
        // Not workong yet.
        remoteObject.GetEmployees.addEventListener("result",GetEmployeesHandler);
        
        remoteObject.IsTodayWorkExist.addEventListener("result",IsTodayWorkExistHandler);
        
        remoteObject.GetStartDayPackage.addEventListener("result",GetStartDayPackageHandler);
        
        remoteObject.SaveStartDayDone.addEventListener("result",SaveStartDayDoneHandler);
        
        remoteObject.GetUnassignedTechnicians.addEventListener("result",GetUnassignedTechniciansHandler);
        
        remoteObject.GetUnassignedVans.addEventListener("result",GetUnassignedVansHandler);
        
        remoteObject.GetPendingTickets.addEventListener("result",GetPendingTicketsHandler);
        
        remoteObject.GetTicket.addEventListener("result",GetTicketHandler);
        
        remoteObject.GetEquipmentEstimate.addEventListener("result",GetEquipmentEstimateHandler);
        
        remoteObject.UpdateWorkStatus.addEventListener("result",UpdateWorkStatusHandler);
        
        remoteObject.GetWorks.addEventListener("result",GetWorksHandler);
        
        remoteObject.CreateWork.addEventListener("result",CreateWorkHandler);
        
        remoteObject.AssignTicketExecution.addEventListener("result",AssignTicketExecutionHandler);
        
        remoteObject.addEventListener("fault", onFault);
      }

    
      public function FindDispatcherByName(name:String, successHandler:Function, failHandler:Function):void
      {
      	this._findDispatcherByNameOK = successHandler;
      	this._findDispatcherByNameFailed = failHandler;
      	
        remoteObject.FindDispatcherByName(name);
      }
      
      private virtual function FindDispatcherByNameHandler(event:ResultEvent):void
      {
        
          var employee:Employee = event.result as Employee;
         this._findDispatcherByNameOK(employee);
         _findDispatcherByNameOK = null;
      }
      
      public function IsDashboardDirty(dispatchId:int, successHandler:Function, failHandler:Function):void
      {
      	this._isDashboardDirtyOK = successHandler;
      	this._isDashboardDirtyFailed = failHandler;
      	
        remoteObject.IsDashboardDirty(dispatchId);
      }
      
      private virtual function IsDashboardDirtyHandler(event:ResultEvent):void
      {
        
          var isDirty:Boolean = event.result as Boolean;
         this._isDashboardDirtyOK(isDirty);
         _isDashboardDirtyOK = null;
      }

      
      public function FindCustomersByTemplate(customer:Customer, successHandler:Function, failHandler:Function):void
      {
      	this._findCustomersByTemplateOK = successHandler;
      	this._findCustomersByTemplateFailed = failHandler;
      	
        remoteObject.FindCustomersByTemplate(customer);
      }
      
      private virtual function FindCustomersByTemplateHandler(event:ResultEvent):void{
      		
      	  var returnValue:Array = event.result as Array;
      	  this._findCustomersByTemplateOK(new ArrayCollection(returnValue));
      	  this._findCustomersByTemplateOK = null;
      	
      }
      
      public function FindJobsAndTicketsByCustomer(customerId:int, successHandler:Function, failHandler:Function):void
      {
      	this._findJobsAndTicketsByCustomerOK = successHandler;
      	this._findJobsAndTicketsByCustomerFailed = failHandler;	
        remoteObject.FindJobsAndTicketsByCustomer(customerId);
      }
      
      private virtual function FindJobsAndTicketsByCustomerHandler(event:ResultEvent):void{
      		
      	  var returnValue:Array = event.result as Array;
      	  this._findJobsAndTicketsByCustomerOK(new ArrayCollection(returnValue));
      	  this._findJobsAndTicketsByCustomerOK = null;
      	
      }
      
      public function FindAllTechniciansAndWork(startDate:Date, successHandler:Function, failHandler:Function):void{
      	this._findAllTechniciansAndWorkOK = successHandler;
      	this._findAllTechniciansAndWorkFailed = failHandler;
      	remoteObject.FindAllTechniciansAndWork(startDate);
      	 
      }
      
      private  function FindAllTechniciansAndWorkHandler(event:ResultEvent):void{
      	  var returnValue:Array = event.result as Array;
      	  this._findAllTechniciansAndWorkOK(new ArrayCollection(returnValue));
      	  this._findAllTechniciansAndWorkOK = null;
      }
      
      public function GetEmployees():void
      {
        remoteObject.GetEmployees();
      }
    
      public function IsTodayWorkExist(technicianEmployeeId:Number):void
      {
        remoteObject.IsTodayWorkExist(technicianEmployeeId);
      }
    
      public function GetStartDayPackage(technicianEmployeeId:Number):void
      {
        remoteObject.GetStartDayPackage(technicianEmployeeId);
      }
    
      public function SaveStartDayDone(par:StartDayDonePackage):void
      {
        remoteObject.SaveStartDayDone(par);
      }
    
      public function GetUnassignedTechnicians(workDate:Date, successHandler:Function, failHandler:Function):void
      {
      	_getUnassignedTechniciansOk = successHandler;
      	_getUnassignedTechniciansFailed = failHandler;
      	
      	remoteObject.GetUnassignedTechnicians(workDate);
      }

    
      public function GetUnassignedVans(workDate:Date, successHandler:Function, failHandler:Function):void
      {
      	_getUnassignedVansOk = successHandler;
      	_getUnassignedVansFailed = failHandler;
      	
        remoteObject.GetUnassignedVans(workDate);
      }
    
      public function GetPendingTickets(successHandler:Function, failHandler:Function):void
      {
      	_getPendingTicketsOk = successHandler;
      	_getPendingTicketsFailed = failHandler;      	
        remoteObject.GetPendingTickets();
      }
    
      public function GetTicket(ticketId:Number):void
      {
        remoteObject.GetTicket(ticketId);
      }
    
      public function GetEquipmentEstimate(tickets:Array, successHandler:Function, failHandler:Function):void
      {
      	_getEquipmentEstimateOk = successHandler;
      	_getEquipmentEstimateFailed = failHandler;
        remoteObject.GetEquipmentEstimate(tickets);
      }
    
      public function UpdateWorkStatus(workId:Number,statusId:Number):void
      {
        remoteObject.UpdateWorkStatus(workId,statusId);
      }
    
      public function GetWorks(date:Date):void
      {
        remoteObject.GetWorks(date);
      }
    
      public function CreateWork(workPackage:WorkPackage, successHandler:Function, failHandler:Function):void
      {
      	_createWorkOk = successHandler;
      	_createWorkFailed = failHandler;      	
        remoteObject.CreateWork(workPackage);
      }
         
      public function AssignTicketExecution(dispatchId:int, technicianId:int, ticketId:int, successHandler:Function, failHandler:Function):void
      {
      	_assignTicketExecutionOk = successHandler;
      	_assignTicketExecutionFailed = failHandler;      	
        remoteObject.AssignTicketExecution(dispatchId, technicianId, ticketId);
      }
    
         
      public virtual function GetEmployeesHandler(event:ResultEvent):void
      {
        
          var returnValue:Array = event.result as Array;
          Alert.show( "Return value - " + returnValue, "Success" );
        
      }
         
      public virtual function IsTodayWorkExistHandler(event:ResultEvent):void
      {
        
          var returnValue:Boolean = event.result as Boolean;
          Alert.show( "Return value - " + returnValue, "Success" );
        
      }
         
      public virtual function GetStartDayPackageHandler(event:ResultEvent):void
      {
        
          var returnValue:StartDayPackage = event.result as StartDayPackage;
          Alert.show( "Return value - " + returnValue, "Success" );
        
      }
         
      public virtual function SaveStartDayDoneHandler(event:ResultEvent):void
      {
        
      }
         
      public virtual function GetUnassignedTechniciansHandler(event:ResultEvent):void
      {
      	  var returnValue:Array = event.result as Array;
      	  this._getUnassignedTechniciansOk(new ArrayCollection(returnValue));
      	  this._getUnassignedTechniciansOk = null;                
      }
         
      public virtual function GetUnassignedVansHandler(event:ResultEvent):void
      {
      	  var returnValue:Array = event.result as Array;
      	  this._getUnassignedVansOk(new ArrayCollection(returnValue));
      	  this._getUnassignedVansOk = null;                
      }
         
      public virtual function GetPendingTicketsHandler(event:ResultEvent):void
      {
      	  var returnValue:Array = event.result as Array;
      	  this._getPendingTicketsOk(new ArrayCollection(returnValue));
      	  this._getPendingTicketsOk = null;                
      }
         
      public virtual function GetTicketHandler(event:ResultEvent):void
      {
        
          var returnValue:TicketPackage = event.result as TicketPackage;
          Alert.show( "Return value - " + returnValue, "Success" );
        
      }
         
      public virtual function GetEquipmentEstimateHandler(event:ResultEvent):void
      {
      	  var returnValue:Array = event.result as Array;
      	  this._getEquipmentEstimateOk(new ArrayCollection(returnValue));
      	  this._getEquipmentEstimateOk = null;                                
      }
         
      public virtual function UpdateWorkStatusHandler(event:ResultEvent):void
      {
        
      }
         
      public virtual function GetWorksHandler(event:ResultEvent):void
      {
        
          var returnValue:Array = event.result as Array;
          Alert.show( "Return value - " + returnValue, "Success" );
        
      }
         
      public virtual function CreateWorkHandler(event:ResultEvent):void
      {
      	  this._createWorkOk(event);
      	  this._createWorkOk = null;                                        
      }

      public virtual function AssignTicketExecutionHandler(event:ResultEvent):void
      {
      	  this._assignTicketExecutionOk(event);
      	  this._assignTicketExecutionOk = null;                                        
      	
      }

    
    // TODO: remove this and add separete handlers
      public function onFault (event:FaultEvent):void
      {
      	if (this._findDispatcherByNameFailed != null){
      		this._findDispatcherByNameFailed(event.fault.faultString);
      		this._findDispatcherByNameFailed = null;
       	}
       	
       	if (this._findCustomersByTemplateFailed != null){
       		this._findCustomersByTemplateFailed(event.fault.faultString);
      		this._findCustomersByTemplateFailed = null;
       	}
       	
   		if (this._findJobsAndTicketsByCustomerFailed != null){
       		this._findJobsAndTicketsByCustomerFailed(event.fault.faultString);
      		this._findJobsAndTicketsByCustomerFailed = null;
       	}
       	
       	if (this._findAllTechniciansAndWorkFailed != null){
       		this._findAllTechniciansAndWorkFailed(event.fault.faultString);
      		this._findAllTechniciansAndWorkFailed = null;
       	}
       	
       	if (this._getUnassignedTechniciansFailed != null){
       		this._getUnassignedTechniciansFailed(event.fault.faultString);
      		this._getUnassignedTechniciansFailed = null;
       	}

       	if (this._getUnassignedVansFailed != null){
       		this._getUnassignedVansFailed(event.fault.faultString);
      		this._getUnassignedVansFailed = null;
       	}
       	
       	if (this._getUnassignedTechniciansFailed != null){
       		this._getUnassignedTechniciansFailed(event.fault.faultString);
      		this._getUnassignedTechniciansFailed = null;
       	}
       	
       	if (this._getPendingTicketsFailed != null){
       		this._getPendingTicketsFailed(event.fault.faultString);
      		this._getPendingTicketsFailed = null;
       	}
       	
       	if (this._getEquipmentEstimateFailed != null){
       		this._getEquipmentEstimateFailed(event.fault.faultString);
      		this._getEquipmentEstimateFailed = null;
       	}
       	
       	if (this._createWorkFailed != null){
       		this._createWorkFailed(event.fault.faultString);
      		this._createWorkFailed = null;
       	}

       	if (this._assignTicketExecutionFailed != null){
       		this._assignTicketExecutionFailed(event.fault.faultString);
      		this._assignTicketExecutionFailed = null;
       	}

       	if (this._isDashboardDirtyFailed != null){
       		this._isDashboardDirtyFailed(event.fault.faultString);
      		this._isDashboardDirtyFailed = null;
       	}
       	
      }


    }
  
}