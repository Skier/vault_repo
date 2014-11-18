using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.Windows.UI.Controls;
using MobileTech.Domain;


namespace MobileTech.Windows.UI.DatabaseManager
{
	public partial class EventsView : BaseForm
    {
        #region Constructors

        public EventsView()
		{
			InitializeComponent();

            m_table.AddColumn(new TableColumn(0,20,new EventsRenderer(m_images)));
            m_table.AddColumn(new TableColumn(2,50));

            ApplyUIResources();
		}

        void ApplyUIResources()
        {
            Resources.Culture = Host.Instance.Culture;

            Text = Resources.TitleEvents;

            //m_mbBack.Text = Properties.Resources.Back;
        }

        #endregion

        #region Event Handlers

        private void OnBackClick(object sender, EventArgs e)
        {
            Destroy();
        }

        #endregion

        #region IView Members

        EventsModel m_model;

        public override void BindData(Object data)
        {
            if (!(data is EventsModel))
                throw new MobileTechInvalidModelExeption();

            m_model = (EventsModel)data;

            m_table.BindModel(m_model.Events);
        }

        #endregion

        #region Custom Renderers

        private class EventsRenderer:ImageTableCellRenderer
        {
            ImageList m_imageList;

            public EventsRenderer(ImageList imageList)
            {
                m_imageList = imageList;
                DrawText = false;
            }


            public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
            {
                base.getTableCellRendererComponent(table, value, isSelected, hasFocus, row, column);

                EventLog eventInfo = (EventLog)table.Model.GetObjectAt(row, column);

                switch ((EventType)eventInfo.EventType)
                {
                    case EventType.Debug:
                        Picture = m_imageList.Images[2];
                        break;
                    case EventType.Log:
                        Picture = m_imageList.Images[2];
                        break;
                    case EventType.Warning:
                        Picture = m_imageList.Images[1];
                        break;
                    case EventType.Exception:
                        Picture = m_imageList.Images[0];
                        break;
                    default:
                        break;
                }

                return this;
            }
        }

        #endregion
    }
}