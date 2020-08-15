using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue, navButtonText;
    public Image speakerSprite;
    public float TextSpeed = 0.04f;

    private int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;
    public Animator myAnimator;
    private Coroutine Typing;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            myAnimator = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("lol");
        }
    }

    public static void StartConversation(Conversation convo)
    {
        instance.myAnimator.SetBool("IsOpen", true);
        instance.currentIndex = 0;

        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";
        instance.navButtonText.text = "->";

        instance.ReadNext();
    }

    public void ReadNext()
    {
        if (currentIndex > currentConvo.GetLength())
        {
            myAnimator.SetBool("IsOpen", false);
            return;
        }

        speakerName.text = currentConvo.GetLinebyIndex(currentIndex).speaker.GetName();

        if (Typing == null)
        {
            Typing = instance.StartCoroutine(TypeText(currentConvo.GetLinebyIndex(currentIndex).dialogue));
        }
        else
        {
            instance.StopCoroutine(Typing);
            Typing = null;
            Typing = instance.StartCoroutine(TypeText(currentConvo.GetLinebyIndex(currentIndex).dialogue));
        }
        
        speakerSprite.sprite = instance.GetSprite(currentIndex);
        currentIndex++;
        if (currentIndex > currentConvo.GetLength())
        {
            navButtonText.text = "X";
        }
    }

    public Sprite GetSprite(int Index)
    {
        if(currentConvo.allLines[Index].Normal)
        {
            return currentConvo.GetNormalSprite(Index);
        }
        else if (currentConvo.allLines[Index].Confused)
        {
            return currentConvo.GetConfusedSprite(Index);
        }
        else if (currentConvo.allLines[Index].Angry)
        {
            return currentConvo.GetAngrySprite(Index);
        }
        else if (currentConvo.allLines[Index].Shocked)
        {
            return currentConvo.GetShockedSprite(Index);
        }
        else if (currentConvo.allLines[Index].Happy)
        {
            return currentConvo.GetHappySprite(Index);
        }
        return currentConvo.GetNormalSprite(Index);
    }

    private IEnumerator TypeText(string Text)
    {
        dialogue.text = "";
        bool complete = false;
        int Index = 0;
        while (!complete)
        {
            dialogue.text += Text[Index];
            Index++;
            yield return new WaitForSeconds(TextSpeed);

            if (Index > Text.Length)
            complete = true;
        }
        Typing = null;
    }
}
