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

        public void Log(IEvent @event)
        {
            #region Guards
            if (@event == null) throw new System.ArgumentNullException(nameof(@event));
            #endregion

            _eventWriters.ForEach(w => w.OnNext(@event));
        }
    }
}
