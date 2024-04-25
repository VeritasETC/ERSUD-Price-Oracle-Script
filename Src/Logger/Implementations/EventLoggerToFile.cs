using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Logger.Interfaces;

namespace Logger.Implementations
{
    class EventLoggerToFile : IEventLogger
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public EventLoggerToFile(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task LogEvent(string ActorId, string ActorType, string OperationName, params object[] args)
        {
            object reqID, token = "";
            httpContextAccessor.HttpContext.Items.TryGetValue("RequestUUID", out reqID);
            httpContextAccessor.HttpContext.Items.TryGetValue("token", out token);
            var ip = httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            var Device = httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            var log = JsonConvert.SerializeObject(new { RequestId = reqID, ActorId, ActorType, OperationName, IP = ip.ToString(), Device, DateTime = DateTime.UtcNow, objs = args });
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppLogs")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppLogs"));
            }
            using StreamWriter file = new StreamWriter(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppLogs"), DateTime.UtcNow.Date.Ticks.ToString() + ".txt"), append: true);
            await file.WriteLineAsync(log);
        }
        public async Task LogRawRequestresponse(string OperationName, params object[] args)
        {
            object reqID, token = "";
            httpContextAccessor.HttpContext.Items.TryGetValue("RequestUUID", out reqID);
            httpContextAccessor.HttpContext.Items.TryGetValue("token", out token);
            var ip = httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            var Device = httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            var log = JsonConvert.SerializeObject(new { RequestId = reqID, OperationName, IP = ip.ToString(), Device, DateTime = DateTime.UtcNow, objs = args });
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppRawLogs")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppRawLogs"));
            }
            using StreamWriter file = new StreamWriter(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppRawLogs"), DateTime.UtcNow.Date.Ticks.ToString() + ".txt"), append: true);
            await file.WriteLineAsync(log);
        }
    }
}
