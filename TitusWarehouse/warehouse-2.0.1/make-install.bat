rmdir /S /Q ..\install
mkdir ..\install 
mkdir ..\install\apps
mkdir ..\install\apps\AerSysCo.CreditHoldManager
mkdir ..\install\apps\AerSysCo.CustomerManager
mkdir ..\install\apps\AerSysCo.ECatalogManager
mkdir ..\install\apps\AerSysCo.InventoryManager
mkdir ..\install\apps\AerSysCo.MultiplierManager
mkdir ..\install\apps\AerSysCo.OrderManager
copy  /B .\server\AerSysCo.CreditHoldManager\bin\Release\*  ..\install\apps\AerSysCo.CreditHoldManager\* 
copy  /B .\server\AerSysCo.CustomerManager\bin\Release\*  ..\install\apps\AerSysCo.CustomerManager\* 
copy  /B .\server\AerSysCo.ECatalogManager\bin\Release\*  ..\install\apps\AerSysCo.ECatalogManager\* 
copy  /B .\server\AerSysCo.InventoryManager\bin\Release\*  ..\install\apps\AerSysCo.InventoryManager\* 
copy  /B .\server\AerSysCo.MultiplierManager\bin\Release\*  ..\install\apps\AerSysCo.MultiplierManager\* 
copy  /B .\server\AerSysCo.OrderManager\bin\Release\*  ..\install\apps\AerSysCo.OrderManager\* 

mkdir ..\install\server
mkdir ..\install\server\acknowledgements
mkdir ..\install\server\bin
mkdir ..\install\server\macpac
mkdir ..\install\server\macpac-copy
mkdir ..\install\server\style

copy /B docs\acknowledgement\titus-acknowledgement.xsl ..\install\server\acknowledgements\*
copy /B docs\acknowledgement\titus-logo.jpg ..\install\server\acknowledgements\*
copy /B server\AerSysCo.Server\bin\* ..\install\server\bin\*
copy /B server\AerSysCo.Server\WarehouseService.asmx ..\install\server\*
copy /B server\AerSysCo.Server\Web.config ..\install\server\*
copy /B client\html-template\AC_OETags.js ..\install\server\*
copy /B client\html-template\history.htm  ..\install\server\*
copy /B client\html-template\history.js   ..\install\server\*
copy /B client\html-template\playerProductInstall.swf ..\install\server\*
copy /B client\html-template\WarehouseService.wsdl ..\install\server\*
copy /B client\html-template\crossdomain.xml ..\install\server\*
copy /B ..\server\admin.swf ..\install\server\*

mkdir ..\install\client
copy /B client\html-template\crossdomain.xml ..\install\client\*
copy /B client\html-template\AC_OETags.js ..\install\client\*
copy /B client\html-template\history.htm  ..\install\client\*
copy /B client\html-template\history.js   ..\install\client\*
copy /B client\html-template\playerProductInstall.swf ..\install\client\*
copy /B client\html-template\WarehouseService.wsdl ..\install\client\*
copy /B ..\server\AC_OETags.js                    ..\install\client\*
copy /B ..\server\bluebk.jpg                      ..\install\client\*
copy /B ..\server\crossdomain.xml                 ..\install\client\*
copy /B ..\server\faq.txt                         ..\install\client\*
copy /B ..\server\header_warehouse.asp            ..\install\client\*
copy /B ..\server\history.htm                     ..\install\client\*
copy /B ..\server\history.js                      ..\install\client\*
copy /B ..\server\history.swf                     ..\install\client\*
copy /B ..\server\main.swf                        ..\install\client\*
copy /B ..\server\main-debug.swf                  ..\install\client\*
copy /B ..\server\playerProductInstall.swf        ..\install\client\*
copy /B ..\server\rules.txt                       ..\install\client\*
copy /B ..\server\sample.jpg                      ..\install\client\*
copy /B ..\server\titus.swf                       ..\install\client\*
copy /B ..\server\warehouse.asp                   ..\install\client\*
copy /B ..\server\warehouse.jpg                   ..\install\client\*
copy /B ..\server\WarehouseService.wsdl           ..\install\client\*
copy /B ..\server\whslanding.html                 ..\install\client\*

7z a -r ..\install.7z ..\install 
zip -r ..\install.zip ..\install 
