
    /*******************************************************************
    * InvoiceService.as
    * Copyright (C) 2006-2010 Midnight Coders, Inc.
    *
    * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    ********************************************************************/
    
    /***********************************************************************
    The generated code provides a simple mechanism for invoking methods
    on the Intuit.Platform.Client.Core.IDS.InvoiceService class using WebORB. 
    You can add the code to your Flex Builder project and use the 
    class as shown below:

           import Intuit.Platform.Client.Core.IDS.InvoiceService;
           import Intuit.Platform.Client.Core.IDS.InvoiceServiceModel;

           [Bindable]
           var model:InvoiceServiceModel = new InvoiceServiceModel();
           var serviceProxy:InvoiceService = new InvoiceService( model );
           // make sure to substitute foo() with a method from the class
           serviceProxy.foo();
           
    Notice the model variable is shown in the example above as Bindable. 
    You can bind your UI components to the fields in the model object.
    ************************************************************************/
  
    package Intuit.Platform.Client.Core.IDS
    {
    import mx.rpc.remoting.RemoteObject;
    import mx.controls.Alert;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.AsyncToken;
    import mx.rpc.IResponder;
    import mx.collections.ArrayCollection;

    
    import Intuit.Platform.Client.Core.vo.*;
    import Intuit.Platform.Client.Core.IDS.vo.*;
    import Intuit.Sb.Cdm.vo.*;
    import defaultPackage.vo.*;
        
    public class InvoiceService
    {
      private var remoteObject:RemoteObject;
      private var model:InvoiceServiceModel; 

      public function InvoiceService( model:InvoiceServiceModel = null )
      {
        remoteObject  = new RemoteObject("GenericDestination");
        remoteObject.source = "Intuit.Platform.Client.Core.IDS.InvoiceService";
        
        remoteObject.AddInvoice.addEventListener("result",AddInvoiceHandler);
        
        remoteObject.AddResource.addEventListener("result",AddResourceHandler);
        
        remoteObject.DeleteInvoice.addEventListener("result",DeleteInvoiceHandler);
        
        remoteObject.DeleteResource.addEventListener("result",DeleteResourceHandler);
        
        remoteObject.FindAll.addEventListener("result",FindAllHandler);
        
        remoteObject.FindAll.addEventListener("result",FindAllHandler);
        
        remoteObject.FindById.addEventListener("result",FindByIdHandler);
        
        remoteObject.FindById.addEventListener("result",FindByIdHandler);
        
        remoteObject.GetInvoices.addEventListener("result",GetInvoicesHandler);
        
        remoteObject.GetResources.addEventListener("result",GetResourcesHandler);
        
        remoteObject.GetResourcesForQuery.addEventListener("result",GetResourcesForQueryHandler);
        
        remoteObject.RevertResource.addEventListener("result",RevertResourceHandler);
        
        remoteObject.UpdateInvoice.addEventListener("result",UpdateInvoiceHandler);
        
        remoteObject.UpdateResource.addEventListener("result",UpdateResourceHandler);
        
        remoteObject.addEventListener("fault", onFault);
        
        if( model == null )
            model = new InvoiceServiceModel();
    
        this.model = model;

      }
      
      public function setCredentials( userid:String, password:String ):void
      {
        remoteObject.setCredentials( userid, password );
      }

      public function GetModel():InvoiceServiceModel
      {
        return this.model;
      }


    
      public function AddInvoice(context:PlatformSessionContext,
      realmId:String,
      newInvoice:Invoice,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.AddInvoice(context,realmId,newInvoice);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function AddResource(context:PlatformSessionContext,
      realmId:String,
      newResource:CdmBase,
      resource:String,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.AddResource(context,realmId,newResource,resource);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function DeleteInvoice(context:PlatformSessionContext,
      realmId:String,
      invoiceToDelete:Invoice,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.DeleteInvoice(context,realmId,invoiceToDelete);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function DeleteResource(context:PlatformSessionContext,
      realmId:String,
      objectToDelete:CdmBase,
      resource:String,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.DeleteResource(context,realmId,objectToDelete,resource);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function FindAll(context:PlatformSessionContext,
      realmId:String,
      resource:String,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.FindAll(context,realmId,resource);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function FindAll(context:PlatformSessionContext,
      realmId:String,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.FindAll(context,realmId);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function FindById(context:PlatformSessionContext,
      realmId:String,
      invoiceIdToFind:IdType,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.FindById(context,realmId,invoiceIdToFind);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function FindById(context:PlatformSessionContext,
      realmId:String,
      resourceIdToFind:IdType,
      resource:String,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.FindById(context,realmId,resourceIdToFind,resource);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function GetInvoices(context:PlatformSessionContext,
      realmId:String,
      invoiceQuery:InvoiceQuery,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.GetInvoices(context,realmId,invoiceQuery);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function GetResources(context:PlatformSessionContext,
      operationContext:IDSOperationContext,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.GetResources(context,operationContext);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function GetResourcesForQuery(context:PlatformSessionContext,
      operationContext:IDSOperationContext,
      queryDocument:ReportQueryBase,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.GetResourcesForQuery(context,operationContext,queryDocument);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function RevertResource(context:PlatformSessionContext,
      realmId:String,
      objectToRevert:CdmBase,
      resource:String,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.RevertResource(context,realmId,objectToRevert,resource);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function UpdateInvoice(context:PlatformSessionContext,
      realmId:String,
      invoiceToUpdate:Invoice,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.UpdateInvoice(context,realmId,invoiceToUpdate);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
    
      public function UpdateResource(context:PlatformSessionContext,
      realmId:String,
      objectToUpdate:CdmBase,
      resource:String,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.UpdateResource(context,realmId,objectToUpdate,resource);
        
        if( responder != null )
            asyncToken.addResponder( responder );

      }
         
      public virtual function AddInvoiceHandler(event:ResultEvent):void
      {
        
          var returnValue:Invoice = event.result as Invoice;
          model.AddInvoiceResult = returnValue;
        
      }
         
      public virtual function AddResourceHandler(event:ResultEvent):void
      {
        
          var returnValue:CdmBase = event.result as CdmBase;
          model.AddResourceResult = returnValue;
        
      }
         
      public virtual function DeleteInvoiceHandler(event:ResultEvent):void
      {
        
      }
         
      public virtual function DeleteResourceHandler(event:ResultEvent):void
      {
        
      }
         
      public virtual function FindAllHandler(event:ResultEvent):void
      {
        
          var returnValue:CdmComplexBase = event.result as CdmComplexBase;
          model.FindAllResult = returnValue;
        
      }
         
      public virtual function FindAllHandler(event:ResultEvent):void
      {
        
          var returnValue:Array = event.result as Array;
          model.FindAllResult = returnValue;
        
      }
         
      public virtual function FindByIdHandler(event:ResultEvent):void
      {
        
          var returnValue:Invoice = event.result as Invoice;
          model.FindByIdResult = returnValue;
        
      }
         
      public virtual function FindByIdHandler(event:ResultEvent):void
      {
        
          var returnValue:CdmComplexBase = event.result as CdmComplexBase;
          model.FindByIdResult = returnValue;
        
      }
         
      public virtual function GetInvoicesHandler(event:ResultEvent):void
      {
        
          var returnValue:Array = event.result as Array;
          model.GetInvoicesResult = returnValue;
        
      }
         
      public virtual function GetResourcesHandler(event:ResultEvent):void
      {
        
          var returnValue:CdmComplexBase = event.result as CdmComplexBase;
          model.GetResourcesResult = returnValue;
        
      }
         
      public virtual function GetResourcesForQueryHandler(event:ResultEvent):void
      {
        
          var returnValue:CdmComplexBase = event.result as CdmComplexBase;
          model.GetResourcesForQueryResult = returnValue;
        
      }
         
      public virtual function RevertResourceHandler(event:ResultEvent):void
      {
        
          var returnValue:CdmBase = event.result as CdmBase;
          model.RevertResourceResult = returnValue;
        
      }
         
      public virtual function UpdateInvoiceHandler(event:ResultEvent):void
      {
        
          var returnValue:Invoice = event.result as Invoice;
          model.UpdateInvoiceResult = returnValue;
        
      }
         
      public virtual function UpdateResourceHandler(event:ResultEvent):void
      {
        
          var returnValue:CdmBase = event.result as CdmBase;
          model.UpdateResourceResult = returnValue;
        
      }
    
      public function onFault (event:FaultEvent):void
      {
        Alert.show(event.fault.faultString, "Error");
      }
    }
  } 
  