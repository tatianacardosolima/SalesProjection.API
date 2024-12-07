using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProjection.Domain.Exceptions
{
    public class UploadCsvException(string message) : Exception(message)
    {
        public static void ThrowWhen(bool invalidRule, string message)
        {
            if (invalidRule)
            {
                throw new UploadCsvException(message);
            }
        }
    }
}
