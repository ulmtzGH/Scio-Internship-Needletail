using Needletail.DataAccess.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Address
    {
        [TableKey(CanInsertKey = true)]
        public Guid Id { get; set; }
        public string Street { get; set; }

        public string ZipCode { get; set; }
        public string Phone { get; set; }
    }
}
