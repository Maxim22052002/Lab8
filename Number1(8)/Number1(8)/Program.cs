using System;
using System.IO;
namespace Number1_8_
{
    class Program
    {
        static int[] GetArray()
        {
            FileStream getfile = new FileStream(@"C:\Users\USER\Desktop\Lab7\Number2(7)\sorted.dat", FileMode.Open);
            BinaryReader binaryReader = new BinaryReader(getfile);
            int[] array = new int[100000];
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = binaryReader.ReadInt32();
            }
            return array;
        }
        static void Main(string[] args)
        {
            int[] mas = GetArray();
            Console.WriteLine("Выберите метод поиска:\n1)Линейный\n2)Бинарный\n3)Интерполяционный\n4)Выход");
            string getansw = Console.ReadLine();
            bool mark = true;
            while (mark)
            {
                switch (getansw)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Введите элемент");
                        int index = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        Console.Clear();
                        TestingLinearSearch(mas, index, 0);
                        Console.ReadKey();
                        Console.Clear();
                        mark = false;
                        break;
                    case "2":
                        Console.Clear();
                        Console.Clear();
                        Console.WriteLine("Введите элемент: ");
                        int position = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        TestingBinarySearch(mas, position, 0);
                        Console.ReadKey();
                        Console.Clear();
                        mark = false;
                        break;
                    case "3":
                        Console.Clear();
                        Console.Clear();
                        Console.WriteLine("Введите элемент: ");
                        int pos = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        TestingInterpolationSearch(mas, pos, 0);
                        Console.ReadKey();
                        Console.Clear();
                        mark = false;
                        break;
                    case "4":
                        mark = false;
                        Console.Clear();
                        break;
                }
            }

        }
        static int LinearSearch(int [] array,int position,out int c)
        {
            int counter = 0;
            int index = 0;
            bool mark = false;
            for(int i = 0; i < array.Length; i++)
            {
                counter++;
                if (array[i] == position)
                {
                    mark = true;
                    index = i;
                    break;
                }
            }
            c = counter;
            if (mark)
            {
                return index;
            }
            else
            {
                return -1;
            }
        }
        static void TestingLinearSearch(int[] array, int position,int c)
        {
            DateTime timest = DateTime.Now;
            int index = LinearSearch(array, position, out c);
            DateTime timeend = DateTime.Now;
            TimeSpan testing = timeend.Subtract(timest);
            if (index != -1)
            {
                Console.WriteLine("Позиция(индекс) искомого элемента: "+index);
            }
            else
            {
                Console.WriteLine("Не найдено!");
            }
            Console.WriteLine("Количество сравнений: "+ c);
            Console.WriteLine("Время работы: "+ testing.Seconds+":"+testing.Milliseconds);
        }
        static int BinarySearch (int [] array,int position,out int c)
        {
            int counter = 0;
            int leftpos = 0;
            int rightpos = array.Length - 1;
            while (rightpos >= leftpos)
            {
                int middlepos = (leftpos + rightpos) / 2;
                counter++;
                if (array[middlepos] == position)
                {
                    c = counter;
                    return middlepos;
                }
                counter++;
                if (array[middlepos] > position)
                {
                    rightpos = middlepos - 1;
                }
                else
                {
                    leftpos = middlepos + 1;
                }
            }
            c = counter;
            return -1;
        }
        static void TestingBinarySearch(int[] array, int position, int c)
        {
            DateTime timest = DateTime.Now;
            int index = BinarySearch(array, position, out c);
            DateTime timeend = DateTime.Now;
            TimeSpan testing = timeend.Subtract(timest);
            if (index != -1)
            {
                Console.WriteLine("Позиция(индекс) искомого элемента: " + index);
            }
            else
            {
                Console.WriteLine("Не найдено!");
            }
            Console.WriteLine("Количество сравнений: " + c);
            Console.WriteLine("Время работы: " + testing.Seconds + ":" + testing.Milliseconds);
        }
        static int InterpolationSearch(int[] array, int elem,out int c)
        {
            int counter = 0;
            int low = 0;
            int mid;
            int high = array.Length - 1;
            try
            {
                while (low <= high)
                {
                    mid=low+(elem-array[low])*(high-low)/(array[high]-array[low]);
                    counter++;
                    if (array[mid] < elem)
                    {
                        low = mid + 1;
                    }
                    else if (array[mid] > elem)
                    {
                        high = mid - 1;
                    }
                    else if (elem == array[mid])
                    {
                        c = counter;
                        return mid;
                    }
                }
                c = counter;
                return -1;
            }
            catch
            {
                c = counter;
                return -1;
            }
        }
        static void TestingInterpolationSearch(int[] array, int position, int c)
        {
            DateTime timest = DateTime.Now;
            int index = InterpolationSearch(array, position, out c);
            DateTime timeend = DateTime.Now;
            TimeSpan testing = timeend.Subtract(timest);
            if (index != -1)
            {
                Console.WriteLine("Позиция(индекс) искомого элемента: " + index);
            }
            else
            {
                Console.WriteLine("Не найдено!");
            }
            Console.WriteLine("Количество сравнений: " + c);
            Console.WriteLine("Время работы: " + testing.Seconds + ":" + testing.Milliseconds);
        }
    }
}
