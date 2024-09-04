using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_1145_2024.Model.Student
{
    public class Student
    {
        public string FIO { get; set; }

        public List<IStudentAttribute> Attributes { get; set; } = new();

        public void AddAttribute(IStudentAttribute attribute)
            => Attributes.Add(attribute);

    }
}
