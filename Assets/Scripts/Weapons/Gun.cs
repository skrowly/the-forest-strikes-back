using UnityEngine;
using System.Collections;

public class Gun : WeaponBase
{
    public Camera cam;
    private LineRenderer lineRenderer;
    public float lineDuration = 0.05f;

    [Header("Impact Effects")]
    public GameObject bloodEffectPrefab;   // for hitting enemies
    public GameObject sparkEffectPrefab;   // for hitting terrain/objects

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.cyan;
        lineRenderer.endColor = Color.white;
        lineRenderer.enabled = false;
    }

    public override void Fire()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + stats.fireRate;

        Vector3 endPoint = cam.transform.position +
            cam.transform.forward * stats.range;

        if (Physics.Raycast(cam.transform.position,
            cam.transform.forward, out RaycastHit hit, stats.range))
        {
            endPoint = hit.point;

            // check what was hit
            if (hit.collider.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(stats.damage);
                // spawn blood effect on enemy hit
                SpawnImpactEffect(bloodEffectPrefab, hit.point, hit.normal);
            }
            else
            {
                // spawn spark effect on terrain/object hit
                SpawnImpactEffect(sparkEffectPrefab, hit.point, hit.normal);
            }
        }

        StartCoroutine(ShowLine(cam.transform.position, endPoint));
    }

    void SpawnImpactEffect(GameObject effectPrefab, Vector3 point, Vector3 normal)
    {
        if (effectPrefab != null)
        {
            // spawn facing away from surface
            GameObject effect = Instantiate(
                effectPrefab,
                point,
                Quaternion.LookRotation(normal)
            );
            Destroy(effect, 0.5f);
        }
        else
        {
            // fallback if no prefab assigned
            StartCoroutine(FallbackImpactFlash(point));
        }
    }

    IEnumerator FallbackImpactFlash(Vector3 point)
    {
        GameObject flash = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        flash.transform.position = point;
        flash.transform.localScale = Vector3.one * 0.15f;
        Destroy(flash.GetComponent<Collider>());

        Renderer rend = flash.GetComponent<Renderer>();
        rend.material = new Material(Shader.Find("Sprites/Default"));
        rend.material.color = Color.red;

        yield return new WaitForSeconds(0.05f);
        Destroy(flash);
    }

    IEnumerator ShowLine(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(lineDuration);
        lineRenderer.enabled = false;
    }

    public override void Reload()
    {
        // infinite ammo nothing needed here
    }
}