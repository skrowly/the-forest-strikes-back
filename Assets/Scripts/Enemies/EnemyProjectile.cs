using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 20f;
    public float lifetime = 3f;

    private Transform player;

    void Start()
    {
        // destroy bullet after lifetime
        Destroy(gameObject, lifetime);

        // find player to aim at
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            // aim at player when spawned
            transform.LookAt(player);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}