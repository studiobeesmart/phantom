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

        if(tileIndexDiterima>9)
        SpawnObstacle();

       
    SpawnSpikeTrap(tileIndexDiterima); // kirim index tile


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
*/

void SpawnObstacle()
{
    if(Random.value > 0.4f)
        CreateObstacle(Random.Range(leftMin, leftMax));

    if(Random.value > 0.5f)
        CreateObstacle(Random.Range(midMin, midMax));

    if(Random.value > 0.4f)
        CreateObstacle(Random.Range(rightMin, rightMax));
}

void CreateObstacle(float xPos)
{
    Vector3 pos = new Vector3(
        xPos,
        obstacleSpawnMiddle.localPosition.y,
        obstacleSpawnMiddle.localPosition.z
    );

    GameObject obs = Instantiate(obstaclePrefab, transform);
    obs.transform.localPosition = pos;
}

void SpawnSpikeTrap(int tileIndex)
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




}
