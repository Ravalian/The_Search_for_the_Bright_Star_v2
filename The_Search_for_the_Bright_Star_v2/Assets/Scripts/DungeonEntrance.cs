using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    public GameObject levelLoader;

    public const string LevelLoader = "LevelLoader";
    public const string PlayerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DungeonEntrance had a collider" + other);

        levelLoader = GameObject.FindWithTag(LevelLoader);

        if (other.CompareTag(PlayerTag) && other.TryGetComponent(out LenzController player))
        {
            if (levelLoader != null)
            {
                player.SaveLenzState();
                //Set last position on Main_Scene to near dungeon exit
                player.SaveLenzPosition(new Vector2(56.33f, 35.13f), SceneNames.MainScene); 
                levelLoader.GetComponent<LevelLoader>().LoadDungeonLevel();
            }
        }
    }
}
