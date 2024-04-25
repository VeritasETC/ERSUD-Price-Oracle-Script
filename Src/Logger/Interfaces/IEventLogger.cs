using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Interfaces
{
    public interface IEventLogger
    {
        public Task LogEvent(string ActorId, string ActorType, string OperationName, params object[] args);
        public Task LogRawRequestresponse(string OperationName, params object[] args);
    }
}
