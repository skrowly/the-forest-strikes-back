
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float health = 50f;
    public float moveSpeed = 2.5f;
    public float damage = 15f;

    [Header("Jump Attack")]
    public float jumpForce = 7f;
    public float forwardForce = 5f;
    public float jumpRange = 5f;

    [Header("Death Spin")]
    public float spinSpeed = 720f;
    public float deathDelay = 1f;

    private Transform player;
    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isRecovering = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player == null || isRecovering) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > jumpRange)
        {
            WalkTowardPlayer();
        }
        else if (isGrounded)
        {
            JumpAtPlayer();
        }
    }

    void WalkTowardPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;

        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    void JumpAtPlayer()
    {
        isGrounded = false;
        isRecovering = true;

        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0f;

        rb.AddForce(dir * forwardForce + Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Invoke(nameof(EndRecovery), 3f);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerFPS>()?.TakeDamage(damage);
        }
    }

    void EndRecovery()
    {
        isRecovering = false;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
            StartCoroutine(DeathSpin());
    }

    System.Collections.IEnumerator DeathSpin()
    {
        rb.isKinematic = true;
        float timer = 0f;

        while (timer < deathDelay)
        {
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
