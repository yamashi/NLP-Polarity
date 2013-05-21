using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public enum SpeechClass
    {
        _NOUN_, //
        _VERB_,//	
        _ADJ_,//	adjective
        _ADV_,//	adverb
        _PRON_,//	pronoun
        _DET_,//	determiner or article
        _ADP_,//	an adposition: either a preposition or a postposition
        _NUM_,//	numeral
        _CONJ_,//	conjunction
        _PRT_	//particle
    }
}
