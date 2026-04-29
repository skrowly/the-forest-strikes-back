using UnityEngine;
using UnityEngine.AI;
using System.Collections;

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

    [Header("Jump Settings")]
    public float jumpForce = 2f;
    public float jumpCooldown = 3f;
    public float minJumpDistance = 1.5f;  // minimum distance before jumping
    public float maxJumpDistance = 4f;    // maximum distance to trigger jump

    private float currentDamage;
    private float currentSpeed;
    private float nextShootTime;
    private float nextJumpTime;
    private float nextDamageTime;
    public float damageCooldown = 1f;

    private Transform player;
    private Rigidbody rb;
    private NavMeshAgent agent;

    void Start()
    {
        currentDamage = baseDamage * globalDamageMultiplier;
        currentSpeed = baseSpeed * globalSpeedMultiplier;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
            agent.speed = currentSpeed;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // move toward player using NavMesh
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
            agent.speed = currentSpeed;
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                currentSpeed * Time.deltaTime
            );
        }

        transform.LookAt(player);

        // only jump if within correct distance range
        // minJumpDistance prevents jumping when too close (on head)
        // maxJumpDistance prevents jumping from too far away
        if (isJumper &&
            distance > minJumpDistance &&
            distance < maxJumpDistance &&
            Time.time >= nextJumpTime)
        {
            JumpAtPlayer();
            nextJumpTime = Time.time + jumpCooldown;
        }

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
            if (agent != null) agent.enabled = false;
            rb.linearVelocity = Vector3.zero;

            Vector3 dir = (player.position - transform.position).normalized;

            //lower arc jump - more horizontal than vertical - to help avoid enemy on head issue that we have been having
            Vector3 jumpDirection = dir + Vector3.up * 0.5f;
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);

            Invoke("ReEnableAgent", 1.5f);
        }
    }

    void ReEnableAgent()
    {
        if (agent != null) agent.enabled = true;
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            firePoint.LookAt(player);
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            nextShootTime = Time.time + shootCooldown;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            Time.time >= nextDamageTime)
        {
            collision.gameObject.GetComponent<PlayerHealth>()
                ?.TakeDamage(currentDamage * globalDamageMultiplier);
            nextDamageTime = Time.time + damageCooldown;
        }
    }
}
