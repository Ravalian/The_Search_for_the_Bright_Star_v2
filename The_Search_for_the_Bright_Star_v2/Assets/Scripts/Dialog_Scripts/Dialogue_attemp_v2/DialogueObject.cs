using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueObject", menuName = "The_Search_for_the_Bright_Star_v2/DialogueObject", order = 0)]
public class DialogueObject : ScriptableObject 
{
    [SerializeField] [TextArea] private string[] dialogue;

    public string[] Dialogue => dialogue;
}
