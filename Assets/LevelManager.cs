using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float timeBeforeNextLevel = 60f; // Waktu sebelum beralih ke level berikutnya
    public string youWinSceneName = "You Win"; // Nama scene menang

    AudioManager audioManager;

    void Start()
    {
        StartCoroutine(LoadNextLevelAfterDelay());
    }

    IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSeconds(timeBeforeNextLevel);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Jika sudah mencapai level terakhir
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            // Memuat scene "YouWin" setelah menyelesaikan level kedua
            SceneManager.LoadScene(youWinSceneName);
        }
        else
        {
            // Memuat level berikutnya dalam urutan build settings
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
