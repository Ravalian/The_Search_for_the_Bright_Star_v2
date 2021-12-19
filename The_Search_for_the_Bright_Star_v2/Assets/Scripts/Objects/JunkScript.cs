using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkScript : MonoBehaviour, IInterctable
{
    [SerializeField] private DialogueObject dialogueObject;
    SpriteRenderer SR;
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Does it enter lever?");
        if (other.CompareTag("Player") && other.TryGetComponent(out LenzController player))
        {
            player.Interctable = this;
        }
    }
    void Start()
    {
      SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Does it enter OnTriggerExit2D?");
        if(other.CompareTag("Player") && other.TryGetComponent(out LenzController player))
        {
            if(player.Interctable is JunkScript junkScript && junkScript == this)
            {
                player.Interctable = null;
            }
        }
    }

    public void Interact(LenzController player)
    {
      player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
