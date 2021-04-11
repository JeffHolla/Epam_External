using System;

namespace Task_3_1
{
    // Чисто формальный класс Human с именем и возможностью клона
    public class Human : ICloneable
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public object Clone()
        {
            return new Human() { Name = this.Name };
        }
    }
}
