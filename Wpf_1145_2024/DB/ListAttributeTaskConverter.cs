using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Wpf_1145_2024.Model.Student;
using Wpf_1145_2024.Model.Task;

namespace Wpf_1145_2024.DB
{
    internal class ListAttributeTaskConverter : JsonConverter<TaskAttribute>
    {
        public override TaskAttribute? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            TaskAttribute result = new();
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                reader.Read();// prop "type"
                reader.Read();// prop "type" value
                result.Inaccuracy = reader.GetSingle();
            }
            reader.Read();// prop "type"
            reader.Read();// prop "type" value
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                reader.Read();// prop "type"
                reader.Read();// prop "type" value
                string typeAttribute = reader.GetString();
                Type? type = GetModelType(typeAttribute);
                var attribute = (IStudentAttribute)Activator.CreateInstance(type);
                attribute.ReadJsonValue(ref reader);
                result.Attribute = attribute;
            }
            reader.Read();
            return result;
        }

        private static Type? GetModelType(string typeAttribute)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetType("Wpf_1145_2024.Model.Student." + typeAttribute);
        }

        public override void Write(Utf8JsonWriter writer, TaskAttribute value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("Inaccuracy", value.Inaccuracy);

            writer.WritePropertyName("attribute");
            writer.WriteStartObject();
            writer.WriteString("type", value.Attribute.GetType().Name);
            writer.WriteString("title", value.Attribute.Title);
            value.Attribute.WriteJsonValue(ref writer);
            writer.WriteEndObject();

            writer.WriteEndObject();
        }
    }
}