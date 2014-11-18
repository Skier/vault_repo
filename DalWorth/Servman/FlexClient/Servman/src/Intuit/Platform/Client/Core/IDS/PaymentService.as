
    /*******************************************************************
    * PaymentService.as
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
    on the Intuit.Platform.Client.Core.IDS.PaymentService class using WebORB. 
    You can add the code to your Flex Builder project and use the 
    class as shown below:

           import Intuit.Platform.Client.Core.IDS.PaymentService;
           import Intuit.Platform.Client.Core.IDS.PaymentServiceModel;

           [Bindable]
           var model:PaymentServiceModel = new PaymentServiceModel();
           var serviceProxy:PaymentService = new PaymentService( model );
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
        
    public class PaymentService
    {
      private var remoteObject:RemoteObject;
      private var model:PaymentServiceModel; 

      public function PaymentService( model:PaymentServiceModel = null )
      {
        remoteObject  = new RemoteObject("GenericDestination");
        remoteObject.source = "Intuit.Platform.Client.Core.IDS.PaymentService";
        
        remoteObject.AddPayment.addEventListener("result",AddPaymentHandler);
        
        remoteObject.AddResource.addEventListener("result",AddResourceHandler);
        
        remoteObject.DeletePayment.addEventListener("result",DeletePaymentHandler);
        
        remoteObject.DeleteResource.addEventListener("result",DeleteResourceHandler);
        
        remoteObject.FindAll.addEventListener("result",FindAllHandler);
        
        remoteObject.FindAll.addEventListener("result",FindAllHandler);
        
        remoteObject.FindById.addEventListener("result",FindByIdHandler);
        
        remoteObject.FindById.addEventListener("result",FindByIdHandler);
        
        remoteObject.GetPayments.addEventListener("result",GetPaymentsHandler);
        
        remoteObject.GetResources.addEventListener("result",GetResourcesHandler);
        
        remoteObject.GetResourcesForQuery.addEventListener("result",GetResourcesForQueryHandler);
        
        remoteObject.RevertResource.addEventListener("result",RevertResourceHandler);
        
        remoteObject.UpdatePayment.addEventListener("result",UpdatePaymentHandler);
        
        remoteObject.UpdateResource.addEventListener("result",UpdateResourceHandler);
        
        remoteObject.addEventListener("fault", onFault);
        
        if( model == null )
            model = new PaymentServiceModel();
    
        this.model = model;

      }
      
      public function setCredentials( userid:String, password:String ):void
      {
        remoteObject.setCredentials( userid, password );
      }

      public function GetModel():PaymentServiceModel
      {
        return this.model;
      }


    
      public function AddPayment(context:PlatformSessionContext,
      realmId:String,
      newPayment:Payment,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.AddPayment(context,realmId,newPayment);
        
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
    
      public function DeletePayment(context:PlatformSessionContext,
      realmId:String,
      paymentToDelete:Payment,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.DeletePayment(context,realmId,paymentToDelete);
        
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
      paymentIdToFind:IdType,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.FindById(context,realmId,paymentIdToFind);
        
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
    
      public function GetPayments(context:PlatformSessionContext,
      realmId:String,
      paymentQuery:PaymentQuery,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.GetPayments(context,realmId,paymentQuery);
        
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
    
      public function UpdatePayment(context:PlatformSessionContext,
      realmId:String,
      paymentToUpdate:Payment,
       responder:IResponder = null ):void
      {
        var asyncToken:AsyncToken = remoteObject.UpdatePayment(context,realmId,paymentToUpdate);
        
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
         
      public virtual function AddPaymentHandler(event:ResultEvent):void
      {
        
          var returnValue:Payment = event.result as Payment;
          model.AddPaymentResult = returnValue;
        
      }
         
      public virtual function AddResourceHandler(event:ResultEvent):void
      {
        
          var returnValue:CdmBase = event.result as CdmBase;
          model.AddResourceResult = returnValue;
        
      }
         
      public virtual function DeletePaymentHandler(event:ResultEvent):void
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
        
          var returnValue:Payment = event.result as Payment;
          model.FindByIdResult = returnValue;
        
      }
         
      public virtual function FindByIdHandler(event:ResultEvent):void
      {
        
          var returnValue:CdmComplexBase = event.result as CdmComplexBase;
          model.FindByIdResult = returnValue;
        
      }
         
      public virtual function GetPaymentsHandler(event:ResultEvent):void
      {
        
          var returnValue:Array = event.result as Array;
          model.GetPaymentsResult = returnValue;
        
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
         
      public virtual function UpdatePaymentHandler(event:ResultEvent):void
      {
        
          var returnValue:Payment = event.result as Payment;
          model.UpdatePaymentResult = returnValue;
        
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
  