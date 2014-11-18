using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class LocationsPage : BasePage
    {
        #region Constants

        private const string ERR_LENGTH_MSG = "{0} parameter must be {1} characters in length.";

        #endregion

        #region Web Form Designer generated code

        protected DropDownList ddlState;
        protected Label lblCity;
        protected DropDownList ddlCity;
        protected DataList dlStoreLocations;
        protected Label lblHeader;
        protected Dpi.Central.Web.Controls.Footer _footer;
        protected Label lblState;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ddlState.DataBinding += new System.EventHandler(this.ddlState_DataBinding);
            this.ddlState.SelectedIndexChanged += new System.EventHandler(this.ddlState_SelectedIndexChanged);
            this.ddlCity.DataBinding += new System.EventHandler(this.ddlCity_DataBinding);
            this.ddlCity.SelectedIndexChanged += new System.EventHandler(this.ddlCity_SelectedIndexChanged);
            this.dlStoreLocations.DataBinding += new System.EventHandler(this.dlStoreLocations_DataBinding);
            this.dlStoreLocations.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dlStoreLocations_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                ddlState.DataBind();

                ddlCity.Enabled = false;
                lblHeader.Visible = dlStoreLocations.Visible = false;
            }
        }

        private void ddlState_DataBinding(object sender, EventArgs e)
        {
            StringCollection states = RetreiveStates();
            states.Insert(0, string.Empty);

            ddlState.DataSource = states;
            ddlState.SelectedIndex = 0;

            ddlState.Enabled = states.Count > 1;
        }

        private void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCity.DataBind();
            lblHeader.Visible = dlStoreLocations.Visible = false;
        }

        private void ddlCity_DataBinding(object sender, EventArgs e)
        {
            string state = ddlState.SelectedValue;

            if (state != string.Empty) {
                StringCollection cities = RetreiveCitiesByState(state);
                cities.Insert(0, string.Empty);

                ddlCity.DataSource = cities;
                ddlCity.SelectedIndex = 0;

                ddlCity.Enabled = cities.Count > 1;
            }
        }

        private void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            dlStoreLocations.DataBind();
        }

        private void dlStoreLocations_DataBinding(object sender, EventArgs e)
        {
            string state = ddlState.SelectedValue;
            string city = ddlCity.SelectedValue;

            if (city != String.Empty && state != String.Empty) {
                IStoreLocation[] locations = RetreiveStoresByStateAndCity(state, city);

                dlStoreLocations.DataSource = locations;

                lblHeader.Text = "dPi Reseller Locations in: " +
                    ddlCity.SelectedValue + ", " + ddlState.SelectedValue;

                lblHeader.Visible = dlStoreLocations.Visible = locations.Length > 0;
            }
        }

        private void dlStoreLocations_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem) {
                IStoreLocation[] locations;
                locations = (IStoreLocation[]) dlStoreLocations.DataSource;
                IStoreLocation location = locations[e.Item.ItemIndex];

                Label label = (Label) e.Item.Controls[1];
                label.Text = location.Name;

                label = (Label) e.Item.Controls[3];
                label.Text = location.Address;

                label = (Label) e.Item.Controls[5];
                label.Text = location.City + "," + location.St + " " + location.Zip;

                label = (Label) e.Item.Controls[7];
                label.Text = "(" + location.Phone.Substring(0, 3) + ") "
                    + location.Phone.Substring(3, 3)
                    + "-" + location.Phone.Substring(6, 4);
            }
        }

        #endregion

        #region Implemenations

        private StringCollection RetreiveStates()
        {
            UOW uow = null;

            try {
                IMap map = IMapFactory.getIMap();
                uow = new UOW(map, "StoreLocationController.RetreiveStates");
                StringCollection states = StoreLocation.getStates(uow);
                return states;
            } finally {
                uow.close();
            }
        }

        private StringCollection RetreiveCitiesByState(string state)
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
                IMap map = IMapFactory.getIMap();
                uow = new UOW(map, "LocSvc.RetreiveCitiesByState");
                StringCollection cities = StoreLocation.getCitiesByState(uow, state);
                return cities;
            } finally {
                uow.close();
            }
        }

        private IStoreLocation[] RetreiveStoresByStateAndCity(string state, string city)
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
                IMap map = IMapFactory.getIMap();
                uow = new UOW(map, "LocSvc.RetreiveStoresByStateAndCity");
                StoreLocation[] locations = StoreLocation.getAllByStateAndCity(uow, state, city);
                return locations;
            } finally {
                uow.close();
            }
        }

        #endregion
    }
}