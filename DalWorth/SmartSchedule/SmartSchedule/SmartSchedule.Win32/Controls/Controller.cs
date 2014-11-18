using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using SmartSchedule.SDK;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.Controls
{

    /// <summary>
    /// Base class for all handlers.
    /// </summary>
    public abstract class Controller:IDisposable
    {
        // Controller

        #region Fields

        private bool m_disposed;

        protected bool Disposed
        {
            get
            {
                return m_disposed;
            }
        }

        #endregion

        #region Initialize

        public virtual void Initialize(Object[] data) 
        {
            if (m_disposed)
                throw new DalworthException("Handler was disposed");
        }

        #endregion

        #region Prepare

        public static TController Prepare<TController>() where TController : Controller, new()
        {
            TController controller = new TController();

            try
            {
                using (WaitCursor waitCursor = new WaitCursor())
                {
                    controller.Initialize(null);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return controller;
        }


        public static TController Prepare<TController>(params Object[] data) where TController : Controller, new()
        {
            TController controller = new TController();

            try
            {
                using (WaitCursor waitCursor = new WaitCursor())
                {
                    controller.Initialize(data);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return controller;
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            if (m_disposed)
                return;

            try
            {
                OnReleaseResources();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                m_disposed = true;
            }
        }
        #endregion

        #region OnReleaseResources
        protected virtual void OnReleaseResources()
        { }
        #endregion

        #region Abort
          /*public virtual void Abort()
        {
            Debug.Assert(m_thread != null, "Thread started in sync mode, or not started");

            m_thread.Abort();

            m_thread = null;
        }*/
        #endregion

        // Command

        public abstract void Execute();
    }

    public abstract class Controller<TModel> : Controller where TModel : new()
    {
        #region Fields
        TModel m_model;
        protected TModel Model
        {
            get { return m_model; }
            set { m_model = value; }
        }
        #endregion

        #region Initialize
        public override void Initialize(Object[] data) 
        {
            base.Initialize(data);

            m_model = new TModel();

            OnModelInitialize(data);
        }
        #endregion 

        #region OnModelInitialize
        protected virtual void OnModelInitialize(Object[] data)
        {
            if (m_model is IModel)
                (m_model as IModel).Init();
        }

        #endregion

        #region OnReleaseResources

        protected override void OnReleaseResources()
        {
            base.OnReleaseResources();

            if (m_model is IDisposable)
                (m_model as IDisposable).Dispose();

            m_model = default(TModel);
        }

        #endregion
    }

    public class Controller<TModel, TView> : Controller<TModel>
        where TView :BaseForm, new()
        where TModel:new()
    {
        #region Events
        public event SimpleEvent Closing;
        #endregion

        #region Fields

        TView m_view;
        protected TView View
        {
            get { return m_view; }
            set { m_view = value; }
        }

        #endregion

        #region Initialize
        public sealed override void Initialize(Object[] data)
        {
            base.Initialize(data);

            m_view = new TView();

#if WINCE
            m_view.MinimizeBox = false;
#endif

            m_view.Closed += new EventHandler(OnViewClosed);
            m_view.Load += new EventHandler(OnViewLoad);
            m_view.Activated += new EventHandler(OnViewActivated);
            m_view.Shown += new EventHandler(OnViewShow);
            m_view.Cancel += new System.ComponentModel.CancelEventHandler(OnViewCancel);

            OnInitialize();
        }

        #endregion

        #region OnViewClosing
        void OnViewCancel(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = OnCancel();

            if (!e.Cancel && Closing != null)
                Closing.Invoke();
        }
        #endregion

        #region OnCancel
        protected virtual bool OnCancel()
        {
            return false;
        }

        #endregion

        #region OnInitialize

        protected virtual void OnInitialize() { }

        #endregion

        #region OnShow

        protected virtual void OnViewShow(object sender, EventArgs e) { }

        #endregion

        #region Close
        public void Close()
        {
            View.Destroy();
        }
        #endregion

        #region OnViewActivated
        protected virtual void OnViewActivated() { }
        void OnViewActivated(object sender, EventArgs e)
        {
            OnViewActivated();
        }
        #endregion

        #region OnViewLoad
        protected virtual void OnViewLoad() { }
        void OnViewLoad(object sender, EventArgs e)
        {
            OnViewLoad();
        }
        #endregion

        #region OnViewClosed
        protected virtual void OnViewClosed() { }
        private void OnViewClosed(object sender, EventArgs e)
        {
            BaseForm form = (BaseForm)sender;

            form.Closed -= new EventHandler(OnViewClosed);
            form.Load -= new EventHandler(OnViewLoad);
            form.Activated -= new EventHandler(OnViewActivated);
            form.Cancel -= new System.ComponentModel.CancelEventHandler(OnViewCancel);

            OnViewClosed();
        }
        #endregion

        #region OnReleaseResources
        protected override void OnReleaseResources()
        {
            base.OnReleaseResources();

            m_view.Dispose();

            m_view = default(TView);
        }
        #endregion

        #region Execute
        /// <summary>
        /// Executing in async mode
        /// </summary>
        public override void Execute()
        {
            Execute(true);
        }

        public void Execute(bool async)
        {
            if (async)
                View.Show();
            else
                View.ShowDialog();
        }
        #endregion
    }

    public class NestedController<TModel, TView> : Controller<TModel>
        where TView :BaseControl, new()
        where TModel:new()
    {       
        #region Fields

        TView m_view;

        public TView View
        {
            get { return m_view; }
            set { m_view = value; }
        }

        #endregion

        #region Initialize
        public sealed override void Initialize(Object[] data)
        {
            base.Initialize(data);

            m_view = new TView();
            m_view.Dock = DockStyle.Fill;
            m_view.Load += new EventHandler(OnViewLoad);
            OnInitialize();
        }

        #endregion

        #region OnInitialize

        protected virtual void OnInitialize() { }

        #endregion

        #region OnViewLoad
        protected virtual void OnViewLoad() { }
        void OnViewLoad(object sender, EventArgs e)
        {
            OnViewLoad();
        }
        #endregion

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
