using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    
    [SerializeField] private GameObject dialogueBox;

    public bool isOpen {get; private set;}

    private TypeWritterEffect typeWritterEffect;

    private void Start() {
        typeWritterEffect = GetComponent<TypeWritterEffect>();
        ClosedDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject){
        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject){
        foreach(string dialogue in dialogueObject.Dialogue){
            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            yield return null;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
        }

        ClosedDialogueBox();
    }

    private IEnumerator RunTypingEffect(string dialogue){
        typeWritterEffect.Run(dialogue, textLabel);

        while(typeWritterEffect.IsRunning){
            yield return null;

            if (Input.GetKeyDown(KeyCode.V))
            {
                typeWritterEffect.Stop();
            }
        }
    }

    private void ClosedDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;   
    }
}
