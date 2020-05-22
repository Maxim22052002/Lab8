using System;
using System.IO;

namespace Number2_8_
{
    class Program
    {
        static void Main(string[] args)
        {
            bool mark = true;
            while (mark)
            {
                Console.WriteLine("Выберите алгоритм поиска:\n1)Простой поиск подстроки\n2)Поиск Кнута-Морриса-Пратта\n3)Поиск Бойера-Мура\n4)Выход");
                string givansw = Console.ReadLine();
                switch (givansw)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Введите строку, в которой будем искать подстроку(образ)");
                        string text1= Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Введите подстроку(образ) для поиска");
                        string pattern1 = Console.ReadLine();
                        Console.Clear();
                        TestSimpleSearch(text1, pattern1, 0);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Введите строку, в которой будем искать подстроку(образ)");
                        string text2 = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Введите подстроку(образ) для поиска");
                        string pattern2 = Console.ReadLine();
                        Console.Clear();
                        TestKnutMorrisPrattSearch(text2, pattern2, 0);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Введите строку, в которой будем искать подстроку(образ)");
                        string text3 = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Введите подстроку(образ) для поиска");
                        string pattern3 = Console.ReadLine();
                        Console.Clear();
                        TestBMSearch(text3, pattern3, 0);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        mark = false;
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }
        static int[] Prefix(string Findstr)
        {
            int length = Findstr.Length;
            int[] prefix = new int[length];
            int j = 0;
            prefix[0] = 0;
            for (int i = 1; i < length; i++)
            {
                while (j > 0 && Findstr[j] != Findstr[i])
                {
                    j = prefix[j];
                }
                if (Findstr[j] == Findstr[i])
                {
                    j++;
                }
                prefix[i] = j;
            }
            return prefix;
        }
        static int KnutMorrisPrattSearch(string text, string pattern, out int c)
        {
            int t = text.Length;
            int p = pattern.Length;
            int counter = 0;
            int[] prefix = Prefix(pattern);
            int j = 0;
            try
            {
                for (int i = 1; j <= t; i++)
                {
                    while (j > 0 && pattern[j] != text[i - 1])
                    {
                        j = prefix[j - 1];
                    }
                    counter++;
                    if (pattern[j] == text[i - 1])
                    {
                        j++;
                    }
                    counter++;
                    if (j == p)
                    {
                        c = counter;
                        return i - p;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                c = counter;
                return -1;
            }
            c = counter;
            return -1;
        }
        static int[] BadCharactersTable(string Findstr)
        {
            int f = Findstr.Length;
            int[] badShift = new int[256];
            for (int i = 0; i < badShift.Length; i++)
            {
                badShift[i] = -1;
            }
            for (int i = 0; i < f - 1; i++)
            {
                badShift[(int)Findstr[i]] = i;
            }
            return badShift;
        }
        static int[] GoodSuffixTable(string Findstr)
        {
            int F = Findstr.Length;
            int[] suffixes = Suffixes(Findstr);
            int[] goodSuffixes = new int[F];
            for (int i = 0; i < goodSuffixes.Length; i++)
            {
                goodSuffixes[i] = F;
            }
            for (int i = F - 1; i >= 0; i--)
            {
                if (suffixes[i] == i + 1)
                {
                    for (int j = 0; j < F - i - 1; j++)
                    {
                        if (goodSuffixes[j] == F)
                        {
                            goodSuffixes[j] = F - i - 1;
                        }
                    }
                }
            }
            for (int i = 0; i < F - 2; i++)
            {
                goodSuffixes[F - 1 - suffixes[i]] = F - i - 1;
            }
            return goodSuffixes;
        }
        static int[] Suffixes(string Findstr)
        {
            int F = Findstr.Length;
            int[] suffixes = new int[F];
            suffixes[F - 1] = F;
            int f = 0, g = F - 1;
            for (int i = F - 2; i >= 0; i--)//
            {
                if (i > g && suffixes[i + F - 1 - f] < i - g)
                {
                    suffixes[i] = suffixes[i + F - 1 - f];
                }
                else if (i < g)
                {
                    g = i;
                }
                f = i;
                while (g >= 0 && Findstr[g] == Findstr[g + F - 1 - f])
                {
                    g--;
                }
                suffixes[i] = f - g;
            }
            return suffixes;
        }
        static int BMSearch(string text, string pattern, out int c)
        {
            int t = text.Length;
            int p = pattern.Length;
            int counter = 0;
            if (p > t)
            {
                c = counter;
                return -1;
            }
            int[] badcharacters = BadCharactersTable(pattern);
            int[] goodsuffix = GoodSuffixTable(pattern);
            int position = 0;
            while (position <= t - p)
            {
                int i;
                for (i = p - 1; i >= 0 && pattern[i] == text[i + position]; i--) ;
                counter++;
                if (i < 0)
                {
                    c = counter;
                    return position;
                }
                position += Math.Max(i - badcharacters[(int)text[position + i]], goodsuffix[i]);
            }
            c = counter;
            return -1;
        }
        static void TestKnutMorrisPrattSearch(string text, string Findstr, int counter)
        {
            DateTime starttime = DateTime.Now;
            int position = KnutMorrisPrattSearch(text, Findstr, out counter);
            DateTime end = DateTime.Now;
            TimeSpan testing = end.Subtract(starttime);
            if (position != -1)
            {
                Console.WriteLine("Индекс вхождения: " + position);
            }
            else
            {
                Console.WriteLine("Не найдено");
            }
            Console.WriteLine("Количество сравнений: " + counter);
            Console.WriteLine($"Время работы: " + testing.Seconds + ":" + testing.Milliseconds);
        }
        static void TestBMSearch(string text, string Findstr, int counter)
        {
            DateTime starttime = DateTime.Now;
            int position = BMSearch(text, Findstr, out counter);
            DateTime endtime = DateTime.Now;
            TimeSpan testing = endtime.Subtract(starttime);
            if (position != -1)
            {
                Console.WriteLine("Индекс вхождения: " + position);
            }
            else
            {
                Console.WriteLine("Не найдено");
            }
            Console.WriteLine("Количество сравнений: " + counter);
            Console.WriteLine($"Время работы: " + testing.Seconds + ":" + testing.Milliseconds);
        }
        
        static int SimpleSearch(string text, string pattern, out int c)
        {
            int t = text.Length;
            int p = pattern.Length;
            int counter = 0;
            int position = -1;
            while (position < t - p)
            {
                counter++;
                position++;
                int j = 0;
                while (j < p && text[position + j] == pattern[j])
                {
                    j++;
                }
                if (j == p)
                {
                    c = counter;
                    return position;
                }
            }
            c = counter;
            return -1;
        }
        static void TestSimpleSearch(string text, string pattern, int counter)
        {
            DateTime starttime = DateTime.Now;
            int position = SimpleSearch(text, pattern, out counter);
            DateTime endtime = DateTime.Now;
            TimeSpan testing = endtime.Subtract(starttime);
            if (position != -1)
            {
                Console.WriteLine("Индекс вхождения: " + position);
            }
            else
            {
                Console.WriteLine("Не найдено");
            }
            Console.WriteLine("Количество сравнений: " + counter);
            Console.WriteLine($"Время работы: " + testing.Seconds + ":" + testing.Milliseconds);
        }
    }
}
