using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    public float damage = 20f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent(out EnemyHealth enemy))
        //{
            ////enemy.TakeDamage(damage);
        //}

        //Destroy(gameObject);
    }
}
