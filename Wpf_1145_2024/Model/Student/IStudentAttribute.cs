using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wpf_1145_2024.Model.Student
{
    public interface IStudentAttribute : IModel
    {
        string Title { get; set; }
        float GetFloatValue();
        void ReadJsonValue(ref Utf8JsonReader reader);
        void WriteJsonValue(ref Utf8JsonWriter writer);
    }
}