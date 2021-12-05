using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInterctable
{
    [SerializeField] private DialogueObject dialogueObject;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Does it enter OnTriggerEnter2D?");
        if (other.CompareTag("Player") && other.TryGetComponent(out LenzController player))
        {
            player.Interctable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Does it enter OnTriggerExit2D?");
        if(other.CompareTag("Player") && other.TryGetComponent(out LenzController player))
        {
            if(player.Interctable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interctable = null;
            }
        }
    }

    public void Interact(LenzController player){
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
