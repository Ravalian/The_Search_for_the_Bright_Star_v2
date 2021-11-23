using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var lenzController = other.GetComponent<LenzController>();

        if (lenzController != null)
        {
            if (lenzController.Health < lenzController.MaxHealth)
            {
                lenzController.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
