using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Backsound : MonoBehaviour
{
    private static Backsound instance;
    private AudioSource audioSource;
    private const string volumeKey = "MusicVolume";
    private const float minVolume = 0.05f; // biar gak pernah benar2 mute

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(volumeKey, 0.3f);
        audioSource.volume = Mathf.Max(savedVolume, minVolume);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Slider slider = FindObjectOfType<Slider>();

        if (slider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat(volumeKey, 0.3f);
            slider.value = savedVolume;

            slider.onValueChanged.RemoveAllListeners();
            slider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float value)
    {
        float finalVol = Mathf.Max(value, minVolume);
        audioSource.volume = finalVol;
        PlayerPrefs.SetFloat(volumeKey, finalVol);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
