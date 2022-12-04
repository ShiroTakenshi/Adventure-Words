using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        //Play Animasi
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(1);

        //load scene
        SceneManager.LoadScene(LevelIndex);
    }
}
