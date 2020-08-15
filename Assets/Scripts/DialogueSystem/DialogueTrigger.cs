#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Conversation convo;
    DialogueManager dialogue;

    void Start()
    {
        dialogue = FindObjectOfType<DialogueManager>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Player>()) {return;}
        other.gameObject.GetComponent<Player>().CanAttack = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.StartConversation(convo);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Player>()) return;
        other.gameObject.GetComponent<Player>().CanAttack = true;
        dialogue.myAnimator.SetBool("IsOpen", false);
    }
}
