using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedFlghtDynamicSystem.Models
{
    public  partial class ISFDSEntities
    {
        public ISFDSEntities(string connectionStringName)
            :base(String.Format("name={0}", connectionStringName))
        {
            
        }
    }
}
