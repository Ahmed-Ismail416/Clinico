using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Modules
{
    public class BaseEntity<Tkey> 
    {
        public Tkey Id { get; set; } = default!;
        //Created At
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
