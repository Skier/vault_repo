/****************************************************************************
 *
 * MODULE:             uart.c
 *
 * COMPONENT:          $RCSfile: uart.c,v $
 *
 * VERSION:            $Name:  $
 *
 * REVISION:           $Revision: 1.3 $
 *
 * DATED:              $Date: 2006/12/11 10:38:48 $
 *
 * STATUS:             $State: Exp $
 *
 * AUTHOR:             Ian Morris
 *
 * DESCRIPTION
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
#include <jendefs.h>
#include <AppHardwareApi.h>

#include "serialq.h"
#include "uart.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/
#define UART                E_AHI_UART_0

#if UART == E_AHI_UART_0
    #define UART_START_ADR  	0x30000000UL
#else
    #define UART_START_ADR  	0x40000000UL
#endif

#define UART_LCR_OFFSET 	0x0C
#define UART_DLM_OFFSET 	0x04

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/
static uint8 chars[256];
static uint8 current;


/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/
PRIVATE void vUART_SetBuadRate(uint32 u32BaudRate);

#if UART == E_AHI_UART_0
PRIVATE void vUART_HandleUart0Interrupt(uint32 u32Device, uint32 u32ItemBitmap);
#else
PRIVATE void vUART_HandleUart1Interrupt(uint32 u32Device, uint32 u32ItemBitmap);
#endif

/****************************************************************************
 *
 * NAME: vUART_Init
 *
 * DESCRIPTION:
 *
 * PARAMETERS:      Name            RW  Usage
 * None.
 *
 * RETURNS:
 * None.
 *
 * NOTES:
 * None.
 ****************************************************************************/
PUBLIC void vUART_Init(uint32 u32BaudRate) {
    /* Enable UART 0 */
    vAHI_UartEnable(UART);

    vAHI_UartReset(UART, TRUE, TRUE);
    vAHI_UartReset(UART, FALSE, FALSE);

    /* Register function that will handle UART interrupts */
    #if UART == E_AHI_UART_0
        vAHI_Uart0RegisterCallback(vUART_HandleUart0Interrupt);
    #else
        vAHI_Uart1RegisterCallback(vUART_HandleUart1Interrupt);
    #endif

    /* Set the clock divisor register to give required buad, this has to be done
       directly as the normal routines (in ROM) do not support all baud rates */
    vUART_SetBuadRate(u32BaudRate); 
    current = 0;

    vAHI_UartSetControl(UART, FALSE, FALSE, E_AHI_UART_WORD_LEN_8, TRUE, FALSE);
    vAHI_UartSetInterrupt(UART, FALSE, FALSE, TRUE, TRUE, E_AHI_UART_FIFO_LEVEL_1);
}

/****************************************************************************
 *
 * NAME: vUART_SetBuadRate
 *
 * DESCRIPTION:
 *
 * PARAMETERS: Name        RW  Usage
 *
 * RETURNS:
 *
 ****************************************************************************/
PRIVATE void vUART_SetBuadRate(uint32 u32BaudRate)
{
    uint8 *pu8Reg;
    uint8  u8TempLcr;
    uint16 u16Divisor;
    uint32 u32Remainder;

    /* Put UART into clock divisor setting mode */
    pu8Reg    = (uint8 *)(UART_START_ADR + UART_LCR_OFFSET);
    u8TempLcr = *pu8Reg;
    *pu8Reg   = u8TempLcr | 0x80;

    /* Write to divisor registers:
       Divisor register = 16MHz / (16 x baud rate) */
    u16Divisor = (uint16)(16000000UL / (16UL * u32BaudRate));

    /* Correct for rounding errors */
    u32Remainder = (uint32)(16000000UL % (16UL * u32BaudRate));

    if (u32Remainder >= ((16UL * u32BaudRate) / 2))
    {
        u16Divisor += 1;
    }

    pu8Reg  = (uint8 *)UART_START_ADR;
    *pu8Reg = (uint8)(u16Divisor & 0xFF);
    pu8Reg  = (uint8 *)(UART_START_ADR + UART_DLM_OFFSET);
    *pu8Reg = (uint8)(u16Divisor >> 8);

    /* Put back into normal mode */
    pu8Reg    = (uint8 *)(UART_START_ADR + UART_LCR_OFFSET);
    u8TempLcr = *pu8Reg;
    *pu8Reg   = u8TempLcr & 0x7F;
}

/****************************************************************************
 *
 * NAME: vUART_StartTx
 *
 * DESCRIPTION:
 *
 * PARAMETERS:      Name            RW  Usage
 * None.
 *
 * RETURNS:
 * None.
 *
 * NOTES:
 * None.
 ****************************************************************************/
PUBLIC void vUART_StartTx(void)
{
    /* Has interrupt driven transmit stalled (tx fifo is empty) */
    if (u8AHI_UartReadLineStatus(UART) & E_AHI_UART_LS_THRE)
    {
        if(!bSerialQ_Empty(TX_QUEUE))
        {
            vAHI_UartWriteData(UART, u8SerialQ_RemoveItem(TX_QUEUE));
        }
    }
}

/****************************************************************************
 *
 * NAME: vUART_TxCharISR
 *
 * DESCRIPTION:
 *
 * PARAMETERS:      Name            RW  Usage
 * None.
 *
 * RETURNS:
 * None.
 *
 * NOTES:
 * None.
 ****************************************************************************/
PUBLIC void vUART_TxCharISR(void)
{
    if(!bSerialQ_Empty(TX_QUEUE))
	{
        vAHI_UartWriteData(UART, u8SerialQ_RemoveItem(TX_QUEUE));
	}
}

/****************************************************************************
 *
 * NAME: vUART_RxCharISR
 *
 * DESCRIPTION:
 *
 * PARAMETERS:      Name            RW  Usage
 * None.
 *
 * RETURNS:
 * None.
 *
 * NOTES:
 * None.
 ****************************************************************************/
PUBLIC void vUART_RxCharISR(uint8 u8RxChar)
{
    vSerialQ_AddItem(RX_QUEUE, u8RxChar);
}

#if UART == E_AHI_UART_0
/****************************************************************************
 *
 * NAME: vUART_HandleUart0Interrupt
 *
 * DESCRIPTION:
 *
 * PARAMETERS:      Name            RW  Usage
 * None.
 *
 * RETURNS:
 * None.
 *
 * NOTES:
 * None.
 ****************************************************************************/
PRIVATE void vUART_HandleUart0Interrupt(uint32 u32Device, uint32 u32ItemBitmap)
{
    uint8 aChar;
    uint8 i;

    if (u32Device == E_AHI_DEVICE_UART0)
    {
        if ((u32ItemBitmap & 0x000000FF) == E_AHI_UART_INT_RXDATA)
        {
            aChar = u8AHI_UartReadData(E_AHI_UART_0);
            if ( '$' == aChar ) {
                if ( 0 != current ) {
                    for ( i=0; i< current; i++ )  {
                        vUART_RxCharISR(chars[i]);
                    }
                }
                current = 0;
                chars[current++] = aChar;  
            } else {
                if ( 0 != current ) {
                    chars[current++] = aChar;
                    if ( 6 == current ) {
                        if ( ! ('$' == chars[0] && 'G' == chars[1] 
                          && 'P' == chars[2] && 'G' == chars[3] 
                          && 'G' == chars[4] && 'A' == chars[5] ) )
                        {
                            current = 0;
                        }
                    }
                }
            }
        }  else if (u32ItemBitmap == E_AHI_UART_INT_TX) {
            vUART_TxCharISR();
        }
    }
}
#else
/****************************************************************************
 *
 * NAME: vUART_HandleUart1Interrupt
 *
 * DESCRIPTION:
 *
 * PARAMETERS:      Name            RW  Usage
 * None.
 *
 * RETURNS:
 * None.
 *
 * NOTES:
 * None.
 ****************************************************************************/
PRIVATE void vUART_HandleUart1Interrupt(uint32 u32Device, uint32 u32ItemBitmap)
{
    if (u32Device == E_AHI_DEVICE_UART1)
    {
        if ((u32ItemBitmap & 0x000000FF) == E_AHI_UART_INT_RXDATA)
        {
            vUART_RxCharISR(u8AHI_UartReadData(E_AHI_UART_1));
        }
        else if (u32ItemBitmap == E_AHI_UART_INT_TX)
        {
            vUART_TxCharISR();
        }
    }
}
#endif
/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
