using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project3
{

    public class Minors
    {
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<string> courses { get; set; }
        public string note { get; set; }
    }

    public class MinorsRoot
    {
        public List<Minors> UgMinors { get; set; }
    }

}
