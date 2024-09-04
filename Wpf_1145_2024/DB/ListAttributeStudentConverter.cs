using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using Wpf_1145_2024.Model.Student;

namespace Wpf_1145_2024.DB
{
    internal class ListAttributeStudentConverter : JsonConverter<List<IStudentAttribute>>
    {
        public override List<IStudentAttribute>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<IStudentAttribute> result = new();
            while (reader.Read()) 
            {
                if (reader.TokenType == JsonTokenType.StartArray ||
                    reader.TokenType == JsonTokenType.EndObject)
                    continue;
                if (reader.TokenType == JsonTokenType.EndArray)
                    return result;

                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    reader.Read();// prop "type"
                    reader.Read();// prop "type" value
                    string typeAttribute = reader.GetString();
                    Type? type = GetModelType(typeAttribute); 
                    var attribute = (IStudentAttribute)Activator.CreateInstance(type);
                    attribute.ReadJsonValue(ref reader);
                    result.Add(attribute);
                }
            }
            return result;
        }

        private static Type? GetModelType(string typeAttribute)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetType("Wpf_1145_2024.Model.Student." + typeAttribute);
        }

        public override void Write(Utf8JsonWriter writer, List<IStudentAttribute> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var attr in value)
            {
                writer.WriteStartObject();
                writer.WriteString("type", attr.GetType().Name);
                writer.WriteString("title", attr.Title);
                attr.WriteJsonValue(ref writer);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}