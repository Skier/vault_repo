wsdl2h.exe http://localhost/zoomservice/ZoomWebService.asmx?wsdl -s -Nzoomws -ozoomws.h
soapcpp2.exe -C -L -w -x -pzoomws zoomws.h
