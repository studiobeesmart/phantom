using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public bool isBadCoin = false;
    public int value = 1;

    public AudioClip goodCoinSound;
    public AudioClip badCoinSound;

    public GameObject badCoinEffect;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (isBadCoin)
        {
            Debug.Log("Kena koin busuk");
            PlaySound(badCoinSound);

            if (badCoinEffect != null)
                Instantiate(badCoinEffect, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Dapat koin " + value);
            PlaySound(goodCoinSound);

            if (ScoreManager.instance != null)
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
        a.spatialBlend = 0f;
        a.Play();
        Destroy(temp, clip.length);
    }
}
