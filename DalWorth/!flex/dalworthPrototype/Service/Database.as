package Service
{
	import mx.collections.ArrayCollection;
	import Domain.*;
	import mx.states.State;
	
	public class Database
	{
		
		public var Customers:ArrayCollection = new ArrayCollection();
		public var Employees:ArrayCollection = new ArrayCollection();
		public var jobTicketTypes:ArrayCollection = new ArrayCollection();
		public var jobStatuses:ArrayCollection = new ArrayCollection();
		
		[Bindable]
		public var pendingJobTickets:ArrayCollection;
		
		public var jobTickets:ArrayCollection = new ArrayCollection();
		public var jobs:ArrayCollection = new ArrayCollection();
		public var vans:ArrayCollection = new ArrayCollection();
		public var routes:ArrayCollection  = new ArrayCollection();
		
		public var Addresses:ArrayCollection = new ArrayCollection();
		
	
		public function Database():void{
			pendingJobTickets = jobTickets;
			pendingJobTickets.filterFunction = pendingTicketsFilter;
			initVans();
			initJobStatuses();
			initAddresses();
			initEmployees();
			initCustomers();
			initJobs();
			initTickets();	
			initRoutes();
			pendingJobTickets.refresh();
		}
		
	
		private function pendingTicketsFilter(jobTicket:Object):Boolean{
			
			if (JobTicket(jobTicket).status == JobTicket.PENDING_DISPATCH_STATUS){
				return true;
			}
			return false;
		}
		
		public static var Instance:Database = new Database();
		
		public function findEmployeeByLoginAndPassword(login:String, password:String):Employee{
				for (var i:int = 0; i < Employees.length; i++){
					var emp:Employee = Employee(Employees.getItemAt(i));
					if (emp.login == login){
						if (emp.password == password){
							return emp;
						}
						else{
							return null;
						}
						
					}
				}
			return null;
				
		}
		
		public function getAllTechs(): ArrayCollection{
			var res:ArrayCollection = new ArrayCollection();
			
				for (var i:int = 0; i < Employees.length; i++){
					var emp:Employee = Employee(Employees.getItemAt(i));
					
					if (emp.type == "TECH"){
						res.addItem(emp);
					}
				}
			return res;
				
		}

		public function getRoute(tech:Technician):Route{
			for (var i:int; i < routes.length; i++){
				if (Route(routes[i]).technician.employeeId == tech.employeeId){
					return Route(routes[i]);
				}
			}
			return null;
		}		
		
		public function findCustomerByPhoneNumber(phoneNumber:String):ArrayCollection{
			
			var res:ArrayCollection = new ArrayCollection();
			 
			for (var i:int=0; i < Customers.length; i++){
				var cust:Customer = Customer(Customers.getItemAt(i));
				if (cust.phone == phoneNumber){
					res.addItem(cust);
				}
				
			}
			return res;
		}
		
		private  function generateCustomerId():int{
			var max:int = 0;
			for (var i:int=0; i < Customers.length; i++){
				var cust:Customer = Customer(Customers.getItemAt(i));
				if (cust.customerId > max) {
					max = cust.customerId;
				}
			}
			return max + 1;
		}
		
		
		
		public function createCustomer(cust:Customer):Customer{
			cust.customerId = generateCustomerId();
			Customers.addItem(cust);
			return cust;
		}
		
		private function generateJobNumber():int{
			var max:int = 0;
			for (var i:int=0; i < jobs.length; i++){
				var job:Job = Job(jobs.getItemAt(i));
				if (job.jobNumber > max) {
					max = job.jobNumber;
				}
			}
			return max + 1;
		}
		
		private function generateJobTicketNumber():int{
			var max:int = 0;
			for (var i:int=0; i < jobTickets.source.length; i++){
				var temp:JobTicket = JobTicket(jobTickets.source[i]);
				if (temp.ticketNumber > max) {
					max = temp.ticketNumber;
				}
			}
			return max + 1;
		}
		
		public function createJob(job:Job):Job{
			
			job.jobNumber = generateJobNumber();
			job.status = Job.PENDING_DISPATCH_STATUS;
			if (job.type == Job.JOB_TYPE_CLEAN_RUG){
				
				var ticket:JobTicket = new  JobTicket();
				ticket.job = job;
				
				ticket.ticketNumber = generateJobTicketNumber();
				ticket.status = JobTicket.PENDING_DISPATCH_STATUS;
				ticket.type = JobTicket.RUG_PICKUP_TICKET_TYPE;
				ticket.createDate = new Date(2007, 06, 10);
				ticket.serviceDate = new Date(2007, 06, 11);
				job.tickets.addItem(ticket);
				jobTickets.addItem(ticket);
			}
			
			jobs.addItem(job);
			return job;
		}
		public  function findJobsByCustomer(customer:Customer):ArrayCollection{
			var res:ArrayCollection = new ArrayCollection();
			
			for (var i:int=0; i < jobs.length; i++){
				var job:Job = Job(jobs[i]);
				if (job.customer.customerId == customer.customerId){
					res.addItem(job);
				}
				
			}
			
			return res;
		}
	
		
		private function initJobStatuses():void{
			var tmp:JobStatus = new JobStatus();
			tmp.status = JobStatus.PENDING_DISPATCH_STATUS;
			tmp.description = "Pending dispatch";
			jobStatuses.addItem(tmp);
			
			tmp= new JobStatus();
			tmp.status = JobStatus.INWORK;
			tmp.description = "IN WORK";
			jobStatuses.addItem(tmp);
			
			tmp = new JobStatus();
			tmp.status = JobStatus.DISPATCHED;
			tmp.description = "DISPATCHED";
			jobStatuses.addItem(tmp);
			
			
			
			
		}
		private function initEmployees():void{
			var employee:Employee = new  Dispatcher();
				employee.employeeId = 1;
				employee.firstName = "Boris";
				employee.lastName = "Furman";
				employee.type = "Dispatch";
				employee.login="boris";
				employee.password="password";
				employee.hireDate = new Date();
				employee.hireDate.setFullYear(2006, 1, 1);
				Employees.addItem(employee);
				
				
				employee = new  Technician();
				employee.employeeId = 2;
				employee.firstName = "Shane";
				employee.lastName = "Hobbs";
				employee.type = "TECH";
				employee.login="shane";
				employee.password="dalworth";
				employee.hireDate = new Date();
				employee.hireDate.setFullYear(2006, 1, 1);
				Employees.addItem(employee);
				
				employee = new Technician();
				employee.employeeId = 3;
				employee.firstName = "Nick";
				employee.lastName = "Hobbs";
				employee.type = "TECH";
				employee.login="paul";
				employee.password="knight";
				employee.hireDate = new Date();
				employee.hireDate.setFullYear(2006, 1, 1);
				Employees.addItem(employee);
				
				employee = new Technician();
				employee.employeeId = 4;
				employee.firstName = "Wade";
				employee.lastName = "Hanry";
				employee.type = "TECH";
				employee.login="david";
				employee.password="nolan";
				employee.hireDate = new Date();
				employee.hireDate.setFullYear(2006, 1, 1);
				Employees.addItem(employee);
				
				employee = new Technician();
				employee.employeeId = 5;
				employee.firstName = "James";
				employee.lastName = "Smith";
				employee.type = "TECH";
				employee.login="david";
				employee.password="nolan";
				employee.hireDate = new Date();
				employee.hireDate.setFullYear(2006, 1, 1);
				Employees.addItem(employee);
				
				employee = new Technician();
				employee.employeeId = 6;
				employee.firstName = "Rob";
				employee.lastName = "Love";
				employee.type = "TECH";
				employee.login="david";
				employee.password="nolan";
				employee.hireDate = new Date();
				employee.hireDate.setFullYear(2006, 1, 1);
				Employees.addItem(employee);
				
				employee = new Technician();
				employee.employeeId = 7;
				employee.firstName = "Dewayne";
				employee.lastName = "Brethower";
				employee.type = "TECH";
				employee.login="Dewayne";
				employee.password="Brethower";
				employee.hireDate = new Date();
				employee.hireDate.setFullYear(2006, 1, 1);
				Employees.addItem(employee);
				
				employee = new Technician();
				employee.employeeId = 8;
				employee.firstName = "Dillan";
				employee.lastName = "Blew";
				employee.type = "TECH";
				employee.login="Dillan";
				employee.password="Blew";
				employee.hireDate = new Date();
				employee.hireDate.setFullYear(2006, 1, 1);
				Employees.addItem(employee);
				
				employee = new Technician();
				employee.employeeId = 9;
				employee.firstName = "Jack";
				employee.lastName = "Ross";
				employee.type = "TECH";
				employee.login="Dillan";
				employee.password="Blew";
				employee.hireDate = new Date();
				employee.hireDate.setFullYear(2006, 1, 1);
				Employees.addItem(employee);
				
				employee = new Technician();
				employee.employeeId = 10;
				employee.firstName = "Chris";
				employee.lastName = "Gains";
				employee.type = "TECH";
				employee.login="Dillan";
				employee.password="Blew";
				employee.hireDate = new Date();
				employee.hireDate.setFullYear(2006, 1, 1);
				Employees.addItem(employee);
				
				employee = new Technician();
				employee.employeeId = 10;
				employee.firstName = "Don";
				employee.lastName = "Basta";
				employee.type = "TECH";
				employee.login="Dillan";
				employee.password="Blew";
				employee.hireDate = new Date();
				employee.hireDate.setFullYear(2006, 1, 1);
				Employees.addItem(employee);
				
				
				
				
				
				
				
				
		}
		
		private function initVans():void{
			
			for (var i:int; i < 10; i ++){
				
				var tmp:Van = new Van();
				tmp.vanId = i+1;
				tmp.licenseNumber = i.toString() + "ABC";
				this.vans.addItem(tmp);		
			}	
			
		}
		
		private function initRoutes():void{
			
			var route:Route = new Route();
			
			route.routeId = 1;
			route.dispatcher = Employees[0] as Dispatcher;
			route.technician = Employees[1] as  Technician;
			route.van = vans[0];
			route.startDate = new Date(2007, 10, 10);
			route.status = "NEW";
			route.startMessage = "Call dispatch before leaving";
			route.startMessage = "Please clean up your truck";
			
			var stop:RouteStop = new RouteStop();
			stop.routeStopId = 1;
			stop.route = route;
			stop.jobTicket = JobTicket(jobTickets[0]);
			stop.sequence = 1;
			stop.status = RouteStop.PENDING_STATUS;	
			route.routeStops.addItem(stop);
			
			routes.addItem(route);
			
			route = new Route();
			
			route.routeId = 2
			route.dispatcher = Employees[0] as Dispatcher;
			route.technician = Employees[2] as  Technician;
			route.van = vans[1];
			route.startDate = new Date(2007, 10, 10);
			route.status = "NEW";
			route.startMessage = "Call dispatch before leaving";
			route.startMessage = "Please clean up your truck";
			
			stop = new RouteStop();
			stop.routeStopId = 1;
			stop.route = route;
			stop.jobTicket = JobTicket(jobTickets[1]);
			stop.sequence = 1;
			stop.status = RouteStop.PENDING_STATUS;	
			route.routeStops.addItem(stop);
			
			stop = new RouteStop();
			stop.routeStopId = 2;
			stop.route = route;
			stop.jobTicket = JobTicket(jobTickets[2]);
			stop.sequence = 2;
			stop.status = RouteStop.PENDING_STATUS;	
			route.routeStops.addItem(stop);
			
			
			routes.addItem(route);
		}
		
		private function initCustomers():void{
			
			var customer:Customer = new Customer();
			customer.customerId = 1;
			customer.firstName="Jack";
			customer.lastName="Ross";
			customer.phone="2141231234";	
			customer.mobile="9721231234";	
			customer.billAddress = Address(Addresses.getItemAt(0));
			Customers.addItem(customer);
			
			customer = new Customer();
			customer.billAddress = Address(Addresses.getItemAt(1));
			customer.customerId = 2;
			customer.firstName="Joe";
			customer.lastName="TheCustomer";
			customer.phone="2143354143";	
			customer.mobile="9723354143";
			Customers.addItem(customer);
			
			
		}
		
		private function initAddresses():void{
			var addr:Address = new Address();
			addr.addressId = 1;
			addr.address1 = "809 Matilda";
			addr.address2 = "";
			addr.city = "Plano";
			addr.state = "TX";
			addr.zip = "75025";
			Addresses.addItem(addr);
			
			addr = new Address();
			addr.addressId = 2;
			addr.address1 = "7016 Randall Way";
			addr.address2 = "";
			addr.city = "Plano";
			addr.state = "TX";
			addr.zip = "75025";
			Addresses.addItem(addr);
		}
		
		private function initJobs():void{
			
			var job:Job = new Job();
			job.customer = Customer(Customers[1]);
			job.jobNumber = 1;
			job.serviceAddress = Address(Addresses.getItemAt(1));
			job.status = JobStatus.INWORK;
			job.type = JobTicket.RUG_PICKUP_TICKET_TYPE;
			
			jobs.addItem(job);
			
			job = new Job();
			job.customer = Customer(Customers[0]);
			job.jobNumber = 2;
			job.serviceAddress = Address(Addresses.getItemAt(0));
			job.status = JobStatus.INWORK;
			job.type = JobTicket.RUG_PICKUP_TICKET_TYPE;
			
			
			
			jobs.addItem(job);
		}
		  
		private function initTickets():void{
			
			
			var ticket:JobTicket = new  JobTicket();
			ticket.job = jobs[0];
			Job(jobs[0]).tickets.addItem(ticket);
			ticket.ticketNumber = 1;
			ticket.status = JobTicket.INWORK;
			ticket.type = JobTicket.RUG_PICKUP_TICKET_TYPE;
			ticket.createDate = new Date(2007, 06, 10);
			ticket.serviceDate = new Date(2007, 06, 11);
			
			jobTickets.addItem(ticket);
			
			ticket = new  JobTicket();
			ticket.job = jobs[1];
			Job(jobs[1]).tickets.addItem(ticket);
			ticket.ticketNumber = 2;
			ticket.status = JobTicket.INWORK;
			ticket.type = JobTicket.RUG_DELIVERY_TICKET_TYPE;
			ticket.createDate = new Date(2007, 06, 10);
			ticket.serviceDate = new Date(2007, 06, 11);
			
			jobTickets.addItem(ticket);
			
			
			ticket = new  JobTicket();
			ticket.job = jobs[1];
			Job(jobs[1]).tickets.addItem(ticket);
			ticket.ticketNumber = 3;
			ticket.status = JobTicket.INWORK;
			ticket.type = JobTicket.RUG_PICKUP_TICKET_TYPE;
			ticket.createDate = new Date(2007, 06, 10);
			ticket.serviceDate = new Date(2007, 06, 11);
			
			jobTickets.addItem(ticket);
			
			
			
			
			
		}
		
		
	}
}