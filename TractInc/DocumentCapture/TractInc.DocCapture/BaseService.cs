using System;

namespace TractInc.DocCapture
{
    class BaseService
    {
    
        public String GetStorageUrl()
        {
            return DocUploader.StorageUrl;
        }

        public String GetUploaderUrl()
        {
            return DocUploader.UploaderUrl;
        }

        public String GetGUID()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
