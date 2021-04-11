using System;

namespace CustomStrings
{
    public class CustomString
    {
        private char[] _custStr;

        public int Length { get { return _custStr.Length; } }

        #region Constructors
        public CustomString()
        {
            _custStr = new char[25];
        }

        public CustomString(int capacity)
        {
            _custStr = new char[capacity];
        }

        public CustomString(char[] array)
        {
            _custStr = new char[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                _custStr[i] = array[i];
            }
        }
        #endregion

        #region User functions
        public char FindFirstChar(char chr)
        {
            for (int i = 0; i < this.Length; i++)
            {
                if (_custStr[i] == chr)
                    return _custStr[i];
            }

            throw new InvalidOperationException("Введённый элемент не найден");
        }

        public char FindLastChar(char chr)
        {
            for (int i = this.Length - 1; i >= 0; i--)
            {
                if (_custStr[i] == chr)
                    return _custStr[i];
            }

            throw new InvalidOperationException("Введённый элемент не найден");
        }

        public CustomString Insert(int index, char chr)
        {
            if (index > _custStr.Length || index < 0)
            {
                throw new IndexOutOfRangeException("Неверный индекс строки");
            }
            else
            {
                char[] arr = new char[this.Length + 1];

                int currIndex = 0;
                for (int i = 0; i < arr.Length; i++, currIndex++)
                {
                    if (index == i)
                    {
                        arr[i] = chr;
                        --currIndex;
                    }
                    else
                    {
                        arr[i] = _custStr[currIndex];
                    }
                }

                return new CustomString(arr);
            }
        }

        public CustomString Remove(int index)
        {
            if (index > _custStr.Length || index < 0)
            {
                throw new IndexOutOfRangeException("Неверный индекс строки");
            }
            else
            {
                char[] arr = new char[this.Length - 1];

                int currIndex = 0;
                for (int i = 0; i < arr.Length; i++, currIndex++)
                {
                    if (index == i)
                    {
                        ++currIndex;

                        arr[i] = _custStr[currIndex];
                    }
                    else
                    {
                        arr[i] = _custStr[currIndex];
                    }
                }

                return new CustomString(arr);
            }
        }

        public char[] ToArray()
        {
            char[] toReturn = new char[_custStr.Length];

            for (int i = 0; i < _custStr.Length; i++)
            {
                toReturn[i] = _custStr[i];
            }

            return toReturn;
        }
        #endregion

        public char this[int index] {
            get {
                if (index > _custStr.Length || index < 0)
                    throw new IndexOutOfRangeException("Неверный индекс строки");
                else
                    return _custStr[index];
            }
            set {
                if (index > _custStr.Length || index < 0)
                    throw new IndexOutOfRangeException("Неверный индекс строки");
                else
                    _custStr[index] = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            CustomString custStr = obj as CustomString;
            if (custStr is null)
                return false;

            return this == custStr;
        }

        public override string ToString()
        {
            return new string(_custStr);
        }

        #region Operators
        private static bool IsEqual(CustomString str1, CustomString str2)
        {
            bool isEqual = true;

            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                    isEqual = false;
            }

            return isEqual;
        }

        public static bool operator ==(CustomString str1, CustomString str2)
        {
            if (str1.Length != str2.Length)
                return false;

            return IsEqual(str1, str2);
        }

        public static bool operator !=(CustomString str1, CustomString str2)
        {
            if (str1.Length != str2.Length)
                return true;

            return IsEqual(str1, str2) ? false : true;
        }

        public static CustomString operator +(CustomString str1, CustomString str2)
        {
            char[] tmpStr = new char[str1.Length + str2.Length];

            int currIndex = 0;

            for (int i = 0; i < str1.Length; i++, currIndex++)
            {
                tmpStr[currIndex] = str1[i];
            }

            for (int i = 0; i < str2.Length; i++, currIndex++)
            {
                tmpStr[currIndex] = str2[i];
            }

            return new CustomString(tmpStr);
        }
        #endregion
    }
}
