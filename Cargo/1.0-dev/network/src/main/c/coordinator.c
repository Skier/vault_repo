/****************************************************************************
 *
 * MODULE:			   WSN - Coordinator
 *
 * COMPONENT:          $RCSfile: WSN_Coordinator.c,v $
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
 * Implements a Wireless Sensor Network Coordinator Node using Jennic Zigbee
 * stack. Receives data from compatible nodes via the radio and retransmits to
 * to host using UART.
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
#include <LedControl.h>
#include <AppHardwareApi.h>
#include <Utilities.h>
#include <JZ_Api.h>

#include "serial.h"
#include "printf.h"
#include "profile.h"
#include "message.h"
#include "toggle.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/
/* Timing values */
#define APP_TICK_PERIOD_ms			 500
#define UART_BAUD_RATE      115200

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/
PRIVATE void vInit(void);
PRIVATE void vToggleLed(void *pvMsg, uint8 u8Dummy);
PRIVATE void vTxSerialDataFrame(uint16 u16NodeId, uint8* data);

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/
PRIVATE bool_t bNwkStarted = FALSE;
PRIVATE bool_t bAppTimerStarted = FALSE;

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
PRIVATE void vTxSerialDataFrame(uint16 u16NodeId, uint8* data)
{
    netMessage* aMessage; 
    uint8* low;
    uint8* high;
    int mac[8];
    int i;
    int temp;
    uint8* c;

    aMessage = (netMessage*)data;

    low = (uint8*)&(aMessage->address.u32L);
    high = (uint8*)&(aMessage->address.u32H);

    mac[0] = high[0];
    mac[1] = high[1];
    mac[2] = high[2];
    mac[3] = high[3];
    mac[4] = low[0];
    mac[5] = low[1];
    mac[6] = low[2];
    mac[7] = low[3];


    printf("I;");

    for (i=0; i<8; i++) {
       printf("%02X%s", mac[i], i==7?";":":");
    }

    printf("%s;", aMessage->utcTime);
    printf("%s;", aMessage->latitude);
    printf("%c;", aMessage->latNS);
    printf("%s;", aMessage->longitude);
    printf("%c;", aMessage->lngEW);

//stupide Jennic, all other way go to hang coordinator
    c = (uint8*)(&(aMessage->u16Humid));
    temp = *c;
    temp <<= 8;
    temp += *(c+1);
    printf("%d;", temp);  

    c = (uint8*)(&(aMessage->u16Temp));
    temp = *c;
    temp <<= 8;
    temp += *(c+1);
    printf("%d;", temp);  

    c = (uint8*)(&(aMessage->u16Volt));
    temp = *c;
    temp <<= 8;
    temp += *(c+1);
    printf("%d;0;*", temp);  
// end of stupide Jennic
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
    Toggle(0);

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
    if ((sAfSrcAddr.hAddrMode == DEV_16BIT_ADDR) &&
        (u8DstEndPoint == WSN_DATA_SINK_ENDPOINT))
    {
        if(u8ClusterID == WSN_CID_SENSOR_READINGS) {
            Toggle(1);
            vTxSerialDataFrame(sAfSrcAddr.u16Address, pau8AfduInd);
        }
    }
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
