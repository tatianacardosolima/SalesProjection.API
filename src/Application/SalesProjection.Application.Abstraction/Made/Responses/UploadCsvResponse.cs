using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProjection.Application.Abstraction.Made.Responses
{
    public class UploadCsvResponse
    {
        public UploadCsvResponse(Guid id) 
        {
            Id = id;

        }
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
