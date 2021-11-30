using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeStump_Dialog : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    float timerDisplay;
    public Text dialogueText;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        sentences = new Queue<string>();
        timerDisplay = -1.0f;   
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if(timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }   
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }

    public void EndDialog()
    {
        dialogBox.SetActive(false);
    }
}
