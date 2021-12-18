using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var lenzController = other.GetComponent<LenzController>();

        if (lenzController != null)
        {
            if (lenzController.Health < LenzController.MaxHealth)
            {
                lenzController.ChangeHealth(1);
                Destroy(gameObject);
                lenzController.PlaySound(collectedClip);
            }
        }
    }
}
