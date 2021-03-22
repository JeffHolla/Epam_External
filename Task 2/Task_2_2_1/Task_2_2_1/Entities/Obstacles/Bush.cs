using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_2_1.Entities
{
    class Bush : AbstractObstacle
    {
        public override Point centerOfObject { get; set; }

        public Bush(Point center)
        {
            centerOfObject = center;
        }

        public override bool IsHaveCollision()
        {
            throw new NotImplementedException();
        }
    }
}
