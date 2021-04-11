using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_3_1
{
    // Круговой List сделан на основе обычного листа
    // Можно сделать на основе Node и ссылок, но я не уверен что было бы лучше для задания
    public class CircleList<T>
    {
        private List<T> objects;//= new List<T>();

        public int Length { get => objects.Count; } 

        public CircleList(IEnumerable<T> list)
        {
            objects = new List<T>(list);
        }

        public void Add(T obj)
        {
            objects.Add(obj);
        }

        public void RemoveAt(int index)
        {
            var realIndex = this[index];
            objects.RemoveAt(realIndex);
        }

        public void PrintAll()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                Console.WriteLine(objects[i]);
            }
        }

        public IEnumerable<T> GetAll()
        {
            return objects;
        }

        // Круговая реализация
        private int this[int index] {
            get
            {
                // Если значение меньше нуля, то бросаем exception
                if (index <= 0)
                {
                    throw new IndexOutOfRangeException("Не существует индекса со значение 0!");
                }
                else
                {
                    // Если же больше, чем длина, то 
                    if (index <= Length)
                    {
                        return index - 1;
                    }
                    else if (index % Length == 0)
                    {
                        return Length - 1;
                    }
                    else
                    {
                        return (index % Length) - 1;
                    }
                }
            }
        }
    }
}
