using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Dalworth.SDK;

namespace Dalworth.Windows
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
                throw new DalworthException("Handler was disposed");      }

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
                throw new DalworthException("Unable to initialize handler", e);
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
                throw new DalworthException("Unable to initialize handler", e);
            }

            return controller;
        }

        #endregion

        bool m_isLeftActionExists;
        public virtual bool IsLeftActionExist
        {
            get { return m_isLeftActionExists; }
            protected set
            {
                if (m_isLeftActionExists != value)
                {
                    m_isLeftActionExists = value;
                    OnLeftActionSettingChanged();
                }
            }
        }

        String m_leftActionName;
        public virtual String LeftActionName
        {
            get { return m_leftActionName; }
            protected set
            {
                if (m_leftActionName != value)
                {
                    m_leftActionName = value;
                    OnLeftActionSettingChanged();
                }
            }
        }

        bool m_isRightActionExists;
        public virtual bool IsRightActionExist
        {
            get { return m_isRightActionExists; }
            protected set
            {
                if (m_isRightActionExists != value)
                {
                    m_isRightActionExists = value;
                    OnRightActionSettingChanged();
                }
            }
        }

        String m_rightActionName;
        public virtual String RightActionName
        {
            get { return m_rightActionName; }
            protected set
            {
                if (m_rightActionName != value)
                {
                    m_rightActionName = value;
                    OnRightActionSettingChanged();
                }
            }
        }

        public virtual void OnLeftAction() { }
        public virtual void OnRightAction() { }

        protected void OnLeftActionSettingChanged()
        {
            MainFormController.RefreshLeftAction();
        }

        protected void OnRightActionSettingChanged()
        {
            MainFormController.RefreshRightAction();
        }

        protected void InitLeftAction(String name, bool allow)
        {
            m_leftActionName = name;
            m_isLeftActionExists = allow;
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

        protected bool OnCancel(String confirmMessage)
        {
            return MessageDialog.Show(
                            MessageDialogType.Question, confirmMessage)
                            == DialogResult.No;
        }
    }
}
