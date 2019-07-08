using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Кодирование длин серий (RLE) — алгоритм сжатия данных, заменяющий повторяющиеся символы на один символ и число его повторов.
 * Серией называется последовательность, состоящая из нескольких одинаковых символов (более одного).
 * При кодировании строка одинаковых символов, составляющих серию, заменяется строкой,
 * содержащей сам повторяющийся символ и количество его повторов.

Например, строка AAAABBB будет сжата в строку A4B3, 
а строка AAAAAAAAAAAAAAABAAAAA — в строку A15BA5.

Вам дана сжатая строка, найдите длину исходной строки. 
Длина исходной строки не превосходит 1000 символов, 
все символы исходной строки заглавные большие буквы латинского алфавита.

Формат ввода
В единственной строке входных данных содержится непустая строка s. 
Гарантируется, что s результат корректного сжатия некоторой строки.

Формат вывода
Выведите длину исходной строки.

A15BA5
21

ABCDR
5

Z123XY
125
 */

namespace RunLengthEncodingCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = Console.ReadLine();
            var encodedStr = Encode(text);
            Console.WriteLine(encodedStr);

            var decodedStr = Decode(text, out int sum);
            Console.WriteLine(sum);
            Console.WriteLine(decodedStr);
        }

        public static StringBuilder Encode(String text)
        {
            StringBuilder encodedStr = new StringBuilder();

            int count = 0;

            char currentSybmol = text[0];

            foreach (char symbol in text)
            {
                if (symbol == currentSybmol)
                    count++;
                else
                {
                    AddSymbols(ref encodedStr, currentSybmol, count);
                    currentSybmol = symbol;
                    count = 1;
                }
            }

            AddSymbols(ref encodedStr, currentSybmol, count);

            return encodedStr;
        }

        private static void AddSymbols(ref StringBuilder encodedStr, char currentSybmol, int count)
        {
            encodedStr.Append(currentSybmol);
            if (count > 1)
                encodedStr.Append(count);
        }

        public static StringBuilder Decode(String text, out int sum)
        {
            StringBuilder decodedStr = new StringBuilder();
            StringBuilder number = new StringBuilder();
            sum = 0;
            bool isPrevSymbolInt = false;
            char previousLetter = ' ';

            foreach (char symbol in text)
            {
                if (char.IsUpper(symbol))
                {
                    if (isPrevSymbolInt)
                    {
                        AddSymbols(ref sum, ref number, previousLetter, ref decodedStr);
                    }
                    sum++;
                    decodedStr.Append(symbol);
                    isPrevSymbolInt = false;
                    previousLetter = symbol;
                }
                else
                {
                    if (char.IsDigit(symbol))
                    {
                        number.Append(symbol);
                        isPrevSymbolInt = true;
                    }
                }
            }

            if (isPrevSymbolInt)
            {
                AddSymbols(ref sum, ref number, previousLetter, ref decodedStr);
            }

            return decodedStr;
        }

        private static void AddSymbols(ref int sum, ref StringBuilder number, char symbol, ref StringBuilder decodedStr)
        {
            int addend = int.Parse(number.ToString()) - 1;
            sum += addend;
            for (int i = 0; i < addend; i++)
                decodedStr.Append(symbol);
            number.Clear();
        }
    }
}
