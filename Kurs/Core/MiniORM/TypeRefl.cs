using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs.Core.MiniORM
{
    class TypeRefl
    {
        public ADT.Vec<FieldData> Fields { get; private set; }

        public static TypeRefl for_<T>() 
        {
            TypeRefl outp = new TypeRefl();
            outp.Fields = new ADT.Vec<FieldData>();
            var fields = typeof(T).GetProperties();
            ulong i = 0;
            foreach (var field in fields)
            {
                FieldType type;
                Type field_type = field.PropertyType;
                if (field_type.Equals(typeof(long))) 
                {
                    type = FieldType.I64;
                } 
                else if (field_type.Equals(typeof(string))) 
                {
                    type = FieldType.STRING;
                }
                else if (field_type.Equals(typeof(decimal))) 
                {
                    type = FieldType.DECIMAL;
                }
                else
                {
                    // eww exceptions
                    throw new Exception($"Unsupported type: {field_type.ToString()}");
                }

                bool prim_key = (field.Name.ToLowerInvariant() == "id") && (i == 0);
                outp.Fields.push_back(new FieldData { Ord = i++, Name = field.Name, Type = type, PrimaryKey = prim_key });
            }

            return outp;
        }
    }
}
