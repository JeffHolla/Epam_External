using System.Collections.Generic;

namespace Task_3_3_3_PizzaTime
{
    // По хорошему класс Order объявить бы в классе Pizzaria и сделать приватным, чтобы нельзя было создать Order просто так
    // Тем самым нельзя было бы создать кучу пустых Order для увеличения счётчика или прочего,
    // но я не уверен, что это не затруднит читаемость класса Pizzaria и делают ли так на проде вообще :о
    public class Order
    {
        private static int IdOrder { get; set; } = 0;

        public List<Pizza> Pizzas { get; }
        public string WhoOrdered { get; private set; }
        public int Id { get; private set; }

        public Order(string nameWhoOrdered)
        {
            Pizzas = new List<Pizza>();

            WhoOrdered = nameWhoOrdered;

            IdOrder += 1;

            Id = IdOrder;
        }

        public Order(Pizza pizza, string nameWhoOrdered) : this(nameWhoOrdered)
        {
            Pizzas.Add(pizza);
        }

        public Order(IEnumerable<Pizza> pizzas, string nameWhoOrdered)
        {
            Pizzas = new List<Pizza>(pizzas);

            WhoOrdered = nameWhoOrdered;

            IdOrder += 1;

            Id = IdOrder;
        }

        public void AddPizza(Pizza pizza)
        {
            Pizzas.Add(pizza);
        }

        public void AddPizzas(IEnumerable<Pizza> pizzas)
        {
            Pizzas.AddRange(pizzas);
        }
    }



}
