using UnityEngine;
using UnityEngine.InputSystem;

public class Drag2DObject : MonoBehaviour
{
    public float followSpeed = 10f;  // Speed at which the object follows the mouse
    private Rigidbody2D rb;
    private Vector3 offset;
    private bool dragging;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            // Add Rigidbody2D if not present
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Prevent falling
            rb.linearDamping = 5f; // Smooth deceleration
        }
    }

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0; // Keep object in 2D plane

        if (dragging)
        {
            // Move the object smoothly toward the mouse position
            Vector3 desiredPosition = mouseWorldPos + offset;
            rb.MovePosition(Vector3.Lerp(rb.position, desiredPosition, followSpeed * Time.deltaTime));
        }
    }

    void OnMouseDown()
    {
        dragging = true;
        // Calculate offset to keep relative mouse-object position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0;
        offset = transform.position - mouseWorldPos;
    }

    void OnMouseUp()
    {
        dragging = false;
    }
}