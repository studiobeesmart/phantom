using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public bool Terputar = true;
    public int KecepatanPutaran;

    void Update()
    {
       KecepatanPutaran = 1;
            transform.Rotate(
                10,
                0,
                0
            );
       
    }
}




