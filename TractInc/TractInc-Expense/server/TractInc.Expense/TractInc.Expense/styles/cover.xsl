<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.1" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format" exclude-result-prefixes="fo">

    <xsl:output method="xml" version="1.0" omit-xml-declaration="no" indent="yes" />

    <xsl:template match="cover">
        <fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format">
            <fo:layout-master-set>
                <fo:simple-page-master master-name="simpleA4" page-height="29.7cm" page-width="21cm" margin-top="1cm" margin-bottom="1cm" margin-left="1cm" margin-right="1cm">
                    <fo:region-body />
                </fo:simple-page-master>
            </fo:layout-master-set>
            <fo:page-sequence master-reference="simpleA4">
                <fo:flow flow-name="xsl-region-body">
                    <fo:block font-family="Times">
                        <fo:table table-layout="fixed">
                            <fo:table-column />
                            <fo:table-column />
                            <fo:table-body>
                                <fo:table-row>
                                    <fo:table-cell column-width="5cm">
                                        <fo:block text-align="center">
                                            <fo:external-graphic width="80" height="80">
                                                <xsl:attribute name="src"><xsl:value-of select="@dir" />logo.jpg</xsl:attribute>
                                            </fo:external-graphic>
                                        </fo:block>
                                        <fo:block font-size="12pt" font-weight="bold">
                                            Invoice # <xsl:value-of select="@invoice" />
                                        </fo:block>
                                        <fo:block font-size="12pt" font-weight="bold">
                                            Billed to: <xsl:value-of select="@companyName" />
                                        </fo:block>
                                        <fo:block font-size="12pt" font-weight="bold">
                                        </fo:block>
                                        <fo:block font-size="12pt" font-weight="bold">
                                            <xsl:value-of select="@address" />
                                        </fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                        <fo:block text-align="center" font-size="12pt" font-weight="bold">
                                            TRACT RESEARCH AND ACQUISITION COMPANY OF TEXAS, INC.
                                        </fo:block>
                                        <fo:block text-align="center" font-size="12pt" font-weight="bold">
                                            PO Box 230
                                        </fo:block>
                                        <fo:block text-align="center" font-size="12pt" font-weight="bold">
                                            Bullard, TX 75757
                                        </fo:block>
                                        <fo:block text-align="center" font-size="12pt" font-weight="bold" padding-top="8pt">
                                            Invoice Date <xsl:value-of select="@date" />
                                        </fo:block>
                                        <fo:block text-align="center" font-size="12pt" font-weight="bold" padding-top="8pt">
                                            Starting Date <xsl:value-of select="@dateFrom" />
                                        </fo:block>
                                        <fo:block text-align="center" font-size="12pt" font-weight="bold">
                                            Ending Date <xsl:value-of select="@dateTo" />
                                        </fo:block>
                                    </fo:table-cell>
                                </fo:table-row>
                            </fo:table-body>
                        </fo:table>
                        <xsl:apply-templates select="landmans" />
                        <xsl:apply-templates select="types" />
                    </fo:block>
                </fo:flow>
            </fo:page-sequence>
        </fo:root>
    </xsl:template>

    <xsl:template match="landmans">
        <fo:block font-size="10pt" padding-top="5mm">
            <fo:table table-layout="fixed">
                <fo:table-column column-width="8cm" />
                <fo:table-column />
                <fo:table-column column-width="20pt" />
                <fo:table-column />
                <fo:table-column />
                <fo:table-body>
                    <fo:table-row>
                        <fo:table-cell>
                            <fo:block>LANDMAN</fo:block>
                        </fo:table-cell>
                        <fo:table-cell />
                        <fo:table-cell />
                        <fo:table-cell />
                        <fo:table-cell />
                    </fo:table-row>
                    <xsl:apply-templates />
                    <fo:table-row>
                        <fo:table-cell />
                        <fo:table-cell />
                        <fo:table-cell />
                        <fo:table-cell />
                        <fo:table-cell />
                    </fo:table-row>
                    <fo:table-row>
                        <fo:table-cell>
                            <fo:block font-weight="bold">Landman Totals:</fo:block>
                        </fo:table-cell>
                        <fo:table-cell>
                            <fo:block font-weight="bold" text-align="right"><xsl:value-of select="@days" /> days</fo:block>
                        </fo:table-cell>
                        <fo:table-cell>
                            <fo:block font-weight="bold" text-align="center">@</fo:block>
                        </fo:table-cell>
                        <fo:table-cell>
                            <fo:block font-weight="bold" text-align="right"><xsl:value-of select="@rate" /> = </fo:block>
                        </fo:table-cell>
                        <fo:table-cell>
                            <fo:block font-weight="bold" text-align="right"><xsl:value-of select="@amount" /></fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                </fo:table-body>
            </fo:table>
        </fo:block>
    </xsl:template>

    <xsl:template match="landman">
        <fo:table-row>
            <fo:table-cell>
                <fo:block><xsl:value-of select="@lastName" />, <xsl:value-of select="@firstName" /></fo:block>
            </fo:table-cell>
            <fo:table-cell>
                <fo:block text-align="right"><xsl:value-of select="@days" /> days</fo:block>
            </fo:table-cell>
            <fo:table-cell>
                <fo:block text-align="center">@</fo:block>
            </fo:table-cell>
            <fo:table-cell>
                <fo:block text-align="right"><xsl:value-of select="@rate" /> = </fo:block>
            </fo:table-cell>
            <fo:table-cell>
                <fo:block text-align="right"><xsl:value-of select="@amount" /></fo:block>
            </fo:table-cell>
        </fo:table-row>
    </xsl:template>

    <xsl:template match="types">
        <fo:block font-size="10pt" font-weight="bold" break-before="page">
            <fo:block text-align="center">TRACT RESEARCH AND ACQUISITION COMPANY OF TEXAS, INC.</fo:block>
            <fo:block text-align="center">PO Box 230</fo:block>
            <fo:block text-align="center">Bullard, TX 75757</fo:block>
            <fo:block />
            <fo:block>
                <fo:table table-layout="fixed">
                    <fo:table-column />
                    <fo:table-column />
                    <fo:table-body>
                        <xsl:apply-templates />
                        <fo:table-row>
                            <fo:table-cell><fo:block>---</fo:block></fo:table-cell>
                            <fo:table-cell />
                        </fo:table-row>
                        <fo:table-row>
                            <fo:table-cell>
                                <fo:block>Total Expense</fo:block>
                            </fo:table-cell>
                            <fo:table-cell>
                                <fo:block text-align="right">
                                    <xsl:value-of select="@totalExpense" />
                                </fo:block>
                            </fo:table-cell>
                        </fo:table-row>
                        <fo:table-row>
                            <fo:table-cell>
                                <fo:block>Landman Totals:</fo:block>
                            </fo:table-cell>
                            <fo:table-cell>
                                <fo:block text-align="right">
                                    <xsl:value-of select="@landmanTotals" />
                                </fo:block>
                            </fo:table-cell>
                        </fo:table-row>
                        <fo:table-row>
                            <fo:table-cell>
                                <fo:block text-align="center">GRAND TOTAL</fo:block>
                            </fo:table-cell>
                            <fo:table-cell>
                                <fo:block text-align="center">
                                    <xsl:value-of select="@grandTotal" />
                                </fo:block>
                            </fo:table-cell>
                        </fo:table-row>
                    </fo:table-body>
                </fo:table>
            </fo:block>
        </fo:block>
        <fo:block text-align="center">PAYMENT DUE UPON RECEIPT</fo:block>
    </xsl:template>

    <xsl:template match="type">
        <fo:table-row>
            <fo:table-cell>
                <fo:block>
                    <xsl:value-of select="@description" />
                </fo:block>
            </fo:table-cell>
            <fo:table-cell>
                <fo:block text-align="right">
                    <xsl:value-of select="@amount" />
                </fo:block>
            </fo:table-cell>
        </fo:table-row>
    </xsl:template>

</xsl:stylesheet>
