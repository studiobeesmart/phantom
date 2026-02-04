using UnityEngine;

public class TrapMover : MonoBehaviour
{
    public float tinggiGerak = 4f;     // seberapa jauh naik
    public float kecepatan = 80f;     // lambat = berat
    public float delayAtas = 4f;     // jeda saat sampai atas
    public float delayBawah = 5f;    // jeda saat sampai bawah

    private Vector3 startPos;
    private float timer = 0f;
    private bool naik = true;
    private bool sedangDelay = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (sedangDelay) return;

        float targetY = naik ? tinggiGerak : 0f;

        float ySekarang = Mathf.Lerp(
            transform.position.y,
            startPos.y + targetY,
            Time.deltaTime * kecepatan
        );

        transform.position = new Vector3(startPos.x, ySekarang, startPos.z);

        if (Mathf.Abs(transform.position.y - (startPos.y + targetY)) < 0.05f)
        {
            StartCoroutine(Tunggu());
        }
    }

    System.Collections.IEnumerator Tunggu()
    {
        sedangDelay = true;

        yield return new WaitForSeconds(naik ? delayAtas : delayBawah);

        naik = !naik;
        sedangDelay = false;
    }
}
