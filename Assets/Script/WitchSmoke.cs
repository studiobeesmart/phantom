using UnityEngine;

public class WitchSmoke : MonoBehaviour
{
   public Transform witch;
    ParticleSystem ps;
    bool stopped = false;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void LateUpdate()
    {
        if (witch == null) return;

        // tetap ikuti Witch
        transform.position = witch.position + new Vector3(0, 1f, 0);
    }

    // dipanggil saat Witch mulai hilang
    public void StopSmoke()
    {
        if (!stopped)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            stopped = true;
        }
    }
}