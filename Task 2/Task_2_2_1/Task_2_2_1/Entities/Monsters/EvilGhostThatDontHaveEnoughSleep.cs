using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_2_1.Entities.Monsters
{
    class EvilGhostThatDontHaveEnoughSleep : AbstractMonster, IMovable
    {
        public override Point centerOfObject { get; set; }

        public override double Damage { get; set; }
        public override double Health { get; set; }
        public override double Speed { get; set; }

        public EvilGhostThatDontHaveEnoughSleep(Point center)
        {
            centerOfObject = center;
        }

        public override double DealSomeDamage()
        {
            throw new NotImplementedException();
        }

        public void MoveUp()
        {

        }

        public void MoveDown()
        {

        }

        public void MoveLeft()
        {

        }

        public void MoveRight()
        {

        }
    }
}
