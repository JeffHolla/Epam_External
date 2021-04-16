using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomDynamicArray
{
    public class DynamicArray<T> : IEnumerable, IEnumerable<T>, ICloneable
    {
        private T[] array;

        private int ActualNumberOfObjects { get; set; } = 0;

        public int Length { get => ActualNumberOfObjects; }
        public int Capacity
        {
            get
            {
                return array.Length;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Capacity не может быть меньше нуля!");
                }

                if (value != Capacity)
                {
                    SetCapacity(value);
                }
            }
        }

        public DynamicArray()
        {
            array = new T[8];
        }

        public DynamicArray(int capacity)
        {
            array = new T[capacity];
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            //array = new T[collection.Count()];
            array = collection.ToArray();

            ActualNumberOfObjects = array.Length;
        }

        public DynamicArray(T[] array)
        {
            this.array = new T[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                this.array[i] = array[i];
            }

            ActualNumberOfObjects = array.Length;
        }

        private void IncreaseCapacity(int multiplier = 2)
        {
            T[] newArray = new T[array.Length * multiplier];
            array.CopyTo(newArray, 0);

            array = newArray;

            Capacity = array.Length;
        }

        private void SetCapacity(int newCapacity)
        {

            T[] newArray = new T[newCapacity];

            if (newCapacity > Capacity)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    newArray[i] = array[i];
                }
            }
            else
            {
                for (int i = 0; i < newCapacity; i++)
                {
                    newArray[i] = array[i];
                }
            }

            array = newArray;

            Capacity = array.Length;

            if (newCapacity < ActualNumberOfObjects)
            {
                ActualNumberOfObjects = newCapacity;
            }

        }

        public void Add(T item)
        {
            if (ActualNumberOfObjects + 1 > Capacity)
            {
                IncreaseCapacity();
            }

            array[ActualNumberOfObjects] = item;

            ActualNumberOfObjects += 1;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (ActualNumberOfObjects + collection.Count() > Capacity)
            {
                int multiplier = (int)Math.Ceiling((double)(ActualNumberOfObjects + collection.Count()) / Capacity);
                IncreaseCapacity(multiplier);
            }


            // Я не уверен что быстрее и оптимальнее, но можно сделать довольно разными способами
            //IEnumerator<T> enumerator = collection.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    array[ActualNumberOfObjects] = enumerator.Current;

            //    ActualNumberOfObjects += 1;
            //}

            // Или лучше классический foreach?
            //foreach (var item in collection)
            //{
            //    array[ActualNumberOfObjects] = item;

            //    ActualNumberOfObjects += 1;
            //}

            // Не уверен, что это вообще оптимально.
            // Если и использовать кучу раз ToArray, а тем более Count от коллекции,
            // то может стоит создать переменную для collection типа T[]?
            collection.ToArray().CopyTo(array, ActualNumberOfObjects);
            ActualNumberOfObjects += collection.Count();
        }

        public bool RemoveAt(int index)
        {
            if (index > ActualNumberOfObjects - 1 || index < 0)
            {
                throw new IndexOutOfRangeException("Неверный индекс");
            }
            else
            {
                for (int i = index; i < ActualNumberOfObjects - 1; i++)
                {
                    array[i] = array[i + 1];
                }

                ActualNumberOfObjects -= 1;
                return true;
            }
        }

        public bool Remove(T value)
        {
            int indexOfValue = IndexOf(value);
            if (indexOfValue != -1)
            {
                for (int i = indexOfValue; i < ActualNumberOfObjects - 1; i++)
                {
                    array[i] = array[i + 1];
                }

                ActualNumberOfObjects -= 1;
                return true;
            }

            return false;
        }

        public int IndexOf(T value)
        {
            for (int i = 0; i < ActualNumberOfObjects; i++)
            {
                if (value.Equals(array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Insert(T value, int position)
        {
            if (position > ActualNumberOfObjects || position < 0)
            {
                throw new IndexOutOfRangeException("Неверный индекс");
            }
            else
            {
                if (ActualNumberOfObjects + 1 > Capacity)
                {
                    IncreaseCapacity();
                }

                if (ActualNumberOfObjects == position)
                {
                    array[position] = value;

                    ActualNumberOfObjects += 1;

                    return true;
                }

                for (int i = ActualNumberOfObjects; i > position; i--)
                {
                    array[i] = array[i - 1];
                }

                ActualNumberOfObjects += 1;

                array[position] = value;

                return true;
            }
        }

        public void PrintAll()
        {
            for (int i = 0; i < ActualNumberOfObjects; i++)
            {
                Console.WriteLine($"[{i}] = {array[i]}");
            }
            Console.WriteLine($"Capacity = {Capacity}, Length = {ActualNumberOfObjects}");
        }

        public T this[int index]
        {
            get
            {
                // Если значение меньше нуля или больше количества объектов, то бросаем exception
                if (index < -ActualNumberOfObjects || index > ActualNumberOfObjects)
                {
                    throw new IndexOutOfRangeException("Неверный индекс!");
                }
                else
                {
                    if (index < 0)
                    {
                        // + index т.к. index отрицательный
                        return array[ActualNumberOfObjects + index];
                    }
                    else
                    {
                        return array[index];
                    }
                }
            }

            set
            {
                // Если значение меньше нуля или больше количества объектов, то бросаем exception
                if (index < 0 || index > ActualNumberOfObjects)
                {
                    throw new IndexOutOfRangeException("Неверный индекс!");
                }
                else
                {
                    array[index] = value;
                }
            }
        }

        class DynamicArrayEnumerator : IEnumerator<T>
        {
            T[] array;
            int length;
            int position = -1;

            public DynamicArrayEnumerator(T[] array, int length)
            {
                this.array = array;
                this.length = length;
            }

            public T Current {
                get
                {
                    if (position == -1)
                    {
                        throw new InvalidOperationException("Current position of Enumerator = -1!");
                    }
                    return array[position];
                }
            }

            object IEnumerator.Current => throw new NotImplementedException();

            public bool MoveNext()
            {
                if (position < length - 1)
                {
                    position += 1;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                position = -1;
            }

            public void Dispose() { }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DynamicArrayEnumerator(array, ActualNumberOfObjects);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DynamicArrayEnumerator(array, ActualNumberOfObjects);
        }

        public object Clone()
        {
            return new DynamicArray<T>(array);
        }

        public T[] ToArray()
        {
            T[] newArray = new T[ActualNumberOfObjects];
            for (int i = 0; i < ActualNumberOfObjects; i++)
            {
                newArray[i] = array[i];
            }

            return newArray;
        }
    }
}
