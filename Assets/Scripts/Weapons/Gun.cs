using UnityEngine;

public class Gun : WeaponBase
{
    public Camera cam;
    private LineRenderer lineRenderer;
    public float lineDuration = 0.05f;

    void Start() {
    lineRenderer = gameObject.AddComponent<LineRenderer>();
    lineRenderer.startWidth = 0.1f;    // thicker
    lineRenderer.endWidth = 0.05f;     // tapers at end
    lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    lineRenderer.startColor = Color.cyan;   // bright cyan
    lineRenderer.endColor = Color.white;    // fades to white
    lineRenderer.enabled = false;
}

    public override void Fire()
    {
        //only check fire rate not ammo - did not like reload part so took it off
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + stats.fireRate;

        Vector3 endPoint = cam.transform.position +
            cam.transform.forward * stats.range;

        if (Physics.Raycast(cam.transform.position,
            cam.transform.forward, out RaycastHit hit, stats.range))
        {
            if (hit.collider.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(stats.damage);
            }
            endPoint = hit.point;
        }

        StartCoroutine(ShowLine(cam.transform.position, endPoint));
    }

    System.Collections.IEnumerator ShowLine(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(lineDuration);
        lineRenderer.enabled = false;
    }

    public override void Reload()
    {
        //infinite ammo nothing needed here - took this off
    }
}