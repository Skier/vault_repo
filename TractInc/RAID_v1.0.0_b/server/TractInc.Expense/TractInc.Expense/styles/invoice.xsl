<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.1" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format" exclude-result-prefixes="fo">

    <xsl:output method="xml" version="1.0" omit-xml-declaration="no" indent="yes" />

    <xsl:template match="invoice">
        <fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format">
            <fo:layout-master-set>
                <fo:simple-page-master master-name="simpleA4" page-height="21cm" page-width="29.7cm" margin-top="1cm" margin-bottom="1cm" margin-left="1cm" margin-right="1cm">
                    <fo:region-body />
                </fo:simple-page-master>
            </fo:layout-master-set>
            <fo:page-sequence master-reference="simpleA4">
                <fo:flow flow-name="xsl-region-body">
                    <fo:block text-align="center" font-size="12pt" font-weight="bold">
                        Broker Invoice
                    </fo:block>
                    <fo:block text-align="center" font-size="12pt" font-weight="bold">
                        <xsl:value-of select="@dateFrom"/> to <xsl:value-of select="@dateTo" />
                    </fo:block>
                    <fo:block font-size="12pt" font-weight="bold">
                        Invoice No. <xsl:value-of select="@invoice" /> XTO
                    </fo:block>
                    <xsl:apply-templates select="afes" />
                </fo:flow>
            </fo:page-sequence>
        </fo:root>
    </xsl:template>

    <xsl:template match="afes">
        <xsl:apply-templates />
    </xsl:template>

    <xsl:template match="afe">
        <fo:block font-size="8pt" font-weight="bold" padding-top="2mm">
            <fo:table table-layout="fixed">
                <fo:table-column column-width="3cm" />
                <fo:table-column />
                <fo:table-body>
                    <fo:table-row>
                        <fo:table-cell>
                            <fo:block>
                                AFE: <xsl:value-of select="@code" />
                            </fo:block>
                        </fo:table-cell>
                        <fo:table-cell>
                            <fo:block>
                                <xsl:value-of select="@name" />
                            </fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                    <fo:table-row>
                        <fo:table-cell number-columns-spanned="2">
                            <fo:block>
                                <fo:table table-layout="fixed">
                                    <fo:table-column column-width="2.8cm" />
                                    <fo:table-column />
                                    <fo:table-column />
                                    <xsl:for-each select="//types/type">
                                        <xsl:if test="not(@id = 1)">
                                            <fo:table-column />
                                        </xsl:if>
                                    </xsl:for-each>
                                    <fo:table-column />
                                    <fo:table-body>
                                        <fo:table-row>
                                            <fo:table-cell>
                                                <fo:block font-weight="normal" text-align="left">Landman</fo:block>
                                            </fo:table-cell>
                                            <fo:table-cell>
                                                <fo:block font-weight="normal" text-align="center">Days</fo:block>
                                            </fo:table-cell>
                                            <fo:table-cell>
                                                <fo:block font-weight="normal" text-align="center">Cost</fo:block>
                                            </fo:table-cell>
                                            <xsl:apply-templates select="//types/type" />
                                            <fo:table-cell>
                                                <fo:block font-weight="normal" text-align="center">Total</fo:block>
                                            </fo:table-cell>
                                        </fo:table-row>
                                        <xsl:apply-templates select="landmans" />
                                        <fo:table-row>
                                            <fo:table-cell>
                                                <fo:block font-weight="normal" text-align="left">
                                                    <xsl:value-of select="@code" />: <xsl:value-of select="@name" />
                                                </fo:block>
                                            </fo:table-cell>
                                        </fo:table-row>
                                    </fo:table-body>
                                </fo:table>
                            </fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                </fo:table-body>
            </fo:table>
        </fo:block>
    </xsl:template>

    <xsl:template match="type">
        <xsl:if test="not(@id = 1)">
            <fo:table-cell>
                <fo:block font-weight="normal" text-align="center">
                    <xsl:value-of select="@name" />
                </fo:block>
            </fo:table-cell>
        </xsl:if>
    </xsl:template>

    <xsl:template match="landmans">
        <xsl:apply-templates />
    </xsl:template>

    <xsl:template match="landman">
        <fo:table-row>
            <fo:table-cell>
                <fo:block font-weight="normal">
                    <xsl:value-of select="@lastName" />, <xsl:value-of select="@firstName" />
                </fo:block>
            </fo:table-cell>
            <fo:table-cell>
                <fo:block font-weight="normal" text-align="center">
                    <xsl:value-of select="@days" />
                </fo:block>
            </fo:table-cell>
            <fo:table-cell>
                <fo:block font-weight="normal" text-align="center">
                    <xsl:value-of select="@cost" />
                </fo:block>
            </fo:table-cell>
            <xsl:apply-templates select="item">
                <xsl:sort select="@typeId" order="ascending" data-type="number" />
            </xsl:apply-templates>
            <fo:table-cell>
                <fo:block font-weight="normal" text-align="center">
                    <xsl:value-of select="@total" />
                </fo:block>
            </fo:table-cell>
        </fo:table-row>
        <fo:table-row>
            <fo:table-cell />
            <fo:table-cell number-columns-spanned="3">
                <fo:block font-size="6pt" font-weight="bold" padding-bottom="1mm">Associated Project: (none) </fo:block>
            </fo:table-cell>
            <fo:table-cell number-columns-spanned="2">
                <fo:block font-size="6pt" font-weight="bold" padding-bottom="1mm">A.P. Day Qty: 0</fo:block>
            </fo:table-cell>
        </fo:table-row>
    </xsl:template>

    <xsl:template match="item">
        <fo:table-cell>
            <fo:block font-weight="normal" text-align="right">
                <xsl:value-of select="@cost" />
            </fo:block>
        </fo:table-cell>
    </xsl:template>

</xsl:stylesheet>
