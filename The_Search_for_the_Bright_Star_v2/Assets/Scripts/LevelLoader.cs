using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Code adapted from https://www.youtube.com/watch?v=CE9VOZivb3I&ab_channel=Brackeys
 */
public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public void LoadDungeonLevel()
    {
        StartCoroutine(LoadLevel(SceneNames.DungeonScene));
    }

    public void LoadOverworldLevel()
    {
        StartCoroutine(LoadLevel(SceneNames.MainScene));
    }

    IEnumerator LoadLevel(string dungeonScene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(dungeonScene);
    }
}
