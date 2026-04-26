
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerFPS : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 10f;
    public float jumpForce = 2.5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 3f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    [Header("Idle Sway")]
    public float swayAmount = 0.05f;
    public float swaySpeed = 1.5f;
    private Vector3 cameraStartPos;

    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Camera Shake")]
    public float shakeIntensity = 0.07f;
    public float shakeDuration = 0.15f;

    [Header("References")]
    public Camera playerCamera;
    public Canvas deathCanvas;
    public Text deathText;

    private float shakeTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        cameraStartPos = playerCamera.transform.localPosition;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        deathCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        Look();
        Move();
        IdleSway();
        CameraShake();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -2f;

            if (Input.GetKeyDown(KeyCode.Space))
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void IdleSway()
    {
        bool idle = Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0;

        if (idle)
        {
            float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
            playerCamera.transform.localPosition = cameraStartPos + Vector3.up * sway;
        }
        else
        {
            playerCamera.transform.localPosition = cameraStartPos;
        }
    }

    void CameraShake()
    {
        if (shakeTimer > 0)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
            playerCamera.transform.localPosition = cameraStartPos + shakeOffset;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            playerCamera.transform.localPosition = cameraStartPos;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        shakeTimer = shakeDuration;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        deathCanvas.gameObject.SetActive(true);
        deathText.text = "Get Good";
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
