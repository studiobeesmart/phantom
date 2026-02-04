using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 9f;
    public LayerMask groundMask;

    Rigidbody rb;
    Animator anim;
    //Collider col;
    private bool isSliding = false;
private float slideTimer = 0f;

public float slideDuration = 0.8f;
public float slideHeight = 1f;
public float normalHeight = 2f;

private float originalCenterY;
private float originalHeight;


private CapsuleCollider col;

    float horizontalInput;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        //col = GetComponent<Collider>();
        col = GetComponent<CapsuleCollider>();

        originalHeight = col.height;
        originalCenterY = col.center.y;
    }

   void StartSlide()
{
    isSliding = true;
    slideTimer = slideDuration;
    anim.SetBool("isSliding", true);

    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);

    float bottom = col.center.y - col.height / 2f;

    col.height = slideHeight;
    col.center = new Vector3(0, bottom + slideHeight / 2f, 0);
}

void StopSlide()
{
    isSliding = false;
    anim.SetBool("isSliding", false);

    col.height = originalHeight;
    col.center = new Vector3(0, originalCenterY, 0);

    // pastikan balik run
    anim.SetBool("isJumping", false);
}


    void Update()
{
    horizontalInput = Input.GetAxis("Horizontal");

    float height = col.bounds.size.y;
    bool wasGrounded = isGrounded;
    //isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);
    
Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;


isGrounded = Physics.Raycast(
    rayOrigin,
    Vector3.down,
    (height / 2) + 0.2f,
    groundMask,
    QueryTriggerInteraction.Ignore
);




    anim.SetBool("isGrounded", isGrounded);

    // SLIDE
    if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded && !isSliding)
    {
        StartSlide();
    }

    // SLIDE TIMER
    if (isSliding)
    {
        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0)
            StopSlide();
    }

    // LOMPAT
    if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

        anim.SetBool("isJumping", true);
        anim.SetBool("isFalling", false);
        anim.SetBool("isLanding", false);
    }

        // SAAT TURUN
        if (!isGrounded && rb.velocity.y < 0)
        {
            anim.SetBool("isFalling", true);
            anim.SetBool("isJumping", false);
        }

        // MOMEN MENDARAT
        if (!wasGrounded && isGrounded)
        {
            anim.SetBool("isLanding", true);
            anim.SetBool("isFalling", false);
            anim.SetBool("isJumping", false);
        }

        // RESET LANDING
        if (isGrounded && rb.velocity.y == 0)
        {
            anim.SetBool("isLanding", false);
        }
    }


    void FixedUpdate()
    {
        Vector3 move = transform.forward * speed;
        move += transform.right * horizontalInput * speed;

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }
}
