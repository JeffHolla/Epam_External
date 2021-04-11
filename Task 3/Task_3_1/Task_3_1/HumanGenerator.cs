using System;
using System.Collections.Generic;

namespace Task_3_1
{
    // Этот класс был создан лишь потому, что CircleList и Weakestlink обособленны от типов.
    // А это значит, что чтобы пользователь мог передавать N людей, то их нужно создать, чего мы не можем сделать в этих классах
    public static class HumanGenerator
    {
        public static IEnumerable<Human> GenerateHumans(int count)
        {
            Human[] humans = new Human[count];
            for (int i = 0; i < count; i++)
            {
                var tmp = Guid.NewGuid().ToString();
                humans[i] = new Human() { Name = tmp.Substring(0, 5) };
            }

            return humans;
        }
    }
}
