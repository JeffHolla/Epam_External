using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/* Задание
 * К вам пришёл редактор модного журнала. Ему очень нужна программа, которую он описал ниже.
 * 
 * Задан английский текст. Ваша задача понять, какие слова автор «любит» больше всего и
 * подловить его на однообразности речи. Или, наоборот, похвалить за разнообразие.
 * 
 * Для каждого слова в тексте указать, сколько раз оно встречается.
 * 
 * Подумайте, имеет ли значение регистр, какие разделители могут использоваться в тексте.
 * Попробуйте использовать свои наработки из Task 1.2. «String, not Sting».
 * 
 * Ввод и вывод также придумайте сами. В рамках консоли постарайтесь создать приятный и
 * понятный интерфейс – вашей программой будет пользоваться профессионал журналистики.
 */

namespace Task_3_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Два тестовых текста

            string text = "Этот текст был выдуман только для задания и только для того, чтобы проверить " +
                "сколько раз встречаются разные слова. Допустим, что этот текст попадёт в журналистику, " +
                "тогда этот текст там будет воспринят не сильно хорошо. Для задания нужны повторяющиеся слова," +
                " но откуда их взять? Текст есть текст! После тестов выяснилось, что нужно больше слов. Эх! " +
                "А ещё я просто очень плохо выдумываю всякие такие вещи, поэтому напишу ещё пару раз текст, чтобы выглядело" +
                "чуть больше. С другой строны, возможно, что этот текст ещё и читать кто-нибудь будет. Спойлер - нинада! :) " +
                "А ещё этот смайлик сотрётся делиторами, или проще говоря, в сплите. Ах да, я ещё и не заметил, что текст" +
                "должен быть на английском. Он должен быть явно проще.";

            string englishText = @"The editor of a fashion magazine has come to you. He really needs the program, which he described below.
English text set.Your task is to understand which words the author ""loves"" the most and
catch him on the monotony of speech. Or, conversely, praise the diversity.
For each word in the text, indicate how many times it occurs.
Consider whether the case matters, what are the message delimiters in the text.
Try using your work from Task 1.2. ""A thread, not a sting.""
Input and output also come up with yourself. Within the console, try to create a pleasant and
understandable - your program will be used by a professional journalism. ";


            TextAnalizatorHandler.Start(englishText);
        }
    }
}
