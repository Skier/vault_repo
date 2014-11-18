//===============================================================================
// Microsoft patterns & practices
// Mobile Client Software Factory - July 2006
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Dalworth.ConnectionMonitor
{
    /// <summary>
    ///		This class is a native API wrapper for two native APIs:
    ///					- ConnMgrEnumDestinations
    ///					- ConnMgrQueryDetailedStatus
    /// 	These APIs are part of the Windows Mobile 5.0 Connection Manager functions.
    ///		This class uses this native functions to get the network list (exposed as
    ///		<see cref="GetNetworkList"/>) and to get the current connection and 
    ///		network (exposed as <see cref="GetCurrentConnectionType"/>).
    /// </summary>
    public static class ConnectionMonitorNativeHelper
    {
        //
        // API Constants
        //
        enum UnmanagedConnectionType : uint
        {
            CM_CONNTYPE_UNKNOWN = 0,
            CM_CONNTYPE_CELLULAR = 1, //GPRS/EDGE/1x
            CM_CONNTYPE_NIC = 2, //Ethernet/WiFi
            CM_CONNTYPE_BLUETOOTH = 3,
            CM_CONNTYPE_UNIMODEM = 4,
            CM_CONNTYPE_VPN = 5,
            CM_CONNTYPE_PROXY = 6,
            CM_CONNTYPE_PC = 7  //DTPT
        }

        //
        // API Structures
        //
        class CONNMONITOR_DESTINATION_INFO
        {
            public System.Guid guid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public String szDescription;

        }
        struct SYSTEMTIME
        {
            public Int16 wYear;
            public Int16 wMonth;
            public Int16 wDayOfWeek;
            public Int16 wDay;
            public Int16 wHour;
            public Int16 wMinute;
            public Int16 wSecond;
            public Int16 wMilliseconds;
        };
        class CONNMONITOR_CONNECTION_DETAILED_STATUS
        {
            public IntPtr pNext;
            public uint dwVer;
            public uint dwParams;
            public UnmanagedConnectionType dwType;
            public uint dwSubtype;
            public uint dwFlags;
            public uint dwSecure;
            public Guid guidDestNet;
            public Guid guidSourceNet;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszDescription;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszAdapterName;
            public uint dwConnectionStatus;
            public SYSTEMTIME lastConnectionTime;
            public uint dwSignalQuality;
			public uint pIPAddr; //Actually this is a void*
        }

        //
        // Imports
        //
        [DllImport("cellcore.dll", EntryPoint = "ConnMgrEnumDestinations", SetLastError = true)]
        internal static extern int ConnMgrEnumDestinations(int nIndex, IntPtr pDestinationInfo);

        [DllImport("cellcore.dll", EntryPoint = "ConnMgrQueryDetailedStatus", SetLastError = true)]
        internal static extern int ConnMgrQueryDetailedStatus(IntPtr pStatusBuffer, ref uint pcBufferSize);

		[DllImport("ws2.dll", EntryPoint = "WSAAddressToString", SetLastError = true)]
		internal static extern int WSAAddressToString(uint lpsaAddress, uint dwAddressLength, IntPtr lpProtocolInfo, StringBuilder lpszAddressString, ref uint lpdwAddressStringLength);

		// String helper
		static string NewLine = "\r\n";
        
		//
        // Managed API
        //

        /// <summary>
        ///		Get the current connection type and network.
        /// </summary>
        /// <param name="type">
        ///		Out parameter to get the current connection type.
        /// </param>
        /// <param name="currentNetworkName">
        ///		Out parameter to get the current network name.
        /// </param>
        static public string GetCurrentNetworkNameForConnection(NativeConnectionType type)
        {
            //API Constants
            const int CONNMONITOR_STATUS_CONNECTED = 0x10;

            //1. Initialize out parameters
			//type = CurrentConnectionType.Disconnected;
            string currentNetworkName = string.Empty;

            //2. Get info from unmanaged API

            CONNMONITOR_CONNECTION_DETAILED_STATUS inStatus = new CONNMONITOR_CONNECTION_DETAILED_STATUS();
            uint bufferSize = 0;
            //This call is only to get the real buffer size needed
            int hResult = ConnMgrQueryDetailedStatus(IntPtr.Zero, ref bufferSize);
            //Now I can alloc the buffers
            IntPtr buffer = IntPtr.Zero;
            IntPtr destBuffer = IntPtr.Zero;

            try
            {
                buffer = Marshal.AllocCoTaskMem((int)bufferSize);
                destBuffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(CONNMONITOR_DESTINATION_INFO)));

                hResult = ConnMgrQueryDetailedStatus(buffer, ref bufferSize);

				if (hResult != 0)
				{
                    throw new ConnectionMonitorException(string.Format(CultureInfo.InvariantCulture, "ConnMonitorQueryDetailedStatus has failed. HResult = {0}", hResult), hResult);
				}

                //3. Browse the linked list looking for the current connection
                for (IntPtr curItem = buffer; curItem != IntPtr.Zero; curItem = inStatus.pNext)
                {
                    Marshal.PtrToStructure(curItem, inStatus);
                    //If this is the current connection ... and it's not a Proxy, VPN or Bluetooth information
                    if (inStatus.dwConnectionStatus == CONNMONITOR_STATUS_CONNECTED &&
						(  (type == NativeConnectionType.Cell && inStatus.dwType == UnmanagedConnectionType.CM_CONNTYPE_CELLULAR) ||
						   (type == NativeConnectionType.NIC && inStatus.dwType == UnmanagedConnectionType.CM_CONNTYPE_NIC) ||
						   (type == NativeConnectionType.PC && inStatus.dwType == UnmanagedConnectionType.CM_CONNTYPE_PC) ))
                    {
                        //3.2. Get the network name
                        CONNMONITOR_DESTINATION_INFO destInfo = new CONNMONITOR_DESTINATION_INFO();
                        int index = 0;
                        while (ConnMgrEnumDestinations(index++, destBuffer) >= 0)
                        {
                            Marshal.PtrToStructure(destBuffer, destInfo);
                            if (destInfo.guid == inStatus.guidDestNet)
                            {
                                currentNetworkName = destInfo.szDescription;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            finally
            {
                //Free alocated memory
                if (destBuffer != IntPtr.Zero) Marshal.FreeCoTaskMem(destBuffer);
                if (buffer != IntPtr.Zero) Marshal.FreeCoTaskMem(buffer);
            }

			return currentNetworkName;
        }

        /// <summary>
        /// Get the destination networks list.
        /// </summary>
        /// <returns>
        ///		A list of strings with the network names.
        /// </returns>
        static public List<string> GetNetworkList()
        {
            List<string> networks = new List<string>();

            CONNMONITOR_DESTINATION_INFO destInfo = new CONNMONITOR_DESTINATION_INFO();
            IntPtr destBuffer = IntPtr.Zero;
			try
			{
				//Memory allocation
				destBuffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(CONNMONITOR_DESTINATION_INFO)));
				int index = 0;
				while (ConnMgrEnumDestinations(index++, destBuffer) >= 0)
				{
					Marshal.PtrToStructure(destBuffer, destInfo);
					networks.Add(destInfo.szDescription);
				}
			}
            finally
            {
                //Always free allocated memory
                if (destBuffer != IntPtr.Zero) Marshal.FreeCoTaskMem(destBuffer);
            }
            return networks;
        }

        /// <summary>
        ///		Gets an string with detailed information about the status of the given
		///		native connection type.
        /// </summary>
        /// <param name="type">NativeConnectionType to get the detailed status information string.</param>
        /// <returns>
		///		String with detailed connection status information.
		/// </returns>
        static public string GetConnectionDetailedStatusText(NativeConnectionType type)
        {
            StringBuilder result = new StringBuilder();
			result.Append(type.ToString()).Append(NewLine);
            result.Append("Detailed Information").Append(NewLine);
            result.Append("********************").Append(NewLine);

            //Get info from unmanaged API

            CONNMONITOR_CONNECTION_DETAILED_STATUS inStatus = new CONNMONITOR_CONNECTION_DETAILED_STATUS();
            uint bufferSize = 0;
            //This call is only to get the real buffer size needed
            int hResult = ConnMgrQueryDetailedStatus(IntPtr.Zero, ref bufferSize);
            //Now I can alloc the buffers
            IntPtr buffer = IntPtr.Zero;
            IntPtr destBuffer = IntPtr.Zero;

            try
            {
                buffer = Marshal.AllocCoTaskMem((int)bufferSize);
                destBuffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(CONNMONITOR_DESTINATION_INFO)));

                hResult = ConnMgrQueryDetailedStatus(buffer, ref bufferSize);

				if (hResult != 0)
				{
                    throw new ConnectionMonitorException(string.Format("ConnMonitorQueryDetailedStatus has failed. HResult = {0}", hResult), hResult);
				}

                //3. Browse the linked list
                for (IntPtr curItem = buffer; curItem != IntPtr.Zero; curItem = inStatus.pNext)
                {
                    Marshal.PtrToStructure(curItem, inStatus);
                    //If this is the current connection ... and it's not a Proxy, VPN or Bluetooth information
                    if (  (type == NativeConnectionType.Cell && 
                            inStatus.dwType == UnmanagedConnectionType.CM_CONNTYPE_CELLULAR) ||
                            (type == NativeConnectionType.NIC &&
                            inStatus.dwType == UnmanagedConnectionType.CM_CONNTYPE_NIC) ||
                            (type == NativeConnectionType.PC &&
                            inStatus.dwType == UnmanagedConnectionType.CM_CONNTYPE_PC)
                            )
                    {
                        //Build the string
                        result.Append("[").Append(inStatus.pszDescription).Append("]\r\n");
                        result.Append(string.Format("Subtype: {0}", inStatus.dwSubtype.ToString())).Append(NewLine);
                        result.Append(string.Format("Flags: {0}", inStatus.dwFlags.ToString())).Append(NewLine);
                        result.Append(string.Format("Secure: {0}", inStatus.dwSecure.ToString())).Append(NewLine);
                        result.Append(string.Format("GuidDestNet: {0}", inStatus.guidDestNet.ToString())).Append(NewLine);
                        result.Append(string.Format("GuidSourceNet: {0}", inStatus.guidSourceNet.ToString())).Append(NewLine);
                        result.Append(string.Format("AdapterName: {0}", inStatus.pszAdapterName)).Append(NewLine);
                        result.Append(string.Format("ConnectionStatus: {0}", inStatus.dwConnectionStatus.ToString())).Append(NewLine);
                        result.Append(string.Format("LastConnectionTime: {0}/{1}/{2}-{3}:{4}:{5}",
                                        inStatus.lastConnectionTime.wDay.ToString(),
                                        inStatus.lastConnectionTime.wMonth.ToString(),
                                        inStatus.lastConnectionTime.wYear.ToString(),
                                        inStatus.lastConnectionTime.wHour.ToString(),
                                        inStatus.lastConnectionTime.wMinute.ToString(),
										inStatus.lastConnectionTime.wSecond.ToString())).Append(NewLine);
                        result.Append(string.Format("SignalQuality: {0}", inStatus.dwSignalQuality.ToString(CultureInfo.InvariantCulture))).Append(NewLine);
                        result.Append(string.Format("IPAddressInfo: {0}", GetStringFromIPInfo(inStatus.pIPAddr))).Append(NewLine);

                        //3.2. Get the network name
                        CONNMONITOR_DESTINATION_INFO destInfo = new CONNMONITOR_DESTINATION_INFO();
                        int index = 0;
                        while (ConnMgrEnumDestinations(index++, destBuffer) >= 0)
                        {
                            Marshal.PtrToStructure(destBuffer, destInfo);
                            if (destInfo.guid == inStatus.guidDestNet)
                            {
                                result.Append("DestNetworkName: ").Append(destInfo.szDescription).Append(NewLine);
                                break;
                            }
                        }
                        result.Append("-------------------------------------\r\n");
                    }
                }
            }
            finally
            {
                //Free alocated memory
                if (destBuffer != IntPtr.Zero) Marshal.FreeCoTaskMem(destBuffer);
                if (buffer != IntPtr.Zero) Marshal.FreeCoTaskMem(buffer);
            }

            return result.ToString();
        }

		private static string GetStringFromIPInfo(uint intPtr)
		{
			if (intPtr == (uint)IntPtr.Zero) return string.Empty;
			if (intPtr == 0) return string.Empty;

			uint IPAddr = intPtr + 4; //first 4 bytes for count (always 1)
			StringBuilder ipAddressString = new StringBuilder(250);
			uint length = (uint)ipAddressString.Capacity;
			int hResult = WSAAddressToString(IPAddr, 128, IntPtr.Zero, ipAddressString, ref length);

			if (hResult != 0)
			{
                throw new ConnectionMonitorException(string.Format(CultureInfo.InvariantCulture, "GetStringFromIPInfo has failed. HResult = {0}", hResult), hResult);
			}
			
			return ipAddressString.ToString();
		}
    }
}
