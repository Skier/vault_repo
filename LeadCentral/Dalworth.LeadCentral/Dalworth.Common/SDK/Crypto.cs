/*
    Crypto class
    --
    Uses crypto API functions to encrypt and decrypt data. A passphrase 
    string is used to create a 128-bit hash that is used to create a 
    40-bit crypto key. The same key is required to encrypt and decrypt 
    the data.
*/

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Dalworth.Common.SDK
{
    /// <summary>
    /// Encrypts and decrypts data using the crypto APIs.
    /// </summary>
    public class Crypto
    {
        // API functions
        private class WinApi
        {
            #region Crypto API imports

            private const uint AlgClassHash = (4 << 13);
            private const uint AlgTypeAny = (0);
            private const uint AlgClassDataEncrypt = (3 << 13);
            private const uint AlgTypeBlock = (3 << 9);

            private const uint AlgSidRc2 = 2;
            private const uint AlgSidMd5 = 3;

            public const string MsDefProv = "Microsoft Base Cryptographic Provider v1.0";

            public const uint ProvRsaFull = 1;
            public const uint CryptVerifycontext = 0xf0000000;
            public const uint CryptExportable = 0x00000001;

            public const uint CalgMd5 = (AlgClassHash | AlgTypeAny | AlgSidMd5);
            public const uint CalgRc2 = (AlgClassDataEncrypt | AlgTypeBlock | AlgSidRc2);

            const string CryptDll = "advapi32.dll";

            [DllImport(CryptDll)]
            public static extern bool CryptAcquireContext(
                ref IntPtr phProv, string pszContainer, string pszProvider,
                uint dwProvType, uint dwFlags);

            [DllImport(CryptDll)]
            public static extern bool CryptReleaseContext(
                IntPtr hProv, uint dwFlags);

            [DllImport(CryptDll)]
            public static extern bool CryptDeriveKey(
                IntPtr hProv, uint algid, IntPtr hBaseData,
                uint dwFlags, ref IntPtr phKey);

            [DllImport(CryptDll)]
            public static extern bool CryptCreateHash(
                IntPtr hProv, uint algid, IntPtr hKey,
                uint dwFlags, ref IntPtr phHash);

            [DllImport(CryptDll)]
            public static extern bool CryptHashData(
                IntPtr hHash, byte[] pbData,
                uint dwDataLen, uint dwFlags);

            [DllImport(CryptDll)]
            public static extern bool CryptEncrypt(
                IntPtr hKey, IntPtr hHash, bool final, uint dwFlags,
                byte[] pbData, ref uint pdwDataLen, uint dwBufLen);

            [DllImport(CryptDll)]
            public static extern bool CryptDecrypt(
                IntPtr hKey, IntPtr hHash, bool final, uint dwFlags,
                byte[] pbData, ref uint pdwDataLen);

            [DllImport(CryptDll)]
            public static extern bool CryptDestroyHash(IntPtr hHash);

            [DllImport(CryptDll)]
            public static extern bool CryptDestroyKey(IntPtr hKey);

            #endregion

            #region Error reporting imports

            #endregion
        }

        // all static methods
        private Crypto()
        {
        }

        public static String Encrypt(String passphrase, String plainTextValue)
        {
            return Convert.ToBase64String(
                Encrypt(passphrase,
                Encoding.UTF8.GetBytes(plainTextValue)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passphrase"></param>
        /// <param name="base64Value"></param>
        /// <returns>base64 encoded string</returns>
        public static String Decrypt(String passphrase, String base64Value)
        {
            byte[] decodedBytes = Decrypt(passphrase,
                    Convert.FromBase64String(base64Value));

            return Encoding.UTF8.GetString(decodedBytes,
                0, decodedBytes.Length);
        }

        /// <summary>
        /// Encrypt data. Use passphrase to generate the encryption key. 
        /// Returns a byte array that contains the encrypted data.
        /// </summary>
        static public byte[] Encrypt(string passphrase, byte[] data)
        {
            // holds encrypted data
            byte[] buffer;

            // crypto handles
            IntPtr hProv = IntPtr.Zero;
            IntPtr hKey = IntPtr.Zero;

            try
            {
                // get crypto provider, specify the provider (3rd argument)
                // instead of using default to ensure the same provider is 
                // used on client and server
                if (!WinApi.CryptAcquireContext(ref hProv, null, WinApi.MsDefProv,
                    WinApi.ProvRsaFull, WinApi.CryptVerifycontext))
                    Failed("CryptAcquireContext");

                // generate encryption key from passphrase
                hKey = GetCryptoKey(hProv, passphrase);

                // determine how large of a buffer is required
                // to hold the encrypted data
                uint dataLength = (uint)data.Length;
                uint bufLength = (uint)data.Length;
                if (!WinApi.CryptEncrypt(hKey, IntPtr.Zero, true,
                    0, null, ref dataLength, bufLength))
                    Failed("CryptEncrypt");

                // allocate and fill buffer with encrypted data
                buffer = new byte[dataLength];
                Buffer.BlockCopy(data, 0, buffer, 0, data.Length);

                dataLength = (uint)data.Length;
                bufLength = (uint)buffer.Length;
                if (!WinApi.CryptEncrypt(hKey, IntPtr.Zero, true,
                    0, buffer, ref dataLength, bufLength))
                    Failed("CryptEncrypt");
            }

            finally
            {
                // release crypto handles
                if (hKey != IntPtr.Zero)
                    WinApi.CryptDestroyKey(hKey);

                if (hProv != IntPtr.Zero)
                    WinApi.CryptReleaseContext(hProv, 0);
            }

            return buffer;
        }


        /// <summary>
        /// Decrypt data. Use passphrase to generate the encryption key. 
        /// Returns a byte array that contains the decrypted data.
        /// </summary>
        static public byte[] Decrypt(string passphrase, byte[] data)
        {
            // make a copy of the encrypted data
            byte[] dataCopy = data.Clone() as byte[];

            // holds the decrypted data
            byte[] buffer = null;

            // crypto handles
            IntPtr hProv = IntPtr.Zero;
            IntPtr hKey = IntPtr.Zero;

            try
            {
                // get crypto provider, specify the provider (3rd argument)
                // instead of using default to ensure the same provider is 
                // used on client and server
                if (!WinApi.CryptAcquireContext(ref hProv, null, WinApi.MsDefProv,
                    WinApi.ProvRsaFull, WinApi.CryptVerifycontext))
                    Failed("CryptAcquireContext");

                // generate encryption key from the passphrase
                hKey = GetCryptoKey(hProv, passphrase);

                // decrypt the data
                if (dataCopy != null)
                {
                    var dataLength = (uint)dataCopy.Length;
                    if (!WinApi.CryptDecrypt(hKey, IntPtr.Zero, true,
                                             0, dataCopy, ref dataLength))
                        Failed("CryptDecrypt");

                    // copy to a buffer that is returned to the caller
                    // the decrypted data size might be less then
                    // the encrypted size
                    buffer = new byte[dataLength];
                    Buffer.BlockCopy(dataCopy, 0, buffer, 0, (int)dataLength);
                }
            }
            finally
            {
                // release crypto handles
                if (hKey != IntPtr.Zero)
                    WinApi.CryptDestroyKey(hKey);

                if (hProv != IntPtr.Zero)
                    WinApi.CryptReleaseContext(hProv, 0);
            }

            return buffer;
        }


        /// <summary>
        /// Create a crypto key form a passphrase. This key is 
        /// used to encrypt and decrypt data.
        /// </summary>
        static private IntPtr GetCryptoKey(IntPtr hProv, string passphrase)
        {
            // crypto handles
            IntPtr hHash = IntPtr.Zero;
            IntPtr hKey = IntPtr.Zero;

            try
            {
                // create 128 bit hash object
                if (!WinApi.CryptCreateHash(hProv,
                    WinApi.CalgMd5, IntPtr.Zero, 0, ref hHash))
                    Failed("CryptCreateHash");

                // add passphrase to hash
                byte[] keyData = Encoding.ASCII.GetBytes(passphrase);
                if (!WinApi.CryptHashData(hHash, keyData, (uint)keyData.Length, 0))
                    Failed("CryptHashData");

                // create 40 bit crypto key from passphrase hash
                if (!WinApi.CryptDeriveKey(hProv, WinApi.CalgRc2,
                    hHash, WinApi.CryptExportable, ref hKey))
                    Failed("CryptDeriveKey");
            }

            finally
            {
                // release hash object
                if (hHash != IntPtr.Zero)
                    WinApi.CryptDestroyHash(hHash);
            }

            return hKey;
        }


        /// <summary>
        /// Throws SystemException with GetLastError information.
        /// </summary>
        static private void Failed(string command)
        {
            throw new Exception(command + " failed");
            
            
//            uint lastError = WinApi.GetLastError();
//            StringBuilder sb = new StringBuilder(500);
//
//            try
//            {
//                // get message for last error
//                WinApi.FormatMessage(WinApi.FORMAT_MESSAGE_FROM_SYSTEM,
//                    null, lastError, 0, sb, 500, null);
//            }
//            catch
//            {
//                // error calling FormatMessage
//                sb.Append("N/A.");
//            }
//
//            throw new SystemException(
//                string.Format("{0} failed.\r\nLast error - 0x{1:x}.\r\nError message - {2}",
//                command, lastError, sb.ToString()));
        }
    }
}
