package com.affilia.cargo.agent;

import java.io.IOException;
import java.io.InputStream;

import java.util.LinkedList;
import java.util.Collections;
import java.util.TooManyListenersException;

import gnu.io.CommPortIdentifier;
import gnu.io.SerialPort;
import gnu.io.SerialPortEvent;
import gnu.io.NoSuchPortException;
import gnu.io.PortInUseException;
import gnu.io.SerialPortEventListener;
import gnu.io.UnsupportedCommOperationException;

import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.HttpStatus;
import org.apache.commons.httpclient.methods.PostMethod;
import org.apache.commons.httpclient.methods.multipart.FilePart;
import org.apache.commons.httpclient.methods.multipart.MultipartRequestEntity;
import org.apache.commons.httpclient.methods.multipart.Part;
import org.apache.commons.httpclient.methods.multipart.StringPart;
import org.apache.commons.httpclient.params.HttpMethodParams;

public class Agent 
    implements SerialPortEventListener 
{
    private static final String NETWORK = "2";
    private static final String PORT = "COM1";
    private static final String SERVER = "http://localhost:8080/cargo/stream";

    public static void main(String[] args) {
//        Agent agent = new Agent(Agent.NETWORK, Agent.PORT, Agent.SERVER);
        Agent agent = new Agent(args[0], args[1], args[2]);
        agent.start();
    } 

    private String network = null;
    private String serverURL = null;
    private CommPortIdentifier portId = null;
    private SerialPort serialPort = null;
    private InputStream inputStream = null;

    private byte[] readBuffer = new byte[128];
    private int readCounter = 0;
    private boolean inPackage = false;
    private LinkedList<byte[]> queue = new LinkedList<byte[]>();

    public Agent(final String network, final String port, final String server) {
        this.network = network;
        serverURL = server;
        try {
            portId = CommPortIdentifier.getPortIdentifier(port);
            if ( portId.isCurrentlyOwned() ) {
                throw new RuntimeException("Error: Port is currently in use");
            }
        } catch (NoSuchPortException e) {
            throw new RuntimeException(e);
        }

        // initalize serial port
        try {
            serialPort = (SerialPort) portId.open(getClass().getName(), 2000);
        } catch (PortInUseException e) {
            throw new RuntimeException(e);
        }
   
        try {
            inputStream = serialPort.getInputStream();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
   
        try {
            serialPort.addEventListener(this);
        } catch (TooManyListenersException e) {
            throw new RuntimeException(e);
        }
      
        // activate the DATA_AVAILABLE notifier
        serialPort.notifyOnDataAvailable(true);
   
        try {
         // set port parameters
            serialPort.setSerialPortParams(115200, SerialPort.DATABITS_8, 
                     SerialPort.STOPBITS_1, 
                     SerialPort.PARITY_NONE);
        } catch (UnsupportedCommOperationException e) {
            throw new RuntimeException(e);
        }
      
    }

    public void start() {
        while ( true ) {
            byte[] pack = null;
            synchronized ( queue ) {
                pack = queue.poll();
            }

            if ( null != pack ) {
                PostMethod post = new PostMethod(serverURL);
                post.addParameter("network", network);
                post.addParameter("package", new String(pack));
                
                HttpClient httpclient = new HttpClient();
                try {
                    int result = httpclient.executeMethod(post);
                    System.out.println("Response status code: " + result);
                    System.out.println("Response body: ");
                    System.out.println(post.getResponseBodyAsString());
                } catch (Exception e) {
                    e.printStackTrace();
                } finally {
                    post.releaseConnection();
                }
            } else {
                try {
                    Thread.sleep(10000);
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }

        // to do: implement it
    }

    public void serialEvent(SerialPortEvent event) {
        switch (event.getEventType()) {
            case SerialPortEvent.BI:
            case SerialPortEvent.OE:
            case SerialPortEvent.FE:
            case SerialPortEvent.PE:
            case SerialPortEvent.CD:
            case SerialPortEvent.CTS:
            case SerialPortEvent.DSR:
            case SerialPortEvent.RI:
            case SerialPortEvent.OUTPUT_BUFFER_EMPTY:
                break;
            case SerialPortEvent.DATA_AVAILABLE:
                try {
                    //System.out.println("\n" + (byte) '*' + "\n");
                    while (inputStream.available() > 0) {
                        byte b = (byte) inputStream.read();
                        //System.out.print(""+b);
                        if ( ((byte) '*') == b ) {
                            if ( inPackage ) {
                                byte[] packet = new byte[readCounter];
                                System.arraycopy(readBuffer, 0, packet, 0, readCounter);
                                synchronized ( queue ) {
                                    queue.offer(packet);
                                }
                                
                                String result  = new String(packet);
                                System.out.println("Package: " + result);
                            }
                            readCounter = 0;
                            inPackage = true;
                        } else {
                            if ( inPackage ) {
                                readBuffer[readCounter++] = b;
                            } 
                        }
                    } 
                } catch (IOException e) {
                    e.printStackTrace();
                }
                break;
        }
    } 

}
