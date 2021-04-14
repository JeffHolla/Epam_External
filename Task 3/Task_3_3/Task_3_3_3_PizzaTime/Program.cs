using System;
using System.Collections.Generic;
using System.Text;

namespace Task_3_3_3_PizzaTime
{
    class Program
    {
        public static void Main(string[] args)
        {
            WorkingConsole show = new WorkingConsole();

            show.StartShow();
        }
    }

    public class WorkingConsole
    {
        Pizzaria pizzaria;
        public WorkingConsole()
        {
            pizzaria = new Pizzaria();

            pizzaria.OnOrderInProgress += OnOrderIsInProgress;
            pizzaria.OnOrderIsReady += OnOrderIsReady;
        }

        public void StartShow()
        {
            List<PizzasName> orderMany = new List<PizzasName>()
            {
                PizzasName.Pepperoni,
                PizzasName.Venice
            };

            pizzaria.CreateOrder("Dmitriy", orderMany);
            pizzaria.CreateOrder("NotDmitriy", PizzasName.Hawaiian);

            Console.ReadKey();
        }
        
        public void OnOrderIsInProgress(object sender, Order order)
        {
            Console.WriteLine($"Order number {order.Id} is on kitchen for {order.WhoOrdered}!");
        }

        public void OnOrderIsReady(object sender, Order order)
        {
            Console.WriteLine($"Order number {order.Id} is cooked for {order.WhoOrdered}!");
        }
    }


}
