using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProjection.Domain.Abstractions
{
    public interface IEntity
    {     
    }

    public abstract class EntityBase : IEntity
    {
        [Ignore]
        public Guid Id { get; set; }
    }
}
