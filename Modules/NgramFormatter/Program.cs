using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgramFormatter
{
    class Program
    {
        static void ParseDic(string lang)
        {
            string dir = "dic/" + lang + "/";
            Format(dir + "z");
            Format(dir + "a");
        }

        static void Format(string file)
        {
            int counter = 0;
            string line;

            Dictionary<string, Dictionary<string, int>> words = new Dictionary<string, Dictionary<string, int>>();

            System.IO.StreamWriter sw = new System.IO.StreamWriter(file + ".dic");
            System.IO.StreamReader sr =new System.IO.StreamReader(file);

            while ((line = sr.ReadLine()) != null)
            {
                string[] tabbed = line.Split("\t".ToCharArray());
                string w = tabbed[0];
                string[] pos = w.Split("_".ToCharArray()[0]);

                w = pos[0].ToLower();

                w = w.Split(".".ToCharArray())[0];

                if (pos.Length == 2)
                {
                    if (!words.ContainsKey(w))
                    {
                        words.Add(w, new Dictionary<string, int>());
                    }
                    var dic = words[w];

                    if (!dic.ContainsKey(pos[1]))
                    {
                        dic.Add(pos[1], 0);
                    }

                    dic[pos[1]] += int.Parse(tabbed[2]);
                    
                }

                
                //Console.WriteLine(w);
                counter++;
            }

            foreach (var dic in words)
            {
                int max = -1;
                string tag = "";
                foreach (var pos in dic.Value)
                {
                    if (pos.Value > max)
                        tag = pos.Key;
                }

                sw.WriteLine(dic.Key + "\t" + tag);
            }

            Console.WriteLine("Current file contains : " + counter + " lines and " + words.Count + " pos tagged words.");


            sr.Close();
            sw.Close();
        }

        static void Main(string[] args)
        {
            ParseDic("EN");

            Console.ReadKey();
        }
    }
}
