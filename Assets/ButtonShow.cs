using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonShow : MonoBehaviour
{
    [SerializeField] GameObject Button;

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Trigger");
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(6f);
         Button.SetActive(true);
    }

    public void backToMain(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        StopAllCoroutines();
        Button.SetActive(false);
        SoundManager.Instance.musicSource.mute = true;
    }
}
