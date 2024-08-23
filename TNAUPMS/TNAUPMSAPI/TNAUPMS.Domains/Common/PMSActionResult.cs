using System;
using System.Collections.Generic;
using System.Text;

namespace TNAUPMS.Domains.Common
{
    public class TNAUPMSApiActionResult<T>
    {
        public T Result { get; set; }
        public List<string> ErrorMsgs { get; set; } = new List<string>();
    }
}
