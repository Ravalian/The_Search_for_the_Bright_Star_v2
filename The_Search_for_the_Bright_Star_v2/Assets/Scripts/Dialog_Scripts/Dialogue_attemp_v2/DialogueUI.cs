using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    //[SerializeField] private DialogueObject testDialogue;
    [SerializeField] private GameObject dialogueBox;

    public bool isOpen {get; private set;}

    private TypeWritterEffect typeWritterEffect;

    private void Start() {
        //textlabel.text = "Behind the old tree stump \nYou found an broken stone tablet! ";
        //GetComponent<TypeWritterEffect>().Run("Behind the old tree stump \nYou found an broken stone tablet! ", textLabel);

        typeWritterEffect = GetComponent<TypeWritterEffect>();
        ClosedDialogueBox();
        //ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject){
        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject){
        foreach(string dialogue in dialogueObject.Dialogue){
            yield return typeWritterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
        }

        ClosedDialogueBox();
    }

    private void ClosedDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;   
    }
}
