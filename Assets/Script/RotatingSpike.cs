using UnityEngine;

public class SpikeRoller : MonoBehaviour
{
    public float RPM = 120f;

    void Update()
    {
        float degPerSec = RPM * 360f / 60f;
        transform.Rotate(Vector3.up * degPerSec * Time.deltaTime, Space.Self);
    }

}
