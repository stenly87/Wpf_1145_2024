using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Wpf_1145_2024.Model.Student;
using task = Wpf_1145_2024.Model.Task;

namespace Wpf_1145_2024.DB
{
    internal class DataBase
    {
        string file = "students.db";
        List<Student> students;

        string fileTask = "tasks.db";
        List<task.Task> tasks;

        JsonSerializerOptions optionsStudentJson;
        JsonSerializerOptions optionsTaskJson;

        private DataBase()
        {
            optionsStudentJson = new JsonSerializerOptions();
            optionsTaskJson = new JsonSerializerOptions();
            optionsStudentJson.Converters.Add(new ListAttributeStudentConverter());
            optionsTaskJson.Converters.Add(new ListAttributeTaskConverter());
            if (File.Exists(file))
            {
                using (var fs = File.OpenRead(file))
                    students = JsonSerializer.Deserialize<List<Student>>(fs, options: optionsStudentJson);
            }
            else
                students = new();

            if (File.Exists(fileTask))
            {
                using (var fs = File.OpenRead(fileTask))
                    tasks = JsonSerializer.Deserialize<List<task.Task>>(fs, options: optionsTaskJson);
            }
            else
                tasks = new();
        }
        static DataBase instance;

        public static DataBase GetInstance()
            => instance ?? (instance = new());

        public void Save()
        {
            using (var fs = File.Create(file))
                JsonSerializer.Serialize(fs, students,options: optionsStudentJson);
            
            using (var fs = File.Create(fileTask))
                JsonSerializer.Serialize(fs, tasks, options: optionsTaskJson);
        }

        internal void Add(Student student)
        {
            students.Add(student);
        }

        internal void Add(task.Task task)
        {
            tasks.Add(task);
        }

        internal IEnumerable<Student> GetStudents()
            => students;

        internal IEnumerable<task.Task> GetTasks()
            => tasks;
    }
}
