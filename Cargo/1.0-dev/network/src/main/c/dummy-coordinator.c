/****************************************************************************/
/***        Include files                                                 ***/
/****************************************************************************/
#include <jendefs.h>
#include <LedControl.h>
#include <AppHardwareApi.h>
#include <Utilities.h>
#include <JZ_Api.h>

#include "serial.h"
#include "printf.h"
#include "profile.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/
/* Timing values */
#define APP_TICK_PERIOD_ms  10000
#define UART_BAUD_RATE      115200

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/
PRIVATE void vInit(void);
PRIVATE void SendUnitStatus(void);
PRIVATE void vToggleLed(void *pvMsg, uint8 u8Dummy);

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/
PRIVATE bool_t bNwkStarted = FALSE;
PRIVATE bool_t bAppTimerStarted = FALSE;

PRIVATE char* MACs[] = {"01:01:01:01:01:01:01:01",
                        "02:02:02:02:02:02:02:02",
                        "03:03:03:03:03:03:03:03"};

PRIVATE uint32 aLat   = 64000;
PRIVATE uint32 aLng   = 72000;
PRIVATE uint32 aHum   = 2342;
PRIVATE uint32 aTemp  = 34;
PRIVATE uint32 aVolt  = 35;
PRIVATE uint32 aLight = 456;
PRIVATE uint32 aTrans = 0;

PRIVATE uint16 aH = 12;
PRIVATE uint16 aM = 00;
PRIVATE uint16 aS = 00;

/****************************************************************************
 *
 * NAME: AppColdStart
 *
 * DESCRIPTION:
 * Entry point for application from boot loader. Initialises system and runs
 * main loop.
 *
 * RETURNS:
 * Never returns.
 *
 ****************************************************************************/
PUBLIC void AppColdStart(void)
{
    /* Set network information */
    JZS_sConfig.u32Channel = WSN_CHANNEL;
    JZS_sConfig.u16PanId   = WSN_PAN_ID;

    /* General initialisation */
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
    vLedInitFfd();
    vLedControl(0,0);
    vLedControl(1,0);

    /* Intialise serial comms */
    vSerial_Init(UART_BAUD_RATE);

    /* Start BOS */
    (void)bBosRun(TRUE);

    /* No return from the above function call */
}

/****************************************************************************
 *
 * NAME: vTxSerialDataFrame
 *
 * DESCRIPTION:
 * Transmits node data (address and sensor readings) to host via serial port.
 *
 * PARAMETERS: Name           RW  Usage
 *             u16NodeId      R   Short address of node that generated the data
 *             u16Humidity    R   Reading from humidity sensor (%)
 *             u16Temperature R   Reading from temperature sensor (degrees C)
 *             u16BattVoltage R   ADC reading of supply voltage (mv)
 *
 ****************************************************************************/
PRIVATE void SendUnitStatus(void) {
    aTrans += 1;
    aS += 10;
    if ( aS > 59 ) { 
       aS = aS % 60;
       aM += 1;
    }
    if ( aM > 59 ) {
       aM  = aM % 60;
       aH += 1;
    }
    if ( aH > 23 ) {
       aH = aH % 24;
    }

    if ( 0 != (aTrans % 37) ) { // Print information
        printf("I;%s;%02d%02d%02d.023;36.%d;N;96.%d;W;%d;%d;%d;%d*",
           MACs[aTrans%3], 
           aH, 
           aM, 
           aS,
           aLat + (aTrans % 99)*30, 
           aLng + (aTrans % 67)*40, 
           aHum  + (aTrans % 13),
           aTemp + (aTrans % 13),
           aVolt + (aTrans % 13),
           aLight+ (aTrans % 13) );
    } else { // Print join network 
        printf("J;%s;%02d%02d%02d.034;36.%d;N;96.%d;W*",
           MACs[aTrans%3], 
           aH, 
           aM, 
           aS,
           aLat + (aTrans % 99)*3, 
           aLng + (aTrans % 67)*4);
    }
}

/****************************************************************************
 *
 * NAME: vToggleLed
 *
 * DESCRIPTION:
 * Gets called by a BOS timer. Toggles LED1 to indicate we are alive.
 *
 ****************************************************************************/
PRIVATE void vToggleLed(void *pvMsg, uint8 u8Dummy)
{
    uint8 u8Msg;
    uint8 u8TimerId;
    static bool_t bToggle;

    if (bToggle) {
        vLedControl(0,0);
    } else {
        vLedControl(0,1);
    }
    bToggle = !bToggle;

    SendUnitStatus();

    (void)bBosCreateTimer(vToggleLed, &u8Msg, 0, (APP_TICK_PERIOD_ms / 10), &u8TimerId);
}

/****************************************************************************/
/***               Functions called by the stack                          ***/
/****************************************************************************/

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
void JZA_vAppEventHandler(void)
{
    uint8 u8Msg;
    uint8 u8TimerId;

    if (!bAppTimerStarted)
    {
        if (bNwkStarted)
        {
            bAppTimerStarted = TRUE;
            (void)bBosCreateTimer(vToggleLed, &u8Msg, 0, (APP_TICK_PERIOD_ms / 10), &u8TimerId);
        }
    }
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
PUBLIC void JZA_vPeripheralEvent(uint32 u32Device, uint32 u32ItemBitmap)
{
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
 * NAME: JZA_boAppStart
 *
 * DESCRIPTION:
 * Called by Zigbee stack during initialisation. Sets up the profile
 * information and starts the networking activity
 *
 * RETURNS:
 * TRUE
 *
 ****************************************************************************/
PUBLIC bool_t JZA_boAppStart(void)
{
    uint8 u8InputClusterCnt      = 1;
    uint8 au8InputClusterList[]  = {WSN_CID_SENSOR_READINGS};
    uint8 u8OutputClusterCnt     = 0;
    uint8 au8OutputClusterList[] = {};

    (void)afmeAddSimpleDesc(WSN_DATA_SINK_ENDPOINT,
                            WSN_PROFILE_ID,
                            0x0000,
                            0x00,
                            0x00,
                            u8InputClusterCnt,
                            au8InputClusterList,
                            u8OutputClusterCnt,
                            au8OutputClusterList);

    JZS_vStartStack();

    return TRUE;
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
 * NAME: JZA_pu8AfMsgObject
 *
 * DESCRIPTION:
 * Called when a MSG transaction has been received with a matching endpoint.
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
    if (eEventId == JZS_EVENT_NWK_STARTED)
    {
        bNwkStarted = TRUE;
    }
}

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
