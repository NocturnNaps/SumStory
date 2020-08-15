#pragma warning disable 0649
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public Speaker speaker;
    [Header("Emotion")]
    public bool Normal;
    public bool Confused;
    public bool Angry;
    public bool Shocked;
    public bool Happy;
    [TextArea(12,3)]
    public string dialogue;
}
