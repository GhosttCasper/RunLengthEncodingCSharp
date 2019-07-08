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
            StringBuilder number = new StringBuilder();

            int sum = 0;
            bool isPrevSymbolInt = false;

            foreach (char symbol in text)
            {
                if (char.IsUpper(symbol))
                {
                    sum++;
                    if (isPrevSymbolInt)
                    {
                        AddCount(ref sum, ref number);
                    }
                    isPrevSymbolInt = false;
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
                AddCount(ref sum, ref number);
            }

            Console.WriteLine(sum);
        }

        private static void AddCount(ref int sum, ref StringBuilder number)
        {
            sum += int.Parse(number.ToString());
            sum -= 1;
            number.Clear();
        }
    }
}
