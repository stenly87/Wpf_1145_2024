using System.Text.Json;

namespace Wpf_1145_2024.Model.Student
{
    internal class StudentAttributeString : StudentAttribute<string>
    {
        public override float GetFloatValue()
        {
            return 0f;
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
                    Value = reader.GetString();
                }
            }
        }

        public override void WriteJsonValue(ref Utf8JsonWriter writer)
        {
            writer.WriteString("value", Value);
        }
    }
}
