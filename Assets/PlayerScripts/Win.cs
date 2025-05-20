using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    [Header("Win UI Elements")]
    public GameObject winScreen;
    public Button continueButton;
    public Button mainMenuButton;


    [Header("Settings")]
    public float delayBeforeShowingUI = 1.5f;

    private bool hasWon = false;

    public void Start()
    {
        winScreen.SetActive(false);
        continueButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ExitGame);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasWon)
        {
            PlayerWon();
        }
    }

    public void PlayerReachedExit()
    {
        if (!hasWon)
        {
            PlayerWon();
        }
    }

    private void PlayerWon()
    {
        hasWon = true;

        StartCoroutine(ShowWinScreen());
    }

    private IEnumerator ShowWinScreen()
    {
        yield return new WaitForSeconds(delayBeforeShowingUI);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Debug.Log("Exiting game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}