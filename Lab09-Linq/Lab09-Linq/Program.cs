using Lab09_Linq.Class;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft;
using Newtonsoft.Json;
using System.Linq;

namespace Lab09_Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"../../../data.json";

            
            string jsonString = File.ReadAllText(path);
            Data data = JsonConvert.DeserializeObject<Data>(jsonString);
            Console.WriteLine("");
            Console.WriteLine("---------------------All-----------------------------");
            Console.WriteLine("");
            OutputAll(data);
            Console.WriteLine("");
            Console.WriteLine("---------------------No Names filtered out-----------------------------");
            Console.WriteLine("");

            FilterNoNames(data);

            Console.WriteLine("");
            Console.WriteLine("---------------------No Names/Duplicates filtered out-----------------------------");
            Console.WriteLine("");
            FilterDuplicates(data);

            Console.WriteLine("");
            Console.WriteLine("---------------------No Names filtered out-----------------------------");
            Console.WriteLine("");
            FilterBlanks(data);

            Console.WriteLine("");
            Console.WriteLine("---------------------No Names/Dupes filtered out-----------------------------");
            Console.WriteLine("");
            FilterDuplicates2(data);


        }
        /// <summary>
        /// Outputs every Neighborhood from the Json file to the console
        /// </summary>
        /// <param name="data">instance of the Object of the json file</param>
        public static void OutputAll(Data data)
        {
            int count = 1;
            foreach (Feature feature in data.Features)
            {
                Console.WriteLine($"{count}. {feature.Properties.Neighborhood}");
                count++;

            }
        }
        /// <summary>
        /// Filters all blanks out and only displays neighborhoods that have names
        /// </summary>
        /// <param name="data">instance of the Object of the json file</param>
        public static void FilterNoNames(Data data)
        {
            var result = from Feature feature in data.Features 
                         where feature.Properties.Neighborhood != "" 
                         select feature.Properties.Neighborhood;
            int count = 1;
            foreach (string neighborhood in result)
            {
                Console.WriteLine($"{count}. {neighborhood}");
                count++;

            }
        }
        /// <summary>
        /// Filters out the no name neighborhoods using an if statement vs LINQ and displays them to the console
        /// </summary>
        /// <param name="data">instance of the Object of the json file</param>
        public static void FilterBlanks(Data data)
        {
            int count = 1;
            foreach(Feature feature in data.Features)
            {

            if(feature.Properties.Neighborhood != "")
                {
                    Console.WriteLine($"{count}. {feature.Properties.Neighborhood}");
                    count++;
                }

            }
        }
        /// <summary>
        /// Filters out duplicates and blanks using LINQ and outputs it to the console
        /// </summary>
        /// <param name="data">instance of the Object of the json file</param>
        public static void FilterDuplicates(Data data)
        {

            var result = data.Features.Where(x => x.Properties.Neighborhood != "").Select(x => x.Properties.Neighborhood).Distinct();

            int count = 1;

            foreach (string neighborhood in result)
            {
                Console.WriteLine($"{count}. {neighborhood}");
                count++;

            }
        }
        /// <summary>
        /// Filters out blanks and duplicates using a different form of LINQ and outputs to the console
        /// </summary>
        /// <param name="data">instance of the Object of the json file</param>
        public static void FilterDuplicates2(Data data)
        {

            var result = from Feature feature in data.Features
                         where feature.Properties.Neighborhood != ""
                         select feature.Properties.Neighborhood;

            var noDupes = result.Distinct();

            int count = 1;

            foreach (string neighborhood in noDupes)
            {
                Console.WriteLine($"{count}. {neighborhood}");
                count++;

            }
        }
    }
}
