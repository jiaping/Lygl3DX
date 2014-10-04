namespace Lygl.UI.Framework.ViewModelBase
{
    using System;

    /// <summary>
    /// Contains information about the error that
    /// has occurred.
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the Exception object for the error
        /// that occurred.
        /// </summary>
        public Exception Error { get; internal set; }
    }
}
