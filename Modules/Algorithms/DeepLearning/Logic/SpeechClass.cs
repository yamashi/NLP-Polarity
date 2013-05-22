using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    [DataContract]
    public enum SpeechClass
    {
        [EnumMember]
        _NOUN_, //
        [EnumMember]
        _VERB_,//	
        [EnumMember]
        _ADJ_,//	adjective
        [EnumMember]
        _ADV_,//	adverb
        [EnumMember]
        _PRON_,//	pronoun
        [EnumMember]
        _DET_,//	determiner or article
        [EnumMember]
        _ADP_,//	an adposition: either a preposition or a postposition
        [EnumMember]
        _NUM_,//	numeral
        [EnumMember]
        _CONJ_,//	conjunction
        [EnumMember]
        _PRT_	//particle
    }
}
