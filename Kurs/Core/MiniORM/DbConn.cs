using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kurs.Core.MiniORM
{
    class DbConn
    {
        private SQLiteConnection _conn;

        public DbConn(string file_path)
        {
            _conn = new SQLiteConnection($"Data Source = {file_path}");
            _conn.Open();
        }

        public void ensure_created<T>() 
        {
            TypeRefl refl = TypeRefl.for_<T>();
            StringBuilder qsb = new StringBuilder();
            qsb.Append($"CREATE TABLE IF NOT EXISTS '{typeof(T).Name}' (");
            foreach (var item in refl.Fields)
            {
                qsb.Append($"{item.Name} {to_sqldt(item.Type)}");
                if (item.PrimaryKey)
                    qsb.Append($" PRIMARY KEY AUTOINCREMENT");
                qsb.Append(", ");
            }

            qsb.Remove(qsb.Length - 2, 2);
            qsb.Append($");");

            var cmd = _conn.CreateCommand();
            cmd.CommandText = qsb.ToString();
            cmd.ExecuteNonQuery();
        }

        public void insert<T>(T value)
        {
            TypeRefl refl = TypeRefl.for_<T>();
            StringBuilder qsb = new StringBuilder();
            qsb.Append($"INSERT INTO '{typeof(T).Name}' (");
            foreach (var item in refl.Fields)
            {
                if (item.Name.ToLowerInvariant() != "id") // kostil
                    qsb.Append($"{item.Name}, ");
            }
            qsb.Remove(qsb.Length - 2, 2);
            qsb.Append($") VALUES (");
            foreach (var item in refl.Fields)
            {
                if (item.Name.ToLowerInvariant() != "id") // kostil
                {
                    if (item.Type == FieldType.STRING)
                        qsb.Append("\"");
                    qsb.Append($"{typeof(T).GetTypeInfo().GetProperty(item.Name).GetValue(value)}");
                    if (item.Type == FieldType.STRING)
                        qsb.Append("\"");

                    qsb.Append(", ");
                }
            }
            qsb.Remove(qsb.Length - 2, 2);
            qsb.Append($");");

            var cmd = _conn.CreateCommand();
            cmd.CommandText = qsb.ToString();
            cmd.ExecuteNonQuery();
        }

        public ADT.Vec<T> select_all<T>()
            where T: new()
        {
            TypeRefl refl = TypeRefl.for_<T>();
            StringBuilder qsb = new StringBuilder();
            qsb.Append($"SELECT ");
            foreach (var item in refl.Fields)
            {
                qsb.Append($"{item.Name}, ");
            }

            qsb.Remove(qsb.Length - 2, 2);
            qsb.Append($" FROM \"{typeof(T).Name}\";");

            ADT.Vec<T> result = new ADT.Vec<T>();
            var cmd = _conn.CreateCommand();
            cmd.CommandText = qsb.ToString();
            
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                T val = new T();
                foreach (var item in refl.Fields)
                {
                    object value = rd.GetValue(item.Name);
                    typeof(T).GetTypeInfo().GetProperty(item.Name)!.SetValue(val, value);
                }
                result.push_back(val);
            }

            return result;
        }

        private static string to_sqldt(FieldType type)
        {
            switch(type)
            {
                case FieldType.I64: return "INTEGER";
                case FieldType.STRING: return "TEXT";
                case FieldType.DECIMAL: return "DECIMAL";
            }

            return ""; // why???????????? the switch is exhaustive
        }
    }
}
