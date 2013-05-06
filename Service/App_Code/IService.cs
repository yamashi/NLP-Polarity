using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract]
public interface IService
{

	[OperationContract]
	List<TweetEntry> Compute(string corpus);
}

[DataContract]
public class Corpus
{
    List<TweetEntry> entries;
    List<string> words;

    [DataMember]
    public List<TweetEntry> Entries
    {
        get { return entries; }
        set { entries = value; }
    }

    [DataMember]
    public List<string> Words
    {
        get { return words; }
        set { words = value; }
    }
};

[DataContract]
public class TweetEntry
{
	float normalizedOutput;
	string sentence;

	[DataMember]
    public float NormalizedOutput
	{
        get { return normalizedOutput; }
        set { normalizedOutput = value; }
	}

	[DataMember]
    public string Sentence
	{
        get { return sentence; }
        set { sentence = value; }
	}
}
