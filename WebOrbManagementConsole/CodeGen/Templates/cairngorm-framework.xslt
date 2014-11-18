<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:codegen="urn:weborb-cogegen-xslt-lib:xslt"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:import href="codegen.xslt"/>
  
  <xsl:template name="codegen.service">

      <folder name="business">
        <file name="{@name}Delegate.as">
          <xsl:call-template name="codegen.description">
            <xsl:with-param name="file-name" select="concat(@name,'Delegate.as')" />
          </xsl:call-template>
          <xsl:call-template name="delegate" />
        </file>
      </folder>
      <folder name="command">
        <xsl:for-each select='method'>
          <file name="{@name}Command.as">
            <xsl:call-template name="codegen.description">
              <xsl:with-param name="file-name" select="concat(@name,'Command.as')" />
            </xsl:call-template>
            <xsl:call-template name="command" />
          </file>
        </xsl:for-each>
      </folder>
      <xsl:call-template name="codegen.vo.folder" />
  </xsl:template>
  
  <xsl:template name="delegate">
    package <xsl:value-of select="@namespace" />.business
    {
    import mx.rpc.events.ResultEvent;
    import mx.rpc.AsyncToken;
    import com.adobe.cairngorm.business.*;

    public class <xsl:value-of select="@name"/>Delegate
    {
      private var responder : Responder;
      private var service : Object;

      public <xsl:value-of select="@name"/>(responder : Responder )
      {
        this.service = ServiceLocator.getInstance().getService( "<xsl:value-of select='@name'/>" );
        this.responder = responder;
      }
      
      <xsl:for-each select='method'>
      public function <xsl:value-of select='@name' />(<xsl:for-each select="arg">
        <xsl:if test="position() != 1">,</xsl:if>
        <xsl:value-of select="@name"/>:<xsl:value-of select="@type" />
      </xsl:for-each>) : Void
      {
        var call = service.getProducts(<xsl:for-each select="arg">
        <xsl:if test="position() != 1">,</xsl:if>
        <xsl:value-of select="@name"/>
      </xsl:for-each>);
      
        call.resultHandler = Delegate.create( responder, responder.onResult );
        call.faultHandler = Delegate.create( responder, responder.onFault );
      }
      </xsl:for-each>

    }

    }    
  </xsl:template>

  <xsl:template name="command">
    package <xsl:value-of select="../@namespace" />.command
    {
    import com.adobe.cairngorm.business.Responder;
    import com.adobe.cairngorm.commands.Command;
    import com.adobe.cairngorm.control.CairngormEvent;
    import mx.controls.Alert;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    
    public class <xsl:value-of select="@name"/>Command implements Command, Responder
    {
    private var responder : Responder;
    private var service : Object;

    public execute(event : event:CairngormEvent)
    {
    var delegate : <xsl:value-of select="../@name"/>Delegate = new <xsl:value-of select="../@name"/>Delegate( this );
    delegate.<xsl:value-of select="@name"/>();
    }

    public function onResult( event:ResultEvent ) : Void
    {
    <xsl:if test="@type != 'void'">
      var returnValue:<xsl:value-of select="@type" /> = event.result as <xsl:value-of select="@type" />;
    </xsl:if>
    }

    public function onFault( event:ResultEvent ) : Void
    {
      Alert.show(event.fault.faultString, "Error");
    }

    }

    }
  </xsl:template>

</xsl:stylesheet>
