using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starry.Data.Tests.Models
{
    class DbNullEntityEx : DbNullEntity
    {
        public DbNullEntityEx()
        {
            this.Value = 1;
        }
    }
}
