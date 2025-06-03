using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Animator animator;
    public float speed;
    private Rigidbody2D rb;
    private Vector3 velocity;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float hor = 0f;
        float ver = 0f;
        if (Input.GetKey(KeyCode.A)) hor = -1f;
        else if (Input.GetKey(KeyCode.D)) hor = 1f;

        if (Input.GetKey(KeyCode.W)) ver = 1f;
        else if (Input.GetKey(KeyCode.S)) ver = -1f;

        if (hor != 0 || ver != 0)
        {
            animator.SetFloat("Horizontal", hor);
            animator.SetFloat("Vertical", ver);
            animator.SetFloat("Speed", 1);

            Vector3 direction = (Vector3.up * ver + Vector3.right * hor).normalized;
            velocity = direction * speed;
        }
        else
        {
            animator.SetFloat("Speed", 0);
            velocity = Vector3.zero;
        }
    }   

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }
}
