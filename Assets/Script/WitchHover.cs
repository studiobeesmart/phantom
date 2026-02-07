
using UnityEngine;

public class WitchHover : MonoBehaviour
{
    Vector3 basePos;

    [Header("Side Movement")]
    public float sideRange = 1.5f;
    public float sideSpeed = 0.7f;

    [Header("Up Down Float")]
    public float floatHeight = 0.6f;
    public float floatSpeed = 1.2f;

    [Header("Distance Safety")]
    public float maxForward = -2.5f;  // BATAS PALING MAJU (jangan lebih dekat dari ini)
    public float maxBack = -6f;       // biar nggak terlalu jauh juga

    [Header("Fade Out Simple")]
    public float fadeSpeed = 1.2f;
    public float normalSpeed = 2f;
    public float slowSpeed = 0.6f;
    public float fadeStart = 2f;
   
    [Header("Fade Smooth")]
public float fadeDuration = 6f; // makin besar makin halus
float fadeTimer = 0f;


    float timer;
    bool fading;
    Material mat;
    Color color;


    float randomOffset;

    void Start()
    {
        basePos = transform.localPosition;
        randomOffset = Random.Range(0f, 100f);

        mat = GetComponentInChildren<Renderer>().material;
        color = mat.color;

    }

    void LateUpdate()
    {
            timer += Time.deltaTime;
        if (timer > fadeStart) fading = true;

        float side = Mathf.PerlinNoise(Time.time * sideSpeed, randomOffset) * 2f - 1f;
        float up = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        Vector3 target = basePos + new Vector3(side * sideRange, up, 0);
        target.z = Mathf.Clamp(target.z, maxBack, maxForward);

        // kalau fade, gerak diperlambat biar terlihat tertinggal
        float speed = fading ? slowSpeed : normalSpeed;
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * speed);

        // rotasi miring tetap
        float tilt = side * 15f;
        transform.localRotation = Quaternion.Euler(0, 0, -tilt);

        // fade
        if (fading)
        {
            fadeTimer += Time.deltaTime;

            float t = fadeTimer / fadeDuration;

            FindObjectOfType<WitchSmoke>().StopSmoke();

            // bikin turunnya pelan di awal, cepat di akhir
            t = Mathf.SmoothStep(0f, 1f, t);

            color.a = Mathf.Lerp(1f, 0f, t);
            mat.color = color;

            if (t >= 1f)
                gameObject.SetActive(false);
        }



    }
}


/*
using UnityEngine;

public class WitchHover : MonoBehaviour
{
    Vector3 basePos;

    [Header("Side Movement")]
    public float sideRange = 1.5f;      // seberapa jauh kiri kanan
    public float sideSpeed = 0.7f;

    [Header("Up Down Float")]
    public float floatHeight = 0.6f;
    public float floatSpeed = 1.2f;

    float randomOffset;

    void Start()
    {
        basePos = transform.localPosition;
        randomOffset = Random.Range(0f, 100f);
    }

    void LateUpdate()
    {
        float side = Mathf.PerlinNoise(Time.time * sideSpeed, randomOffset) * 2f - 1f;
        float up = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        Vector3 target = basePos + new Vector3(side * sideRange, up, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * 2f);
    }
}
*/

