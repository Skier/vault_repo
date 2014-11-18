/****************************************************************************
 *
 * MODULE:			   WSN - Router
 *
 * COMPONENT:          $RCSfile: WSN_Router.c,v $
 *
 * VERSION:            $Name:  $
 *
 * REVISION:           $Revision: 1.4 $
 *
 * DATED:              $Date: 2006/12/11 10:38:48 $
 *
 * STATUS:             $State: Exp $
 *
 * AUTHOR:             IDM
 *
 * DESCRIPTION:
 *
 * Implements a Wireless Sensor Network Router using the Jennic Zigbee stack.
 * Reads temperature, humidity and battery voltage and transmits these to
 * network coordinator. Assumes code is running on a evaluation kit sensor
 * board.
 *
 * LAST MODIFIED BY:   $Author: imorr $
 *                     $Modtime: $
 *
 ****************************************************************************
 *
 * This software is owned by Jennic and/or its supplier and is protected
 * under applicable copyright laws. All rights are reserved. We grant You,
 * and any third parties, a license to use this software solely and
 * exclusively on Jennic products. You, and any third parties must reproduce
 * the copyright and warranty notice and any other legend of ownership on each
 * copy or partial copy of the software.
 *
 * THIS SOFTWARE IS PROVIDED "AS IS". JENNIC MAKES NO WARRANTIES, WHETHER
 * EXPRESS, IMPLIED OR STATUTORY, INCLUDING, BUT NOT LIMITED TO, IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE,
 * ACCURACY OR LACK OF NEGLIGENCE. JENNIC SHALL NOT, IN ANY CIRCUMSTANCES,
 * BE LIABLE FOR ANY DAMAGES, INCLUDING, BUT NOT LIMITED TO, SPECIAL,
 * INCIDENTAL OR CONSEQUENTIAL DAMAGES FOR ANY REASON WHATSOEVER.
 *
 * Copyright Jennic Ltd 2005, 2006. All rights reserved
 *
 ****************************************************************************/

/****************************************************************************/
/***        Include files                                                 ***/
/****************************************************************************/
#include <jendefs.h>
#include <ALSdriver.h>
#include <HTSdriver.h>
#include <LedControl.h>
#include <AppHardwareApi.h>
#include <JZ_Api.h>

#include "uart.h"
#include "serial.h"
#include "serialq.h"
#include "profile.h"
#include "message.h"
#include "toggle.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/
#define UART_BAUD_RATE      4800

/* Timing values */
#define APP_TICK_PERIOD_ms		  100
#define APP_TICK_PERIOD     	  (APP_TICK_PERIOD_ms * 32)

#define APP_DATA_SEND_PERIOD_ms	  1000
#define APP_DATA_SEND_PERIOD	  (APP_DATA_SEND_PERIOD_ms / APP_TICK_PERIOD_ms)

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

/* Battery reading state definitions */
typedef enum
{
    E_STATE_READ_BATT_VOLT_IDLE,
    E_STATE_READ_BATT_VOLTS_ADC_CONVERTING,
    E_STATE_READ_BATT_VOLTS_COMPLETE,
    E_STATE_READ_BATT_VOLTS_READY
}teStateReadBattVolt;

/* Temperature/Humidity Sensor - reading state definitions */
typedef enum
{
	E_STATE_READ_TEMP_HUMID_IDLE,
	E_STATE_READ_HUMID_RUNNING,
	E_STATE_READ_TEMP_HUMID_COMPLETE,
	E_STATE_READ_TEMP_START,
	E_STATE_READ_TEMP_HUMID_RUNNING,
	E_STATE_READ_TEMP_COMPLETE,
	E_STATE_READ_TEMP_HUMID_READY
}teStateReadTempHumidity;

/* Battery measurement data */
typedef struct
{
	uint16 u16Reading;
	teStateReadBattVolt eState;
}tsBattSensor;

/* Temp/Humidity measurement data */
typedef struct
{
	uint16 u16TempReading;
	uint16 u16HumidReading;
	teStateReadTempHumidity eState;
}tsTempHumiditySensor;

typedef enum 
{
    E_GPS_READ_NONE,
    E_GPS_READ_RUNNING,
    E_GPS_READ_COMPLETE
} tsStateGPSRead;

typedef struct 
{
    char utcTime[10];
    char latitude[9];
    char latNS;
    char longitude[10];
    char lngEW;
    
    tsStateGPSRead eState;
}tsGPSSensor;

/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/
PRIVATE void vInit(void);
PRIVATE void vSendData(void);
PRIVATE void vReadGPS(void);
PRIVATE void vInitSensors(void);
PRIVATE void vReadTempHumidity(void);
PRIVATE void vReadBatteryVoltage(void);
PRIVATE void vAppTick(void *pvMsg, uint8 u8Param);
PRIVATE void vParseGPS(void);

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/
PRIVATE uint8 u8AppTicks = 0;
PRIVATE tsBattSensor sBattSensor;
PRIVATE tsTempHumiditySensor sTempHumiditySensor;
PRIVATE tsGPSSensor sGPSSensor;
PRIVATE bool_t bAppTimerStarted = FALSE;
PRIVATE bool_t bNwkJoined = FALSE;



/****************************************************************************
 *
 * NAME: AppColdStart
 *
 * DESCRIPTION:
 * Entry point for application. Initialises system, starts scan then
 * processes interrupts.
 *
 * RETURNS:
 * void, never returns
 *
 ****************************************************************************/
PUBLIC void AppColdStart(void)
{
    /* General initialisation: reset hardware */
    JZS_sConfig.u32Channel 	= WSN_CHANNEL;
    JZS_sConfig.u16PanId 	= WSN_PAN_ID;

    /* General initialisation: reset hardware */
    vInit();

    /* No return from the above function call */
}

/****************************************************************************
 *
 * NAME: AppWarmStart
 *
 * DESCRIPTION:
 * Entry point for application from boot loader. Simply jumps to AppColdStart
 * as, in this instance, application will never warm start.
 *
 * RETURNS:
 * Never returns.
 *
 ****************************************************************************/
PUBLIC void AppWarmStart(void)
{
    AppColdStart();
}

/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/
/****************************************************************************
 *
 * NAME: vInit
 *
 * DESCRIPTION:
 * Initialises Zigbee stack and hardware. Final action is to start BOS, from
 * which there is no return. Subsequent application actions occur in the
 * functions defined above.
 *
 * RETURNS:
 * No return from this function
 *
 ****************************************************************************/
PRIVATE void vInit(void)
{
    /* Initialise Zigbee stack */
    JZS_u32InitSystem(TRUE);

    /* Set DIO for LEDs */
    vLedInitRfd();
    vLedControl(0,0);
    vLedControl(1,0);

    /* Set sensors */
    vInitSensors();

    /* Intialise serial comms */
    vSerial_Init(UART_BAUD_RATE);

    /* Start BOS */
    (void)bBosRun(TRUE);

    /* No return from the above function call */
}

/****************************************************************************
 *
 * NAME: vInitSensors
 *
 * DESCRIPTION:
 * Initialise the temperature/humidity sensor and set the ADC to measure the
 * supply voltage.
 *
 ****************************************************************************/
PRIVATE void vInitSensors(void)
{
    /* Initialise temp/humidity sensor interface */
    vHTSreset();
    sTempHumiditySensor.eState = E_STATE_READ_TEMP_HUMID_IDLE;

    /* Initialise ADC for internal battery voltage measurement */
    sBattSensor.eState = E_STATE_READ_BATT_VOLT_IDLE;

    sGPSSensor.eState = E_GPS_READ_NONE;

    vAHI_ApConfigure(E_AHI_AP_REGULATOR_ENABLE,
                     E_AHI_AP_INT_DISABLE,
                     E_AHI_AP_SAMPLE_2,
                     E_AHI_AP_CLOCKDIV_2MHZ,
                     E_AHI_AP_INTREF);

    /* Wait until the analogue peripheral regulator has come up before setting
       the ADC. */
    while(!bAHI_APRegulatorEnabled());

    vAHI_AdcEnable(E_AHI_ADC_CONVERT_DISABLE,
                   E_AHI_AP_INPUT_RANGE_2,
                   E_AHI_ADC_SRC_VOLT);
}

/****************************************************************************
 *
 * NAME: vAppTick
 *
 * DESCRIPTION:
 *
 * Called by a BOS timer expiry. Reads sensor data and if complete transmits
 * to coordinator.
 *
 ****************************************************************************/
#if 0
PRIVATE void vAppTick(void *pvMsg, uint8 u8Param)
{
    uint8 u8Msg;
    uint8 u8TimerId;
    Toggle(1);

    /* Read sensor data */
    vReadTempHumidity();
    vReadBatteryVoltage();
    vReadGPS(); 

    if (u8AppTicks++ > APP_DATA_SEND_PERIOD) {
        /* If sensor reads are compete */
        if (   (sBattSensor.eState         == E_STATE_READ_BATT_VOLTS_READY) 
            && (sTempHumiditySensor.eState == E_STATE_READ_TEMP_HUMID_READY)
            && (E_GPS_READ_NONE == sGPSSensor.eState) )
        {
    	    /* Toggle LED1 to show we are alive */
            Toggle(0);

    	    u8AppTicks = 0;

        /* Transmit data to coordinator */
    	    vSendData();

            sBattSensor.eState         = E_STATE_READ_BATT_VOLT_IDLE;
            sTempHumiditySensor.eState = E_STATE_READ_TEMP_HUMID_IDLE;
            sGPSSensor.eState = E_GPS_READ_NONE;
    	}
    }
    (void)bBosCreateTimer(vAppTick, &u8Msg, 0, (APP_TICK_PERIOD_ms / 10), &u8TimerId);
}
#endif
// dummy
PRIVATE void vAppTick(void *pvMsg, uint8 u8Param)
{
    uint8 u8Msg;
    uint8 u8TimerId;
    Toggle(1);

    /* Read sensor data */
    vReadTempHumidity();
    vReadBatteryVoltage();
    vReadGPS(); 

    if (u8AppTicks++ > APP_DATA_SEND_PERIOD) {
        if (   (sBattSensor.eState         == E_STATE_READ_BATT_VOLTS_READY) 
            && (sTempHumiditySensor.eState == E_STATE_READ_TEMP_HUMID_READY) 
            && (E_GPS_READ_COMPLETE == sGPSSensor.eState) )
        {  
            Toggle(0);

            u8AppTicks = 0;

            /* Transmit data to coordinator */
            vSendData();

            sBattSensor.eState         = E_STATE_READ_BATT_VOLT_IDLE;
            sTempHumiditySensor.eState = E_STATE_READ_TEMP_HUMID_IDLE;
            sGPSSensor.eState = E_GPS_READ_NONE;
        }
    }
    (void)bBosCreateTimer(vAppTick, &u8Msg, 0, (APP_TICK_PERIOD_ms / 10), &u8TimerId);
}

/****************************************************************************
 *
 * NAME: vReadBatteryVoltage
 *
 * DESCRIPTION:
 *
 * Uses ADC to read supply voltage. Measurement is performed using a state
 * machine to ensure that it never blocks.
 *
 ****************************************************************************/
PRIVATE void vReadBatteryVoltage(void)
{
    uint16 u16AdcReading;

	switch(sBattSensor.eState)
	{
		case E_STATE_READ_BATT_VOLT_IDLE:
	    	vAHI_AdcStartSample();
	    	sBattSensor.eState = E_STATE_READ_BATT_VOLTS_ADC_CONVERTING;
			break;

		case E_STATE_READ_BATT_VOLTS_ADC_CONVERTING:
	    	if (!bAHI_AdcPoll()) {
	    	    sBattSensor.eState = E_STATE_READ_BATT_VOLTS_COMPLETE;
	    	}
			break;

		case E_STATE_READ_BATT_VOLTS_COMPLETE:

		    u16AdcReading = u16AHI_AdcRead();

		    /* Input range is 0 to 2.4V. ADC has full scale range of 12 bits.
		       Therefore a 1 bit change represents a voltage of approx 586uV */
		    sBattSensor.u16Reading = ((uint32)((uint32)(u16AdcReading * 586) +
                                     ((uint32)(u16AdcReading * 586) >> 1)))  /
                                     1000;

	    	sBattSensor.eState = E_STATE_READ_BATT_VOLTS_READY;
			break;

		case E_STATE_READ_BATT_VOLTS_READY:
			break;

		default:
			break;
	}
}

/****************************************************************************
 *
 * NAME: vReadTempHumidity
 *
 * DESCRIPTION:
 *
 * Read temperature/humidity sensor. Reading is performed using a state machine
 * to ensure that it never blocks.
 *
 ****************************************************************************/
PRIVATE void vReadTempHumidity(void)
{
    switch(sTempHumiditySensor.eState)
	{
		case E_STATE_READ_TEMP_HUMID_IDLE:
		    vHTSstartReadHumidity();
			sTempHumiditySensor.eState = E_STATE_READ_HUMID_RUNNING;
		break;

		case E_STATE_READ_HUMID_RUNNING:
			if ((u32AHI_DioReadInput() & HTS_DATA_DIO_BIT_MASK) == 0)
			{
				sTempHumiditySensor.eState = E_STATE_READ_TEMP_HUMID_COMPLETE;
			}
			break;

		case E_STATE_READ_TEMP_HUMID_COMPLETE:
			sTempHumiditySensor.u16HumidReading = u16HTSreadHumidityResult();
			sTempHumiditySensor.eState     = E_STATE_READ_TEMP_START;
			break;

		case E_STATE_READ_TEMP_START:
		    vHTSstartReadTemp();
			sTempHumiditySensor.eState = E_STATE_READ_TEMP_HUMID_RUNNING;
			break;

		case E_STATE_READ_TEMP_HUMID_RUNNING:
			if ((u32AHI_DioReadInput() & HTS_DATA_DIO_BIT_MASK) == 0)
			{
				sTempHumiditySensor.eState = E_STATE_READ_TEMP_COMPLETE;
			}
			break;

		case E_STATE_READ_TEMP_COMPLETE:
			sTempHumiditySensor.u16TempReading = u16HTSreadTempResult();
			sTempHumiditySensor.eState     = E_STATE_READ_TEMP_HUMID_READY;
			break;

		case E_STATE_READ_TEMP_HUMID_READY:
			break;

		default:
			break;
	}
}

/****************************************************************************
 *
 * NAME: vSendData
 *
 * DESCRIPTION:
 *
 * Transmit sensor data to coordinator.
 *
 ****************************************************************************/

PRIVATE void vSendData(void)
{
    AFDE_DATA_REQ_INFO  asAfdeDataReq[1];
    AF_ADDRTYPE         hDstAddr;
    netMessage          aMessage;
    int i;
    uint8*              rawMessage;


    rawMessage = (uint8*)&aMessage;
    for ( i=0; i<sizeof(netMessage); i++ ) { rawMessage[i] = 0; }
    hDstAddr.hAddrMode  = DEV_16BIT_ADDR;
    hDstAddr.u16Address = 0x0000;
    hDstAddr.u8EndPoint = WSN_DATA_SINK_ENDPOINT;

    asAfdeDataReq[0].u8SequenceNum = gsAIS.u8AfTransactionSequence++;
    asAfdeDataReq[0].u8DividedAfduLen = sizeof(netMessage);

    // fill data
    for (i=0;i<10;i++) {aMessage.utcTime[i] = sGPSSensor.utcTime[i];}
    for (i=0;i<9; i++) {aMessage.latitude[i] = sGPSSensor.latitude[i];}
    for (i=0;i<10;i++) {aMessage.longitude[i] = sGPSSensor.longitude[i];}

    aMessage.latNS     = sGPSSensor.latNS;
    aMessage.lngEW     = sGPSSensor.lngEW;
    aMessage.u16Temp   = sTempHumiditySensor.u16TempReading;
    aMessage.u16Humid  = sTempHumiditySensor.u16HumidReading;
    aMessage.u16Volt   = sBattSensor.u16Reading;

    aMessage.address.u32L = kzExtendedAddress.u32L;
    aMessage.address.u32H = kzExtendedAddress.u32H;

    // send data
    afdeDataRequest(hDstAddr,                   /* Destination address */
                    WSN_DATA_SOURCE_ENDPOINT,   /* Source endpoint */
                    WSN_PROFILE_ID,             /* Profile ID */
                    WSN_CID_SENSOR_READINGS,    /* Cluster ID */
                    MSG,                        /* Frame type */
                    1,                          /* Transactions */
                    asAfdeDataReq,              /* Transaction info */
                    (uint8*)(&aMessage),        /* Transaction data */
                    APS_TXOPTION_NONE,          /* Transmit options */
                    SUPPRESS_ROUTE_DISCOVERY,   /* Route discovery mode */
                    0);                         /* Radius count */
}

/****************************************************************************/
/***               Functions called by the stack                          ***/
/****************************************************************************/

/****************************************************************************
 *
 * NAME: JZA_vZdpResponse
 *
 * DESCRIPTION:
 * Called when a ZDP response frame has been received. In this application no
 * action is taken as no ZDP responses are anticipated.
 *
 * PARAMETERS:      Name           RW  Usage
 *                  u8Type         R   ZDP response type
 *                  pu8Payload     R   Payload buffer
 *                  u8PayloadLen   R   Length of payload
 *
 ****************************************************************************/
PUBLIC void JZA_vZdpResponse(uint8  u8Type,
                             uint8  u8LQI,
                             uint8 *pu8Payload,
                             uint8  u8PayloadLen)

{
}

/****************************************************************************
 *
 * NAME: JZA_pu8AfMsgObject
 *
 * DESCRIPTION:
 * Called when a MSG transaction has been received with a matching endpoint.
 * In this application no action is taken as no MSG transactions are expected.
 *
 * PARAMETERS:      Name           RW  Usage
 *                  afSrcAddr      R   Address of sender device
 *                  dstEndPoint    R   Endpoint at receiver
 *                  clusterID      R   Pointer to cluster ID
 *                  afduLength     R   Pointer to length of data
 *                  pAfdu          R   Data array
 *
 * RETURNS:
 * NULL
 *
 ****************************************************************************/
PUBLIC uint8 JZA_u8AfMsgObject(AF_ADDRTYPE sAfSrcAddr,
                               uint8       u8ClusterID,
                               uint8       u8DstEndPoint,
                               uint8       u8LQI,
                               uint8      *pau8AfduInd,
                               uint8      *pu8ClusterIDRsp,
                               uint8      *pau8AfduRsp)
{
    return 0;
}

/****************************************************************************
 *
 * NAME: JZA_vAfKvpResponse
 *
 * DESCRIPTION:
 * Called after a KVP transaction with acknowledgement request, when the
 * acknowledgement arrives. In this application no action is taken as no
 * KVP transaction acknowledgements are expected.
 *
 * PARAMETERS:      Name                   RW  Usage
 *                  srcAddressMod          R   Address of sender device
 *                  transactionSequenceNum R   KVP transaction number
 *                  commandTypeIdentifier  R   KVP command type
 *                  dstEndPoint            R   Endpoint at receiver
 *                  clusterID              R   Cluster ID
 *                  attributeIdentifier    R   KVP attribute ID
 *                  errorCode              R   Result code
 *                  afduLength             R   Length of payload data
 *                  pAfdu                  R   Payload data array
 *
 ****************************************************************************/
PUBLIC void JZA_vAfKvpResponse(AF_ADDRTYPE         srcAddressMod,
                               uint8               u8LQI,
                               uint8               transactionSequenceNum,
                               AF_COMMAND_TYPE_ID  commandTypeIdentifier,
                               uint8               dstEndPoint,
                               uint8               clusterID,
                               uint16              attributeIdentifier,
                               uint8               errorCode,
                               uint8               afduLength,
                               uint8              *pAfdu )
{
}

/****************************************************************************
 *
 * NAME: JZA_eAfKvpObject
 *
 * DESCRIPTION:
 * Called when a KVP transaction has been received with a matching endpoint.
 *
 * PARAMETERS:      Name           RW  Usage
 *                  afSrcAddr      R   Address of sender device
 *                  u8DstEndpoint  R   Endpoint at receiver
 *                  pu8ClusterId   R   Pointer to cluster ID
 *                  eCommandTypeId R   KVP command type
 *                  u16AttributeId R   KVP attribute ID
 *                  pu8AfduLength  R   Pointer to length of data
 *                  pu8Afdu        R   Data array
 *
 * RETURNS:
 * AF_ERROR_CODE
 *
 ****************************************************************************/
PUBLIC AF_ERROR_CODE JZA_eAfKvpObject(AF_ADDRTYPE         afSrcAddr,
                                      uint8               u8LQI,
                                      uint8               u8DstEndpoint,
                                      uint8               u8SequenceNum,
                                      uint8              *pu8ClusterId,
                                      AF_COMMAND_TYPE_ID  eCommandTypeId,
                                      uint16              u16AttributeId,
                                      uint8              *pu8AfduLength,
                                      uint8              *pu8Afdu)
{
    return KVP_SUCCESS;
}

/****************************************************************************
 *
 * NAME: JZA_vAppDefineTasks
 *
 * DESCRIPTION:
 * Called by Zigbee stack during initialisation to allow the application to
 * initialise any tasks that it requires. This application requires none.
 *
 * RETURNS:
 * void
 *
 ****************************************************************************/
PUBLIC void JZA_vAppDefineTasks(void)
{
}

/****************************************************************************
 *
 * NAME: JZA_vPeripheralEvent
 *
 * DESCRIPTION:
 * Called when a hardware event causes an interrupt. This function is called
 * from within the interrupt context so should be brief. In this case, the
 * information is placed on a simple FIFO queue to be processed later.
 *
 * PARAMETERS: Name          RW  Usage
 *             u32Device     R   Peripheral generating interrupt
 *             u32ItemBitmap R   Bitmap of interrupt sources within peripheral
 *
 ****************************************************************************/
PUBLIC void JZA_vPeripheralEvent(uint32 u32Device, uint32 u32ItemBitmap) {
}

/****************************************************************************
 *
 * NAME: JZA_vAppEventHandler
 *
 * DESCRIPTION:
 * Called regularly by the task scheduler. This function reads the hardware
 * event queue and processes the events therein. It is important that this
 * function exits after a relatively short time so that the other tasks are
 * not adversely affected.
 *
 ****************************************************************************/
PUBLIC void JZA_vAppEventHandler(void)
{
    uint8 u8Msg;
    uint8 u8TimerId;

    if (!bAppTimerStarted)
    {
        if (bNwkJoined)
        {
            bAppTimerStarted = TRUE;
            Toggle(1);
            (void)bBosCreateTimer(vAppTick, &u8Msg, 0, (APP_TICK_PERIOD_ms / 10), &u8TimerId);
        }
    }
}

/****************************************************************************
 *
 * NAME: JZA_boAppStart
 *
 * DESCRIPTION:
 * Called by Zigbee stack during initialisation.
 *
 * RETURNS:
 * TRUE
 *
 ****************************************************************************/
PUBLIC bool_t JZA_boAppStart(void)
{
    JZS_vStartStack();
    return TRUE;
}

/****************************************************************************
 *
 * NAME: JZA_vStackEvent
 *
 * DESCRIPTION:
 * Called by Zigbee stack to pass an event up to the application.
 *
 * RETURNS:
 * TRUE
 *
 ****************************************************************************/
PUBLIC void JZA_vStackEvent(teJZS_EventIdentifier eEventId,
                            tuJZS_StackEvent *puStackEvent)
{
    if (eEventId == JZS_EVENT_NWK_JOINED_AS_ROUTER) {
        bNwkJoined = TRUE;
    }
}


static char rawMessage[250];
static int position;

PRIVATE void vReadGPS(void) 
{
    char aChar;
    aChar = '%';
    switch(sGPSSensor.eState) {
        case E_GPS_READ_NONE: {
            position = 0;
            while ( !bSerialQ_Empty(RX_QUEUE)) {
                aChar = i16Serial_RxChar();
                if ( '$' == aChar ) {
                    rawMessage[position++] = aChar;
                }
                sGPSSensor.eState = E_GPS_READ_RUNNING;
                break;
            } 
            if ( E_GPS_READ_RUNNING != sGPSSensor.eState ) {
                break;
            }
        }
        case E_GPS_READ_RUNNING: {
            while ( !bSerialQ_Empty(RX_QUEUE)) {
                if ( 6 == position ) {
                    if (!(  'G' == rawMessage[1] 
                         && 'P' == rawMessage[2] 
                         && 'G' == rawMessage[3] 
                         && 'G' == rawMessage[4] 
                         && 'A' == rawMessage[5] ))
                    {
                        position = 0;
                        sGPSSensor.eState = E_GPS_READ_NONE; 
                    } 
                }
                aChar = i16Serial_RxChar();
                if ( 0x0d == aChar ) {
                    rawMessage[position++] = 0x00;
                    vParseGPS(); 
                    sGPSSensor.eState = E_GPS_READ_COMPLETE;
                    break;
                } else {
                    rawMessage[position++] = aChar;
                } 
            }
            break;
        }
        case E_GPS_READ_COMPLETE: {
            while ( !bSerialQ_Empty(RX_QUEUE)) {
                aChar = i16Serial_RxChar();
            }
            break;
        }
    }
}


PRIVATE void vParseGPS(void) {

    uint16 pos;
    uint16 part;
    uint16 start;
    uint16 i;

    pos = 0;
    part = 0;
    start = 0;
    while ( 0x00 != rawMessage[pos] ) {
        while ( ',' != rawMessage[pos] && 0x00 != rawMessage[pos] ) {
            pos++;
        }
        if ( 1 == part ) {
            for ( i=start; i<pos && i<(start+10); i++ ) {
               sGPSSensor.utcTime[i-start]=rawMessage[i];
            }
        } else if ( 2 == part ) {
            for ( i=start; i<pos && i<(start+9); i++ ) {
               sGPSSensor.latitude[i-start]=rawMessage[i];
            }
        } else if ( 3 == part ) {
            sGPSSensor.latNS = rawMessage[start];
        } else if ( 4 == part ) {
            for ( i=start; i<pos && i<(start+10); i++ ) {
               sGPSSensor.longitude[i-start]=rawMessage[i];
            }
        } else if ( 5 == part ) {
            sGPSSensor.lngEW = rawMessage[start];
        } else if ( 0 != part ) {
            break;
        }
        part++;
        pos++;
        start = pos;
    }

}

#if 0
// Dummy implementation
PRIVATE void vParseGPS(void) {
    int i;
    char* utc = "112233.345";
    char* lat = "23.234534";
    char* lng = "123.234534";

    for ( i=0; i<10; i++ ) {
       sGPSSensor.utcTime[i]=utc[i];
    }
    for ( i=0; i<9; i++ ) {
       sGPSSensor.latitude[i]=lat[i];
    }
    sGPSSensor.latNS = 'N';
    for ( i=0; i<10; i++ ) {
       sGPSSensor.longitude[i]=lng[1];
    }
    sGPSSensor.lngEW = 'W';
}

#endif
/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
