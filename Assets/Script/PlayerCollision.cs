using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement movement;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>(); 
        movement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       if ((collision.gameObject.CompareTag("Obstacle") ||
         collision.gameObject.CompareTag("Spike")) && !isDead)
    {
        isDead = true;
        movement.enabled = false;
        anim.SetBool("isDie", true);
        StartCoroutine(RestartAfterDelay());
    }
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
