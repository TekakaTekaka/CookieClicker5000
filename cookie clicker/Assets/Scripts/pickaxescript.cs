using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    TargetJoint2D hinge;
    Rigidbody2D body;
    Vector3 mousepos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello World");
        hinge = GetComponent<TargetJoint2D>();
        body = GetComponent<Rigidbody2D>();
        //body.centerOfMass = new Vector2(5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        //body.linearVelocity = new Vector3(0, 0, 0);
        
        hinge.target =  worldPos;//new Vector2(mousepos.x, mousepos.y)
        //body.centerOfMass = new Vector2(5, 5);
    }
}
