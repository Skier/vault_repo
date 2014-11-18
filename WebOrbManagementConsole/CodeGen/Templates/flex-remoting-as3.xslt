<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:codegen="urn:weborb-cogegen-xslt-lib:xslt"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:import href="codegen.xslt"/>
  <xsl:import href="codegen.invoke.xslt"/>

  <xsl:template name="codegen.info">
    Code instruction text
  </xsl:template>
  
  <xsl:template name="codegen.service">
    <folder name="service">
      <file name="{@name}.as">
        <xsl:call-template name="codegen.code" />
      </file>
    </folder>
    <xsl:if test="method[@containsvalues=1]">

    <folder name="testdrive">
      <xsl:for-each select="method[@containsvalues=1]">
        <file name="{@name}Invoke.as">
          <xsl:call-template name="codegen.description">
            <xsl:with-param name="file-name" select="concat(@name,'Invoke.as')" />
          </xsl:call-template>

      package <xsl:value-of select="../@namespace" />.testdrive
      {
        import <xsl:value-of select="../@namespace" />.vo.*;
        import <xsl:value-of select="../@namespace" />.service.*;
        
        public class <xsl:value-of select="@name" />Invoke
        {
          var service:<xsl:value-of select="../@name"/> = new <xsl:value-of select="../@name"/>();
        
          public function Execute():void
          {
            <xsl:call-template name="codegen.invoke.method" />
          }
        }
      }
        </file>   
      </xsl:for-each>
    </folder>

    </xsl:if>

     <xsl:call-template name="codegen.vo.folder" />
  </xsl:template>


  <xsl:template name="codegen.invoke.method.name">
    service.<xsl:value-of select="@name"/>
  </xsl:template>
  
  <xsl:template name="codegen.code">
    <xsl:call-template name="codegen.description">
      <xsl:with-param name="file-name" select="concat(@name,'.as')" />
    </xsl:call-template>
    package <xsl:value-of select="@namespace" />.service
    {
    import mx.rpc.remoting.RemoteObject;
    import mx.controls.Alert;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import <xsl:value-of select="@namespace" />.vo.*;
    
    
    public class <xsl:value-of select="@name"/>
    {
      private var remoteObject:RemoteObject;

      public function <xsl:value-of select="@name"/>()
      {
        remoteObject  = new RemoteObject("GenericDestination");
        remoteObject.source = "<xsl:value-of select='@fullname'/>";
        <xsl:for-each select="method">
        remoteObject.<xsl:value-of select="@name" />.addEventListener("result",<xsl:value-of select="@name" />Handler);
        </xsl:for-each>
        remoteObject.addEventListener("fault", onFault);
      }

    <xsl:for-each select="method">
      public function <xsl:value-of select="@name"/>(<xsl:for-each select="arg">
        <xsl:if test="position() != 1">,</xsl:if>
        <xsl:value-of select="@name"/>:<xsl:value-of select="@type" />
      </xsl:for-each>):void
      {
        remoteObject.<xsl:value-of select="@name"/>(<xsl:for-each select="arg">
          <xsl:if test="position() != 1">,</xsl:if>
          <xsl:value-of select="@name"/>
        </xsl:for-each>);
      }
    </xsl:for-each>
    
    <xsl:for-each select="method">     
      public virtual function <xsl:value-of select="@name" />Handler(event:ResultEvent):void
      {
        <xsl:if test="@type != 'void'">
          var returnValue:<xsl:value-of select="@type" /> = event.result as <xsl:value-of select="@type" />;
        </xsl:if>
      }
    </xsl:for-each>
      public function onFault (event:ResultEvent):void
      {
        Alert.show(event.fault.faultString, "Error");
      }
    }

    }
  </xsl:template>

</xsl:stylesheet>
