using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic;
using System.Collections.Generic;

namespace Tester
{
    class Program
    {
        static List<double[]> ex = new List<double[]>();
        static List<double[]> res = new List<double[]>();

        static void Populate(WordMatrix matrix, string word, SpeechClass c)
        {
            matrix.Add(word, new SpeechClass[] { c });
        }

        static void RawPopulate(WordMatrix matrix)
        {
            SpeechEntity v = matrix["_VOID_"];

            Populate(matrix, "_DET_", SpeechClass._DET_);
            Populate(matrix, "_NOUN_", SpeechClass._NOUN_);
            Populate(matrix, "_VERB_", SpeechClass._VERB_);
            Populate(matrix, "_ADJ_", SpeechClass._ADJ_);
            Populate(matrix, "_ADP_", SpeechClass._ADP_);
        }

        static void VocabularyPopulate(WordMatrix mat)
        {
            Populate(mat, "the", SpeechClass._DET_);
            Populate(mat, "angry", SpeechClass._ADJ_);
            Populate(mat, "mean", SpeechClass._ADJ_);
            Populate(mat, "green", SpeechClass._ADJ_);
            Populate(mat, "red", SpeechClass._ADJ_);
            Populate(mat, "dirty", SpeechClass._ADJ_);
            Populate(mat, "cat", SpeechClass._NOUN_);
            Populate(mat, "mouse", SpeechClass._NOUN_);
            Populate(mat, "carpet", SpeechClass._NOUN_);
            Populate(mat, "outside", SpeechClass._NOUN_);
            Populate(mat, "is", SpeechClass._VERB_);
            Populate(mat, "eating", SpeechClass._VERB_);
            Populate(mat, "on", SpeechClass._ADP_);
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
                
            switch(Console.ReadLine())
            {
                case "_NOUN_":
                    Populate(mat, w, SpeechClass._NOUN_);
                    break;
                case "_VERB_":
                    Populate(mat, w, SpeechClass._VERB_);
                    break;
                case "_ADJ_":
                    Populate(mat, w, SpeechClass._ADJ_);
                    break;
                case "_DET_":
                    Populate(mat, w, SpeechClass._DET_);
                    break;
                case "_ADP_":
                    Populate(mat, w, SpeechClass._ADP_);
                    break;
                case "_PRON_":
                    Populate(mat, w, SpeechClass._PRON_);
                    break;
                case "_CONJ_":
                    Populate(mat, w, SpeechClass._CONJ_);
                    break;
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
            WordMatrix mat = WordMatrix.Load("dic/en.ser");
            RecursiveNetwork net = new RecursiveNetwork(mat);

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
                Console.Write("Type sentence : ");

                string str = Console.ReadLine();

                if(str == "QUIT")
                    break;

                string[] words = str.Split(" ".ToCharArray());
                QueryWords(mat, words);

                Console.WriteLine("");
                net.Parse(str);
                Console.WriteLine("");
            }
            //net.Parse("The mean angry cat is eating the green mouse on the red dirty carpet");

            WordMatrix.Dump(mat, "dic/en.ser");
        }
    }
}