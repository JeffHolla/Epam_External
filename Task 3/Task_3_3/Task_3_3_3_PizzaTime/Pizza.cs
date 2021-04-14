using System;

namespace Task_3_3_3_PizzaTime
{
    public class Pizza : ICloneable
    {
        public string Name { get; set; }

        public int MinuteToCook { get; private set; }

        public Pizza(string name, int minutes)
        {
            Name = name;
            MinuteToCook = minutes;
        }

        public object Clone()
        {
            return Name.Clone();
        }

        public override string ToString()
        {
            return Name;
        }
    }



}
