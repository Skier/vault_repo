<?xml version="1.0"?>
<xsl:stylesheet version="2.0" 
  xmlns:codegen="urn:cogegen-xslt-lib:xslt"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:import href="codegen.xslt"/>
  
  <xsl:variable name="up" select="'ABCDEFGHIJKLMNOPQRSTUVWXYZ'"/>
  <xsl:variable name="lo" select="'abcdefghijklmnopqrstuvwxyz'"/>

<xsl:template name="codegen.appmain">
    <file name="main.mxml">
&lt;mx:Application xmlns:mx="http://www.adobe.com/2006/mxml" xmlns:swiz="http://swiz.swizframework.org" layout="absolute" initialize="onLoad()">
&lt;mx:Script>
  &lt;![CDATA[
  import <xsl:value-of select="//service/@namespace"/>.DataTypeInitializer;
  import <xsl:value-of select="//service/@namespace"/>.models.presentations.<xsl:value-of select="//service/@name"/>PresentationModel;
  import org.swizframework.Swiz;

  [Autowire]
  [Bindable]
  public var modelPresentation:<xsl:value-of select="//service/@name"/>PresentationModel;

  public function onLoad():void
  {
    new DataTypeInitializer();
    Swiz.autowire(this);
  }
  ]]&gt;
&lt;/mx:Script>

  &lt;swiz:SwizConfig
    strict="true"
    mediateBubbledEvents="true"
    eventPackages='<xsl:value-of select="//service/@namespace"/>.events'            
    beanLoaders="{ [ <xsl:value-of select="//service/@namespace"/>.Beans ] }" />

&lt;/mx:Application>
    </file>
  </xsl:template>

<xsl:template name="codegen.service">
    <xsl:call-template name="codegen.datatypeinitializer"/>
    <folder name="models">
      <file name="{@name}Model.as">
        <xsl:call-template name="codegen.description">
          <xsl:with-param name="file-name" select="concat(@name,'Model.as')" />
        </xsl:call-template>
        <xsl:call-template name="model" />       
      </file>
        <folder name="presentations">
            <file name="{@name}PresentationModel.as">
                <xsl:call-template name="codegen.description">
                    <xsl:with-param name="file-name" select="concat(@name,'PresentationModel.as')" />
                </xsl:call-template>
                <xsl:call-template name="presentationmodel" />
            </file>
        </folder>
    </folder>


    <folder name="events">
        <xsl:for-each select="method">
            <xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
            <xsl:variable name="className" select = "concat(translate($firstLetter, $lo, $up), substring(@name, 2, string-length(@name)-1))"/>

            <file name="{$className}Event.as">
                <xsl:call-template name="codegen.description">
                    <xsl:with-param name="file-name" select="concat($className,'Event.as')" />
                </xsl:call-template>
                <xsl:call-template name="event" />
            </file>
        </xsl:for-each>
    </folder>

    <folder name="delegates">
        <file name="{@name}Delegate.as">
          <xsl:call-template name="codegen.description">
            <xsl:with-param name="file-name" select="concat(@name,'Delegate.as')" />
          </xsl:call-template>
          <xsl:call-template name="delegates" />
        </file>
      </folder>
	  
	   <folder name="controllers">        
    		<xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
    		<xsl:variable name="className" select = "concat(translate($firstLetter, $lo, $up), substring(@name, 2, 30))"/>
		
        <file name="{$className}Controller.as">
         <xsl:call-template name="codegen.description">
              <xsl:with-param name="file-name" select="concat($className,'Controller.as')" />
         </xsl:call-template>
        <xsl:call-template name="controllers" />
        </file>
       
      </folder>
	  
      <file name="Beans.mxml" type="xml">
       &lt;!-- <xsl:call-template name="codegen.description">
          <xsl:with-param name="file-name" select="Beans.mxml" />
        </xsl:call-template> -->
        <xsl:call-template name="beans" />
      </file>
  </xsl:template>

<xsl:template name="beans">
  <xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
  <xsl:variable name="className" select = "concat(translate($firstLetter, $up, $lo), substring(@name, 2, 30))"/>

    <BeanLoader xmlns="http://swiz.swizframework.org" xmlns:mx="http://www.adobe.com/2006/mxml">

    <xsl:element name="Script" namespace="http://www.adobe.com/2006/mxml">
        &lt;![CDATA[
        import <xsl:value-of select="@namespace"/>.models.presentations.<xsl:value-of select="@name"/>PresentationModel;
        ]]&gt;
    </xsl:element>

    <xsl:element name="{@name}Controller" namespace="{@namespace}.controllers.*">
		<xsl:attribute name="id"><xsl:value-of select="$className"/>Controller</xsl:attribute>
    </xsl:element>
	  
    <xsl:element name="{@name}Delegate" namespace="{@namespace}.delegates.*">
		<xsl:attribute name="id"><xsl:value-of select="$className"/>Delegate</xsl:attribute>
    </xsl:element>

    <xsl:element name="{@name}Model" namespace="{@namespace}.models.*">
      <xsl:attribute name="id"><xsl:value-of select="$className"/>Model</xsl:attribute>
    </xsl:element>

        <xsl:element name="Prototype" namespace="http://swiz.swizframework.org">
            <xsl:attribute name="id"><xsl:value-of select="$className"/>PresentationModel</xsl:attribute>
            <xsl:attribute name="classReference">{ <xsl:value-of select="@name"/>PresentationModel }</xsl:attribute>
            <xsl:attribute name="constructorArguments">{ dispatcher }</xsl:attribute>
            <xsl:attribute name="singleton">true</xsl:attribute>
        </xsl:element>

     <mx:RemoteObject id="{$className}Service" 
		showBusyCursor="true" 
		source="{@fullname}" destination="GenericDestination"/>
	 
	  
    </BeanLoader>
	
 </xsl:template>

<xsl:template name="delegates"> 
	package <xsl:value-of select="@namespace" />.delegates
	{
		import mx.rpc.AsyncToken;
		import mx.rpc.remoting.RemoteObject;
    <xsl:call-template name="codegen.import.alltypes"/>

			<xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
			<xsl:variable name="className" select = "concat(translate($firstLetter, $up, $lo), substring(@name, 2, 30))"/>
		
		public class <xsl:value-of select="@name"/>Delegate
		{
			[Autowire(bean="<xsl:value-of select="$className"/>Service")]
			public var <xsl:value-of select="$className"/>Service : RemoteObject;
			
			public function <xsl:value-of select="@name"/>Delegate() { }
		
			<xsl:for-each select="method">
			public function <xsl:value-of select="@name"/>(<xsl:for-each select="arg">
				<xsl:if test="position() != 1">, </xsl:if>
				<xsl:value-of select="@name"/>:<xsl:value-of select="@type" />
			</xsl:for-each>): AsyncToken 
			{
        	return <xsl:value-of select="$className"/>Service.<xsl:value-of select="@name"/>(<xsl:for-each select="arg">
			<xsl:if test="position() != 1">,</xsl:if>
			<xsl:value-of select="@name"/>
			</xsl:for-each>);
			}
			</xsl:for-each>		
		}
	}
 </xsl:template>
 
<xsl:template name="controllers">
	package <xsl:value-of select="//service/@namespace" />.controllers
	{
  	import mx.rpc.events.ResultEvent;
  	import mx.rpc.events.FaultEvent;
  	import mx.utils.ObjectUtil;
  	import org.swizframework.Swiz;
  	import org.swizframework.controller.AbstractController;
  	import mx.controls.Alert;
  	import <xsl:value-of select="@namespace" />.delegates.*;
    import <xsl:value-of select="@namespace" />.models.*;
  <xsl:call-template name="codegen.import.alltypes"/>
		
	public class <xsl:value-of select="//service/@name"/>Controller extends AbstractController
	{	<xsl:variable name="firstLetter" select="substring(//service/@name, 1,1)"/>
		<xsl:variable name="className" select = "concat(translate($firstLetter, $up, $lo), substring(//service/@name, 2, 30))"/>		
	  [Autowire(bean="<xsl:value-of select="$className"/>Delegate")]
	  public var <xsl:value-of select="$className"/>Delegate : <xsl:value-of select="//service/@name"/>Delegate;

	  [Autowire(bean="<xsl:value-of select="$className"/>Model")]
      public var <xsl:value-of select="$className"/>Model: <xsl:value-of select="//service/@name"/>Model;

  <!--<xsl:for-each select="//method">
		[Bindable] public var <xsl:value-of select="@name"/>Message : <xsl:value-of select="@type" />;
		</xsl:for-each>-->
	
		public function <xsl:value-of select="//service/@name"/>Controller() {}
		
		<xsl:for-each select="//method">
        <xsl:variable name="firstLetter2" select="substring(@name, 1,1)"/>
        <xsl:variable name="methodName" select ="concat(translate($firstLetter2, $lo, $up), substring(@name, 2, 30))"/>
        [Mediate( event="<xsl:value-of select="$methodName"/>Event.EVENT_NAME" <xsl:if test="arg">, properties='<xsl:for-each select="arg"><xsl:if test="position() != 1">,</xsl:if><xsl:value-of select="@name"/></xsl:for-each>'</xsl:if> )]
		public function <xsl:value-of select="@name"/>(<xsl:for-each select="arg">
        <xsl:if test="position() != 1">, </xsl:if>
        <xsl:value-of select="@name"/>:<xsl:value-of select="@type" />
		</xsl:for-each>):void
	    {
			executeServiceCall(<xsl:value-of select="$className"/>Delegate.<xsl:value-of select="@name"/>(<xsl:for-each select="arg">
        <xsl:if test="position() != 1">, </xsl:if>
        <xsl:value-of select="@name"/>
		</xsl:for-each>), on<xsl:value-of select="@name"/>, onFault);
		}
		</xsl:for-each>
 
		<xsl:for-each select="//method">
		private function on<xsl:value-of select="@name"/>( event : ResultEvent ) : void 
        {		
			<xsl:if test="@type != 'void'">
        <xsl:value-of select="$className"/>Model.<xsl:value-of select="@name"/>Result = event.result as <xsl:value-of select="@type"/>;
			</xsl:if>	
		}
		</xsl:for-each>
		
		private function onFault(event:FaultEvent):void 
		{
			Alert.show( event.fault.faultString );
		}
	}
}
</xsl:template>

<xsl:template name="event">   
package <xsl:value-of select="../@namespace" />.events
{
    import flash.events.Event;
    <xsl:call-template name="codegen.import.alltypes"/>
	
	<xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
	<xsl:variable name="className" select = "concat(translate($firstLetter, $lo, $up), substring(@name, 2, string-length(@name)-1))"/>

	public class <xsl:value-of select="$className"/>Event extends Event
	{
		public static const EVENT_NAME:String = "<xsl:value-of select="@name"/>Event";

		public function <xsl:value-of select="$className"/>Event( <xsl:for-each select='arg'> _<xsl:value-of select="@name" />:<xsl:value-of select="@type" />, </xsl:for-each> bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super( EVENT_NAME, bubbles, cancelable );
    		<xsl:for-each select='arg'><xsl:value-of select="@name" /> = _<xsl:value-of select="@name" />;
    		</xsl:for-each>
		}
		<xsl:for-each select='arg'>
		public var	<xsl:value-of select="@name" />:<xsl:value-of select="@type" />;
		</xsl:for-each>
		

		override public function clone() : Event
		{
			return new <xsl:value-of select="$className"/>Event(<xsl:for-each select='arg'> <xsl:value-of select="@name" />, </xsl:for-each> bubbles, cancelable);
		}	
	}
}
</xsl:template> 

<xsl:template name="model">
  package <xsl:value-of select="@namespace" />.models
  {
  <xsl:call-template name="codegen.import.alltypes"/>

  <xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
  <xsl:variable name="className" select = "concat(translate($firstLetter, $up, $lo), substring(@name, 2, 30))"/>
     [Bindable]
     public class <xsl:value-of select="@name"/>Model
     {<xsl:for-each select="method[@type!='void']">
       public var <xsl:value-of select="@name"/>Result : <xsl:value-of select="@type"/>;
      </xsl:for-each>
     }
  }
</xsl:template>

<xsl:template name="presentationmodel">
  package <xsl:value-of select="@namespace" />.models.presentations
  {
     import <xsl:value-of select="@namespace" />.models.<xsl:value-of select="//service/@name" />Model;
  <xsl:call-template name="codegen.import.alltypes"/>


    <xsl:for-each select="method"><xsl:variable name="firstLetter2" select="substring(@name, 1,1)"/><xsl:variable name="methodName" select = "concat(translate($firstLetter2, $lo, $up), substring(@name, 2, 30))"/>
      import <xsl:value-of select="../@namespace" />.events.<xsl:value-of select="$methodName"/>Event;</xsl:for-each>


  <xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
  <xsl:variable name="className" select = "concat(translate($firstLetter, $up, $lo), substring(@name, 2, 30))"/>

    public class <xsl:value-of select="@name"/>PresentationModel
    {
        [Autowire(bean="<xsl:value-of select="$className"/>Model")]
        [Bindable]
        public var <xsl:value-of select="$className"/>Model: <xsl:value-of select="//service/@name"/>Model;


       <xsl:for-each select="method[@type!='void']">
        [Autowire(bean="<xsl:value-of select="$className"/>Model", property="<xsl:value-of select='@name'/>Result")]
        [Bindable]
        public var <xsl:value-of select="@name"/>Result : <xsl:value-of select="@type"/>;
       </xsl:for-each>

        // dispatcher that will be set from constructor (via Prototype tag in Beans.mxml),
        // allowing this non-view class to dispatch bubbling events
        private var dispatcher:IEventDispatcher;

        public function <xsl:value-of select="@name"/>PresentationModel( dispatcher:IEventDispatcher )
        {
         this.dispatcher = dispatcher;
        }

     <xsl:for-each select="method">

         <xsl:variable name="firstLetter2" select="substring(@name, 1,1)"/>
         <xsl:variable name="methodName" select = "concat(translate($firstLetter2, $lo, $up), substring(@name, 2, 30))"/>
       public function <xsl:value-of select="@name"/>( <xsl:for-each select='arg'>
         <xsl:if test="position() != 1">, </xsl:if>
         <xsl:value-of select="@name" />:<xsl:value-of select="@type" />
     </xsl:for-each>):void
       {
         dispatcher.dispatchEvent( new <xsl:value-of select="$methodName"/>Event (<xsl:for-each select='arg'>
             <xsl:if test="position() != 1">, </xsl:if><xsl:value-of select="@name" /></xsl:for-each>));
       }
     </xsl:for-each>
    }
  }
</xsl:template>

</xsl:stylesheet>
