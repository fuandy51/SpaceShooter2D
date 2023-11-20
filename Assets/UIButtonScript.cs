using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    [SerializeField] GameObject buttonGroup;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level 1");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        buttonGroup.SetActive(true);
        StartCoroutine(UpdateTimeScale(0));
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        buttonGroup.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    IEnumerator UpdateTimeScale(float newTimeScale)
    {
        yield return null; // Wait for one frame
        Time.timeScale = newTimeScale;
    }
}
