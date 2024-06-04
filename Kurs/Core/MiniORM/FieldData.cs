using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs.Core.MiniORM
{
    enum FieldType
    {
        I64,
        STRING,
        DECIMAL,
    }

    class FieldData
    {
        public ulong Ord { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public bool PrimaryKey { get; set; }
    }
}
