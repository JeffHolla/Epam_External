using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2
{
    public class User
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;

            if(user == null)
            {
                return false;
            }

            return Name == user.Name;
        }
    }
}
