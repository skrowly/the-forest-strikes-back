using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public static float globalDamageMultiplier = 1f;
    public static float globalSpeedMultiplier = 1f;

    public float baseDamage = 10f;
    public float baseSpeed = 3f;
    public float attackRange = 2f;
    public bool isJumper = false;
    public bool isShooter = false;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootCooldown = 2f;
    public float jumpForce = 5f;

    private float currentDamage;
    private float currentSpeed;
    private float nextShootTime;
    private Transform player;
    private Rigidbody rb;

    void Start()
    {
        currentDamage = baseDamage * globalDamageMultiplier;
        currentSpeed = baseSpeed * globalSpeedMultiplier;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player == null) return;
        float distance = Vector3.Distance(transform.position, player.position);
        transform.position = Vector3.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);
        transform.LookAt(player);

        if (isJumper && distance < attackRange) JumpAtPlayer();
        if (isShooter && Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    void JumpAtPlayer()
    {
        if (rb != null)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            rb.AddForce((dir + Vector3.up) * jumpForce, ForceMode.Impulse);
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    private float nextDamageTime = 0f;
    public float damageCooldown = 1f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(currentDamage * globalDamageMultiplier);
            nextDamageTime = Time.time + damageCooldown;
        }
    }



}
