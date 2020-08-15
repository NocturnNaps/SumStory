#pragma warning disable 0649
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/New Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField] public DialogueLine[] allLines;

    public DialogueLine GetLinebyIndex(int Index)
    {
        return allLines[Index];
    }

    public int GetLength()
    {
        return allLines.Length - 1;
    }
    public Sprite GetNormalSprite(int Index)
    {
        return allLines[Index].speaker.speakerSprite;
    }
    public Sprite GetConfusedSprite(int Index)
    {
        return allLines[Index].speaker.confusedSprite;
    }
    public Sprite GetAngrySprite(int Index)
    {
        return allLines[Index].speaker.angrySprite;
    }
    public Sprite GetShockedSprite(int Index)
    {
        return allLines[Index].speaker.shockedSprite;
    }
    public Sprite GetHappySprite(int Index)
    {
        return allLines[Index].speaker.happySprite; 
    }
}