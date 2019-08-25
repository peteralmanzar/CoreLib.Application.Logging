using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CoreLib.Application.Logging.EventWriters
{
    public class WindowsEventLogWriter
    {
        #region Members
        private EventLog _eventLog;
        private static string _defaultSourceName => Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
        private static string _defaultLogName => $"{_defaultSourceName}_Log";
        #endregion

        #region Constructors
        public WindowsEventLogWriter(string LogName, string SourceName, SourceLevels sourceLevels = SourceLevels.All)
        {
            #region Guards
            if (string.IsNullOrEmpty(LogName)) LogName = _defaultLogName;
            if (string.IsNullOrEmpty(SourceName)) LogName = _defaultSourceName;
            #endregion

            var eventLog = new System.Diagnostics.EventLog(LogName, Environment.MachineName, SourceName);
        }
        #endregion
    }
}
