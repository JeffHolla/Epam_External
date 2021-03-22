using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_2_1.Entities.Bonuses
{
    class Apple : AbstactBonus
    {
        public override Point centerOfObject { get; set; }

        public Apple(Point center)
        {
            centerOfObject = center;
        }

        public override double AddSomeHP()
        {
            throw new NotImplementedException();
        }

        public override double RemoveSomeHP()
        {
            throw new NotImplementedException();
        }
    }
}
