using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_1145_2024.Model.Task
{
    public class Task
    {
        public string Title { get; set; }

        public List<TaskAttribute> Attributes { get; set; } = new();
    }
}
