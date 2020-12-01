using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WisdomQuestions
{
    Dictionary<string, (string, string, string)> extraDic = new Dictionary<string, (string, string, string)>
    {
        {"todo",("intro","neutral","extra") },
        {"todo",("intro","neutral","extra") },
        {"todo",("intro","neutral","extra") },
        {"todo",("intro","neutral","extra") }
    };
    
    Dictionary<string, (string, string, string)> neuroDic = new Dictionary<string, (string, string, string)>
    {
        {"todo",("stable","neutral","neuro") },
        {"todo",("stable","neutral","neuro") },
        {"todo",("stable","neutral","neuro") },
        {"todo",("stable","neutral","neuro") }
    };
    
    Dictionary<string, (string, string, string)> psychoDic = new Dictionary<string, (string, string, string)>
    {
        {"todo",("","neutral","neuro") },
    };
}
