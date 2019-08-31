using CoreLib.Application.Logging.Abstraction;
using System;
using System.Diagnostics;
using System.IO;

namespace CoreLib.Application.Logging.EventWriters
{
    public class WindowsEventLogWriter : baseEventWriter
    {
        #region Properties
        public string LogName => _eventLog.Log;
        public string SourceName => _eventLog.Source;
        public long MaximumKilobytes
        {
            get { return _eventLog.MaximumKilobytes; }
            set { _eventLog.MaximumKilobytes = value; }
        }
        public int MinimumRetentionDays
        {
            get { return _eventLog.MinimumRetentionDays; }
            set { _eventLog.ModifyOverflowPolicy(OverflowReaction, value); }
        }
        public OverflowAction OverflowReaction
        {
            get { return _eventLog.OverflowAction; }
            set { _eventLog.ModifyOverflowPolicy(value, MinimumRetentionDays); }
        }
        #endregion

        #region Members
        private System.Diagnostics.EventLog _eventLog;
        private static string _defaultSourceName => Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
        private static string _defaultLogName => $"{_defaultSourceName}_Log";
        #endregion

        #region Constructors
        public WindowsEventLogWriter(string logName, string sourceName, SourceLevels sourceLevels = SourceLevels.All)
        {
            #region Guards
            if (string.IsNullOrEmpty(logName)) logName = _defaultLogName;
            if (string.IsNullOrEmpty(sourceName)) logName = _defaultSourceName;
            #endregion

            _eventLog = new System.Diagnostics.EventLog(logName, Environment.MachineName, sourceName);
        }
        #endregion

        #region Public Functions
        public override void OnNext(IEvent value)
        {
            #region Guards
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (!System.Diagnostics.EventLog.SourceExists(_eventLog.Source)) System.Diagnostics.EventLog.CreateEventSource(_eventLog.Source, _eventLog.Log);
            #endregion

            _eventLog.WriteEntry(value.Details, (EventLogEntryType)value.Level, value.ID);
        }

        public static void CreateEventSource(string sourceName, string logName)
        {
            #region Guards
            if (string.IsNullOrEmpty(sourceName)) throw new ArgumentException(nameof(sourceName));
            if (string.IsNullOrEmpty(logName)) throw new ArgumentException(nameof(logName));
            #endregion

            System.Diagnostics.EventLog.CreateEventSource(sourceName, logName);
        }

        public static void DeleteEventSource(string sourceName)
        {
            #region Guards
            if (sourceName == null) throw new ArgumentNullException(nameof(sourceName));
            #endregion
            
            System.Diagnostics.EventLog.DeleteEventSource(sourceName, Environment.MachineName);
        }

        public static void DeleteLog(string logName)
        {
            #region Guards
            if (logName == null) throw new ArgumentNullException(nameof(logName));
            #endregion

            if (System.Diagnostics.EventLog.Exists(logName)) System.Diagnostics.EventLog.Delete(logName);
        }
        #endregion
    }
}
