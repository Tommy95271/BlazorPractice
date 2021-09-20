using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.ViewModels
{
    public class SweetConfirmViewModel
    {
        public string RequestTitle { get; set; }
        public string RequestText { get; set; }
        public string ResponseTitle { get; set; }
        public string ResponseText { get; set; }
    }
}
