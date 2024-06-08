using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText; // UI Text untuk menampilkan countdown
    public float countdownTime = 240f; // Waktu countdown dalam detik
    public GameObject endGamePanel; // Panel yang akan muncul saat countdown habis
    public Canvas targetCanvas;
    private float currentTime;
    private bool isPaused = false; // Flag untuk status pause

    void Start()
    {
        currentTime = countdownTime; // Atur waktu awal countdown
        UpdateCountdownText();
        endGamePanel.SetActive(false); // Sembunyikan panel pada awalnya
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false); // Ensure the canvas is hidden at the start
        }
    }

    void Update()
    {
        if (!isPaused)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime; // Kurangi waktu saat ini dengan delta time
                UpdateCountdownText();
            }
            else
            {
                currentTime = 0; // Pastikan waktu tidak menjadi negatif
                UpdateCountdownText();
                OnCountdownEnd();
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (targetCanvas != null)
            {
                PauseGame();
                targetCanvas.gameObject.SetActive(true); // Show the canvas
                AudioManager.Instance.PlayMusic("FF7"); // Play the music named "FF7"
            }
        }
    }

    void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60); // Hitung menit
        int seconds = Mathf.FloorToInt(currentTime % 60); // Hitung detik

        countdownText.text = "Time : " + string.Format("{0:00}:{1:00}", minutes, seconds); // Perbarui teks UI dengan format 00:00
    }

    void OnCountdownEnd()
    {
        // Logika saat countdown selesai
        Debug.Log("Countdown selesai!");
        PauseGame(); // Jeda game
        endGamePanel.SetActive(true); // Tampilkan panel
    }

    void PauseGame()
    {
        Time.timeScale = 0; // Pause game dengan menghentikan waktu
        isPaused = true; // Set flag isPaused ke true
    }

    void ResumeGame()
    {
        Time.timeScale = 1; // Lanjutkan game dengan mengembalikan waktu
        isPaused = false; // Set flag isPaused ke false
    }
    public void RestartCurrentScene()
    {
        AudioManager.Instance.StopMusic();
        Debug.Log("tesss");
        targetCanvas.gameObject.SetActive(false);
        // Dapatkan nama scene saat ini
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Muat ulang scene saat ini
        SceneManager.LoadScene(currentSceneName);
    }
}
