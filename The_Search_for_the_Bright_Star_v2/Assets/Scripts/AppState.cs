using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppState : MonoBehaviour
{
    public static AppState Instance;

    public int? Health { get; set; }
    public float? Mana { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
