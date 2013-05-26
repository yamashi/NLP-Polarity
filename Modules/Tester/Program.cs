using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic;
using System.Collections.Generic;
using System.Drawing;

namespace Tester
{
    class Program
    {
        static List<double[]> ex = new List<double[]>();
        static List<double[]> res = new List<double[]>();

        static int CalculateWidth(SpeechEntity node)
        {
            Font drawFont = new Font("Arial", 16);
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                node.Width = (int)g.MeasureString(node.Entity, drawFont).Width;
            }

            if (node.Left != null)
            {
                CalculateWidth(node.Left);
                node.Width += node.Left.Width;
            }
            if (node.Right != null)
            {
                CalculateWidth(node.Right);
                node.Width += node.Right.Width;
            }

            return node.Width;
        }

        static void DrawNode(Graphics g, SpeechEntity node, double x, double y)
        {
            Pen blackPen = new Pen(Color.Black, 3);
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            SizeF size = g.MeasureString(node.Entity, drawFont);

            float left = (float)x + (node.Width - 100) / 2;
            float top = (float)y;

           // g.DrawEllipse(blackPen, left, top, (float)100, (float)32);
            g.DrawString(node.Entity, drawFont, drawBrush, left + 50 - size.Width / 2, top + 8);

            if (node.Left != null)
            {
                float leafRight = (float)x + (node.Left.Width - 100) / 2 + 80;

                g.DrawLine(blackPen, left + 20, top + 36, leafRight, top + 80);

                DrawNode(g, node.Left, x, y + 80);
            }
            if (node.Right != null)
            {
                float leafLeft = (float)x + node.Width - node.Right.Width + (node.Right.Width - 100) / 2 + 20;

                g.DrawLine(blackPen, left + 80, top + 36, leafLeft, top + 80);

                DrawNode(g, node.Right, x + node.Width - node.Right.Width, y + 80);
            }
        }

        static void Draw(SpeechEntity root)
        {
            Bitmap myBitmap = new Bitmap(CalculateWidth(root) + 20, 600);

            Graphics myGraphics = Graphics.FromImage(myBitmap);

            DrawNode(myGraphics, root, 0, 0);

            myBitmap.Save("test.bmp");
            
            myGraphics.Dispose();
        }

        static void Populate(WordMatrix matrix, string word, SpeechClass c)
        {
            matrix.Add(word, new SpeechClass[] { c });
        }

        static void RawPopulate(WordMatrix matrix)
        {
            SpeechEntity v = matrix["_VOID_"];

            string[] names = Enum.GetNames(typeof(SpeechClass));
            SpeechClass[] values = (SpeechClass[])Enum.GetValues(typeof(SpeechClass));

            for (int i = 0; i < names.Length; i++)
            {
                Populate(matrix, names[i], values[i]);
            }
        }

        static void AddSample(WordMatrix mat, string word1, string word2, SpeechEntity result, double expected)
        {
            ex.Add(SpeechEntity.Group(mat[word1], mat[word2]));
            res.Add(result.ToArray().Concat(new double[] { expected }).ToArray());
        }

        static void QueryWord(WordMatrix mat, string w)
        {
            Console.WriteLine("The word \"" + w + "\" is unknow");
            Console.Write("Type the word's grammar type : ");
            
            while(true)
            {
                string className = Console.ReadLine();
                if(Enum.IsDefined(typeof(SpeechClass), className))
                {
                    Populate(mat, w, (SpeechClass)Enum.Parse(typeof(SpeechClass), className, true));
                    break;
                }
                else
                {
                    Console.WriteLine("This class does not exist.");
                }
            }
        }

        static void QueryWords(WordMatrix mat, string[] w)
        {
            foreach (var word in w)
            {
                if (mat[word] == null)
                {
                    QueryWord(mat, word);
                }
            }
            
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Type the first 2 letters of the language");
            Console.Write("> ");
            string lang = Console.ReadLine();

            WordMatrix mat = WordMatrix.Load("dic/" + lang + ".ser");
            RecursiveNetwork net = RecursiveNetwork.Load("dic/network.ser");

            RawPopulate(mat);

            net.Matrix = mat;

            AddSample(mat, "_NOUN_", "_VERB_", SpeechEntity.Merge(mat["_NOUN_"], mat["_VERB_"]), 0.5);
            AddSample(mat, "_PRON_", "_VERB_", SpeechEntity.Merge(mat["_NOUN_"], mat["_VERB_"]), 1.0);
            AddSample(mat, "_VERB_", "_VERB_", mat["_VERB_"], 1.0);
            AddSample(mat, "_DET_", "_NOUN_", mat["_NOUN_"], 0.9);
            AddSample(mat, "_DET_", "_ADJ_", mat["_DET_"], -1.0);
            AddSample(mat, "_ADJ_", "_NOUN_", mat["_NOUN_"], 1.0);
            AddSample(mat, "_VERB_", "_DET_", mat["_VERB_"], -1.0);
            AddSample(mat, "_NOUN_", "_ADP_", mat["_NOUN_"], 0.5);
            AddSample(mat, "_ADP_", "_NOUN_", mat["_NOUN_"], 0.5);
            AddSample(mat, "_NOUN_", "_CONJ_", mat["_NOUN_"], 0.8);
            AddSample(mat, "_CONJ_", "_NOUN_", mat["_NOUN_"], 0.8);

            net.Train(ex, res);

            while (true)
            {
                Console.Write("> ");

                string str = Console.ReadLine();

                if(str == "q")
                    break;

                string[] words = str.Split(" ".ToCharArray());
                QueryWords(mat, words);

                Console.WriteLine("");
                Draw(net.Parse(str));
                Console.WriteLine("");
            }
            //net.Parse("The mean angry cat is eating the green mouse on the red dirty carpet");

            WordMatrix.Dump(mat, "dic/" + lang + ".ser");
            RecursiveNetwork.Dump(net, "dic/network.ser");
        }
    }
}