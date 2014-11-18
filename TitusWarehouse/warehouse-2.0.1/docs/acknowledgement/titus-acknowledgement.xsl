<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
    <xsl:output version="1.0" method="xml" encoding="UTF-8" indent="no"/>
    <xsl:variable name="fo:layout-master-set">
        <fo:layout-master-set>
            <fo:simple-page-master master-name="default-page" 
                page-height="297mm" page-width="210mm" 
                margin-top="3mm" margin-bottom="3mm"
                margin-left="3mm" margin-right="3mm">
            <fo:region-body margin-top="3mm" margin-left="3mm" />
            </fo:simple-page-master>
        </fo:layout-master-set>
    </xsl:variable>
    <xsl:template match="/">
        <fo:root font-size="10pt" font-weight="bold">
            <xsl:copy-of select="$fo:layout-master-set"/>
            <fo:page-sequence master-reference="default-page" initial-page-number="1" format="1">
                <fo:flow flow-name="xsl-region-body">
                    <fo:block>
                        <xsl:for-each select="acknowledgement">
                            <fo:table table-layout="fixed" border-collapse="separate">
                                <fo:table-column column-width="100mm"/>
                                <fo:table-column column-width="100mm"/>
                                <fo:table-body>
                                    <fo:table-row>
                                        <fo:table-cell number-columns-spanned="2">
                                            <fo:block>
                                                <fo:external-graphic content-width="30mm" 
src="D:\customers\affilia\ASC\server\acknowledgements\titus-logo.jpg"/>
                                            </fo:block>
                                        </fo:table-cell>
                                    </fo:table-row>
                                    <fo:table-row>
                                        <fo:table-cell font-size="16pt" border-bottom-style="solid" border-bottom-width="1pt" border-bottom-color="#C0C0C0">
                                            <fo:block color="#2e3d82">
                                                <fo:inline>
                                                    <xsl:text>Sales Order Acknowledgement</xsl:text>
                                                </fo:inline>
                                            </fo:block>
                                        </fo:table-cell>
                                        <fo:table-cell display-align="after" border-bottom-style="solid" border-bottom-width="1pt" border-bottom-color="#C0C0C0">
                                            <fo:block text-align="right" color="#2e3d82">
                                                <fo:inline>
                                                    <xsl:text>Thank you for your order.</xsl:text>
                                                </fo:inline>
                                            </fo:block>
                                        </fo:table-cell>
                                    </fo:table-row>
                                </fo:table-body>
                            </fo:table>
                            <fo:block  text-align="center" padding-before="6mm">
                                <fo:table table-layout="fixed" border-collapse="separate">
                                    <fo:table-column column-width="50mm"/>
                                    <fo:table-column column-width="10mm"/>
                                    <fo:table-column column-width="30mm"/>
                                    <fo:table-column column-width="10mm"/>
                                    <fo:table-column column-width="proportional-column-width(1)"/>
                                    <fo:table-column column-width="10mm"/>
                                    <fo:table-column column-width="30mm"/>
                                    <fo:table-header>
                                        <fo:table-row>
                                            <fo:table-cell background-color="#2e3d82" border-style="solid" border-width="0.5pt" border-color="#C0C0C0" padding="1mm">
                                                <fo:block color="white">
                                                    <fo:inline>
                                                        <xsl:text>Date</xsl:text>
                                                    </fo:inline>
                                                </fo:block>
                                            </fo:table-cell>
                                            <fo:table-cell/>
                                            <fo:table-cell background-color="#2e3d82" border-style="solid" border-width="0.5pt" border-color="#C0C0C0" padding="1mm">
                                                <fo:block color="white">
                                                    <fo:inline>
                                                        <xsl:text>Customer #</xsl:text>
                                                    </fo:inline>
                                                </fo:block>
                                            </fo:table-cell>
                                            <fo:table-cell/>
                                            <fo:table-cell background-color="#2e3d82" border-style="solid" border-width="0.5pt" border-color="#C0C0C0" padding="1mm">
                                                <fo:block color="white">
                                                    <fo:inline>
                                                        <xsl:text>Ordered By</xsl:text>
                                                    </fo:inline>
                                                </fo:block>
                                            </fo:table-cell>
                                            <fo:table-cell/>
                                            <fo:table-cell background-color="#2e3d82" border-style="solid" border-width="0.5pt" border-color="#C0C0C0" padding="1mm">
                                                <fo:block color="white">
                                                    <fo:inline>
                                                        <xsl:text>Grand Total</xsl:text>
                                                    </fo:inline>
                                                </fo:block>
                                            </fo:table-cell>
                                        </fo:table-row>
                                    </fo:table-header>
                                    <fo:table-body>
                                        <fo:table-row>
                                            <fo:table-cell
                                                    border-bottom-style="solid" border-bottom-width="0.5pt"
                                                    border-left-style="solid" border-left-width="0.5pt"
                                                    border-right-style="solid" border-right-width="0.5pt"
                                                    border-color="#C0C0C0" padding="1mm">
                                                <fo:block>
                                                    <xsl:for-each select="date">
                                                        <fo:inline>
                                                            <xsl:apply-templates/>
                                                        </fo:inline>
                                                    </xsl:for-each>
                                                </fo:block>
                                            </fo:table-cell>
                                            <fo:table-cell/>
                                            <fo:table-cell
                                                    border-bottom-style="solid" border-bottom-width="0.5pt"
                                                    border-left-style="solid" border-left-width="0.5pt"
                                                    border-right-style="solid" border-right-width="0.5pt"
                                                    border-color="#C0C0C0" padding="1mm">
                                                <fo:block>
                                                    <xsl:for-each select="customer">
                                                        <fo:inline>
                                                            <xsl:apply-templates/>
                                                        </fo:inline>
                                                    </xsl:for-each>
                                                </fo:block>
                                            </fo:table-cell>
                                            <fo:table-cell/>
                                            <fo:table-cell
                                                    border-bottom-style="solid" border-bottom-width="0.5pt"
                                                    border-left-style="solid" border-left-width="0.5pt"
                                                    border-right-style="solid" border-right-width="0.5pt"
                                                    border-color="#C0C0C0" padding="1mm">
                                                <fo:block>
                                                    <xsl:for-each select="ordered-by">
                                                        <fo:inline>
                                                            <xsl:apply-templates/>
                                                        </fo:inline>
                                                    </xsl:for-each>
                                                </fo:block>
                                            </fo:table-cell>
                                            <fo:table-cell/>
                                            <fo:table-cell
                                                    border-bottom-style="solid" border-bottom-width="0.5pt"
                                                    border-left-style="solid" border-left-width="0.5pt"
                                                    border-right-style="solid" border-right-width="0.5pt"
                                                    border-color="#C0C0C0" padding="1mm">
                                                <fo:block>
                                                    <xsl:for-each select="grand-total">
                                                        <fo:inline>
                                                            <xsl:apply-templates/>
                                                        </fo:inline>
                                                    </xsl:for-each>
                                                </fo:block>
                                            </fo:table-cell>
                                        </fo:table-row>
                                    </fo:table-body>
                                </fo:table>
                            </fo:block>
                            <fo:block padding-before="6mm">
                                <fo:table table-layout="fixed" border-collapse="separate">
                                    <fo:table-column column-width="97mm"/>
                                    <fo:table-column column-width="6mm"/>
                                    <fo:table-column column-width="97mm"/>
                                    <fo:table-header>
                                        <fo:table-row>
                                            <fo:table-cell background-color="#2e3d82" border-style="solid" border-width="0.5pt" border-color="#C0C0C0" padding="1mm">
                                                <fo:block color="white">
                                                    <fo:inline>
                                                        <xsl:text>Sold To</xsl:text>
                                                    </fo:inline>
                                                </fo:block>
                                            </fo:table-cell>
                                            <fo:table-cell/>
                                        
                                            <fo:table-cell background-color="#2e3d82" border-style="solid" border-width="0.5pt" border-color="#C0C0C0" padding="1mm">
                                                <fo:block color="white">
                                                    <fo:inline>
                                                        <xsl:text>Ship To</xsl:text>
                                                    </fo:inline>
                                                </fo:block>
                                            </fo:table-cell>
                                        </fo:table-row>
                                    </fo:table-header>
                                    <fo:table-body>
                                        <fo:table-row>
                                            <fo:table-cell
                                                    border-bottom-style="solid" border-bottom-width="0.5pt"
                                                    border-left-style="solid" border-left-width="0.5pt"
                                                    border-right-style="solid" border-right-width="0.5pt"
                                                    border-color="#C0C0C0" padding="1mm">
                                                <xsl:for-each select="sold-to">
                                                    <fo:block>
                                                        <xsl:for-each select="name">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                    <fo:block>
                                                        <xsl:for-each select="address1">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                    <fo:block>
                                                        <xsl:for-each select="address2">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                    <fo:block>
                                                        <xsl:for-each select="city">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                        <xsl:text>, </xsl:text>
                                                        <xsl:for-each select="state">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                        <xsl:text> </xsl:text>
                                                        <xsl:for-each select="zip">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                </xsl:for-each>
                                            </fo:table-cell>
                                            <fo:table-cell/>
                                            <fo:table-cell
                                                    border-bottom-style="solid" border-bottom-width="0.5pt"
                                                    border-left-style="solid" border-left-width="0.5pt"
                                                    border-right-style="solid" border-right-width="0.5pt"
                                                    border-color="#C0C0C0" padding="1mm">
                                                <xsl:for-each select="ship-to">
                                                    <fo:block>
                                                        <xsl:for-each select="name">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                    <fo:block>
                                                        <xsl:for-each select="address1">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                    <fo:block>
                                                        <xsl:for-each select="address2">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                    <fo:block>
                                                        <xsl:for-each select="city">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                        <xsl:text>, </xsl:text>
                                                        <xsl:for-each select="state">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                        <xsl:text> </xsl:text>
                                                        <xsl:for-each select="zip">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                    <fo:block>
                                                        <xsl:for-each select="country">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                </xsl:for-each>
                                            </fo:table-cell>
                                        </fo:table-row>
                                    </fo:table-body>
                                </fo:table>                         
                            </fo:block>
                            <xsl:for-each select="shipping">
                                <xsl:if test="mark-order != '' or jobsite-contact != '' or delivery-request != ''">
                                    <fo:block padding-before="6mm">
                                        <fo:table table-layout="fixed" border-collapse="separate">
                                            <!-- fo:table-column column-width="55mm"/>
                                            <fo:table-column column-width="75mm"/>
                                            <fo:table-column column-width="70mm"/ -->
                                            <fo:table-column column-width="33%"/>
                                            <fo:table-column column-width="34%"/>
                                            <fo:table-column column-width="33%"/>
                                            <fo:table-body>
                                                <fo:table-row>
                                                    <fo:table-cell background-color="#2e3d82" border-style="solid" border-width="0.5pt" border-color="#C0C0C0" padding="1mm" text-align="center"
                                                            number-columns-spanned="3">
                                                        <fo:block color="white">
                                                            <fo:inline>
                                                                <xsl:text>Shipping Instructions</xsl:text>
                                                            </fo:inline>
                                                        </fo:block>
                                                    </fo:table-cell>
                                                </fo:table-row>
                                                <fo:table-row keep-together="always">
                                                    <fo:table-cell padding="1mm">
                                                        <fo:block>
                                                            <xsl:if test="mark-order != ''">
                                                                <xsl:text>Mark Order: </xsl:text>
                                                                <xsl:for-each select="mark-order">
                                                                    <fo:inline>
                                                                        <xsl:apply-templates/>
                                                                    </fo:inline>
                                                                </xsl:for-each>
                                                            </xsl:if>
                                                        </fo:block>
                                                    </fo:table-cell>
                                                    <fo:table-cell padding="1mm">
                                                        <fo:block text-align="center">
                                                            <xsl:if test="jobsite-contact != ''">
                                                                <xsl:text> Jobsite Contact: </xsl:text>
                                                                <xsl:for-each select="jobsite-contact">
                                                                    <fo:inline>
                                                                        <xsl:apply-templates/>
                                                                    </fo:inline>
                                                                </xsl:for-each>
                                                            </xsl:if>
                                                        </fo:block>
                                                    </fo:table-cell>
                                                    <fo:table-cell padding="1mm">
                                                        <fo:block text-align="right">
                                                            <xsl:if test="delivery-request != ''">
                                                                <xsl:text> Delivery Request: </xsl:text>
                                                                <xsl:for-each select="delivery-request">
                                                                    <fo:inline>
                                                                        <xsl:apply-templates/>
                                                                    </fo:inline>
                                                                </xsl:for-each>
                                                            </xsl:if>
                                                        </fo:block>
                                                    </fo:table-cell>
                                                </fo:table-row>
                                            </fo:table-body>
                                        </fo:table>
                                    </fo:block>
                                </xsl:if>
                            </xsl:for-each>
                            <fo:block padding-before="6mm">
                                <fo:table table-layout="fixed" border-collapse="separate">
                                    <fo:table-column column-width="200mm"/>
                                    <fo:table-body>
                                        <fo:table-row>
                                            <fo:table-cell background-color="#2e3d82" border-style="solid" border-width="0.5pt" border-color="#C0C0C0" padding="1mm">
                                                <fo:block color="white" text-align="center">
                                                    <fo:inline>
                                                        <xsl:text>Order Detail</xsl:text>
                                                    </fo:inline>
                                                </fo:block>
                                            </fo:table-cell>
                                        </fo:table-row>
                                    </fo:table-body>
                                </fo:table>
                                <xsl:for-each select="orders/order">
                                    <fo:table table-layout="fixed" border-collapse="separate">
                                        <!-- fo:table-column column-width="55mm"/>
                                        <fo:table-column column-width="75mm"/>
                                        <fo:table-column column-width="70mm"/ -->
                                        <fo:table-column column-width="33%"/>
                                        <fo:table-column column-width="34%"/>
                                        <fo:table-column column-width="33%"/>
                                        <fo:table-header>                                   
                                            <fo:table-row keep-together="always">
                                                <fo:table-cell padding="1mm">
                                                    <fo:block>
                                                        <xsl:text>Warehouse: </xsl:text>
                                                        <xsl:for-each select="warehouse">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                </fo:table-cell>
                                                <fo:table-cell padding="1mm">
                                                    <fo:block text-align="center">
                                                        <xsl:text>PO Number: </xsl:text>
                                                        <xsl:for-each select="ponumber">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                </fo:table-cell>
                                                <fo:table-cell padding="1mm">
                                                    <fo:block text-align="right">
                                                        <xsl:text>Online Order Confirmation #: </xsl:text>
                                                        <xsl:for-each select="id">
                                                            <fo:inline>
                                                                <xsl:apply-templates/>
                                                            </fo:inline>
                                                        </xsl:for-each>
                                                    </fo:block>
                                                </fo:table-cell>
                                            </fo:table-row>
                                        </fo:table-header>
                                        <fo:table-body>
                                            <fo:table-row keep-together="always">
                                                <fo:table-cell number-columns-spanned="3">
                                                    <fo:block font-size="8pt">
                                                        <fo:table table-layout="fixed" border-collapse="separate">
                                                            <fo:table-column column-width="10mm"/>
                                                            <fo:table-column column-width="10mm"/>
                                                            <fo:table-column column-width="20mm"/>
                                                            <fo:table-column column-width="60mm"/>
                                                            <fo:table-column column-width="20mm"/>
                                                            <fo:table-column column-width="20mm"/>
                                                            <fo:table-column column-width="60mm"/>
                                                            <fo:table-header>
                                                                <fo:table-row keep-together="always">
                                                                    <fo:table-cell number-columns-spanned="7" border-style="solid" border-width="0.5pt" border-color="#C0C0C0" padding="0.5pt">
                                                                        <fo:block>
                                                                            <fo:table table-layout="fixed" border-collapse="separate">
                                                                                <fo:table-column column-width="10mm"/>
                                                                                <fo:table-column column-width="10mm"/>
                                                                                <fo:table-column column-width="20mm"/>
                                                                                <fo:table-column column-width="60mm"/>
                                                                                <fo:table-column column-width="20mm"/>
                                                                                <fo:table-column column-width="20mm"/>
                                                                                <fo:table-column column-width="59.29mm"/>
                                                                                <fo:table-body>
                                                                                    <fo:table-row keep-together="always">
                                                                                        <fo:table-cell background-color="#2e3d82" padding-left="0.8mm" padding-top="1mm">
                                                                                            <fo:block color="white">
                                                                                                <fo:inline>
                                                                                                    <xsl:text>Line</xsl:text>
                                                                                                </fo:inline>
                                                                                            </fo:block>
                                                                                        </fo:table-cell>
                                                                                        <fo:table-cell background-color="#2e3d82" padding-top="1mm">
                                                                                            <fo:block color="white" text-align="center">
                                                                                                <fo:inline>
                                                                                                    <xsl:text>SKU</xsl:text>
                                                                                                </fo:inline>
                                                                                            </fo:block>
                                                                                        </fo:table-cell>
                                                                                        <fo:table-cell background-color="#2e3d82" padding-top="1mm">
                                                                                            <fo:block color="white" text-align="center">
                                                                                                <fo:inline>
                                                                                                    <xsl:text>Model</xsl:text>
                                                                                                </fo:inline>
                                                                                            </fo:block>
                                                                                        </fo:table-cell>
                                                                                        <fo:table-cell background-color="#2e3d82" padding-left="0.4mm" padding-top="1mm">
                                                                                            <fo:block color="white">
                                                                                                <fo:inline>
                                                                                                    <xsl:text>Description</xsl:text>
                                                                                                </fo:inline>
                                                                                            </fo:block>
                                                                                        </fo:table-cell>
                                                                                        <fo:table-cell background-color="#2e3d82" padding-top="1mm">
                                                                                            <fo:block color="white" text-align="right">
                                                                                                <fo:inline>
                                                                                                    <xsl:text>Order Qty</xsl:text>
                                                                                                </fo:inline>
                                                                                            </fo:block>
                                                                                        </fo:table-cell>
                                                                                        <fo:table-cell background-color="#2e3d82" padding-top="1mm">
                                                                                            <fo:block color="white" text-align="right">
                                                                                                <fo:inline>
                                                                                                    <xsl:text>Unit Price</xsl:text>
                                                                                                </fo:inline>
                                                                                            </fo:block>
                                                                                        </fo:table-cell>
                                                                                        <fo:table-cell background-color="#2e3d82" padding-top="1mm" padding-right="1mm">
                                                                                            <fo:block color="white" text-align="right">
                                                                                                <fo:inline>
                                                                                                    <xsl:text>SubTotal</xsl:text>
                                                                                                </fo:inline>
                                                                                            </fo:block>
                                                                                        </fo:table-cell>
                                                                                    </fo:table-row>
                                                                                </fo:table-body>
                                                                            </fo:table>
                                                                        </fo:block>
                                                                    </fo:table-cell>
                                                                </fo:table-row>
                                                            </fo:table-header>
                                                            <fo:table-body>
                                                                <xsl:for-each select="lines/line">
                                                                    <fo:table-row keep-together="always">
                                                                        <fo:table-cell border-left-style="solid" border-left-width="0.5pt" border-color="#C0C0C0" padding-left="1mm" padding-top="1mm">
                                                                            <fo:block>
                                                                                <xsl:for-each select="no">
                                                                                    <fo:inline>
                                                                                        <xsl:apply-templates/>
                                                                                    </fo:inline>
                                                                                </xsl:for-each>
                                                                            </fo:block>
                                                                        </fo:table-cell>
                                                                        <fo:table-cell padding-left="1mm" padding-top="1mm">
                                                                            <fo:block text-align="center">
                                                                                <xsl:for-each select="sku">
                                                                                    <fo:inline>
                                                                                        <xsl:apply-templates/>
                                                                                    </fo:inline>
                                                                                </xsl:for-each>
                                                                            </fo:block>
                                                                        </fo:table-cell>
                                                                        <fo:table-cell padding-left="1mm" padding-top="1mm">
                                                                            <fo:block text-align="center">
                                                                                <xsl:for-each select="model">
                                                                                    <fo:inline>
                                                                                        <xsl:apply-templates/>
                                                                                    </fo:inline>
                                                                                </xsl:for-each>
                                                                            </fo:block>
                                                                        </fo:table-cell>
                                                                        <fo:table-cell padding-left="1mm" padding-top="1mm">
                                                                            <fo:block>
                                                                                <xsl:for-each select="description">
                                                                                    <fo:inline>
                                                                                        <xsl:apply-templates/>
                                                                                    </fo:inline>
                                                                                </xsl:for-each>
                                                                            </fo:block>
                                                                        </fo:table-cell>
                                                                        <fo:table-cell padding-left="1mm" padding-top="1mm">
                                                                            <fo:block text-align="right">
                                                                                <xsl:for-each select="qty">
                                                                                    <fo:inline>
                                                                                        <xsl:apply-templates/>
                                                                                    </fo:inline>
                                                                                </xsl:for-each>
                                                                            </fo:block>
                                                                        </fo:table-cell>
                                                                        <fo:table-cell padding-left="1mm" padding-top="1mm">
                                                                            <fo:block text-align="right">
                                                                                <xsl:for-each select="price">
                                                                                    <fo:inline>
                                                                                        <xsl:apply-templates/>
                                                                                    </fo:inline>
                                                                                </xsl:for-each>
                                                                            </fo:block>
                                                                        </fo:table-cell>
                                                                        <fo:table-cell border-right-style="solid" border-right-width="0.5pt" border-color="#C0C0C0"
                                                                                padding-left="1mm" padding-top="1mm" padding-right="1mm">
                                                                            <fo:block text-align="right">
                                                                                <xsl:for-each select="cost">
                                                                                    <fo:inline>
                                                                                        <xsl:apply-templates/>
                                                                                    </fo:inline>
                                                                                </xsl:for-each>
                                                                            </fo:block>
                                                                        </fo:table-cell>
                                                                    </fo:table-row>
                                                                </xsl:for-each>
                                                                <fo:table-row keep-together="always">
                                                                    <fo:table-cell number-columns-spanned="7"
                                                                            border-left-style="solid" border-left-width="0.5pt"
                                                                            border-right-style="solid" border-right-width="0.5pt"
                                                                            border-color="#C0C0C0" height="2mm"/>
                                                                </fo:table-row>
                                                                <fo:table-row>
                                                                    <fo:table-cell number-columns-spanned="6" border-left-style="solid" border-left-width="0.5pt" border-color="#C0C0C0"/>
                                                                    <fo:table-cell
                                                                            border-top-style="solid" border-top-width="0.5pt"
                                                                            border-right-style="solid" border-right-width="0.5pt"
                                                                            border-color="#C0C0C0" padding-top="1mm" padding-right="1mm">
                                                                        <fo:block text-align="right">
                                                                            <xsl:for-each select="warehouse">
                                                                                <fo:inline>
                                                                                    <xsl:apply-templates/>
                                                                                </fo:inline>
                                                                            </xsl:for-each>
                                                                            <xsl:text> SubTotal: </xsl:text>
                                                                            <xsl:for-each select="total-cost">
                                                                                <fo:inline>
                                                                                    <xsl:apply-templates/>
                                                                                </fo:inline>
                                                                            </xsl:for-each>
                                                                        </fo:block>
                                                                    </fo:table-cell>
                                                                </fo:table-row>
                                                                <fo:table-row keep-together="always">
                                                                    <fo:table-cell number-columns-spanned="6" border-left-style="solid" border-left-width="0.5pt" border-color="#C0C0C0"/>
                                                                    <fo:table-cell border-right-style="solid" border-right-width="0.5pt" border-color="#C0C0C0" padding-top="1mm" padding-right="1mm">
                                                                        <fo:block text-align="right">
                                                                            <xsl:text>Ship Via </xsl:text>
                                                                            <xsl:for-each select="ship-via">
                                                                                <fo:inline>
                                                                                    <xsl:apply-templates/>
                                                                                </fo:inline>
                                                                            </xsl:for-each>
                                                                            <xsl:text>: </xsl:text>
                                                                            <xsl:for-each select="ship-total">
                                                                                <fo:inline>
                                                                                    <xsl:apply-templates/>
                                                                                </fo:inline>
                                                                            </xsl:for-each>
                                                                        </fo:block>
                                                                    </fo:table-cell>
                                                                </fo:table-row>
																<xsl:if test="liftgate-cost" >
	                                                                <fo:table-row keep-together="always">
	                                                                    <fo:table-cell number-columns-spanned="6" border-left-style="solid" border-left-width="0.5pt" border-color="#C0C0C0"/>
	                                                                    <fo:table-cell border-right-style="solid" border-right-width="0.5pt" border-color="#C0C0C0" padding-top="1mm" padding-right="1mm">
	                                                                        <fo:block text-align="right">
	                                                                            <xsl:text>Lift gate </xsl:text>
	                                                                            <xsl:text>: </xsl:text>
	                                                                            <xsl:for-each select="liftgate-cost">
	                                                                                <fo:inline>
	                                                                                    <xsl:apply-templates/>
	                                                                                </fo:inline>
	                                                                            </xsl:for-each>
	                                                                        </fo:block>
	                                                                    </fo:table-cell>
	                                                                </fo:table-row>																
																</xsl:if>
                                                                <fo:table-row keep-together="always">
                                                                    <fo:table-cell number-columns-spanned="6"
                                                                            border-left-style="solid" border-left-width="0.5pt"
                                                                            border-bottom-style="solid" border-bottom-width="0.5pt"
                                                                            border-color="#C0C0C0"/>
                                                                    <fo:table-cell
                                                                            border-right-style="solid" border-right-width="0.5pt"
                                                                            border-bottom-style="solid" border-bottom-width="0.5pt"
                                                                            border-color="#C0C0C0" padding-top="1mm" padding-right="1mm">
                                                                        <fo:block text-align="right">
                                                                            <xsl:for-each select="warehouse">
                                                                                <fo:inline>
                                                                                    <xsl:apply-templates/>
                                                                                </fo:inline>
                                                                            </xsl:for-each>
                                                                            <xsl:text> Total: </xsl:text>
                                                                            <xsl:for-each select="grand-total">
                                                                                <fo:inline>
                                                                                    <xsl:apply-templates/>
                                                                                </fo:inline>
                                                                            </xsl:for-each>
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
                                </xsl:for-each>
                            </fo:block>
                            <fo:block padding-before="6mm">
                                <fo:table table-layout="fixed" border-collapse="separate">
                                    <fo:table-column column-width="200mm"/>
                                    <fo:table-body>
                                        <fo:table-row>
                                            <fo:table-cell padding="1mm">
                                                <fo:block>
                                                    <fo:inline>
                                                        <xsl:text>This is not an invoice. You will receive your standard acknowledgement and an invoice separately.</xsl:text>
                                                    </fo:inline>
                                                </fo:block>
                                                <fo:block>
                                                    <fo:inline>
                                                        <xsl:text>If you have any questions regarding this confirmation, please contact your Customer Service Representative.</xsl:text>
                                                    </fo:inline>
                                                </fo:block>
                                                <fo:block padding-top="2mm">
                                                    <fo:inline>
                                                        <xsl:text>You can check your order status in Customer Center.</xsl:text>
                                                    </fo:inline>
                                                </fo:block>
                                            </fo:table-cell>
                                        </fo:table-row>
                                    </fo:table-body>
                                </fo:table>
                            </fo:block>
                        </xsl:for-each> 
                    </fo:block>
                </fo:flow>
            </fo:page-sequence>
        </fo:root>
    </xsl:template>
</xsl:stylesheet>
