using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    public GameObject levelLoader;

    public const string LevelLoader = "LevelLoader";

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DungeonEntrance had a collider" + other);

        levelLoader = GameObject.FindWithTag(LevelLoader);

        if (levelLoader != null)
        {
            levelLoader.GetComponent<LevelLoader>().LoadDungeonLevel();
        }
    }
}
