using System;

namespace CoreLib.Application.Logging.Abstraction
{
    public abstract class EventWriter : IObserver<IEvent>
    {
        #region Members
        protected IDisposable _unsubscriber;
        #endregion

        #region Public Functions
        public virtual void OnCompleted()
        {
            _unsubscriber.Dispose();
        }

        public virtual void OnError(Exception error)
        {
            #region Guards
            if (error == null) throw new ArgumentNullException(nameof(error));
            #endregion

            throw error;
        }

        public abstract void OnNext(IEvent value);

        public virtual void Subscribe(IObservable<IEvent> observable)
        {
            #region Guards
            if (observable == null) throw new ArgumentNullException(nameof(observable));
            #endregion

            _unsubscriber = observable.Subscribe(this);
        }
        #endregion
    }
}
