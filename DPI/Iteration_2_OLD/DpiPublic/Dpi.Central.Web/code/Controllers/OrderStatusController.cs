using DPI.Components;
using DPI.Services;

namespace Dpi.Central.Web.Controllers
{
    /// <summary>
    /// Summary description for OrderStatusController.
    /// </summary>
    public class OrderStatusController : ControllerBase
    {
        #region Static Members

        private static OrderStatusController _instance;

        public static OrderStatusController Instance {
            get {
                if (_instance == null) {
                    lock (typeof (OrderStatusController)) {
                        if (_instance == null) {
                            _instance = new OrderStatusController();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        protected OrderStatusController() : base() {
        }

        public ActivationWorkLog[] GetOrderStatus() {
            return CustSvc.GetOrderStatus(Map, AccountNumber);
        }
    }
}