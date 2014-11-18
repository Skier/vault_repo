/**
 * $Id: Timer.java 189 2007-05-17 14:22:30Z moritur $
 */
package com.affilia.cargo.client;

public class Timer 
    extends com.google.gwt.user.client.Timer {
    
    private TimerListener m_timerListener;
    
    public Timer(TimerListener timerListener) {
        m_timerListener = timerListener;
    }
    
    public void run() {
        m_timerListener.onTime();
    }
    
}
