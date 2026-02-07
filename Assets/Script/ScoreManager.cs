using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;

    public int cursedCoins = 0;

    public TextMeshProUGUI cursedText;  // HARUS public
    public TextMeshProUGUI scoreText;

    public PlayerMovement player;
    public PlayerCollision playerCollision;




    void Awake()
    {
        instance = this;
    }

    void Start()
    {
         UpdateUI();
    UpdateCursedUI(); // supaya mulai dari 0 tampil
    }

   public void AddCursedCoin(int amount)
    {
        
       cursedCoins += 1;   // paksa selalu 1
    Debug.Log("Cursed Coin: " + cursedCoins);
    UpdateCursedUI();

    if (cursedCoins >= 3 && playerCollision != null)
        playerCollision.KillByCurse();

    }


    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score sekarang: " + score);

        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            //scoreText.text = "Gold Coin: " + score;
            scoreText.text = "+ "+score;
    }

    void UpdateCursedUI()
        {
            if (cursedText != null)
                cursedText.text = "+ " + cursedCoins;
        }


}
