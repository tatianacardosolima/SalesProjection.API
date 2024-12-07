using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProjection.Application.Abstraction.Made.Request
{
    public class UploadCsvRequest
    {
        public string FileName { get; set; } = string.Empty;
        public byte[]? FileContent { get; set; }


    }

}
