using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateAPI.Models
{
    public class Todo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public bool Complete { get; set; }
    }
}
