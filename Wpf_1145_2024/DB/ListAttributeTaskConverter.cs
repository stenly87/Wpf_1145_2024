using System.Text.Json;
using System.Text.Json.Serialization;
using Wpf_1145_2024.Model.Task;

namespace Wpf_1145_2024.DB
{
    internal class ListAttributeTaskConverter : JsonConverter<TaskAttribute>
    {
        public override TaskAttribute? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, TaskAttribute value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}