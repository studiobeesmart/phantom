
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

        // Batasi Z supaya tidak maju ke depan player
        target.z = Mathf.Clamp(target.z, maxBack, maxForward);

        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * 2f);

        // miring saat bergerak
        float tilt = side * 15f;
        transform.localRotation = Quaternion.Euler(0, 0, -tilt);
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

