using System;

namespace sharp_lab1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("1.vigener");
                Console.WriteLine("2.fence");
                Console.WriteLine("3.columns");
                Console.WriteLine("0.exit");
                int a = int.Parse(Console.ReadLine());
                switch (a)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        Vigener.VigenerCode();
                        break;
                    case 2:
                        Fense.FenceCode();
                        break;
                    case 3:
                        Columns.ColumnsCode();
                        break;
                }
            }
        }
    }
}