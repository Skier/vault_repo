typedef struct {
    char utcTime[11];
    char latitude[10];
    char latNS;
    char longitude[11];
    char lngEW;
    uint16 u16Temp;
    uint16 u16Humid;
    uint16 u16Volt;
    MAC_ExtAddr_s address;
} netMessage;

