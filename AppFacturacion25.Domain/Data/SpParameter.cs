using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFacturacion25.Domain.Data
{
    public class SpParameter
    {
        public string Name { get; }
        public object Value { get; set; }
        public SqlDbType? Type { get; }
        public int? Size { get; }
        public ParameterDirection Direction { get; }

        public SpParameter(string name, object value)
        {
            Name = name;
            Value = value ?? DBNull.Value;
            Direction = ParameterDirection.Input;
        }

        public SpParameter(string name, object value, SqlDbType type, ParameterDirection direction, int? size = null)
        {
            Name = name;
            Value = value ?? DBNull.Value;
            Type = type;
            Size = size;
            Direction = direction;
        }
    }
}
