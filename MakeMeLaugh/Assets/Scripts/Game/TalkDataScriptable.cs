using System;
using UnityEngine;

[CreateAssetMenu]
public class TalkDataScriptable : ScriptableObject
{
    [Serializable]
    public class TalkSection
    {
        public string Name;
        [TextArea]
        public string Talk;
        public string ContainsGags = string.Empty;

        public bool IsContainsGags()
        {
            return ContainsGags != string.Empty;
        }
    }
    public TalkSection[] Sections;
}

