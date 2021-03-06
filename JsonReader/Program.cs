﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JsonReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = from f in Directory.EnumerateFiles("d:\\data")
                        where f.EndsWith(".txt") || f.EndsWith(".xml") || f.EndsWith(".json")
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
                    case ".json":
                        ReadJson(file);
                        break;
                }
            }
            Console.ReadKey();

        }

        static void ReadTxt(string file)
        {
            try
            {
                foreach (var line in Dycrept.TxtDycrept(File.ReadLines(file).Reverse()))
                {
                    Console.WriteLine(line);
                }

            }
            catch (FileNotFoundException notFount)
            {
                Console.WriteLine(notFount.Message);
            }
        }

        static void ReadXml(string file)
        {
            var xml = XDocument.Load(file);

            var query = from c in xml.Root.Descendants("userstory")
                        select c.Element("content").Value;


            foreach (string name in Dycrept.TxtDycrept(query))
            {
                Console.WriteLine("[XML] Content of the user story is : {0}", name);
            }

            Console.ReadKey();
        }

        static void ReadJson(string file)
        {
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(file))
            {
                var json = reader.ReadToEnd();
                var jsonObject = JObject.Parse(json);
                foreach (var item in jsonObject.Properties())
                {
                    item.Value = item.Value.ToString();
                }
                result = jsonObject.ToString();
                Console.WriteLine("[Json] Content of the user story is : {0}",result);
            }
        }
    }
    public static class Dycrept
    {
        public static IEnumerable<string> TxtDycrept(this IEnumerable<string> file)
        {
            return file.Reverse();
        }
    }
}
