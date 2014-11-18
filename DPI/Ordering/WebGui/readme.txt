**********************************************************************************************
********************************* Web Ordering Readme.txt ************************************
**********************************************************************************************

Readme will have summary of changes implemented within the release
of the code.

**********************************************************************************************


* 10/29/2004
Version 1.1.1.9

// FRAMEWORK //
- file PinReceipt.rpt: Added function for Receipt_Text to replace "/n" with chr(13) + chr(10)
- file Cerberos.cs: added a check for null perms returned form the getpermissions method.
- Added User Name to WIP.
- Modified workflows to capture & record User Name.  

// DATABASE //
- Added User Name attribute to WIP_History table & stored procedures.

// USER INTERFACE //
-


**********************************************************************************************


* 11/8/2004
Version 1.1.2.0

// FRAMEWORK //
- uncluded month 1 fees to the product L2 service.
- added the promotions header to the table class.

// DATABASE //


// USER INTERFACE //
- moved the same address checkbox to the appropriate place on serivce address.
- created a promotions graphic.
- made changes to the account/phone number findcustomer.aspx page.
- new payment warning on new payment screen ordersummary.aspx.



**********************************************************************************************


* 11/11/2004
Version 1.1.2.1

// FRAMEWORK //
- excluded the  month 1 fees to the product L2 service.
- added ranking capabilities and sales MTD to related services.

// DATABASE //


// USER INTERFACE //
- made statistics and logout subheader more flexible.
- added the rankings to the subheader.
- added sales MTD to the subheader.
- excluded the check option from all dropdown boxes.



**********************************************************************************************


* 11/17/2004
Version 1.1.2.3

// FRAMEWORK //
- New Sales are calculated based a number of Verifone transactions + archive, Type 2 entries that are more than $35. 
- Revenues include MDT sum of Local, LD, and Wireless amounts from Verifone & Wireless transactions + archives. 
- The Quick Stats now can show Total Revenue, Local Service Revenue, LD Revenue, and Wireless Revenue, all MTD. 
- We can also show Corporation Top N stores based on Active Customers and / or MTD New Sales. 

// DATABASE //


// USER INTERFACE //
- Bug - New Order error on step 8 of 9 when hitting proceed, if credit is selected as payment method
- New Order Step 5 - Remove "! Remind the customer"  
- New Order Step 5 - the first total should be "Product Total" (not Order Total)
- New Order - Order Confirmation Step 8 - the first total should be "Product Total" (not Order Total), and 'Total Amount' should be 'Total Amount Due'
- Level 2 Product prices should be in black
- New Payment step 5 of 6 - the first total should be "Product Total" (not Order Total)
- Monthly Payment step 2 of 3 - move payment method down, change LD description to match New Order
- Product and Price Lookup step 5 of 5 - change Order Total to Product Total to be consistent with other pages
- Cellular Recharge when input the number 15 or 15.00 in the page for a $15 purchase, will not give change due or proceed to next step. (maybe an issue with connecting to Pre - whatever the issue we must provide a message)
- Response time is very bad on the left navigation and the proceed to next step buttons.  If either is clicked more than one time you are thrown back to the main page.
- Customer Inquiry and monthly payment if you hit enter you are thrown to the main page.
- Level 2 on current promotions bar add "Click to Add Promotions"
- fixed monthly payment receipt issues with account that have no previous bills.
- fixed new payment issues with object not set to an instance.



**********************************************************************************************



* 11/17/2004
Version 1.1.2.4

// FRAMEWORK //
- Added new services for website quickstats.

// DATABASE //


// USER INTERFACE //
- Created website quickstats links on subheader.
- Created quickstats pages for respective stat.
- Added hover links to describe each quickstat.


**********************************************************************************************

* 12/2/2004
Version 1.1.2.6

// FRAMEWORK //
- Added new services for website quickstats.
- Changed connection class to reflect server names instead of IP addresses.
- added certificate security implementation.

// DATABASE //
- added Certificate Required and certificate with storecode required columns to user account table.

// USER INTERFACE //
- Modified the email link on the menuscreen.
- Moved the "Learn More" link to the left.


**********************************************************************************************

* 12/3/2004
Version 1.1.2.7

// FRAMEWORK //
- storecode of certificate overrides storecode of user account.

// DATABASE //

// USER INTERFACE //



**********************************************************************************************

* 12/3/2004
Version 1.1.2.8

// FRAMEWORK //
- storecode of certificate overrides storecode of user account. *MODIFICATIONS*

// DATABASE //

// USER INTERFACE //



**********************************************************************************************

* 12/7/2004
Version 1.1.2.9

// FRAMEWORK //
- storecode of certificate overrides storecode of user account. *MODIFICATIONS*
- suppress zero price state of product.
- Preselect option on product.

// DATABASE //

// USER INTERFACE //
- Created 'timeout.htm' to give warning about session timeout. 
- Added features matrix addition for new products. Added new.gif file.



**********************************************************************************************

* 12/8/2004
Version 1.1.3.1

// FRAMEWORK //
- Validation of certificate storecode versus useraccount storecode to make sure that they belong to the same corporation.

// DATABASE //

// USER INTERFACE //
- Session timeout is 1440 minutes.


**********************************************************************************************

* 12/9/2004
Version 1.1.3.2

// FRAMEWORK //
- fixed a bug on the dissappearing credit (p 450) in the order summary screen.
- fixed the storecode issue with the MTD and total sales ranking grids to highlight the correct store number.

// DATABASE //

// USER INTERFACE //



**********************************************************************************************

* 12/10/2004
Version 1.1.3.3

// FRAMEWORK //
- bug fixes in Services and CusComp.

// DATABASE //

// USER INTERFACE //


**********************************************************************************************

* 12/13/2004
Version 1.1.3.5

// FRAMEWORK //
- Change Order Confirmation to skip rows when product starts in month 2. 

// DATABASE //

// USER INTERFACE //
- Fixed bug in the Previous button. 

**********************************************************************************************

* 12/21/2004
Version 1.1.3.6

// FRAMEWORK //
- Fixed bug dealing with useraccount with status being NULL.
- Added a check for "isinrole" property to prevent user from typing in unwarranted urls. (reportmain.aspx, operations.aspx)
- changes state to unavailable for those preselected products which are in conflict with a selected package.
- modified the pin service to enable the sprint LD product to all applicable corporations. 
- Added option to exclude products from ProductL2.aspx order total.

// DATABASE //

// USER INTERFACE //
- Fixed bug in the Previous button. 
- added customer list report.
- Added event to the customer list report links.

**********************************************************************************************

* 12/22/2004
Version 1.1.3.7

// FRAMEWORK //

// DATABASE //

// USER INTERFACE //
- Added event to the customer list report links.

**********************************************************************************************

* 12/23/2004
Version 1.1.3.8

// FRAMEWORK //
- Restored check for AutoLogin accounts - users will no longer able to login manually using an AutoLogin accounts 
- Active Customer list/ inactive cutomer list implementations.

// DATABASE //

// USER INTERFACE //
- Changed label on ProductL2.aspx  to read "Products Total" and "Not included in Products Total below" text; 
- moved "GO"  button up on reports.

**********************************************************************************************

* 12/24/2004
Version 1.1.3.9

// FRAMEWORK //
- used the const.permissons_operations and const.permissions_reporting

// DATABASE //

// USER INTERFACE //
- Changed the reporting label on sidecontrol2.aspx

**********************************************************************************************

* 12/30/2004
Version 1.1.4.0

// FRAMEWORK //
- edited the ReportSvc.cs line 87.

// DATABASE //

// USER INTERFACE //

**********************************************************************************************

* 1/5/2005
Version 1.1.5.0

// FRAMEWORK //
- added ReversalVoidWF, ReversalVoidWIP, IReversalVoidWF.
- added RevVoidRow, RevVoidHeader to rows class.
- added TableTransVoid public class.
- Store Ranking is now calculated based on a number of active corporate stores.

// DATABASE //
- enabled reversal/void permission.

// USER INTERFACE //
- added reversalvoid.aspx, voidconf.aspx.
- added button graphics.


**********************************************************************************************

* 1/6/2005
Version 1.1.6.0

// FRAMEWORK //
- added transaction service to look up certain transaction record.

// DATABASE //

// USER INTERFACE //
- added forms.aspx.
- added pdfs to project and docs folder. 
	1) Application for Local Telephone Service.pdf
	2) North Carolina Service Agreement.pdf
	3) WEB LOA.pdf
	
- added print version of VoidConf.aspx.
- added transaction info to VoidConf.aspx display.


**********************************************************************************************

* 1/10/2005
Version 1.1.6.1

// FRAMEWORK //
- added transaction type id interface to enums class

// DATABASE //

// USER INTERFACE //
- added phone, payment type fields to void forms.
- added new header with webcentral name.
- added change requests to revesal/void interface.
- added new quickstats interface.

**********************************************************************************************

* 1/11/2005
Version 1.1.6.2

// FRAMEWORK //
- modified IProdPrice.cs to add DisplayUnclickMessage to interface.
- modified ProdPrice.cs to add DisplayUnclickMessage property.
- modified ProdInfo.cs to add DisplayUnclickMessage property and to the ProdInfo constructor.
- modified Product.cs to add DisplayUnclickMessage property.
- modified FormatPhone to include Format() and Format() override to format phone numbers.
- modified ProdLevel2 table constructors to allow for 'unlcick' message on all marked products.

// DATABASE //
- modified product table to add DisplayUnclickMessage(bit) column.
- modified these stored procedures to incorporate DisplayUnclickMessage(bit)
	1)spProduct_get_id
	2)spProduct_ins
	3)spProduct_upd
	4)spProduct_get_all
	
- flagged product(402) for DisplayUnclickMessage boolean(true)	 

// USER INTERFACE //
- added "unclick to select another product" text to Rows constructor.
- made change to forms title from "Web LOA" to "Letter of Agency"
- formatted phone number display on reversal/void interface.
- added comma to reversal/void graphic header.
- modified alert message to read "OK" from "Void" on Reversal/Void confirmation pop-up.

**********************************************************************************************

* 1/12/2005
Version 1.1.6.3

// FRAMEWORK //

// DATABASE //

// USER INTERFACE //
- Changed response.redirect to include the boolean argument (false).

**********************************************************************************************