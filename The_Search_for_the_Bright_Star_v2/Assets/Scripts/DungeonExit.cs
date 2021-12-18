using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExit : MonoBehaviour
{
    public GameObject levelLoader;

    public const string LevelLoader = "LevelLoader";

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DungeonExit had a collider" + other);

        levelLoader = GameObject.FindWithTag(LevelLoader);

        if (levelLoader != null)
        {
            levelLoader.GetComponent<LevelLoader>().LoadOverworldLevel();
        }
    }
}
