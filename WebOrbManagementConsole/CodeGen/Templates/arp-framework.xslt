<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:codegen="urn:weborb-cogegen-xslt-lib:xslt"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:import href="codegen.xslt"/>
  <xsl:import href="codegen.invoke.xslt"/>
  
  <xsl:template name="codegen.file-name"><xsl:value-of select="@name"/>.arp-file-set.as</xsl:template>

  <xsl:template name="codegen.service">
    <xsl:call-template name="codegen.code" />
    <xsl:if test="method[@containsvalues=1]">

      <folder name="testdrive">
        <xsl:for-each select="method[@containsvalues=1]">
          <file name="{@name}Invoke.as">
            <xsl:call-template name="codegen.description">
              <xsl:with-param name="file-name" select="concat(@name,'Invoke.as')" />
            </xsl:call-template>

            package
            {
            var service:<xsl:value-of select="../@name"/> = new <xsl:value-of select="../@name"/>();

            <xsl:call-template name="codegen.invoke.method" />
            }
          </file>
        </xsl:for-each>
      </folder>

    </xsl:if>

    <xsl:call-template name="codegen.vo.folder" />
  </xsl:template>
  
  <xsl:template name="codegen.code">
    <folder name="business">
      <xsl:call-template name="service-locator" />
      <xsl:for-each select="method">
        <xsl:call-template name="delegate" />
      </xsl:for-each>
    </folder>
    <folder name="control">
      <xsl:call-template name="controller" />
    </folder>
    <folder name="command">
      <xsl:for-each select="method">
      <xsl:call-template name="command" />      
    </xsl:for-each>      
    </folder>
  </xsl:template>

<xsl:template name="service-locator">
  <file name="ServiceLocator.as">
    <xsl:call-template name="codegen.description">
      <xsl:with-param name="file-name" select="'ServiceLocator.as'" />
    </xsl:call-template>
    package <xsl:value-of select="@namespace" />.business
    {
    import mx.utils.Delegate;
    import mx.remoting.Service;
    import mx.remoting.debug.NetDebug;
    import com.ariaware.arp.ServiceLocatorTemplate;

    class ServiceLocator extends ServiceLocatorTemplate
    {
    private static var weborbUrl:String = "<xsl:value-of select='@url'/>";
    private static var s_instance:ServiceLocator;

    private function ServiceLocator()
    {
      super();
      
      // debug
      NetDebug.initialize();
    }

    public static function getInstance():ServiceLocator
    {
      if(s_instance == null)
        s_instance = new ServiceLocator();

      return s_instance;
    }

    public function addServices():Void
    {
    var service:RemotingService = new Service(weborbUrl, null, "<xsl:value-of select='@name'/>",null,null);
    addService("<xsl:value-of select='@name'/>", service);
    }

    public function getService ( serviceName:String )
    {
      // Get the service instance
      var theService = super.getService ( serviceName );
      //
      // Do some additional validation that is specific to our application.
      //
      if ( theService instanceof mx.remoting.Service )
      {
      return mx.remoting.Service ( theService );
      }
      else
      {
      trace ("Service Locator Error: Unknown service type requested - "+serviceName);
      }
    }
    
    }
    }
  </file>
  </xsl:template>

  <xsl:template name='controller'>
    <file name='{@name}Controller.as'>
      <xsl:call-template name="codegen.description">
        <xsl:with-param name="file-name" select="concat(@name,'Controller.as')" />
      </xsl:call-template>
      package <xsl:value-of select="@namespace" />.control
      {
      import com.ariaware.arp.ControllerTemplate;

      import command.*;
      import view.*;

      class <xsl:value-of select='@name'/>Controller extends ControllerTemplate
      {
      private static var s_instance:Controller;

      private function addEventListeners ()
      {
      //
      // Listen for events from the view. To separate screens may dispatch
      // the same event and these will be handled by the same event handler.
      // No two screens should use the same event for different purposes.
      //
      }

      private function addCommands ()
      {
      <xsl:for-each select="method">
        addCommand ( "<xsl:value-of select='@name'/>", <xsl:value-of select='@name'/>Command );
      </xsl:for-each>
      }

      public static function getInstance ( appRef )
      {
        if ( s_instance == null )
        {
          s_instance = new Controller();
          s_instance.registerApp ( appRef );
        }
        else
          return s_instance;
      }

      }
    }
    </file>
  </xsl:template>

  <xsl:template name='delegate'>
    <file name="{@name}Delegate.as">
      <xsl:call-template name="codegen.description">
        <xsl:with-param name="file-name" select="concat(@name,'delegate.as')" />
      </xsl:call-template>
      package <xsl:value-of select="../@namespace" />.business
      {
      import mx.remoting.Service;

      import mx.remoting.PendingCall;

      import mx.rpc.Responder;
      import <xsl:value-of select="../@namespace" />.command.*;
      
      <xsl:value-of select="../@namespace" />.vo.*;

      class <xsl:value-of select="@name"/>Delegate
      {
        var serviceLocator:ServiceLocator;
        var responder:Responder;
        var service:mx.remoting.Service;

        function <xsl:value-of select="@name"/>Delegate(responder:Responder)
        {
          this.responder = responder;
          serviceLocator = ServiceLocator.getInstance();

          service = serviceLocator.getService("<xsl:value-of select='../@name' />");

        }
        function <xsl:value-of select='@name'/>(<xsl:for-each select="arg">
      <xsl:if test="position() != 1">,</xsl:if>
      <xsl:value-of select="@name"/>:<xsl:value-of select="@type" />
    </xsl:for-each>)
      {
        var pendingCall:PendingCall = service.<xsl:value-of select="@name"/>(<xsl:for-each select="arg">
      <xsl:if test="position() != 1">,</xsl:if>
      <xsl:value-of select="@name"/>
    </xsl:for-each>);
      pendingCall.responder = responder;
      }
      }
      }
    </file>  
  </xsl:template>
  
  <xsl:template name="command">
    <file name="{@name}Command.as">

      <xsl:call-template name="codegen.description">
        <xsl:with-param name="file-name" select="concat(@name,'Command.as')" />
      </xsl:call-template>
      package <xsl:value-of select="../@namespace" />.command
      {
      import com.ariaware.arp.CommandTemplate;

      import mx.screens.Form;
      import mx.rpc.ResultEvent;
      import mx.rpc.FaultEvent;
      <xsl:value-of select="../@namespace" />.vo.*;
      <xsl:value-of select="../@namespace" />.business.*;
      
      class <xsl:value-of select="@name" />Command
      extends CommandTemplate
      implements mx.rpc.Responder
      {
      private var m_view:Object;

      var m_<xsl:value-of select="@name" />Delegate:<xsl:value-of select="@name" />Delegate;

      public function executeOperation():Void
      {
        m_<xsl:value-of select="@name" />Delegate: = new <xsl:value-of select="@name" />Delegate ( this );

        m_<xsl:value-of select="@name" />Delegate.<xsl:value-of select="@name" />(<xsl:for-each select="arg">
          <xsl:if test="position() != 1">,</xsl:if>
          viewRef.get<xsl:value-of select="@name"/>()
        </xsl:for-each>);
      }

      public function onResult(re:ResultEvent):Void
      {
      <xsl:if test="@type != 'void'">
        var returnValue:<xsl:value-of select="@type" /> = event.result as <xsl:value-of select="@type" />;
      </xsl:if>
      }

      public function onStatus(fe:FaultEvent):Void
      {
      throw new Error ("Command failed: " + fe.fault.description);
      }
      }
      }
    </file>
  </xsl:template>
</xsl:stylesheet>
