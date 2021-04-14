using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_3_3_3_PizzaTime
{
    public class Pizzaria
    {
        public event Action<object, Order> OnOrderInProgress;
        public event Action<object, Order> OnOrderIsReady;

        // Храним как в базе данных)
        // Ну хотя бы заказы, а не пользователей)
        public List<Order> orders;

        // Т.к. пиццерия наша, то пропишем в классе наши блюда
        public Dictionary<PizzasName, Pizza> PizzaName = new Dictionary<PizzasName, Pizza>
            {
                { PizzasName.Pepperoni, new Pizza("Pepperoni", 30) },
                { PizzasName.Venice, new Pizza("Venice", 50) },
                { PizzasName.FourSeason, new Pizza("FourSeason", 40) },
                { PizzasName.Hawaiian, new Pizza("Hawaiian", 90) }
            };

        public Pizzaria()
        {
            orders = new List<Order>();
        }

        public void CreateOrder(string nameWhoOrdered, PizzasName pizza)
        {
            orders.Add(
                new Order(PizzaName[pizza], nameWhoOrdered)
                );

            TaskOrderIsInProgress(orders[orders.Count - 1]);
        }

        public void CreateOrder(string nameWhoOrdered, IEnumerable<PizzasName> pizzas)
        {
            Order order = new Order(EnumPizzasToObjectsPizzas(pizzas), nameWhoOrdered);

            orders.Add(order);

            TaskOrderIsInProgress(orders[orders.Count - 1]);
        }

        public void AddPizzaToOrder(PizzasName pizza, int index)
        {
            orders[index].AddPizza(PizzaName[pizza]);
        }

        public void AddPizzasToOrder(IEnumerable<PizzasName> pizzas, int index)
        {
            orders[index].AddPizzas(EnumPizzasToObjectsPizzas(pizzas));
        }

        private List<Pizza> EnumPizzasToObjectsPizzas(IEnumerable<PizzasName> pizzasNames)
        {
            List<Pizza> pizzas = new List<Pizza>();
            foreach (var pizza in pizzasNames)
            {
                // В случае, если заказали несколько пицц, то мы добавим несколько
                pizzas.Add(
                    PizzaName.Where(x => x.Key == pizza).
                    Select(x => x.Value).
                    First()
                    );
            }

            return pizzas;
        }

        public IEnumerable<Pizza> GetPizzasFromOrder(int index)
        {
            return GetOrder(index).Pizzas;
        }

        public Order GetOrder(int index)
        {
            if (index - 1 < 0 || index > orders.Count)
            {
                throw new IndexOutOfRangeException("Не существует заказа с таким Id!");
            }

            return orders[index - 1];
        }

        public async Task TaskOrderIsInProgress(Order order)
        {
            // Якобы относим на кухню заказ)
            await Task.Delay(new Random().Next(4000, 7000));

            OnOrderInProgress?.Invoke(this, order);

            await TaskOrderIsReady(order);
        }

        public async Task TaskOrderIsReady(Order order)
        {
            // Якобы готовим)
            await Task.Delay(new Random().Next(2000, 5000));

            OnOrderIsReady.Invoke(this, order);
        }
    }



}
