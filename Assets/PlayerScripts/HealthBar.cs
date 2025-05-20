using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth = 100;

    [Header("UI Settings")]
    public Image healthFillImage;
    public GameObject darkOverlay;
    public GameObject gameOver;
    public Button restartButton;
    public PlayerCamera playerCamera;
    public PlayerController playerController;

    void Start()
    {
        darkOverlay.SetActive(false);
        gameOver.SetActive(false);
        restartButton.gameObject.SetActive(false);
        currentHealth = maxHealth;
        UpdateHealthBar();
        restartButton.onClick.AddListener(RestartGame);
        Time.timeScale = 1f;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float newWidth = (currentHealth / (float)maxHealth);
        healthFillImage.rectTransform.localScale = new Vector3(newWidth, 1, 1);

        if (currentHealth <= 0)
        {
            StartCoroutine(GameOverTimeout());
        }
    }

    private void ShowGameOverUI()
    {
        darkOverlay.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameOver.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }
    private IEnumerator GameOverTimeout()
    {
        playerCamera.GetComponent<PlayerCamera>().OnGameOver();
        playerController.GetComponent<PlayerController>().OnGameOver();

        yield return new WaitForSecondsRealtime(2f);

        ShowGameOverUI();
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
