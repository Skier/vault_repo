using System;
using System.IO;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;

namespace Weborb.Samples.Ftp
{
    public class ZipHelper
    {
        public const string ZIP_FILE_EXT = ".zip";

        private const int COMPRESS_LEVEL = 1;

        private long abortFlag = 0;

        private EventWaitHandle abortEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
        

        public void CompressDirectory(string dirName, string fileName)
        {
            FileStream output = File.Create(Path.Combine(dirName, fileName) + ZIP_FILE_EXT);
            
            ZipOutputStream zipStream = null;
            
            try
            {
                 zipStream = new ZipOutputStream(output);

                 zipStream.SetLevel(COMPRESS_LEVEL);

                 AddDirectoryToZip(dirName, Path.Combine(dirName, fileName), zipStream);

                 if (IsAborting()) {

                     zipStream.Close();
                     output.Close();

                     abortEvent.Set();
                     return;
                 }

                 zipStream.Finish();
                
            } finally {
                if (null != zipStream) {
                    zipStream.Close();
                }
                
                output.Close();
            }

        }

        public void Abort()
        {
            long value = Interlocked.Read(ref abortFlag);
            if (value == 0)
            {
                Interlocked.Exchange(ref abortFlag, 1);
            }

            abortEvent.WaitOne();

            Interlocked.Exchange(ref abortFlag, 0);
        }
        
        private void AddDirectoryToZip(string rootFolder, string currentFolder, ZipOutputStream s)
        {
            if (IsAborting())
                return;
            
            String[] filenames = Directory.GetFiles(currentFolder);
            byte[] buffer = new byte[4096];
            foreach (String file in filenames)
            {
                if (IsAborting())
                    return;

                ZipEntry entry = new ZipEntry(file.Substring((rootFolder + Path.DirectorySeparatorChar).Length));

                FileInfo fi = new FileInfo(file);
                entry.DateTime = fi.CreationTime;
                //entry.Size = fi.Length;
                s.PutNextEntry(entry);

                using (FileStream fs = File.OpenRead(file))
                {
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        s.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0 && !IsAborting());
                }
            }
            
            String[] directories = Directory.GetDirectories(currentFolder);
            
            foreach (String directory in directories)
            {
                if (IsAborting())
                    break;
                
                AddDirectoryToZip(rootFolder, directory, s);
            }
                
        }
        
        private Boolean IsAborting()
        {
            long value = Interlocked.Read(ref abortFlag);
            if (value == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
