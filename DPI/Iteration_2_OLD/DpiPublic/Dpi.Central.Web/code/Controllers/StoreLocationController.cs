using System;
using System.Collections.Specialized;
using DPI.Components;
using DPI.Interfaces;

namespace Dpi.Central.Web.Controllers
{
    public class StoreLocationController : ControllerBase
    {
        #region Constants

        private const string ERR_LENGTH_MSG = "{0} parameter must be {1} characters in length.";

        #endregion

        #region Static Members

        private static StoreLocationController _instance;

        public static StoreLocationController Instance
        {
            get
            {
                lock (typeof (StoreLocationController)) {
                    if (_instance == null) {
                        _instance = new StoreLocationController();
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Constructors

        protected StoreLocationController()
        {
        }

        #endregion

        #region Public Methods

        public StringCollection RetreiveStates()
        {
            UOW uow = null;

            try {
                uow = new UOW(Map, "StoreLocationController.RetreiveStates");
                StringCollection states = StoreLocation.getStates(uow);
                return states;
            } finally {
                uow.close();
            }
        }

        public StringCollection RetreiveCitiesByState(string state)
        {
            if (state == null) {
                throw new ArgumentNullException("state");
            }

            if (state.Length != StoreLocation.STATE_LENGTH) {
                throw new ArgumentException(string.Format(
                    ERR_LENGTH_MSG, "state", StoreLocation.STATE_LENGTH), "state");
            }

            UOW uow = null;

            try {
                uow = new UOW(Map, "LocSvc.RetreiveCitiesByState");
                StringCollection cities = StoreLocation.getCitiesByState(uow, state);
                return cities;
            } finally {
                uow.close();
            }
        }

        public IStoreLocation[] RetreiveStoresByStateAndCity(string state, string city)
        {
            if (state == null) {
                throw new ArgumentNullException("state");
            }

            if (state.Length != StoreLocation.STATE_LENGTH) {
                throw new ArgumentException(string.Format(
                    ERR_LENGTH_MSG, "state", StoreLocation.STATE_LENGTH), "state");
            }

            if (city == null || city == string.Empty) {
                throw new ArgumentNullException("city");
            }

            if (city.Length > StoreLocation.CITY_MAX_LENGTH) {
                throw new ArgumentException(string.Format(
                    ERR_LENGTH_MSG, "city", StoreLocation.CITY_MAX_LENGTH), "city");
            }

            UOW uow = null;

            try {
                uow = new UOW(Map, "LocSvc.RetreiveStoresByStateAndCity");
                StoreLocation[] locations = StoreLocation.getAllByStateAndCity(uow, state, city);
                return locations;
            } finally {
                uow.close();
            }
        }

        #endregion
    }
}