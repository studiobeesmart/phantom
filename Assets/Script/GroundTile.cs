using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    public GameObject pillarLeft;
    public GameObject pillarRight;
    public Transform obstacleSpawnLeft;
    public Transform obstacleSpawnMiddle;
    public Transform obstacleSpawnRight;

    public GameObject wallPainting;
    public GameObject wallKosong;
    public GameObject wallArtefak;
    public GameObject wallPatung;

    int tileIndexDiterima;

    float tileWidth = 20f; // plane 10 × scale 2

    float leftMin = -10f;
    float leftMax = -3.3f;

    float midMin  = -3.3f;
    float midMax  =  3.3f;

    float rightMin=  3.3f;
    float rightMax= 10f;

    public GameObject spikeTrapPrefab;
    public Transform spikeSpawnPoint;

    static int lastSpikeTileIndex = -999;
    public int spikeGapTile = 3; // minimal jarak 3 tile

   // static int lastObstacleTile = -5;

   
    public static int lastObstacleTile = -3;
    public static int nextSpikeTile = 12;

    public int obstacleGap = 2;


    public void SetTileIndex(int index)
    {
        tileIndexDiterima = index;
       // Debug.Log($"Tile Index diterima: {tileIndexDiterima}");

        // Rumus modulo 5 untuk mendapatkan nilai 0, 1, 2, 3, 4
      // int posisi = tileIndexDiterima % 5;

       int posisi = index % 5;

        wallPainting.SetActive(false);
        wallKosong.SetActive(false);
        wallArtefak.SetActive(false);
        wallPatung.SetActive(false);


        if (posisi == 4)
        {
            wallPatung.SetActive(true);
        }
        else
        {
            int hasilRandom = Random.Range(1, 4);

            if (hasilRandom == 1) wallPainting.SetActive(true);
            else if (hasilRandom == 2) wallKosong.SetActive(true);
            else if (hasilRandom == 3) wallArtefak.SetActive(true);
        }

    }


    public void SetPillar(bool state)
    {
        if (pillarLeft) pillarLeft.SetActive(state);
        if (pillarRight) pillarRight.SetActive(state);
    }



    // Start is called before the first frame update
    private void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        //Debug.Log("START JALAN Ground TILE");

       if (tileIndexDiterima > 9 &&
            tileIndexDiterima > lastObstacleTile + obstacleGap &&
            Random.value < 1.9f)
        {
            SpawnObstacleRow();
            lastObstacleTile = tileIndexDiterima;
        }


       
    SpawnSpikeTrap(tileIndexDiterima); // kirim index tile


    }

    void SpawnObstacleRow()
        {
            int[] lanes = { 0, 1, 2 };

            // acak urutan lane
            for (int i = 0; i < lanes.Length; i++)
            {
                int rnd = Random.Range(i, lanes.Length);
                int temp = lanes[i];
                lanes[i] = lanes[rnd];
                lanes[rnd] = temp;
            }

            // spawn hanya 2 lane
            for (int i = 0; i < 2; i++)
            {
                SpawnObstacleAtLane(lanes[i]);
            }
        }
    void SpawnObstacleAtLane(int lane)
    {
        float xPos;

        if (lane == 0)
            xPos = Random.Range(leftMin, leftMax);
        else if (lane == 1)
            xPos = Random.Range(midMin, midMax);
        else
            xPos = Random.Range(rightMin, rightMax);

        Vector3 pos = new Vector3(
            xPos,
            obstacleSpawnMiddle.localPosition.y,
            obstacleSpawnMiddle.localPosition.z
        );

        GameObject obs = Instantiate(obstaclePrefab, transform);
        obs.transform.localPosition = pos;
    }


    private void OnTriggerExit (Collider other)
    {
        groundSpawner.SpawnTile();
        Destroy(gameObject,2);
        //Debug.Log("Destroy Exit: " + other.name);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject obstaclePrefab;

 /*
 void SpawnObstacle (){
        //Pilih Spawn Object (Kiri,Kanan,Tengah)
        int obstacleSpawnIndex = Random.Range(2,5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        //Spawn it then
        Instantiate(obstaclePrefab,spawnPoint.position,Quaternion.identity,transform);

    }


void SpawnObstacle()
{
    if(Random.value > 0.4f)
        CreateObstacle(Random.Range(leftMin, leftMax));

    if(Random.value > 0.5f)
        CreateObstacle(Random.Range(midMin, midMax));

    if(Random.value > 0.4f)
        CreateObstacle(Random.Range(rightMin, rightMax));
}
*/

void SpawnObstacle()
{
    int count = Random.Range(1, 3); // 1 atau 2 obstacle
    int[] lanes = { 0, 1, 2 };

    // shuffle
    for (int i = 0; i < lanes.Length; i++)
    {
        int rnd = Random.Range(i, lanes.Length);
        (lanes[i], lanes[rnd]) = (lanes[rnd], lanes[i]);
    }

    for (int i = 0; i < count; i++)
        SpawnLane(lanes[i]);
}

void SpawnLane(int lane)
{
    if (lane == 0)
        CreateObstacle(Random.Range(leftMin, leftMax));
    else if (lane == 1)
        CreateObstacle(Random.Range(midMin, midMax));
    else
        CreateObstacle(Random.Range(rightMin, rightMax));
}



void CreateObstacle(float xPos)
{
    /*
    Vector3 pos = new Vector3(
        xPos,
        obstacleSpawnMiddle.localPosition.y,
        obstacleSpawnMiddle.localPosition.z
    );
    */

    Vector3 pos = new Vector3(
    xPos,
    obstacleSpawnMiddle.localPosition.y + 1.2f,  //  tambahkan offset Y
    obstacleSpawnMiddle.localPosition.z
);

    GameObject obs = Instantiate(obstaclePrefab, transform);
    obs.transform.localPosition = pos;
}

//static int nextSpikeTile = 12; // tile pertama spike
public int minGap = 3;
public int maxGap = 6;


void SpawnSpikeTrap(int tileIndex)
{
    if (tileIndex < 5) return;

    // belum waktunya spawn spike
    if (tileIndex < nextSpikeTile) return;

    // spawn spike
    Vector3 spawnPos = spikeSpawnPoint.position;
    spawnPos.x = transform.position.x + 2;

    Instantiate(spikeTrapPrefab, spawnPos, Quaternion.identity, transform);

    // tentukan tile spike berikutnya
    nextSpikeTile = tileIndex + Random.Range(minGap, maxGap + 1);
}



/*
void SpawnSpikeTrap(int tileIndex) // === spikebar muncul X acak
{
    // Jangan muncul di awal game
    if (tileIndex < 5) return;

    // 10% chance muncul
    if (Random.value > 0.1f) return;

    // Random lane X
    float laneX = Random.Range(0f, 10f); // sesuai lebar tile kamu

    Vector3 spawnPos = spikeSpawnPoint.position;
    spawnPos.x = transform.position.x + laneX;

    Instantiate(spikeTrapPrefab, spawnPos, Quaternion.identity, transform);
}
*/



}
