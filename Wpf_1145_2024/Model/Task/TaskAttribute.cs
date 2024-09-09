using Wpf_1145_2024.Model.Student;

namespace Wpf_1145_2024.Model.Task
{
    public class TaskAttribute
    {
        public IStudentAttribute Attribute { get; set; }
        public float Inaccuracy { get; set; }

        public override string ToString()
        {
            string inaccuracy = Inaccuracy > 0 ? $" Погрешность - " + Inaccuracy : null;
            return Attribute.ToString() + inaccuracy;
        }
    }
}