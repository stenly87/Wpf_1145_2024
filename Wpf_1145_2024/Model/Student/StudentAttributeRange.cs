using System.Text.Json;

namespace Wpf_1145_2024.Model.Student
{
    internal class StudentAttributeRange : StudentAttribute<byte>
    {
        public byte Min { get; set; }
        public byte Max { get; set; }

        public override float GetFloatValue()
        {
            return 0f;
        }

        public override string ToString()
        {
            return $"{Title} ({Min}-{Max}): {Value}";
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
                else if (prop == "min")
                {
                    reader.Read();
                    Min = reader.GetByte();
                }
                else if (prop == "max")
                {
                    reader.Read();
                    Max = reader.GetByte();
                }
                else if (prop == "value")
                {
                    reader.Read();
                    Value = reader.GetByte();
                }
            }
        }

        public override void WriteJsonValue(ref Utf8JsonWriter writer)
        {
            writer.WriteNumber("min", Min);
            writer.WriteNumber("max", Max);
            writer.WriteNumber("value", Value);
        }
    }
}
