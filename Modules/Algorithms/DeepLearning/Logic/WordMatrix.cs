using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    [DataContract]
    [KnownType(typeof(SpeechEntity))]
    public class WordMatrix
    {
        [CollectionDataContract(Name = "dic", ItemName = "entry", KeyName = "word", ValueName = "speechEntry")]
        public class WordEntries : Dictionary<string, SpeechEntity> { }

        [DataMember(Name="dic")]
        private WordEntries _matrix = new WordEntries();

        public WordMatrix()
        {
        }

        public void Add(string word, SpeechClass[] speechClasses)
        {
            if (!_matrix.ContainsKey(word))
            {
                SpeechEntity e = new SpeechEntity(word);
                if(speechClasses != null)
                    foreach (var c in speechClasses)
                    {
                        e.Add(c);
                    }
                _matrix.Add(word, e);
            }
        }

        public static void Dump(WordMatrix matrix, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);

                XmlWriter xw = XmlWriter.Create(fs, new XmlWriterSettings { Indent = true });
                var writer = XmlDictionaryWriter.CreateDictionaryWriter(xw);
                DataContractSerializer ser = new DataContractSerializer(typeof(WordMatrix));
                ser.WriteObject(writer, matrix);

                writer.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
           
        }

        public static WordMatrix Load(string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                var ser = new DataContractSerializer(typeof(WordMatrix));
                WordMatrix matrix = (WordMatrix)ser.ReadObject(reader);
                fs.Close();

                return matrix;
            }
            catch { }

            return new WordMatrix();
            
        }

        public SpeechEntity this[string word]
        {
            get
            {
                if (!_matrix.ContainsKey(word))
                    return null;
                return _matrix[word];
            }
        }

        public Dictionary<string, SpeechEntity> Matrix
        {
            get { return _matrix; }
        }

        public override string ToString() 
        {
            string ret = "Word count : " + Matrix.Count + "\n";
            foreach (var e in Matrix)
            {
                ret += "\"" + e.Key + "\"" + " = " + e.Value + "\n";
            }
            return ret;
        }
    }
}
