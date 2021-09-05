using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public class GuidService : IGuidService
    {
        public string uid { get; set; }
        public GuidService()
        {
            uid = Guid.NewGuid().ToString();
        }
    }
}
