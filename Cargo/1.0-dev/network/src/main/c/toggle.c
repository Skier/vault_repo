#include <jendefs.h>
#include <LedControl.h>
#include <AppHardwareApi.h>
#include "toggle.h"

static bool_t bToggle[2] = {0,0};

PUBLIC void Toggle(int led) {

    if ( 1 == bToggle[led] ) {
        bToggle[led] = 0;  
    } else {
        bToggle[led] = 1;  
    }
    vLedControl(led, bToggle[led]);
}

