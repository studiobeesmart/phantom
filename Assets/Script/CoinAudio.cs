using UnityEngine;

public class CoinAudio : MonoBehaviour
{
    public static CoinAudio instance;
    public AudioClip coinClip;

    AudioSource source;

    void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

   public void PlayCoin()
{
    Debug.Log("COIN SOUND DIPANGGIL");
    source.PlayOneShot(coinClip);
}

}
