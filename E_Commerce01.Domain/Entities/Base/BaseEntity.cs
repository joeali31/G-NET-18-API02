using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Domain.Entities.Base
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; } 
    }
}
