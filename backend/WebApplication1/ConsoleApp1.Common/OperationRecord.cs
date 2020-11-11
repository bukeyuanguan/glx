using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Common
{
    public class OperationRecord
    {
        public OperationRecord(IHttpContextAccessor context)
        {
            UriPath = context.HttpContext.Request.Path;
            ClientIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
            Time = DateTime.UtcNow;
        }

        public string UriPath { get; set; }
        public string ClientIp { get; set; }
        public DateTime Time { get; set; }
    }
}
