using UnityEngine;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    public AudioSource bgmSource;
    public Slider volumeSlider;

    void Start()
    {
        float saved = PlayerPrefs.GetFloat("Volume", 0.5f);

        bgmSource.volume = saved;
        volumeSlider.value = saved;
    }

    public void SetVolume(float value)
    {
        bgmSource.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }
}
