using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp7
{
    internal class Program
    {

        static readonly string Path = String.Format(Directory.GetCurrentDirectory() + @"\" + "Plb.txt");

        static void Main(string[] args)
        {
        start:
            Console.WriteLine("Шифруем: 0 \n" +
                "Дешифруем: 1 \n\n" +
                "############ \n\n" +
                "0/1?");

            string answ = Console.ReadLine();

            if (answ == "0")
            {
                Console.Clear();
                Console.WriteLine("Введите текст");
                Cypher(Console.ReadLine().ToUpper());
            }
            else if (answ == "1")
            {
                Console.Clear();
                Console.WriteLine("Укажите путь");
                Decypher(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Некорректный ввод");
                goto start;
            }
        }


        static void Cypher(string inp)
        {
            #region Шифр
            char[,] plb = new char[5, 5];
            string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            int count = 0;

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    plb[i, j] = (alphabet[count]);
                    count++;
                }
            #endregion

            string outp = string.Empty;
            List<char> tmp = new();

            foreach (char c in inp)
            {
                count = 0;

                while (true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        tmp.Add((char)plb.GetValue(count, i));
                    }

                    if (tmp.Contains(c))
                        break;
                    count++;
                    tmp.Clear();
                }

                outp += $"{count + 1},{tmp.IndexOf(c) + 1}\t";
                count = 0;
                tmp.Clear();
            }

            Console.WriteLine(outp);
            try
            {
                FileInfo fi1 = new FileInfo(Path);

                if (!fi1.Exists)
                {
                    using (StreamWriter sw = fi1.CreateText())
                        sw.WriteLine(outp);
                    Console.WriteLine($"Сохранено в {Path}");
                }
                else
                {
                    fi1.Delete();
                    using (StreamWriter sw = fi1.CreateText())
                        sw.WriteLine(outp);
                    Console.WriteLine($"{Path} перезаписан");
                }
                
            }
            catch (Exception ex)
            { Console.WriteLine(ex.ToString()); }
        }

        static void Decypher(string path)
        {
            #region Шифр
            char[,] plb = new char[5, 5];
            string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            int count = 0;

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    plb[i, j] = (alphabet[count]);
                    count++;
                }
            #endregion

            string outp = string.Empty;
            int x, y;

            FileInfo fi1 = new FileInfo(path);
            using (StreamReader sr = fi1.OpenText())
            {
                count = 0;
                string temp = sr.ReadLine();
                foreach (char c in temp)
                {
                    if (char.IsDigit(c) & temp[count + 1] == ',')
                    {
                        //x = Convert.ToInt32(c);
                        //y = Convert.ToInt32(temp[count + 2]);
                        outp += plb[c - 1, temp[count + 2] - 1];
                    }
                    count++;
                }
                Console.WriteLine(outp);
            }            

        }
    }
}
