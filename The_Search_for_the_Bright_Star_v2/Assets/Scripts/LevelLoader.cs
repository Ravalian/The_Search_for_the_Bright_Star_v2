using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public void LoadDungeonLevel()
    {
        StartCoroutine(LoadLevel("DungeonScene"));
    }

    public void LoadOverworldLevel()
    {
        StartCoroutine(LoadLevel("Main_Screen"));
    }

    IEnumerator LoadLevel(string dungeonScene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(dungeonScene);
    }
}
