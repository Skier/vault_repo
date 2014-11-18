package com.llsvc.registry
{
	import com.llsvc.domain.Client;
	import com.llsvc.domain.Company;
	import com.llsvc.domain.ExpenceType;
	import com.llsvc.domain.Login;
	import com.llsvc.domain.Person;
	import com.llsvc.domain.Project;
	import com.llsvc.domain.State;
	import com.llsvc.domain.User;
	import com.llsvc.domain.vo.clientVO;
	import com.llsvc.domain.vo.companyVO;
	import com.llsvc.domain.vo.defaultexpencetypeVO;
	import com.llsvc.domain.vo.expencetypeVO;
	import com.llsvc.domain.vo.projectVO;
	import com.llsvc.domain.vo.stateVO;
	import com.llsvc.services.AddressService;
	import com.llsvc.services.ClientService;
	import com.llsvc.services.CompanyService;
	import com.llsvc.services.DefaultExpenceTypeService;
	import com.llsvc.services.ExpenceTypeService;
	import com.llsvc.services.LoginService;
	import com.llsvc.services.PersonService;
	import com.llsvc.services.ProjectService;
	import com.llsvc.services.StateService;
	import com.llsvc.services.UserService;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;

	public class MainModel extends EventDispatcher
	{
		private static var _instance:MainModel;
		
       	public static const REMOTE_STORAGE_LINK:String = "ExpenseFiles/InvoiceItemAttachments/";
        
		public function MainModel(target:IEventDispatcher=null)
		{
			super(target);

			if (_instance != null)
				throw new Error("Singleton !");
		}
		
		public static function get instance():MainModel 
		{
			if (_instance == null)
				_instance = new MainModel();
			
			return _instance;
		}
		
		[Bindable] public var currentUser:User;
		[Bindable] private var currentUserLoaded:Boolean = false;
		
		private var states:ArrayCollection;
		public function getStates():ArrayCollection { return states; }
		[Bindable] private var statesLoaded:Boolean = false;
		
		public var defaultExpenseTypes:ArrayCollection = new ArrayCollection();
		
		public function reset():void 
		{
			currentUser = null;
		}

		public function init(userId:int):void 
		{
			if (states == null)
				states = new ArrayCollection();
			states.removeAll();
			StateService.instance.getAll().addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						for each (var s:stateVO in event.result as Array) 
						{
							var state:State = new State();
							state.updateFields(s);
							states.addItem(state);
						}
						statesLoaded = true;
						loadUser(userId);
					}, faultHandler));
		}
		
		public function loadStates():void 
		{
			if (states == null)
				states = new ArrayCollection();
			states.removeAll();
			StateService.instance.getAll().addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						for each (var s:stateVO in event.result as Array) 
						{
							var state:State = new State();
							state.updateFields(s);
							states.addItem(state);
						}
						statesLoaded = true;
						dispatchEvent(new Event("statesLoaded"));
					}, faultHandler));
		}
		
		private function loadUser(userId:int):void 
		{
			if (userId == 0)
				return;
				
			UserService.instance.getUser(userId).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						currentUser = UserService.instance.getLocal(userId);
						loadLogin(currentUser);
						loadCompanies(currentUser);
						loadExpenceTypes(currentUser);
					}, faultHandler));
		}
		
		private var userPersonalLoaded:Boolean = false;
		private var userCompaniesLoaded:Boolean = false;
		private var userExpenceTypesLoaded:Boolean = false;

		private function loadExpenceTypes(user:User):void 
		{
			if (user == null)
				throw new Error("User is null !");
			
			defaultExpenseTypes.removeAll();
			DefaultExpenceTypeService.instance.getAll().addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						for each (var exp:defaultexpencetypeVO in event.result as Array) 
						{
							var expenceType:ExpenceType = DefaultExpenceTypeService.instance.getLocal(exp.defaultexpencetypeid);
							defaultExpenseTypes.addItem(expenceType);

						}
						user.expenceTypes.removeAll();
						ExpenceTypeService.instance.getByUserId(user.userid).addResponder(
							new Responder(
								function (event:ResultEvent):void 
								{
									for each (var exp:expencetypeVO in event.result as Array) 
									{
										var expenceType:ExpenceType = ExpenceTypeService.instance.getLocal(exp.expencetypeid);
										expenceType.defaultItem = DefaultExpenceTypeService.instance.getLocal(expenceType.basedon);
										user.expenceTypes.addItem(expenceType);
									}
									
									userExpenceTypesLoaded = true;
									commitLoadingUser();
								}, faultHandler));
					}, faultHandler));
		}
		
		private function loadCompanies(user:User):void 
		{
			if (user == null)
				throw new Error("User is null !");

			user.companies.removeAll();
			
			CompanyService.instance.getByUserId(user.userid).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						for each (var c:companyVO in event.result) 
						{
							var company:Company = CompanyService.instance.getLocal(c.companyid);
							company.user = user;
							user.companies.addItem(company);
						}
						loadClients(user);						
					}, faultHandler));
		}
		
		private function loadClients(user:User):void 
		{
			if (user == null)
				throw new Error("User is null !");

			user.clients.removeAll();
			
			for each (var c:Company in user.companies) 
			{
				c.clients.removeAll();
			}

			ClientService.instance.getByUserId(user.userid).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						for each (var c:clientVO in event.result) 
						{
							var client:Client = ClientService.instance.getLocal(c.clientid);
							client.person = PersonService.instance.getLocal(client.personid, true);
							user.clients.addItem(client);
							
							for each (var company:Company in user.companies) 
							{
								if (client.companyid == company.companyid) 
								{
									client.company = company;
									company.clients.addItem(client);
								}
							}
						}
						loadProjects(user);						
					}, faultHandler));
		}
		
		private function loadProjects(user:User):void 
		{
			if (user == null)
				throw new Error("User is null !");
			
			user.projects.removeAll();

			ProjectService.instance.getByUserId(user.userid).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						var clientsHash:Object = new Object();
						var client:Client;
						for each (client in user.clients)
						{
							clientsHash[client.clientid] = client;
							client.projects.removeAll();
						}
						for each (var p:projectVO in event.result as Array) 
						{
							var project:Project = ProjectService.instance.getLocal(p.projectid);
							client = clientsHash[project.clientid] as Client;
							client.projects.addItem(project);
							project.client = client;
							
							user.projects.addItem(project);
						}
						
						userCompaniesLoaded = true;
						commitLoadingUser();
					}, faultHandler));
		}
		
		private function loadLogin(user:User):void 
		{
			if (user == null)
				throw new Error("User is null !");

			LoginService.instance.getLogin(user.loginid).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						user.login = LoginService.instance.getLocal(user.loginid);
						loadPerson(user.login);
						
					}, faultHandler));
		}
		
		private function loadPerson(login:Login):void 
		{
			if (login == null)
				throw new Error("User is incomplete !");

			PersonService.instance.getPerson(login.personid).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						login.person = PersonService.instance.getLocal(login.personid);
						loadAddress(login.person);
					}, faultHandler));
		}
		
		private function loadAddress(person:Person):void 
		{
			if (person == null)
				throw new Error("User is incomplete !");

			AddressService.instance.getAddress(person.addressid).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						person.address = AddressService.instance.getLocal(person.addressid);
						person.address.state = StateService.instance.getLocal(person.address.stateid);
						userPersonalLoaded = true;
						commitLoadingUser();
					}, faultHandler));
		}
		
		private function commitLoadingUser():void 
		{
			if (userPersonalLoaded && userCompaniesLoaded && userExpenceTypesLoaded) 
			{
				currentUserLoaded = true;
				dispatchEvent(new Event("currentUserLoaded"));
			}
		}
		
		
		private function faultHandler(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
			dispatchEvent(new Event("initFailed"));
		}
	}
}