using System;
using System.Collections.Generic;

namespace CoreLib.Application.Logging.Abstraction
{
    public abstract class EventListener : IObservable<IEvent>
    {
        private class Unsubscriber : IDisposable
        {
            #region Members
            private List<IObserver<IEvent>> _observers;
            private IObserver<IEvent> _observer;
            #endregion

            #region Constructors
            public Unsubscriber(List<IObserver<IEvent>> observers, IObserver<IEvent> observer)
            {
                #region Guards
                if (observers == null) throw new ArgumentNullException(nameof(observers));
                if (observer == null) throw new ArgumentNullException(nameof(observer));
                #endregion

                _observers = observers;
                _observer = observer;
            }
            #endregion

            #region Public Functions
            public void Dispose()
            {
                #region Guards
                if (_observers == null) throw new ArgumentNullException(nameof(_observers));
                if (_observer == null) throw new ArgumentNullException(nameof(_observer));
                #endregion

                _observers.Remove(_observer);
            }
            #endregion
        }

        #region Properties
        public IEnumerable<IObserver<IEvent>> EventWriters => _eventWriters;
        #endregion

        #region Members
        protected List<IObserver<IEvent>> _eventWriters;
        #endregion

        #region Constructors
        public EventListener()
        {
            _eventWriters = new List<IObserver<IEvent>>();
        }
        #endregion

        #region Public Functions
        public IDisposable Subscribe(IObserver<IEvent> observer)
        {
            if (!_eventWriters.Contains(observer)) _eventWriters.Add(observer);
            return new Unsubscriber(_eventWriters, observer);
        }
        #endregion
    }
}
