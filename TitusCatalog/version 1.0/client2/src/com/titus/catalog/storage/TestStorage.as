package com.titus.catalog.storage
{
	import com.titus.catalog.model.Category;
	import com.titus.catalog.model.ModelItem;
	import com.titus.catalog.model.CatalogItemPackage;
	import com.titus.catalog.model.PromoVideo;
	import com.titus.catalog.model.Submittal;
	import com.titus.catalog.model.search.SearchResultItem;
	import com.titus.catalog.model.search.SearchResultPackage;
	import com.titus.catalog.model.search.SearchResultPage;
	import com.titus.catalog.model.search.SearchResultSection;
	
public class TestStorage implements IStorage
{
	private static var instance:TestStorage;
	
	public function TestStorage()
	{
		if (instance)
			throw new Error("Test Storage is singleton!");
	}

	public static function getInstance():IStorage
	{
		if (!instance)
			instance = new TestStorage();

		return instance;
	}
	
	public function getRootCategory():Category 
	{
		var model111:ModelItem = new ModelItem();
			model111.name = "AeroBlade Filter Return Grilles";
		var model112:ModelItem = new ModelItem();
			model112.name = "AeroBlade Options &amp; Accessories";
		var model113:ModelItem = new ModelItem();
			model113.name = "AeroBlade Return Grilles";
		var model114:ModelItem = new ModelItem();
			model114.name = "AeroBlade Supply Grilles";
		
		var cat11:Category = new Category();
			cat11.name = "AeroBlade Grilles";
			cat11.children.addItem(model111);
			cat11.children.addItem(model112);
			cat11.children.addItem(model113);
			cat11.children.addItem(model114);
			

		var model121:ModelItem = new ModelItem();
			model121.name = "300/350 Filter Return Grilles";
		var model122:ModelItem = new ModelItem();
			model122.name = "00/350 Options &amp; Accessories";
		var model123:ModelItem = new ModelItem();
			model123.name = "300/350 Return Grilles";
		var model124:ModelItem = new ModelItem();
			model124.name = "300/350 Supply Grilles";
		
		var cat12:Category = new Category();
			cat12.name = "300/350 Grilles";
			cat12.children.addItem(model121);
			cat12.children.addItem(model122);
			cat12.children.addItem(model123);
			cat12.children.addItem(model124);


		var model131:ModelItem = new ModelItem();
			model131.name = "Door Return Grilles";
		var model132:ModelItem = new ModelItem();
			model132.name = "Drum Grilles";
		var model133:ModelItem = new ModelItem();
			model133.name = "Eggcrate Grilles";
		var model134:ModelItem = new ModelItem();
			model134.name = "Heavy Duty Grilles";
		var model135:ModelItem = new ModelItem();
			model135.name = "Perforated Grilles";
		var model136:ModelItem = new ModelItem();
			model136.name = "Pole Operated Supply Grilles";
		var model137:ModelItem = new ModelItem();
			model137.name = "Reversible Core Grilles";
		var model138:ModelItem = new ModelItem();
			model138.name = "Spiral Grilles";

		var cat13:Category = new Category();
			cat13.name = "Specialized Grilles";
			cat13.children.addItem(model131);
			cat13.children.addItem(model132);
			cat13.children.addItem(model133);
			cat13.children.addItem(model134);
			cat13.children.addItem(model135);
			cat13.children.addItem(model136);
			cat13.children.addItem(model137);
			cat13.children.addItem(model138);
		
		var cat1:Category = new Category();
			cat1.name = "Grilles";
			cat1.children.addItem(cat11);
			cat1.children.addItem(cat12);
			cat1.children.addItem(cat13);
		

		var model211:ModelItem = new ModelItem();
			model211.name = "Flow Tee"; 
		var model212:ModelItem = new ModelItem();
			model212.name = "Flowbar"; 
		var model213:ModelItem = new ModelItem();
			model213.name = "Flowbar Accessories"; 
		var model214:ModelItem = new ModelItem();
			model214.name = "Moduflow"; 

		var cat21:Category = new Category();
			cat21.name = "Architectural Linear Diffusers";
			cat21.children.addItem(model211);
			cat21.children.addItem(model212);
			cat21.children.addItem(model213);
			cat21.children.addItem(model214);

		var model221:ModelItem = new ModelItem();
			model221.name = "DAT"; 
		var model222:ModelItem = new ModelItem();
			model222.name = "ModuBloc"; 
		var model223:ModelItem = new ModelItem();
			model223.name = "OMNI"; 
		var model224:ModelItem = new ModelItem();
			model224.name = "OMNI-RS"; 
		var model225:ModelItem = new ModelItem();
			model225.name = "R-OMNI"; 
		var model226:ModelItem = new ModelItem();
			model226.name = "TSW"; 

		var cat22:Category = new Category();
			cat22.name = "Architectural Ceiling Diffusers";
			cat22.children.addItem(model221);
			cat22.children.addItem(model222);
			cat22.children.addItem(model223);
			cat22.children.addItem(model224);
			cat22.children.addItem(model225);
			cat22.children.addItem(model226);

		var model231:ModelItem = new ModelItem();
			model231.name = "Accessories"; 
		var model232:ModelItem = new ModelItem();
			model232.name = "Laminar Pattern"; 
		var model233:ModelItem = new ModelItem();
			model233.name = "Linear Air Curtain"; 
		var model234:ModelItem = new ModelItem();
			model234.name = "Radial Pattern"; 

		var cat23:Category = new Category();
			cat23.name = "Critical Environment Diffusers";
			cat23.children.addItem(model231);
			cat23.children.addItem(model232);
			cat23.children.addItem(model233);
			cat23.children.addItem(model234);

		var model241:ModelItem = new ModelItem();
			model241.name = "ML-Supply"; 
		var model242:ModelItem = new ModelItem();
			model242.name = "ML-NT Supply, Narrow Tee"; 
		var model243:ModelItem = new ModelItem();
			model243.name = "MLR-Return"; 
		var model244:ModelItem = new ModelItem();
			model244.name = "MLR-NT-Return, Narrow Tee"; 
		var model245:ModelItem = new ModelItem();
			model245.name = "MP-Plenum"; 

		var cat24:Category = new Category();
			cat24.name = "Linear Slot Ceiling Diffusers";
			cat24.children.addItem(model241);
			cat24.children.addItem(model242);
			cat24.children.addItem(model243);
			cat24.children.addItem(model244);
			cat24.children.addItem(model245);

		var model251:ModelItem = new ModelItem();
			model251.name = "Air Balancing Devices"; 
		var model252:ModelItem = new ModelItem();
			model252.name = "Mounting Frames"; 
		var model253:ModelItem = new ModelItem();
			model253.name = "Sectorizing Buffle"; 

		var cat25:Category = new Category();
			cat25.name = "Grille & Diffuser Accessories";
			cat25.children.addItem(model251);
			cat25.children.addItem(model252);
			cat25.children.addItem(model253);

		var cat2:Category = new Category();
			cat2.name = "Diffusers";
			cat2.children.addItem(cat21);
			cat2.children.addItem(cat22);
			cat2.children.addItem(cat23);
			cat2.children.addItem(cat24);
			cat2.children.addItem(cat25);


		var model311:ModelItem = new ModelItem();
			model311.name = "Dual Duct Terminals"; 
		var model312:ModelItem = new ModelItem();
			model312.name = "Single Duct Terminals"; 

		var cat31:Category = new Category();
			cat31.name = "Single / Dual Duct Terminals";
			cat31.children.addItem(model311);
			cat31.children.addItem(model312);

		var model321:ModelItem = new ModelItem();
			model321.name = "Low Profile Parallel"; 
		var model322:ModelItem = new ModelItem();
			model322.name = "Low Profile Series"; 
		var model323:ModelItem = new ModelItem();
			model323.name = "Parallel Fan Powered"; 
		var model324:ModelItem = new ModelItem();
			model324.name = "Series Fan Powered"; 

		var cat32:Category = new Category();
			cat32.name = "Fan Powered Terminals";
			cat32.children.addItem(model321);
			cat32.children.addItem(model322);
			cat32.children.addItem(model323);
			cat32.children.addItem(model324);

		var model331:ModelItem = new ModelItem();
			model331.name = "T3 VAV Diffusers"; 
		var model332:ModelItem = new ModelItem();
			model332.name = "T3SQ Bypass Terminals"; 
		var model333:ModelItem = new ModelItem();
			model333.name = "Zcom Diffusers (discontinued)"; 

		var cat33:Category = new Category();
			cat33.name = "VAV Diffusers";
			cat33.children.addItem(model331);
			cat33.children.addItem(model332);
			cat33.children.addItem(model333);

		var model341:ModelItem = new ModelItem();
			model341.name = "LHK"; 
		var model342:ModelItem = new ModelItem();
			model342.name = "Linear Access Floor Products"; 
		var model343:ModelItem = new ModelItem();
			model343.name = "PFC"; 
		var model344:ModelItem = new ModelItem();
			model344.name = "Round Access Floor Products"; 
		var model345:ModelItem = new ModelItem();
			model345.name = "TAF-L Perimeter System"; 

		var cat34:Category = new Category();
			cat34.name = "Access Floor Products";
			cat34.children.addItem(model341);
			cat34.children.addItem(model342);
			cat34.children.addItem(model343);
			cat34.children.addItem(model344);
			cat34.children.addItem(model345);

		var model351:ModelItem = new ModelItem();
			model351.name = "Balancing Terminals"; 
		var model352:ModelItem = new ModelItem();
			model352.name = "Bypass Terminals"; 
		var model353:ModelItem = new ModelItem();
			model353.name = "External Round Duct Terminals"; 
		var model354:ModelItem = new ModelItem();
			model354.name = "Induction Terminals"; 
		var model355:ModelItem = new ModelItem();
			model355.name = "Internal & Special Purpose"; 
		var model356:ModelItem = new ModelItem();
			model356.name = "Slide-In Duct Terminals"; 

		var cat35:Category = new Category();
			cat35.name = "VAV Retrofit Terminals";
			cat35.children.addItem(model351);
			cat35.children.addItem(model352);
			cat35.children.addItem(model353);
			cat35.children.addItem(model354);
			cat35.children.addItem(model355);
			cat35.children.addItem(model356);

		var model361:ModelItem = new ModelItem();
			model361.name = "Analog"; 
		var model362:ModelItem = new ModelItem();
			model362.name = "Digital"; 
		var model363:ModelItem = new ModelItem();
			model363.name = "Electric Heating Coils"; 
		var model364:ModelItem = new ModelItem();
			model364.name = "Linear"; 
		var model365:ModelItem = new ModelItem();
			model365.name = "Pneumatic"; 
		var model366:ModelItem = new ModelItem();
			model366.name = "Valve Packages"; 

		var cat36:Category = new Category();
			cat36.name = "TU Accessories & Options";
			cat36.children.addItem(model361);
			cat36.children.addItem(model362);
			cat36.children.addItem(model363);
			cat36.children.addItem(model364);
			cat36.children.addItem(model365);
			cat36.children.addItem(model366);


		var cat3:Category = new Category();
			cat3.name = "Terminal Units";
			cat3.children.addItem(cat31);
			cat3.children.addItem(cat32);
			cat3.children.addItem(cat33);
			cat3.children.addItem(cat34);
			cat3.children.addItem(cat35);
			cat3.children.addItem(cat36);

		
		var model411:ModelItem = new ModelItem();
			model411.name = "Grilles & Diffusers"; 
		var model412:ModelItem = new ModelItem();
			model412.name = "Terminal Units"; 
		var model413:ModelItem = new ModelItem();
			model413.name = "Acoustics"; 
		var model414:ModelItem = new ModelItem();
			model414.name = "Fan Coils";
		 
		var cat41:Category = new Category();
			cat41.name = "Engineering guidelines";
			cat41.children.addItem(model411);
			cat41.children.addItem(model412);
			cat41.children.addItem(model413);
			cat41.children.addItem(model414);

		var root:Category = new Category();
			root.name = "All Categories";
			root.children.addItem(cat41);
			root.children.addItem(cat1);
			root.children.addItem(cat2);
			root.children.addItem(cat3);

		return root;
	}
	
	public function getItemPackage(modelItem:ModelItem):CatalogItemPackage 
	{
		var result:CatalogItemPackage = new CatalogItemPackage();

		var item:ModelItem = new ModelItem();
			item.name = "Sample Product Name";
		
			result.modelItem = item;

			result.catalogPage = 4;

		var sub1:Submittal = new Submittal();
			sub1.name = "Submittall - 1";
		var sub2:Submittal = new Submittal();
			sub2.name = "Submittall - 2";
		var sub3:Submittal = new Submittal();
			sub3.name = "Submittall - 3";
		var sub4:Submittal = new Submittal();
			sub4.name = "Submittall - 4";
		var sub5:Submittal = new Submittal();
			sub5.name = "Submittall - 5";
		var sub6:Submittal = new Submittal();
			sub6.name = "Submittall - 6";
		var sub7:Submittal = new Submittal();
			sub7.name = "Submittall - 7";
		
			result.submittals.addItem(sub1);
			result.submittals.addItem(sub2);
			result.submittals.addItem(sub3);
			result.submittals.addItem(sub4);
			result.submittals.addItem(sub5);
			result.submittals.addItem(sub6);
			result.submittals.addItem(sub7);

		var video1:PromoVideo = new PromoVideo();
			video1.name = "Sample Video Name - 1";
		var video2:PromoVideo = new PromoVideo();
			video2.name = "Sample Video Name - 2";
		var video3:PromoVideo = new PromoVideo();
			video3.name = "Sample Video Name - 3";
		var video4:PromoVideo = new PromoVideo();
			video4.name = "Sample Video Name - 4";
		var video5:PromoVideo = new PromoVideo();
			video5.name = "Sample Video Name - 5";

			result.videos.addItem(video1);
			result.videos.addItem(video2);
			result.videos.addItem(video3);
			result.videos.addItem(video4);
			result.videos.addItem(video5);

		return result;
	}
	
	public function search(searchString:String):SearchResultPackage 
	{
		var result:SearchResultPackage = new SearchResultPackage();
		
			result.searchString = searchString;
			
		var section1:SearchResultSection = new SearchResultSection();
			section1.name = "Sample Section Name";
			
		var page11:SearchResultPage = new SearchResultPage();
			page11.page = 1;
			var mi111:ModelItem = new ModelItem();
				mi111.name = "Model Item 111";
			var mi112:ModelItem = new ModelItem();
				mi112.name = "Model Item 112";
			var mi113:ModelItem = new ModelItem();
				mi113.name = "Model Item 113";
			var mi114:ModelItem = new ModelItem();
				mi114.name = "Model Item 114";

			page11.modelItems.addItem(mi111);
			page11.modelItems.addItem(mi112);
			page11.modelItems.addItem(mi113);
			page11.modelItems.addItem(mi114);
			
		var page12:SearchResultPage = new SearchResultPage();
			page12.page = 2;
			var mi121:ModelItem = new ModelItem();
				mi121.name = "Model Item 121";
			var mi122:ModelItem = new ModelItem();
				mi122.name = "Model Item 122";
			var mi123:ModelItem = new ModelItem();
				mi123.name = "Model Item 123";

			page12.modelItems.addItem(mi121);
			page12.modelItems.addItem(mi122);
			page12.modelItems.addItem(mi123);
			
		var page13:SearchResultPage = new SearchResultPage();
			page13.page = 3;
			var mi131:ModelItem = new ModelItem();
				mi131.name = "Model Item 131";
			var mi132:ModelItem = new ModelItem();
				mi132.name = "Model Item 132";

			page13.modelItems.addItem(mi131);
			page13.modelItems.addItem(mi132);
			
		var page14:SearchResultPage = new SearchResultPage();
			page14.page = 4;
			var mi141:ModelItem = new ModelItem();
				mi141.name = "Model Item 141";
			var mi142:ModelItem = new ModelItem();
				mi142.name = "Model Item 142";
			var mi143:ModelItem = new ModelItem();
				mi143.name = "Model Item 143";
			var mi144:ModelItem = new ModelItem();
				mi144.name = "Model Item 144";

			page14.modelItems.addItem(mi141);
			page14.modelItems.addItem(mi142);
			page14.modelItems.addItem(mi143);
			page14.modelItems.addItem(mi144);
			
		var page15:SearchResultPage = new SearchResultPage();
			page15.page = 5;
			var mi151:ModelItem = new ModelItem();
				mi151.name = "Model Item 151";
			var mi152:ModelItem = new ModelItem();
				mi152.name = "Model Item 152";
			var mi153:ModelItem = new ModelItem();
				mi153.name = "Model Item 153";
			var mi154:ModelItem = new ModelItem();
				mi154.name = "Model Item 154";
			var mi155:ModelItem = new ModelItem();
				mi155.name = "Model Item 155";

			page15.modelItems.addItem(mi151);
			page15.modelItems.addItem(mi152);
			page15.modelItems.addItem(mi153);
			page15.modelItems.addItem(mi154);
			page15.modelItems.addItem(mi155);
		
		section1.pages.addItem(page11);
		section1.pages.addItem(page12);
		section1.pages.addItem(page13);
		section1.pages.addItem(page14);
		section1.pages.addItem(page15);
		
		var section2:SearchResultSection = new SearchResultSection();
			section2.name = "Sample Section Name 2";
			
		var page21:SearchResultPage = new SearchResultPage();
			page21.page = 1;
			var mi211:ModelItem = new ModelItem();
				mi211.name = "Model Item 211";
			var mi212:ModelItem = new ModelItem();
				mi212.name = "Model Item 212";
			var mi213:ModelItem = new ModelItem();
				mi213.name = "Model Item 213";
			var mi214:ModelItem = new ModelItem();
				mi214.name = "Model Item 214";

			page21.modelItems.addItem(mi211);
			page21.modelItems.addItem(mi212);
			page21.modelItems.addItem(mi213);
			page21.modelItems.addItem(mi214);
			
		var page22:SearchResultPage = new SearchResultPage();
			page22.page = 2;
			var mi221:ModelItem = new ModelItem();
				mi221.name = "Model Item 221";
			var mi222:ModelItem = new ModelItem();
				mi222.name = "Model Item 222";
			var mi223:ModelItem = new ModelItem();
				mi223.name = "Model Item 223";

			page22.modelItems.addItem(mi221);
			page22.modelItems.addItem(mi222);
			page22.modelItems.addItem(mi223);
			
		var page23:SearchResultPage = new SearchResultPage();
			page23.page = 3;
			var mi231:ModelItem = new ModelItem();
				mi231.name = "Model Item 231";
			var mi232:ModelItem = new ModelItem();
				mi232.name = "Model Item 232";

			page23.modelItems.addItem(mi131);
			page23.modelItems.addItem(mi132);
			
		var page24:SearchResultPage = new SearchResultPage();
			page24.page = 4;
			var mi241:ModelItem = new ModelItem();
				mi241.name = "Model Item 241";
			var mi242:ModelItem = new ModelItem();
				mi242.name = "Model Item 242";
			var mi243:ModelItem = new ModelItem();
				mi243.name = "Model Item 243";
			var mi244:ModelItem = new ModelItem();
				mi244.name = "Model Item 244";

			page24.modelItems.addItem(mi241);
			page24.modelItems.addItem(mi242);
			page24.modelItems.addItem(mi243);
			page24.modelItems.addItem(mi244);
			
		var page25:SearchResultPage = new SearchResultPage();
			page25.page = 5;
			var mi251:ModelItem = new ModelItem();
				mi251.name = "Model Item 251";
			var mi252:ModelItem = new ModelItem();
				mi252.name = "Model Item 252";
			var mi253:ModelItem = new ModelItem();
				mi253.name = "Model Item 253";
			var mi254:ModelItem = new ModelItem();
				mi254.name = "Model Item 254";
			var mi255:ModelItem = new ModelItem();
				mi255.name = "Model Item 255";

			page25.modelItems.addItem(mi251);
			page25.modelItems.addItem(mi252);
			page25.modelItems.addItem(mi253);
			page25.modelItems.addItem(mi254);
			page25.modelItems.addItem(mi255);
		
		section2.pages.addItem(page21);
		section2.pages.addItem(page22);
		section2.pages.addItem(page23);
		section2.pages.addItem(page24);
		section2.pages.addItem(page25);
		
		var section3:SearchResultSection = new SearchResultSection();
			section3.name = "Sample Section Name 3";
			
		var page31:SearchResultPage = new SearchResultPage();
			page31.page = 1;
			var mi311:ModelItem = new ModelItem();
				mi311.name = "Model Item 311";
			var mi312:ModelItem = new ModelItem();
				mi312.name = "Model Item 312";
			var mi313:ModelItem = new ModelItem();
				mi313.name = "Model Item 313";
			var mi314:ModelItem = new ModelItem();
				mi314.name = "Model Item 314";

			page31.modelItems.addItem(mi311);
			page31.modelItems.addItem(mi312);
			page31.modelItems.addItem(mi313);
			page31.modelItems.addItem(mi314);
			
		var page32:SearchResultPage = new SearchResultPage();
			page32.page = 2;
			var mi321:ModelItem = new ModelItem();
				mi321.name = "Model Item 321";
			var mi322:ModelItem = new ModelItem();
				mi322.name = "Model Item 322";
			var mi323:ModelItem = new ModelItem();
				mi323.name = "Model Item 323";

			page32.modelItems.addItem(mi321);
			page32.modelItems.addItem(mi322);
			page32.modelItems.addItem(mi323);
			
		section3.pages.addItem(page31);
		section3.pages.addItem(page32);

		result.sections.addItem(section1);
		result.sections.addItem(section2);
		result.sections.addItem(section3);
		
		return result;
	}
	
}
}