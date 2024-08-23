using System;
using System.Collections.Generic;
using System.Text;

namespace TNAUPMS.Domains.Common
{
    public class TNAUPMSActionResult
    {
        public string result { get; set; }
        public List<string> ErrorMsgs { get; set; } = new List<string>();
    }
}
