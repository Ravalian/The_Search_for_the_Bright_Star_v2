using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeChestScript : MonoBehaviour, IInterctable
{
    [SerializeField] private DialogueObject dialogueObject;
    SpriteRenderer SR;
    [SerializeField] public Sprite open_Chest;
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
            if(player.Interctable is MazeChestScript mazeChestScript && mazeChestScript == this)
            {
                player.Interctable = null;
            }
        }
    }

    public void Interact(LenzController player)
    {
      ChangeSprite();
      player.DialogueUI.ShowDialogue(dialogueObject);
    }
    private void ChangeSprite(){
      SR.sprite = open_Chest;
    }
}
