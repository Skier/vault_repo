/*
 *  
 *
 *  
 */
using System;
using System.Threading;
using log4net;
using log4net.Core;

namespace AerSysCo.Common
{
    public sealed class Logger
    {

        public const string APP_ERROR_LOGGER = "app";
        public const string SYS_ERROR_LOGGER = "sys";

        private static ILog _contextLog;
        private static string _contextLogName;

        private Logger() {
        }

        public static void ASSERT(bool cond) {
            if ( !cond ) {
                throw new SystemException("Assertion fail");
            }
        }

        public static void ASSERT(bool cond, string message) {
            if ( !cond ) {
                throw new SystemException("Assertion fail '"+message+"'");
            }
        }

        public static ILog GetAppLogger() {
            return LogManager.GetLogger(APP_ERROR_LOGGER);
        }

        public static ILog GetSysLogger() {
            return LogManager.GetLogger(SYS_ERROR_LOGGER);
        }

        public static void Log(ILog logger, Level level, object message, Exception t, params object[] prms) {
            if (logger == null) {
                logger = ContextLog;
            }

            if (level == null) {
                throw new ArgumentNullException("level");
            }

            if (logger.Logger.IsEnabledFor(level)) {
                if (message == null) {
                    throw new ArgumentNullException("message");
                }
                string msg = (prms == null || prms.Length == 0
                                  ? message.ToString()
                                  : string.Format(message.ToString(), prms));
                logger.Logger.Log(logger.Logger.GetType(), level, msg, t);
            }
        }

        public static void All(ILog logger, object message) {
            Log(logger, Level.All, message, null);
        }

        public static void All(ILog logger, object message, params object[] prms) {
            Log(logger, Level.All, message, null, prms);
        }

        public static void All(ILog logger, object message, Exception t, params object[] prms) {
            Log(logger, Level.All, message, t, prms);
        }

        public static void Trace(ILog logger, object message) {
            Log(logger, Level.Trace, message, null);
        }

        public static void Trace(ILog logger, object message, params object[] prms) {
            Log(logger, Level.Trace, message, null, prms);
        }

        public static void Trace(ILog logger, object message, Exception t, params object[] prms) {
            Log(logger, Level.Trace, message, t, prms);
        }

        public static void Debug(ILog logger, object message) {
            Log(logger, Level.Debug, message, null);
        }

        public static void Debug(ILog logger, object message, params object[] prms) {
            Log(logger, Level.Debug, message, null, prms);
        }

        public static void Debug(ILog logger, object message, Exception t, params object[] prms) {
            Log(logger, Level.Debug, message, t, prms);
        }

        public static void Info(ILog logger, object message, params object[] prms) {
            Log(logger, Level.Info, message, null, prms);
        }

        public static void Warn(ILog logger, object message) {
            Log(logger, Level.Warn, message, null);
        }

        public static void Warn(ILog logger, object message, params object[] prms) {
            Log(logger, Level.Warn, message, null, prms);
        }

        public static void Warn(ILog logger, object message, Exception t, params object[] prms) {
            Log(logger, Level.Warn, message, t, prms);
        }

        public static void Error(ILog logger, object message, Exception t, params object[] prms) {
            Log(logger, Level.Error, message, t, prms);
        }

        public static void Fatal(ILog logger, object message, Exception t, params object[] prms) {
            Log(logger, Level.Fatal, message, t, prms);
        }

        public static LoggingEvent GetEvent(ILog logger, Level level, object message, Exception ex) {
            return
                new LoggingEvent(logger.Logger.GetType(), logger.Logger.Repository, logger.Logger.Name, level, message, ex);
        }

        public static void Flush(ILog logger) {
            // TODO: flush all TextWriterAppenders
            //logger.Warn("Flushing...");
        }

        public static ILog ContextLog {
            get {
                if (_contextLog == null) {
                    if (_contextLogName == null) {
                        _contextLog = GetAppLogger();
                    } else {
                        _contextLog = LogManager.GetLogger(_contextLogName);
                    }
                }
                return _contextLog;
            }
        }

        public static string ContextLogName {
            get { return _contextLogName; }
            set {
                _contextLogName = value;
                _contextLog = null;
            }
        }
    }
}