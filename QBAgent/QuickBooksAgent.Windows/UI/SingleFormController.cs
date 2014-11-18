using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace QuickBooksAgent.Windows.UI
{

    public delegate void SingleFormClosedHandler(SingleFormController controller);

    public abstract class SingleFormController : IDisposable
    {

        public event SingleFormClosedHandler Closed;

        //public virtual bool OnCancel()
        //{
        //    return false;
        //}

        public virtual void OnViewLoad() { }
        public virtual void OnViewActivated() { }

        protected bool m_closed;

        public virtual void OnClose() 
        { 
            m_closed = true;

            if (Closed != null)
                Closed.Invoke(this);
        }

        internal bool Save()
        {
            return OnSave();
        }

        protected virtual bool OnSave()
        {
            return true;
        }


        public abstract BaseControl BaseViewInstance { get;}

        /// <summary>
        /// Executing in async mode
        /// </summary>
        public void Execute()
        {

                Host.Trace("SingleFormController::Execute", String.Format(
                        "Executing {0} controller", ToString()));

            MainFormController.Register(this);
        }

        #region IDisposable Members
        bool m_disposed;
        public void Dispose()
        {
            if (!m_disposed)
            {
                OnDispose();
            }

            m_disposed = true;
        }

        public virtual void OnDispose()
        {

        }
        #endregion

        #region Initialize

        public virtual void Initialize(Object[] data)
        {
            if (m_disposed)
                throw new QuickBooksAgentException("Handler was disposed");
        }

        #endregion

        #region Prepare

        public static TController Prepare<TController>()
            where TController : SingleFormController, new()
        {
            TController controller = new TController();

            Host.Trace("SingleFormController::Prepare", String.Format(
                    "Prepearing {0} controller", controller.ToString()));

            try
            {
                using (WaitCursor waitCursor = new WaitCursor())
                {
                    controller.Initialize(null);
                }
            }
            catch (Exception e)
            {
                throw new QuickBooksAgentException("Unable to initialize handler", e);
            }

            return controller;
        }


        public static TController Prepare<TController>(params Object[] data)
            where TController : SingleFormController, new()
        {
            TController controller = new TController();

            Host.WriteToLogFile("SingleFormController::Prepare", String.Format(
                    "Prepearing {0} controller", controller.ToString()));

            try
            {
                using (WaitCursor waitCursor = new WaitCursor())
                {
                    controller.Initialize(data);
                }
            }
            catch (Exception e)
            {
                throw new QuickBooksAgentException("Unable to initialize handler", e);
            }

            return controller;
        }

        #endregion
        bool m_isDefaultActionExists;
        public virtual bool IsDefaultActionExist
        {
            get { return m_isDefaultActionExists; }
            protected set
            {
                if (m_isDefaultActionExists != value)
                {
                    m_isDefaultActionExists = value;
                    OnDefaultActionSettingChanged();
                }
            }
        }

        String m_defaultActionName;
        public virtual String DefaultActionName
        {
            get { return m_defaultActionName; }
            protected set
            {
                if (m_defaultActionName != value)
                {
                    m_defaultActionName = value;
                    OnDefaultActionSettingChanged();
                }
            }
        }

        public virtual void OnDefaultAction() { }

        protected void OnDefaultActionSettingChanged()
        {
            MainFormController.RefreshDefaultAction();
        }

        protected void InitDefaultAction(String name, bool allow)
        {
            m_defaultActionName = name;
            m_isDefaultActionExists = allow;
        }
    }

    public class SingleFormController<TModel, TView> : SingleFormController
        where TView : BaseControl, new()
        where TModel : new()
    {
        TView m_view = new TView();

        public TView View
        {
            get { return m_view; }
        }

        protected TModel m_model;

        public TModel Model
        {
            get { return m_model; }
        }

        public override BaseControl BaseViewInstance
        {
            get { return m_view; }
        }

        #region Initialize
        public sealed override void Initialize(Object[] data)
        {
            base.Initialize(data);

            m_model = new TModel();

            BaseViewInstance.Init();

            OnModelInitialize(data);
            
            OnInitialize();
        }
        #endregion

        protected virtual void OnInitialize() { }

        #region OnModelInitialize
        protected virtual void OnModelInitialize(Object[] data)
        {
            if (m_model is IModel)
                (m_model as IModel).Init();
        }

        #endregion

        protected virtual void AddMenuItem(MenuItem menuItem)
        {
            MainFormController.Instance.Form.m_subMenu.MenuItems.Add(
                menuItem);
        }

        protected bool OnCancel(String confirmMessage)
        {
            return MessageDialog.Show(
                            MessageDialogType.Question, confirmMessage)
                            == DialogResult.No;
        }
    }
}
