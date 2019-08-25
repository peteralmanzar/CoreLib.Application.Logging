using CoreLib.Application.Logging.Abstraction;

namespace CoreLib.Application.Logging
{
    public class EventLog : EventListener
    {
        #region Properties
        public static EventLog Default;
        #endregion

        #region Constructors
        static EventLog()
        {
            Default = new EventLog();
        }
        public EventLog() : base()
        {

        }
        #endregion
    }
}
