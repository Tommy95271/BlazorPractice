using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public interface IGuidService
    {
        public string uid { get; set; }
    }
}
