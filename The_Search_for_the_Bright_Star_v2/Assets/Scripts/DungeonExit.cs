using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExit : MonoBehaviour
{
    public GameObject levelLoader;

    public const string LevelLoader = "LevelLoader";
    public const string PlayerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DungeonExit had a collider" + other);

        levelLoader = GameObject.FindWithTag(LevelLoader);

        if (other.CompareTag(PlayerTag) && other.TryGetComponent(out LenzController player))
        {
            if (levelLoader != null)
            {
                player.SaveLenzState();
                levelLoader.GetComponent<LevelLoader>().LoadOverworldLevel();
            }
        }
    }
}
