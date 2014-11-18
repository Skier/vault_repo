wsdl2h.exe http://moritur/nusoap/lib/fscws.php?wsdl -s -Nfscws -qfscws -ofscws.h
soapcpp2.exe -C -L -w -x -pfscws fscws.h
