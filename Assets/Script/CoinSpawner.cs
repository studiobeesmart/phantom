using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject goodCoin;
    public GameObject badCoin;

    public float laneOffset = 2f;
    public float spawnZDistance = 25f;
    public float spawnInterval = 6f;
    public float coinHeight = 1.8f;

    [Range(0,100)]
    public int badCoinChance = 20;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(SpawnCoin), 2f, spawnInterval);
    }

    void SpawnCoin()
{
    if (player == null) return;

    int lane = Random.Range(-1, 2);

    Vector3 startPos = new Vector3(
        lane * laneOffset,
        coinHeight,
        player.position.z + spawnZDistance
    );

    int amount = Random.Range(4, 8); // panjang barisan

    for (int i = 0; i < amount; i++)
    {
        Vector3 pos = startPos + new Vector3(0, 0, i * 4f);

        bool spawnBad = Random.Range(0, 100) < badCoinChance;

        GameObject coinPrefab = spawnBad ? badCoin : goodCoin;

        Instantiate(coinPrefab, pos, coinPrefab.transform.rotation);
    }
}

}
