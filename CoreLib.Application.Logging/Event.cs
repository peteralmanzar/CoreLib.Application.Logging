using CoreLib.Application.Logging.Abstraction;
using System;

namespace CoreLib.Application.Logging
{
    public class Event : IEvent
    {
        #region Properties
        public int ID { get; }
        public DateTime TimeStamp { get; }
        public string Details { get; set; }
        public int Level { get; set; }
        #endregion

        #region Constructors
        public Event(string details, int level, int id = 0) : this(details, level, DateTime.Now, id)
        {
        }

        public Event(string details, int level, DateTime timeStamp, int id = 0)
        {
            #region Guards
            if (string.IsNullOrEmpty(details)) throw new ArgumentNullException(details);
            #endregion

            TimeStamp = timeStamp;
            Details = details;
            Level = level;
            ID = id;
        }
        #endregion

        #region Public Functions
        public override string ToString()
        {
            return $"{TimeStamp}\t{ID}\t{Level}\t{Details}";
        }
        #endregion
    }
}
