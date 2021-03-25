using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //T_1_2_1_Averages();
            //T_1_2_2_Doubler();
            //T_1_2_3_Lowercase();
            //T_1_2_4_Validator();

            string currentCommand = "";
            while (currentCommand != "0")
            {
                Console.Clear();
                Console.WriteLine(
                    @"Введите комманду : 
    1: T_1_2_1_Averages
    2: T_1_2_2_Doubler
    3: T_1_2_3_Lowercase
    4: T_1_2_4_Validator
    0: Exit
"
);
                currentCommand = Console.ReadLine();
                switch (currentCommand)
                {
                    case "1":
                        T_1_2_1_Averages();
                        break;
                    case "2":
                        T_1_2_2_Doubler();
                        break;
                    case "3":
                        T_1_2_3_Lowercase();
                        break;
                    case "4":
                        T_1_2_4_Validator();
                        break;

                    case "0":
                        currentCommand = "0";
                        break;
                }
                Console.ReadKey();
            }

            Console.ReadKey();
        }


        /*
         * Напишите программу, которая определяет среднюю длину слова во введённой текстовой строке.
         * Учтите, что символы пунктуации на длину слов влиять не должны. 
         * Не стоит искать каждый символ-разделитель вручную: пожалейте своё время и используйте стандартные 
         * методы классов String и Char.
         * Регулярные выражения не использовать.
         * В случае дробного результата (х.5) – можете как оставить его таким, так и округлить. 
         * Стоит оставить комментарий в коде, указывающий, какое решение вы приняли.
         */
        static void T_1_2_1_Averages()
        {
            Console.Write("Введите строку : ");
            string str = Console.ReadLine();
            //string str = "Викентий хорошо отметил день рождения: " +
            //    "покушал пиццу, посмотрел кино, поиграл в приставку, " +
            //    "пообщался со студентами в чате. Что ещё нужно для жизни?";

            char[] splitters = new char[] { ';', ':', '!', ',', '.', '?', ' ' };
            string[] words = str.Trim().Split(splitters, StringSplitOptions.RemoveEmptyEntries);

            int charsCount = 0;
            foreach (var word in words)
            {
                charsCount += word.Length;
            }

            double averageWordLenght = (double)charsCount / words.Length;

            // Без округления
            Console.WriteLine($"Сумма символов = {charsCount}");
            Console.WriteLine($"Количество слов =  {words.Length}");
            Console.WriteLine($"Средняя длина слова {averageWordLenght} ");
        }

        /*
         * Напишите программу, которая удваивает в первой введённой строке 
         * все символы, принадлежащие второй введённой строке.
         */
        static void T_1_2_2_Doubler()
        {
            Console.WriteLine("Введите строку, в которой будут удваиваться символы : ");
            string strToDouble = Console.ReadLine();
            //string strToDouble = "написать программу, которая";

            Console.WriteLine("Введите строку, c помощью которой будут удваиваться символы : ");
            string strSymbolsDouble = Console.ReadLine();
            //string strSymbolsDouble = "описание";

            List<char> symbols = strSymbolsDouble.Select(x => x).Distinct().ToList();

            StringBuilder doubledString = new StringBuilder();
            for (int i = 0; i < strToDouble.Length; i++)
            {
                if (symbols.Contains(strToDouble[i]))
                {
                    doubledString.Append(strToDouble[i]);
                    doubledString.Append(strToDouble[i]);
                }
                else
                {
                    doubledString.Append(strToDouble[i]);
                }
            }

            Console.WriteLine(doubledString);
        }

        /*
         * Напишите программу, которая считает количество слов, начинающихся с маленькой буквы. 
         * Предлоги, союзы и междометия считаются словами. 
         * Финальную точку в предложении (как и любой другой знак) можно не учитывать.
         * Вариант без * - разделителем между словами считать ТОЛЬКО пробелы
         * Вариант со * - разделители между словами могут быть любые: запятые, двоеточия, точки с запятой.
         */
        static void T_1_2_3_Lowercase()
        {
            Console.WriteLine("Введите строку : ");
            string str = Console.ReadLine();
            //string str = "Антон хорошо начал утро: послушал Стинга, выпил кофе и посмотрел Звёздные Войны";
            string[] words = str.Trim().Split(new char[] { ';', ':', '!', ',', '.', '?', ' ' },
                                                        StringSplitOptions.RemoveEmptyEntries);
            int countLowerFirstChr = 0;
            foreach (var word in words)
            {
                if (Char.IsLower(word[0]) == true)
                    countLowerFirstChr += 1;
            }

            Console.WriteLine(countLowerFirstChr);
        }

        /*
         * Напишите программу, которая заменяет первую букву первого слова в предложении на заглавную.
         * В качестве окончания предложения можете считать только «.|?|!».
         * Многоточие и «?!» можете опустить.
         */
        static void T_1_2_4_Validator()
        {
            Console.WriteLine("Введите строку : ");
            string str = Console.ReadLine();
            //string str = "я плохо учил русский язык. " +
            //    "забываю начинать предложения с заглавной. хорошо, что можно написать программу!";

            string regex = "([ а-яА-Я,:;]+[.|!|?])";
            List<string> sentences = new List<string>();

            foreach (Match match in Regex.Matches(str, regex, RegexOptions.IgnoreCase))
            {
                sentences.Add(match.Value.Trim());
            }

            for (int i = 0; i< sentences.Count; ++i)
            {
                sentences[i] = Char.ToUpper(sentences[i][0]) + sentences[i].Substring(1);
            }

            Console.WriteLine(string.Join(" ", sentences));
        }
    }
}
