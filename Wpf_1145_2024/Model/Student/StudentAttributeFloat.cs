using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wpf_1145_2024.Model.Student
{
    internal class StudentAttributeFloat : StudentAttribute<float>
    {
        public override float GetFloatValue()
        {
            return Value;
        }

        public override string ToString()
        {
            return $"{Title}: {Value}";
        }

        public override void ReadJsonValue(ref Utf8JsonReader reader)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return;
                string prop = reader.GetString();
                if (prop == "title")
                {
                    reader.Read();
                    Title = reader.GetString();
                }
                else if (prop == "value")
                {
                    reader.Read();
                    Value = reader.GetSingle();
                }
            }
        }

        public override void WriteJsonValue(ref Utf8JsonWriter writer)
        {
            writer.WriteNumber("value", Value);
        }
    }
}
