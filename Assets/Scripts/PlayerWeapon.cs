using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform bulletSpawner;    // Punto desde donde salen las balas (asignar en inspector)
    public GameObject bulletPrefab;    // Prefab de la bala (asignar en inspector)

    public float projectileSpeed = 5f;     // Velocidad de la bala (ajusta a tu gusto)
    public float fireCooldown = 0.5f;      // Tiempo entre disparos (2 balas por segundo)
    private float lastFireTime;

    private SpriteRenderer spriteRenderer;
    private Vector2 lastDirection = Vector2.right;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        RotateTowardsArrowInput();

        // Disparar mientras alguna flecha esté presionada
        if (Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            TryFire();
        }
    }

    private void RotateTowardsArrowInput()
    {
        float hor = Input.GetAxisRaw("HorizontalArrows");  // Ejes configurados para flechas
        float ver = Input.GetAxisRaw("VerticalArrows");

        Vector2 inputDir = new Vector2(hor, ver);

        if (inputDir != Vector2.zero)
        {
            lastDirection = inputDir.normalized;
        }

        float angle = GetAngleFromDirection(lastDirection);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Voltea el sprite si apunta hacia atrás para que se vea bien
        spriteRenderer.flipY = angle > 90 && angle < 270;
    }

    private float GetAngleFromDirection(Vector2 dir)
    {
        // Calcula el ángulo real en grados para rotación libre
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    private void TryFire()
    {
        if (Time.time - lastFireTime >= fireCooldown)
        {
            FireProjectile();
            lastFireTime = Time.time;
        }
    }

    private void FireProjectile()
    {
        if (bulletPrefab == null || bulletSpawner == null)
        {
            Debug.LogWarning("Falta asignar bulletPrefab o bulletSpawner");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = lastDirection * projectileSpeed;

            // Rotar la bala para que apunte en la dirección del disparo
            float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            Debug.LogWarning("El prefab de bala no tiene Rigidbody2D");
        }
    }
}
