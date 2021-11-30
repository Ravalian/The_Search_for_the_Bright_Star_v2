using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var lenzController = other.GetComponent<LenzController>();

        if (lenzController != null)
        {
            if (lenzController.Mana < lenzController.MaxMana)
            {
                lenzController.ChangeMana(3);
                Destroy(gameObject);
                lenzController.PlaySound(collectedClip);
            }
        }
    }
}
