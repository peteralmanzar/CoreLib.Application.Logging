using CoreLib.Application.Logging.Abstraction;
using System;

namespace CoreLib.Application.Logging
{
    public class Event : IEvent
    {
        #region Properties
        public int ID { get; }
        public string Details { get; set; }
        public int Level { get; set; }
        #endregion

        #region Constructors
        public Event(string details, int level, int id = 0)
        {
            #region Guards
            if (string.IsNullOrEmpty(details)) throw new ArgumentNullException(details);
            #endregion

            Details = details;
            Level = level;
            ID = id;
        }
        #endregion
    }
}
