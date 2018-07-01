using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using xml = System.Xml;

namespace XmlReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = from f in Directory.EnumerateFiles("d:\\data")
                        where f.EndsWith(".txt") || f.EndsWith(".xml")
                        select f;

            foreach (var file in files)
            {
                FileInfo fi = new FileInfo(file);
                switch (fi.Extension)
                {
                    case ".xml":
                        ReadXml(file);
                        break;
                    case ".txt":
                        ReadTxt(file);
                        break;
                }
            }

            string myFilePath = @"C:\data";
            string ext = Path.GetExtension(myFilePath);

            switch (ext)
            {
                case "xml":
                    ReadXml(myFilePath);
                    break;
            }

           
        }

        static void ReadTxt(string file)
        {
            StreamReader sr = null;

            try
            {
                sr = File.OpenText(file);
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

        static void ReadXml(string file)
        {
            var xml = XDocument.Load(file);

            var query = from c in xml.Root.Descendants("userstory")
                        select c.Element("content").Value;


            foreach (string name in query)
            {
                Console.WriteLine("Content of the user story is : {0}", name);
            }

            Console.ReadKey();
        }
    }
}
