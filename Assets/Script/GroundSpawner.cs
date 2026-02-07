
using UnityEngine;


public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 nextSpawnPoint;
    int tileIndex = 0;
    int tileIndexDiterima;

    public void SetTileIndex(int index)
    {
        tileIndexDiterima = index;
       // Debug.Log("Tile Index dikirim: " + tileIndexDiterima);
    }


    public void SpawnTile(){
        GameObject temp = Instantiate(groundTile,nextSpawnPoint, Quaternion.identity);
        GroundTile gt = temp.GetComponent<GroundTile>();
        gt.SetTileIndex(tileIndex); // kirim nilai index ke tile
        if (tileIndex % 4 == 0)   // tiap 4 tile
            gt.SetPillar(true);
            // Debug.Log("Pilar : "+ tileIndex);

             

        tileIndex++;
       


        nextSpawnPoint = temp.transform.GetChild(1).position;
       
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<15;i++){
            SpawnTile();


            // Debug.Log("Loop Ke : "+ i);
        }
       // Debug.Log("GROUND SPAWNER JALAN");

    }

    void Awake()
    {
        GroundTile.lastObstacleTile = -3;
        GroundTile.nextSpikeTile = 12;
    }


   
}
