using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;

    private void Start() {
        //textlabel.text = "Behind the old tree stump \nYou found an broken stone tablet! ";
        GetComponent<TypeWritterEffect>().Run("Behind the old tree stump \nYou found an broken stone tablet! ", textLabel);
    }
}
