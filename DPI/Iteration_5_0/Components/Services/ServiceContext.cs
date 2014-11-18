using System;
using System.Runtime.Serialization;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class ServiceContext
	{
		public ServiceContext()
		{
		}

        public void RunTransaction()
        {
            UOW uow = null;

            try {
                uow = new UOW("");

                uow.commit();
            } catch (Exception ex) {
                if (uow != null) {
                    uow.Rollback();
                }

                //throw 
            } finally {
                if (uow != null) {
                    uow.close();
                }
            }
        }
	}
    
    [Serializable]
    public class ServiceException : Exception 
    {
        public ServiceException() {
        }

        public ServiceException(string message) : base(message) {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException) {
        }

        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
