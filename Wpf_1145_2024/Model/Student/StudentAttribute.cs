using System.Text.Json;

namespace Wpf_1145_2024.Model.Student
{
    public abstract class StudentAttribute<T> : IStudentAttribute
    {
        public string Title { get; set; }
        public T Value { get; set; }

        public abstract float GetFloatValue();
        public abstract void ReadJsonValue(ref Utf8JsonReader reader);
        public abstract void WriteJsonValue(ref Utf8JsonWriter writer);
    }
}