package com.ebs.eroof.model
{
   import com.adobe.cairngorm.model.IModelLocator;
   import com.quickbase.idn.model.QuickBaseMSAModel;
   
   //note that all model classes should be declared bindable to ensure that all 
   //public properties can be used for data binding in the view.
   [Bindable]
   public class eRoofModel implements IModelLocator  //set correct model name
   {
         //The QuickBaseMSAModel, included by composition
         public var quickbaseModel:QuickBaseMSAModel = QuickBaseMSAModel.getInstance();
         
         
         //TODO: Define variables, setters, getters, etc. for your model here
         
         
         //Singleton pattern implementation
         private static var _instance:eRoofModel;
         public static function getInstance():eRoofModel
         {
               if (_instance == null) {
                     _instance = new eRoofModel(new Private());
               }
               return _instance;
         }
         public function eRoofModel(accessPrivate:Private) {}
   }
}

class Private {}
