﻿using System;
using System.IO;

namespace AdditionalCode
{
    class Program
    {
        static void Main(string[] args)
        {
            String input, input1, output;
            
            Console.WriteLine("Введите числа в двоичной системе счисления: ");
            input = Console.ReadLine();
            input1 = Console.ReadLine();
            input = Conversion(input);
            input1 = Conversion(input1);
            output = GetSum(input, input1);
            if (!positiveResult(GetNormalNumber(input), GetNormalNumber(input1))) output = Inverse(output);
            Console.Write(output);
            Console.ReadLine();
        }

        static String Conversion(String input)
        {
            bool minus = input[0] == '1';
            int d = 0;
            if (minus) input = input.Substring(1, input.Length - 1);

            input = GetNormalNumber(input);

            if (minus)
            {
                input = Inverse(input);
                input = GetSum(input, "00000001");
            }

            return input;
        }

        static String GetNormalNumber(String input)
        {
            int d = input[0] == '-' ? 1 : 0;
            if (input.Length > 8 + d) input = input.Substring(0, 8 + d);
            else
            {
                String zeros = "";
                while ((zeros + input).Length < 8 + d)
                {
                    zeros += "0";
                }
                input = input.Insert(d, zeros);
            }
            return input;
        }

        static String Inverse(String input)
        {
            String output = "";
            for (int i = 0; i < input.Length; i++)
            {
                output += input[i] == '0' ? '1' : '0';
            }
            return output;
        }

        static String GetSum(String n1, String n2)
        {
            String output = "";
            bool translation = false;
            for (int i = 7; i >= 0; i--)
            {
                bool a = n1[i] == '1';
                bool b = n2[i] == '1';

                String temp = "0";

                if (translation)
                {
                    translation = a || b;
                    if (a == b) temp = "1";
                }
                else
                {
                    translation = a && b;
                    if (a != b) temp = "1";
                }
                output = output.Insert(0, temp);
            }
            return output;
        }

        static bool positiveResult(String n1, String n2)
        {
            bool m1 = n1[0] == '-';
            bool m2 = n2[0] == '-';
            if (!(m1 || m2)) return true;
            if (m1 && m2) return false;
            else
            {
                if (m1) n1 = n1.Substring(1, 8);
                if (m2) n2 = n2.Substring(1, 8);

                for (int i = 0; i < 8; i++)
                {
                    if (n1[i] == n2[i]) continue;
                    else if (n1[i] == '1' && !m1) return true;
                    else if (n2[i] == '1' && !m2) return true;
                }
                return false;
            }
        }
    }
}