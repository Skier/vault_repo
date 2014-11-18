package com.titus.catalog.storage
{

import com.titus.catalog.model.CatalogItem;
import com.titus.catalog.model.CatalogItemPackage;
import com.titus.catalog.model.CatalogPackage;
import com.titus.catalog.model.PdfPage;
import com.titus.catalog.model.PromoVideo;
import com.titus.catalog.model.Submittal;
import com.titus.catalog.model.search.SearchResultPackage;
import com.titus.catalog.model.search.SearchResultPage;
import com.titus.catalog.model.search.SearchResultSection;
import com.titus.catalog.view.pdf.BitmapPdfPage;

import flash.events.Event;
import flash.events.EventDispatcher;

import mx.collections.ArrayCollection;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.RemoteObject;
	
public class DBStorage
	extends EventDispatcher
	implements IStorage
{
	
	[Event(name="complete", type="flash.events.Event")]

	private const CACHE_SIZE:int = 10;
	
	private static var instance:DBStorage;
	
	public function DBStorage()
	{
		if (instance)
			throw new Error("DB Storage is singleton!");
	}

	public static function getInstance():IStorage
	{
		if (!instance)
			instance = new DBStorage();

		return instance;
	}
	
	private var rootCatalog:CatalogItem = null;
	
	private var pages:ArrayCollection = new ArrayCollection();
	
	private var imagesCache:Array;
	
	private var sectionsHash:Array = new Array();
	
	public function getCatalog():CatalogItem
	{
		if (null == rootCatalog) {
			rootCatalog = new CatalogItem();
			rootCatalog.name = "Loading...";
			
        	var service:RemoteObject = new RemoteObject("GenericDestination");
        	service.source = "Titus.ECatalog.Service.CatalogService";
        	
        	service.GetCatalog.addEventListener(ResultEvent.RESULT, onCatalogLoaded);
        	service.GetCatalog.addEventListener(FaultEvent.FAULT, onCatalogLoadFailed);
        	service.GetCatalog();
  		}
        
        return rootCatalog;
	}
	
	public function getCatalogPages():ArrayCollection {
		return pages;
		
		//test pages
/* 		
		var result:ArrayCollection = new ArrayCollection();
		
		for (var i:int = 0; i<3; i++) 
		{
			var page1:PdfPage = new PdfPage();
				page1.imageUrl = "assets/images/titus/01.png";

			var page2:PdfPage = new PdfPage();
				page2.imageUrl = "assets/images/titus/02.png";
			
			var page3:PdfPage = new PdfPage();
				page3.imageUrl = "assets/images/titus/03.png";

			var page4:PdfPage = new PdfPage();
				page4.imageUrl = "assets/images/titus/04.png";

			var page5:PdfPage = new PdfPage();
				page5.imageUrl = "assets/images/titus/05.png";

			var page6:PdfPage = new PdfPage();
				page6.imageUrl = "assets/images/titus/06.png";

			var page7:PdfPage = new PdfPage();
				page7.imageUrl = "assets/images/titus/07.png";

			var page8:PdfPage = new PdfPage();
				page8.imageUrl = "assets/images/titus/08.png";

			result.addItem(page1);
			result.addItem(page2);
			result.addItem(page3);
			result.addItem(page4);
			result.addItem(page5);
			result.addItem(page6);
			result.addItem(page7);
			result.addItem(page8);
		}
		
		return result;
 */
	}
	
	private function onCatalogLoadFailed(event:FaultEvent):void {
		rootCatalog.name = "Loading failed.";
	}
	
	public function findPageByCode(code:String):int {
		if (2 > code.length) {
			return 0;
		}
		
		var section:String = code.charAt(0);
		if (null == sectionsHash[section]) {
			return 0; 
		}
		
		var sectionPage:int;
		try {
			sectionPage = parseInt(code.substr(1, code.length - 1));
		} catch(err:Error) {
			return 0;
		}
		
		return (sectionsHash[section] as int) + sectionPage - 1;
	}
	
	private function onCatalogLoaded(event:ResultEvent):void {
		rootCatalog.children.removeAll();
		rootCatalog.name = "All Categories";
		
		var result:CatalogPackage = event.result as CatalogPackage;
		
		buildPages(result.Pages);
		
		for each (var item:CatalogItem in result.Categories) {
			rootCatalog.children.addItem(item);
		}
		
		buildCatalogHash(result.Categories);
		
        dispatchEvent(new Event(Event.COMPLETE)); 
	}
	
	private function buildCatalogHash(items:Array):void {
		for each (var item:CatalogItem in items) {
			item.debugName(); // DEBUG
			if (null == sectionsHash[item.PageCode]) {
				sectionsHash[item.PageCode] = item.PageNumber;
			} else {
				var pageNumber:int = sectionsHash[item.PageCode] as int;
				if (pageNumber > item.PageNumber) {
					sectionsHash[item.PageCode] = item.PageNumber;
				}
			}
			
			if (null != item.children) {
				buildCatalogHash(item.children.toArray());
			}
		}
	}
	
	private function buildPages(pages:Array):void {
		for each (var page:PdfPage in pages) {
			this.pages.addItem(page);
    		var bitmap:BitmapPdfPage = new BitmapPdfPage();
   			bitmap.setStyle("backgroundColor", 0xFFFFFF);
   			bitmap.percentWidth = 100;
   			bitmap.percentHeight = 100;
			bitmap.imageUrl = page.imageUrl;
    		page.bitmap = bitmap;
		}
	}
	
	public function getCatalogItemPackage(catalogItem:CatalogItem):CatalogItemPackage
	{
		var result:CatalogItemPackage = new CatalogItemPackage();

		result.catalogItem = catalogItem;

		// result.catalogPage = modelItem.PageNumber;

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
	
	public function searchModel(modelName:String, exactMatch:Boolean, caseSensitive:Boolean):ArrayCollection {
		var result:ArrayCollection = new ArrayCollection();
		recursiveSearchModel(result, rootCatalog, modelName, exactMatch, caseSensitive);
		return result;
	}
	
	private function recursiveSearchModel(result:ArrayCollection, node:CatalogItem, modelName:String, exactMatch:Boolean, caseSensitive:Boolean):void {
		for each (var item:CatalogItem in node.children) {
			if (compareModelName(item.name, modelName, exactMatch, caseSensitive)) {
				result.addItem(item);
			}
			
			if (null != item.children) {
				recursiveSearchModel(result, item, modelName, exactMatch, caseSensitive);
			}
		}
	}
	
	private function compareModelName(name1:String, name2:String, exactMatch:Boolean, caseSensitive:Boolean):Boolean {
		if (exactMatch) {
			return (caseSensitive)? name1 == name2: name1.toUpperCase() == name2.toUpperCase();
		}
		
		var name1Prepared:String = prepareStringToCompare(name1);
		var name2Prepared:String = prepareStringToCompare(name2);
		
		return (name1Prepared == name2Prepared)
			|| (-1 < name1Prepared.indexOf(name2Prepared))
			|| (-1 < name2Prepared.indexOf(name1Prepared));
	}
	
	private function prepareStringToCompare(str:String):String {
		return str.replace(" ", "").replace("_", "").replace("-", "").toUpperCase();
	}

	public function search(searchString:String):SearchResultPackage 
	{
		var result:SearchResultPackage = new SearchResultPackage();
		
			result.searchString = searchString;
			
		var section1:SearchResultSection = new SearchResultSection();
			section1.name = "Sample Section Name";
			
		var page11:SearchResultPage = new SearchResultPage();
			page11.page = 1;
			var mi111:CatalogItem = new CatalogItem();
				mi111.name = "Model Item 111";
			var mi112:CatalogItem = new CatalogItem();
				mi112.name = "Model Item 112";
			var mi113:CatalogItem = new CatalogItem();
				mi113.name = "Model Item 113";
			var mi114:CatalogItem = new CatalogItem();
				mi114.name = "Model Item 114";

			page11.modelItems.addItem(mi111);
			page11.modelItems.addItem(mi112);
			page11.modelItems.addItem(mi113);
			page11.modelItems.addItem(mi114);
			
		var page12:SearchResultPage = new SearchResultPage();
			page12.page = 2;
			var mi121:CatalogItem = new CatalogItem();
				mi121.name = "Model Item 121";
			var mi122:CatalogItem = new CatalogItem();
				mi122.name = "Model Item 122";
			var mi123:CatalogItem = new CatalogItem();
				mi123.name = "Model Item 123";

			page12.modelItems.addItem(mi121);
			page12.modelItems.addItem(mi122);
			page12.modelItems.addItem(mi123);
			
		var page13:SearchResultPage = new SearchResultPage();
			page13.page = 3;
			var mi131:CatalogItem = new CatalogItem();
				mi131.name = "Model Item 131";
			var mi132:CatalogItem = new CatalogItem();
				mi132.name = "Model Item 132";

			page13.modelItems.addItem(mi131);
			page13.modelItems.addItem(mi132);
			
		var page14:SearchResultPage = new SearchResultPage();
			page14.page = 4;
			var mi141:CatalogItem = new CatalogItem();
				mi141.name = "Model Item 141";
			var mi142:CatalogItem = new CatalogItem();
				mi142.name = "Model Item 142";
			var mi143:CatalogItem = new CatalogItem();
				mi143.name = "Model Item 143";
			var mi144:CatalogItem = new CatalogItem();
				mi144.name = "Model Item 144";

			page14.modelItems.addItem(mi141);
			page14.modelItems.addItem(mi142);
			page14.modelItems.addItem(mi143);
			page14.modelItems.addItem(mi144);
			
		var page15:SearchResultPage = new SearchResultPage();
			page15.page = 5;
			var mi151:CatalogItem = new CatalogItem();
				mi151.name = "Model Item 151";
			var mi152:CatalogItem = new CatalogItem();
				mi152.name = "Model Item 152";
			var mi153:CatalogItem = new CatalogItem();
				mi153.name = "Model Item 153";
			var mi154:CatalogItem = new CatalogItem();
				mi154.name = "Model Item 154";
			var mi155:CatalogItem = new CatalogItem();
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
			var mi211:CatalogItem = new CatalogItem();
				mi211.name = "Model Item 211";
			var mi212:CatalogItem = new CatalogItem();
				mi212.name = "Model Item 212";
			var mi213:CatalogItem = new CatalogItem();
				mi213.name = "Model Item 213";
			var mi214:CatalogItem = new CatalogItem();
				mi214.name = "Model Item 214";

			page21.modelItems.addItem(mi211);
			page21.modelItems.addItem(mi212);
			page21.modelItems.addItem(mi213);
			page21.modelItems.addItem(mi214);
			
		var page22:SearchResultPage = new SearchResultPage();
			page22.page = 2;
			var mi221:CatalogItem = new CatalogItem();
				mi221.name = "Model Item 221";
			var mi222:CatalogItem = new CatalogItem();
				mi222.name = "Model Item 222";
			var mi223:CatalogItem = new CatalogItem();
				mi223.name = "Model Item 223";

			page22.modelItems.addItem(mi221);
			page22.modelItems.addItem(mi222);
			page22.modelItems.addItem(mi223);
			
		var page23:SearchResultPage = new SearchResultPage();
			page23.page = 3;
			var mi231:CatalogItem = new CatalogItem();
				mi231.name = "Model Item 231";
			var mi232:CatalogItem = new CatalogItem();
				mi232.name = "Model Item 232";

			page23.modelItems.addItem(mi131);
			page23.modelItems.addItem(mi132);
			
		var page24:SearchResultPage = new SearchResultPage();
			page24.page = 4;
			var mi241:CatalogItem = new CatalogItem();
				mi241.name = "Model Item 241";
			var mi242:CatalogItem = new CatalogItem();
				mi242.name = "Model Item 242";
			var mi243:CatalogItem = new CatalogItem();
				mi243.name = "Model Item 243";
			var mi244:CatalogItem = new CatalogItem();
				mi244.name = "Model Item 244";

			page24.modelItems.addItem(mi241);
			page24.modelItems.addItem(mi242);
			page24.modelItems.addItem(mi243);
			page24.modelItems.addItem(mi244);
			
		var page25:SearchResultPage = new SearchResultPage();
			page25.page = 5;
			var mi251:CatalogItem = new CatalogItem();
				mi251.name = "Model Item 251";
			var mi252:CatalogItem = new CatalogItem();
				mi252.name = "Model Item 252";
			var mi253:CatalogItem = new CatalogItem();
				mi253.name = "Model Item 253";
			var mi254:CatalogItem = new CatalogItem();
				mi254.name = "Model Item 254";
			var mi255:CatalogItem = new CatalogItem();
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
			var mi311:CatalogItem = new CatalogItem();
				mi311.name = "Model Item 311";
			var mi312:CatalogItem = new CatalogItem();
				mi312.name = "Model Item 312";
			var mi313:CatalogItem = new CatalogItem();
				mi313.name = "Model Item 313";
			var mi314:CatalogItem = new CatalogItem();
				mi314.name = "Model Item 314";

			page31.modelItems.addItem(mi311);
			page31.modelItems.addItem(mi312);
			page31.modelItems.addItem(mi313);
			page31.modelItems.addItem(mi314);
			
		var page32:SearchResultPage = new SearchResultPage();
			page32.page = 2;
			var mi321:CatalogItem = new CatalogItem();
				mi321.name = "Model Item 321";
			var mi322:CatalogItem = new CatalogItem();
				mi322.name = "Model Item 322";
			var mi323:CatalogItem = new CatalogItem();
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
