<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.1" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format" exclude-result-prefixes="fo">

    <xsl:output method="xml" version="1.0" omit-xml-declaration="no" indent="yes" />

    <xsl:template match="invoice">
        <fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format">
            <fo:layout-master-set>
                <fo:simple-page-master master-name="simpleA4" page-height="29.7cm" page-width="21cm" margin-top="1cm" margin-bottom="1cm" margin-left="1cm" margin-right="1cm">
                    <fo:region-body />
                </fo:simple-page-master>
            </fo:layout-master-set>
            <fo:page-sequence master-reference="simpleA4">
                <fo:flow flow-name="xsl-region-body">
                    <fo:block text-align="center" font-size="12pt" font-weight="bold">
                        Invoice Work Log
                    </fo:block>
                    <fo:block text-align="center" font-size="12pt" font-weight="bold">
                        <xsl:value-of select="@dateFrom"/> to <xsl:value-of select="@dateTo" />
                    </fo:block>
                    <xsl:apply-templates select="projects" />
                </fo:flow>
            </fo:page-sequence>
        </fo:root>
    </xsl:template>

    <xsl:template match="projects">
        <xsl:apply-templates />
    </xsl:template>

    <xsl:template match="project">
        <fo:block font-size="8pt" font-weight="normal" padding-top="2mm">
            <fo:table table-layout="fixed">
                <fo:table-column column-width="3cm" />
                <fo:table-column />
                <fo:table-body>
                    <fo:table-row>
                        <fo:table-cell>
                            <fo:block font-weight="bold">
                                Project: <xsl:value-of select="@code" />
                            </fo:block>
                        </fo:table-cell>
                        <fo:table-cell>
                            <fo:block font-weight="bold">
                                <xsl:value-of select="@name" />
                            </fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                    <fo:table-row>
                        <fo:table-cell number-columns-spanned="2">
                            <fo:block>
                                <xsl:apply-templates />
                            </fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                </fo:table-body>
            </fo:table>
        </fo:block>
    </xsl:template>

    <xsl:template match="landmans">
        <xsl:apply-templates />
    </xsl:template>

    <xsl:template match="landman">
        <fo:block font-weight="bold" padding-top="2mm">
            Landman: <xsl:value-of select="@lastName" />, <xsl:value-of select="@firstName" />
        </fo:block>
        <fo:table>
            <fo:table-column />
            <fo:table-body>
                <xsl:apply-templates />
            </fo:table-body>
        </fo:table>
    </xsl:template>

    <xsl:template match="item">
        <fo:table-row>
            <fo:table-cell>
                <fo:block>
                    Date: <xsl:value-of select="@date" />, Hours: <xsl:value-of select="@hours" />
                </fo:block>
            </fo:table-cell>
        </fo:table-row>
        <fo:table-row>
            <fo:table-cell>
                <fo:block padding-bottom="2mm">
                    <xsl:value-of select="@log" />
                </fo:block>
            </fo:table-cell>
        </fo:table-row>
    </xsl:template>

</xsl:stylesheet>
