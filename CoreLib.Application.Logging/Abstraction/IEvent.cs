namespace CoreLib.Application.Logging.Abstraction
{
    public interface IEvent
    {
        #region Properties
        int ID { get; }
        string Details { get; }
        int Level { get; }
        #endregion
    }
}