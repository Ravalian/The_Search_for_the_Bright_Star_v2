using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppState : MonoBehaviour
{
    public static AppState Instance;

    public int? Health { get; set; }
    public float? Mana { get; set; }
    public Dictionary<string, Vector2> Positions { get; set; } = new Dictionary<string, Vector2>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            Positions[SceneNames.MainScene] = new Vector2(-4.96f, -1.33f); //Main scene starting position
            Positions[SceneNames.DungeonScene] = new Vector2(-1.48f, -7.93f); //Dungeon scene starting position
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
