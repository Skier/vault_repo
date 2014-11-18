/*
 * Copyright (c) 2010-2011 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 *
 * Contributors:
 *    Intuit Partner Platform – initial contribution
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Configuration;

namespace Intuit.Platform.Client.Core.IDS
{
    internal class Logger
    {
        private Logger()
        {
        }

        public static void WriteToLog(TraceLevel messageLevel, string messageToWrite)
        {
            TraceSwitch traceSwt = new TraceSwitch("logSwitch", "Test Switch");
           
            if ((int)traceSwt.Level < (int)messageLevel)
            {
                return;
            }

            StackTrace st = new StackTrace(1, true);
            StackFrame sf = new StackFrame();
            sf = st.GetFrame(0);

            StringBuilder logMessage = new StringBuilder();
            logMessage.Append(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + " -- ");
            switch (messageLevel)
            {
                case TraceLevel.Info:
                    logMessage.Append("Information" + " -- ");
                    break;
                case TraceLevel.Verbose:
                    logMessage.Append("Verbose" + " -- ");
                    break;
                case TraceLevel.Warning:
                    logMessage.Append("Warning" + " -- ");
                    break;
                case TraceLevel.Error:
                    logMessage.Append("Error" + " -- ");
                    break;
                case TraceLevel.Off:
                    break;
            }

            string fileName = Path.GetFileName(sf.GetFileName());

            logMessage.Append(fileName + " - " + sf.GetFileLineNumber() + " - " + sf.GetMethod() + " - " + messageToWrite);
            Trace.WriteLine(logMessage.ToString());
            
        }

    }
}
