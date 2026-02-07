using UnityEngine;

public class CursedCoin : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ScoreManager>().AddCursedCoin(value);
            ScoreManager.instance.AddCursedCoin(value); // WAJIB INI
            Debug.Log("Cursed coin kena player");
            Destroy(gameObject);
        }
    }


}
