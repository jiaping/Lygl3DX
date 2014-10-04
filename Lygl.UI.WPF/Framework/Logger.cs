using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Lygl.UI.Framework
{
    [Export(typeof(Logger))]
    public class Logger
    {
        public Logger()
        {
            //设置caliburn的日志代理，通过代理记录到log4net中
            Caliburn.Micro.LogManager.GetLog = type => CaliburnLogInstance;
        }

        //设置caliburn的日志代理，通过代理记录到log4net中
        static readonly Caliburn.Micro.ILog CaliburnLogInstance = new Lygl.UI.Framework.Logger.CaliburnLogHelper();


        /// <summary>
        /// log4net日志
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger("Lygl");
        public log4net.ILog Log
        {
            get
            {
                return _log;
            }
        }

        private class CaliburnLogHelper : Caliburn.Micro.ILog
        {

            /// Logs the message as info.
            /// </summary>
            /// <param name="format">A formatted message.</param>
            /// <param name="args">Parameters to be injected into the formatted message.</param>
            void Caliburn.Micro.ILog.Info(string format, params object[] args)
            {
                _log.InfoFormat(format, args);
                //IoC.Get<Logger>().Log.InfoFormat(format, args);
            }
            /// <summary>
            /// Logs the message as a warning.
            /// </summary>
            /// <param name="format">A formatted message.</param>
            /// <param name="args">Parameters to be injected into the formatted message.</param>
            void Caliburn.Micro.ILog.Warn(string format, params object[] args)
            {
                _log.WarnFormat(format, args);
            }
            /// <summary>
            /// Logs the exception.
            /// </summary>
            /// <param name="exception">The exception.</param>
            void Caliburn.Micro.ILog.Error(Exception exception)
            {
                _log.Error("Caliburn Framework Error:", exception);
            }
        }
    }
}
