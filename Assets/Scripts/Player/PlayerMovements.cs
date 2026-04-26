using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");  // A/D or Left/Right
        float v = Input.GetAxis("Vertical");    // W/S or Up/Down
        Vector3 move = transform.right * h + transform.forward * v;
        controller.Move(move * walkSpeed * Time.deltaTime);
    }
}
