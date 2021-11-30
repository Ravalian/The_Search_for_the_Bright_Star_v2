using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeWritterEffect : MonoBehaviour
{
    [SerializeField] private float typeWritterSpeed = 50f;
    public Text dialogueText;

    public void Run(string textToType, TMP_Text textLabel)
    {
        StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        // float t = 0;
        // int charIndex = 0;

        // while (charIndex < textToType.Length)
        // {
        //     t += Time.deltaTime * typeWritterSpeed;
        //     charIndex = Mathf.FloorToInt(t);
        //     charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

        //     textLabel.text = textToType.Substring(0, charIndex);

        //     yield return null;
        // }

        dialogueText.text = "";
        foreach (char letter in textToType.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

        textLabel.text = textToType;
    }
}
