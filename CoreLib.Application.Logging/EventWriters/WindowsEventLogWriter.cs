using CoreLib.Application.Logging.Abstraction;
using System;
using System.Diagnostics;
using System.IO;

namespace CoreLib.Application.Logging.EventWriters
{
    public class WindowsEventLogWriter : EventWriter
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
        public WindowsEventLogWriter(string LogName, string SourceName, SourceLevels sourceLevels = SourceLevels.All)
        {
            #region Guards
            if (string.IsNullOrEmpty(LogName)) LogName = _defaultLogName;
            if (string.IsNullOrEmpty(SourceName)) LogName = _defaultSourceName;
            #endregion

            _eventLog = new System.Diagnostics.EventLog(LogName, Environment.MachineName, SourceName);
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

        public static void DeleteEventSource(string Name)
        {
            #region Guards
            if (Name == null) throw new ArgumentNullException(nameof(Name));
            #endregion

            System.Diagnostics.EventLog.DeleteEventSource(Name, Environment.MachineName);
        }
        #endregion
    }
}
