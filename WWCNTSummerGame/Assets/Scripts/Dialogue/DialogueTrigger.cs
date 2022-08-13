using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Dialogue dialogue;

    public override void Interact()
    {
        base.Interact();
        // Log
        Debug.Log("Interacting with Fox");
        // DisplayDialogue();
        TriggerDialogue();
    }

    public void TriggerDialogue () {
        // Debug.Log("Dialogue Trigger");
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
