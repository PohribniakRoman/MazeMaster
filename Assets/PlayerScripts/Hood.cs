using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [Header("Title Screen Elements")]
    public GameObject backgroundPanel;

    [Header("Interactive Elements (Stay Visible)")]
    public Button startButton;
    public Button exitButton;

    [Header("Game References")]
    public PlayerCamera playerCamera;
    public PlayerController playerController;
    public HealthBar healthBar;

    private bool gameStarted = false;

    void Start()
    {
        backgroundPanel.SetActive(true);

        if (playerCamera != null)
            playerCamera.enabled = false;
        if (playerController != null)
            playerController.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        if (gameStarted)
            return;

        gameStarted = true;

        backgroundPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (playerCamera != null)
            playerCamera.enabled = true;
        if (playerController != null)
            playerController.enabled = true;


        Time.timeScale = 1f;

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