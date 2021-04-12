﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomDynamicArray
{
    public class DynamicArray<T> : IEnumerable, IEnumerable<T>
    {
        private T[] array;

        private int ActualNumberOfObjects { get; set; } = 0;

        public int Length { get => ActualNumberOfObjects; }
        public int Capacity { get => array.Length; set { } }

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
        }

        private void IncreaseCapacity(int multiplier = 2)
        {
            T[] newArray = new T[array.Length * multiplier];
            array.CopyTo(newArray, 0);

            array = newArray;

            Capacity = array.Length;
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
            Console.WriteLine($"Capacity = {Capacity}, Lenght = {ActualNumberOfObjects}");
        }

        public T this[int index] {
            get
            {
                // Если значение меньше нуля или больше количества объектов, то бросаем exception
                if (index < 0 || index > ActualNumberOfObjects)
                {
                    throw new IndexOutOfRangeException("Lol, you died!");
                }
                else
                {
                    return array[index];
                }
            }

            set
            {
                // Если значение меньше нуля или больше количества объектов, то бросаем exception
                if (index < 0 || index > ActualNumberOfObjects)
                {
                    throw new IndexOutOfRangeException("Lol, you died!");
                }
                else
                {
                    array[index] = value;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)array).GetEnumerator();
        }



        //public char[] ToArray()
        //{
        //    char[] toReturn = new char[array.Length];

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        toReturn[i] = array[i];
        //    }

        //    return toReturn;
        //}
        //#endregion

        //public char this[int index] {
        //    get
        //    {
        //        if (index > array.Length || index < 0)
        //            throw new IndexOutOfRangeException("Неверный индекс строки");
        //        else
        //            return array[index];
        //    }
        //    set
        //    {
        //        if (index > array.Length || index < 0)
        //            throw new IndexOutOfRangeException("Неверный индекс строки");
        //        else
        //            array[index] = value;
        //    }
        //}

        //public override bool Equals(object obj)
        //{
        //    if (obj == null)
        //        return false;

        //    CustomString custStr = obj as CustomString;
        //    if (custStr is null)
        //        return false;

        //    return this == custStr;
        //}

        //public override string ToString()
        //{
        //    return new string(array);
        //}

        //#region Operators
        //private static bool IsEqual(CustomString str1, CustomString str2)
        //{
        //    bool isEqual = true;

        //    for (int i = 0; i < str1.Length; i++)
        //    {
        //        if (str1[i] != str2[i])
        //            isEqual = false;
        //    }

        //    return isEqual;
        //}

        //public static bool operator ==(CustomString str1, CustomString str2)
        //{
        //    if (str1.Length != str2.Length)
        //        return false;

        //    return IsEqual(str1, str2);
        //}

        //public static bool operator !=(CustomString str1, CustomString str2)
        //{
        //    if (str1.Length != str2.Length)
        //        return true;

        //    return IsEqual(str1, str2) ? false : true;
        //}

        //public static CustomString operator +(CustomString str1, CustomString str2)
        //{
        //    char[] tmpStr = new char[str1.Length + str2.Length];

        //    int currIndex = 0;

        //    for (int i = 0; i < str1.Length; i++, currIndex++)
        //    {
        //        tmpStr[currIndex] = str1[i];
        //    }

        //    for (int i = 0; i < str2.Length; i++, currIndex++)
        //    {
        //        tmpStr[currIndex] = str2[i];
        //    }

        //    return new CustomString(tmpStr);
        //}
    }
}
