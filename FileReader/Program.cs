using System;
using System.IO;

namespace FileReader
{
    class Program
    {
        public static void read(string path)
        {
            StreamReader sr = null;

            try
            {
                sr = File.OpenText(path);
                Console.WriteLine(sr.ReadToEnd());
            }
            catch (FileNotFoundException notFount)
            {
                Console.WriteLine(notFount.Message);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }
        static void Main(string[] args)
        {
            read(@"c:\data.txt");
            Console.ReadLine();
        }
    }
}
