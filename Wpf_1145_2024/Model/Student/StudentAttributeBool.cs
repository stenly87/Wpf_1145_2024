using System.Text.Json;

namespace Wpf_1145_2024.Model.Student
{
    internal class StudentAttributeBool : StudentAttribute<bool>
    {
        public override float GetFloatValue()
        {
            return Value ? 1 : 0;
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
                    Value = reader.GetBoolean();
                }
            }
        }

        public override string ToString()
        {
            return Value ? Title : "НЕ " + Title;
        }

        public override void WriteJsonValue(ref Utf8JsonWriter writer)
        {
            writer.WriteBoolean("value", Value);
        }
    }
}
