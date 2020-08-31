using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static readonly XmlSerializer serializer = new XmlSerializer(typeof(UndergroundLine));
        static int count = 0;
        static UndergroundLine currObj = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello! Change what do u want:\n" +
                    "1 - load object from file\n" +
                    "2 - save object to file\n" +
                    "3 - add new object\n" +
                    "4 - change curr object\n" +
                    "5 - delete curr object\n" +
                    "6 - find object on name\n" +
                    "7 - exit");

            while (true)
            {
                count = Directory.GetFiles(".", "*.xml").Length;

                string answer = Console.ReadLine();

                FileStream stream = null;

                switch (answer)
                {
                    case ("1"):
                        stream = new FileStream(count - 1 + ".xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                        currObj = serializer.Deserialize(stream) as UndergroundLine;

                        if (currObj != null)
                            Console.WriteLine(currObj.ToString());
                        else
                            Console.WriteLine("Invalid object");

                        stream.Close();
                        break;
                    case ("2"):
                        if (currObj != null)
                        {
                            stream = new FileStream(count - 1 + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
                            serializer.Serialize(stream, currObj);
                        }

                        stream.Close();

                        Console.WriteLine($"Object savedin {count - 1}.xml!\n");
                        break;
                    case ("3"):
                        stream = new FileStream(count + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
                        serializer.Serialize(stream, new UndergroundLine("newItem", new List<Station>() { new Station(), new Station(), new Station() }, count));

                        stream.Close();

                        Console.WriteLine($"Object added in {count}.xml!\n");
                        count++;
                        break;
                    case ("4"):
                        currObj.Name = "ChangedName" + count;
                        currObj.SomeProp++;

                        Console.WriteLine($"Object name changed on: {currObj.Name}!\n");
                        break;
                    case ("5"):
                        File.Delete(count + ".xml");

                        Console.WriteLine($"File {count}.xml deleted!\n");
                        break;
                    case ("6"):
                        Console.WriteLine("Enter the name:");
                        string name = Console.ReadLine();

                        //foreach (var item in Directory.GetFiles(".", "*.xml"))
                        var paths = Directory.GetFiles(".", "*.xml");
                        for (int i = 0; i < paths.Length; i++)
                        {
                            stream = new FileStream(paths[i], FileMode.Open, FileAccess.Read, FileShare.Read);
                            currObj = serializer.Deserialize(stream) as UndergroundLine;

                            if (currObj != null && currObj.Name == name)
                            {
                                count = i;
                                break;
                            }

                            stream.Close();
                        }

                        if (stream != null)
                            stream.Close();

                        if (currObj != null)
                            Console.WriteLine(currObj.ToString());
                        else
                            Console.WriteLine("Invalid object");
                        break;
                    case ("7"):
                        goto Exit;
                        break;
                    default:
                        Console.WriteLine("Invalid operation");
                        break;
                }
            }
            Exit:;
        }
    }
}
