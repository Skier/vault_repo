<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:codegen="urn:cogegen-xslt-lib:xslt"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:import href="codegen.xslt"/>
  
  <xsl:variable name="up" select="'ABCDEFGHIJKLMNOPQRSTUVWXYZ'"/>
  <xsl:variable name="lo" select="'abcdefghijklmnopqrstuvwxyz'"/>

  <xsl:template name="codegen.appmain">
    <file name="main.mxml">
&lt;mx:Application xmlns:mx="http://www.adobe.com/2006/mxml" layout="absolute" initialize="onLoad()"
xmlns:maps = "<xsl:value-of select="//service/@namespace"/>.maps.*"
xmlns:views="<xsl:value-of select="//service/@namespace"/>.views.*">
&lt;mx:Script>
    &lt;![CDATA[
    import <xsl:value-of select="//service/@namespace"/>.DataTypeInitializer;
    public function onLoad():void
    {
      new DataTypeInitializer();
    }
    ]]&gt;
    &lt;/mx:Script>

&lt;maps:<xsl:value-of select="//service/@name"/>EventMap />
&lt;views:<xsl:value-of select="//service/@name"/>Panel />

&lt;/mx:Application>
    </file>
  </xsl:template>

 <xsl:template name="codegen.service">
   <xsl:call-template name="codegen.datatypeinitializer"/>
   <folder name="business">
      <file name="{@name}Manager.as">
        <xsl:call-template name="codegen.description">
          <xsl:with-param name="file-name" select="concat(@name,'Manager.as')" />
        </xsl:call-template>
        <xsl:call-template name="manager" />
      </file>
    </folder>


    <folder name="views">
      <xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
      <xsl:variable name="className" select = "concat(translate($firstLetter, $lo, $up), substring(@name, 2, 30))"/>

      <file name="{$className}Panel.mxml" type="xml">
<mx:Panel xmlns:mx="http://www.adobe.com/2006/mxml" layout="absolute" width="800" height="600">

</mx:Panel>
      </file>      
    </folder>

	  <folder name="maps">	  
	   <xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
	   <xsl:variable name="className" select = "concat(translate($firstLetter, $lo, $up), substring(@name, 2, 30))"/>
			
        <file name="{$className}EventMap.mxml" type="xml">
&lt;!--
          <xsl:call-template name="codegen.description">
            <xsl:with-param name="file-name" select="concat($className,'EventMap.mxml')" />
          </xsl:call-template>
-->
          <xsl:call-template name="eventMap" />
        </file>
      </folder>
	  	  
      <folder name="events">
        <xsl:for-each select='method'>
  			<xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
  			<xsl:variable name="className" select = "concat(translate($firstLetter, $lo, $up), substring(@name, 2, 30))"/>
  		
        <file name="{$className}Event.as">
          <xsl:call-template name="codegen.description">
            <xsl:with-param name="file-name" select="concat($className,'Event.as')" />
          </xsl:call-template>
          <xsl:call-template name="event" />
        </file>
		  
		    <file name="{$className}FaultEvent.as">
            <xsl:call-template name="codegen.description">
              <xsl:with-param name="file-name" select="concat($className,'FaultEvent.as')" />
            </xsl:call-template>
            <xsl:call-template name="faultEvent" />
          </file>
        </xsl:for-each>
      </folder>
	 
  </xsl:template>

 <xsl:template name="manager">
	package <xsl:value-of select="//service/@namespace" />.business
	{
		<xsl:call-template name="codegen.import.alltypes"/>

        [Bindable]		
		public class <xsl:value-of select="//service/@name"/>Manager
		{		
			<xsl:for-each select="method">
            <xsl:if test="@type!='void'">		
			public var resultOf<xsl:value-of select="@name"/>:<xsl:value-of select="@type"/>;
            </xsl:if>
        
            public function <xsl:value-of select="@name"/>Handler(<xsl:if test="@type!='void'">value:<xsl:value-of select="@type"/></xsl:if>):void
            {
                <xsl:if test="@type!='void'">
                resultOf<xsl:value-of select="@name"/> = value;
                </xsl:if>
            }
	
		</xsl:for-each>
		
		}
	}
    </xsl:template>

 <xsl:template name="event"> 
package <xsl:value-of select="../@namespace" />.events
{
	import flash.events.Event;
 <xsl:call-template name="codegen.import.alltypes"/>

	<xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
	<xsl:variable name="className" select = "concat(translate($firstLetter, $lo, $up), substring(@name, 2, 30))"/>
	
	public class <xsl:value-of select="$className"/>Event extends Event
	{
		public static const EventName:String = '<xsl:value-of select="@name"/>Request';
	
		public function <xsl:value-of select="$className"/>Event( type:String, <xsl:for-each select='arg'> <xsl:value-of select="@name" />:<xsl:value-of select="@type" />, </xsl:for-each> bubbles:Boolean=true, cancelable:Boolean=false)
		{
			super( type, bubbles, cancelable );		
			<xsl:for-each select='arg'>
			_<xsl:value-of select="@name" /> = <xsl:value-of select="@name" />;
			</xsl:for-each>
		}
		<xsl:for-each select='arg'>
		private var	_<xsl:value-of select="@name" />:<xsl:value-of select="@type" />;
		</xsl:for-each>
		<xsl:for-each select='arg'>
		public function get <xsl:value-of select="@name" />():<xsl:value-of select="@type" />
		{
			return _<xsl:value-of select="@name" />;
		}
		</xsl:for-each>
	}
}
 </xsl:template>
 
 <xsl:template name="faultEvent">  
	package <xsl:value-of select="../@namespace" />.events
	{
		import flash.events.Event;
		import mx.rpc.Fault;
		 <xsl:call-template name="codegen.import.alltypes"/>
		
		<xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
		<xsl:variable name="className" select = "concat(translate($firstLetter, $lo, $up), substring(@name, 2, 30))"/>

		public class <xsl:value-of select="$className" />FaultEvent extends Event
		{
			public static const FAULT_<xsl:value-of select="@name"/>:String = 'fault<xsl:value-of select="@name"/>';
		
			public function <xsl:value-of select="$className"/>FaultEvent(type:String, fault:Fault, bubbles:Boolean=true, cancelable:Boolean=false)
			{
				super(type, bubbles, cancelable);
				_fault = fault;
			}
		
			private var _fault:Fault;

			public function get fault():Fault
			{
				return _fault;
			}		
		}
	}
  </xsl:template>
  
 <xsl:template name="eventMap">  
 
<EventMap xmlns:mx="http://www.adobe.com/2006/mxml" xmlns="http://mate.asfusion.com/">
 
		<mx:Script>	
	
		<xsl:text disable-output-escaping="yes">&lt;![CDATA[ </xsl:text>
		
		import <xsl:value-of select="@namespace" />.views.*;
		import <xsl:value-of select="@namespace" />.business.*;
		import <xsl:value-of select="@namespace" />.events.*;
		
		<xsl:text disable-output-escaping="yes"> ]]&gt;</xsl:text>
		
		</mx:Script>

		
		 <xsl:for-each select='method'>
		 
		<xsl:variable name="firstLetter" select="substring(@name, 1,1)"/>
		<xsl:variable name="className" select = "concat(translate($firstLetter, $lo, $up), substring(@name, 2, 30))"/>
		<xsl:variable name="classEventGET" select = "concat('{', $className, 'Event.EventName', '}')"/>
		
		<xsl:variable name="manager" select = "concat('{', //service/@name, 'Manager', '}')"/>
		<xsl:variable name="methodResultHandler" select = "concat(@name, 'Handler')"/>
		
		<xsl:variable name="panel" select="concat('{', //service/@name, 'Panel', '}')"/>
		<xsl:variable name="argStructure" select="concat('{', @argStructure, '}')"/>

		<xsl:variable name="fullName" select = "//service/@fullname"/>
		<xsl:variable name="resultObject" select = "concat('{', 'resultObject', '}')" />

		<EventHandlers type="{$classEventGET}" debug="true">

			&lt;RemoteObjectInvoker destination="GenericDestination" source='<xsl:value-of select="$fullName"/>'
			method='<xsl:value-of select="@name"/>'
			arguments= "{[<xsl:for-each select="arg/@name">event.<xsl:value-of select="."/><xsl:if test="position()!=last()">,</xsl:if>
    </xsl:for-each>]}"
			debug="true">
			
			<resultHandlers>
				<MethodInvoker generator="{$manager}" 
					method="{$methodResultHandler}" arguments="{$resultObject}"/>
			</resultHandlers>
			
			&lt;/RemoteObjectInvoker>
		</EventHandlers> 


        <xsl:if test="@type!='void'">
    	<Injectors target="{$panel}">
    		<PropertyInjector targetKey="resultOf{@name}" source="{$manager}" sourceKey="resultOf{@name}" />
    	</Injectors>
        </xsl:if>
	
	</xsl:for-each>
	
</EventMap>

  </xsl:template>

</xsl:stylesheet>
