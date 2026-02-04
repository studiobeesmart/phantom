using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    private AudioSource backsound;

    void Awake()
    {
        slider = GetComponent<Slider>(); // ambil slider ROOT

        Backsound bg = FindObjectOfType<Backsound>();
        if (bg != null)
            backsound = bg.GetComponent<AudioSource>();
    }

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);

        slider.value = savedVolume;

        if (backsound != null)
            backsound.volume = savedVolume;

        slider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        if (backsound != null)
            backsound.volume = value;

        PlayerPrefs.SetFloat("MusicVolume", value);
    }
}
