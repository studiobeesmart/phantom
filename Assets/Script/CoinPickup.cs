using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public bool isBadCoin = false;
    public int value = 1;

    public AudioClip goodCoinSound;
    public AudioClip badCoinSound;

    private bool sudahDiambil = false;

    void OnTriggerEnter(Collider other)
    {
        if (sudahDiambil) return;
        if (!other.CompareTag("Player")) return;

        sudahDiambil = true;
        GetComponent<Collider>().enabled = false;

        if (isBadCoin)
        {
            Debug.Log("Kena koin busuk");
            PlaySound(badCoinSound);
            ScoreManager.instance.AddCursedCoin(1);
        }
        else
        {
            Debug.Log("Dapat koin");
            PlaySound(goodCoinSound);
            ScoreManager.instance.AddScore(value);
            
        }

        Destroy(gameObject);
    }

    void PlaySound(AudioClip clip)
{
    if (clip == null) return;

    GameObject temp = new GameObject("CoinSound");
    AudioSource a = temp.AddComponent<AudioSource>();
    a.clip = clip;
    a.volume = 1f;
    a.spatialBlend = 0f; // 2D
    a.Play();

    Destroy(temp, clip.length);
}

}
