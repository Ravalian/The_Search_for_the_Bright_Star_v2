using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private TypeWritterEffect typeWritterEffect;

    private void Start() {
        //textlabel.text = "Behind the old tree stump \nYou found an broken stone tablet! ";
        //GetComponent<TypeWritterEffect>().Run("Behind the old tree stump \nYou found an broken stone tablet! ", textLabel);

        typeWritterEffect = GetComponent<TypeWritterEffect>();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject){
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject){
        foreach(string dialogue in dialogueObject.Dialogue){
            yield return typeWritterEffect.Run(dialogue, textLabel);
        }
    }
}
