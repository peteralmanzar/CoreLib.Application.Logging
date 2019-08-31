using System;

namespace CoreLib.Application.Logging.Abstraction
{
    public interface IEvent
    {
        #region Properties
        int ID { get; }
        DateTime TimeStamp { get; }
        string Details { get; }
        int Level { get; }
        #endregion
    }
}