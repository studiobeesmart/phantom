using UnityEngine;
using UnityEngine.UI;

public class UI_TogglePause : MonoBehaviour
{
    public Sprite pauseIcon;
    public Sprite playIcon;
    private Image buttonImage;
    private bool isPaused = false;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        SetToRunning(); // Default game jalan
    }

    public void TogglePause()
    {
        if (isPaused)
            SetToRunning();
        else
            SetToPaused();
    }

    void SetToPaused()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;   //  ini pause semua audio
        buttonImage.sprite = playIcon;
        isPaused = true;
    }

    void SetToRunning()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;   // ini pause semua audio
        buttonImage.sprite = pauseIcon;
        isPaused = false;
    }
}
