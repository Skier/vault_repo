wsdl2h.exe http://moritur/zofws.php?wsdl -s -Nzofws -qzofws -ozofws.h
soapcpp2.exe -C -L -w -x -pzofws zofws.h
