using CoreLib.Application.Logging.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLib.Application.Logging.EventWriters
{
    public class ConsoleLogWriter : baseEventWriter
    {
        public override void OnNext(IEvent value)
        {
            #region Guards
            if (value == null) throw new ArgumentNullException(nameof(value));
            #endregion

            Console.WriteLine(value);
        }
    }
}
