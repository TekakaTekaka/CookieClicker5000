using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.ReloadAttribute;

public class mousehelperscript : MonoBehaviour
{

    TargetJoint2D hinge;
    Rigidbody2D body;

    float timebetween = 0;

    [SerializeField] gamemanagerscript Manager;
    [SerializeField] GameObject pickaxe;


    pickaxescript pickaxescript;

    float destroyvelocity = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickaxescript = pickaxe.GetComponent<pickaxescript>();
        hinge = GetComponent<TargetJoint2D>();
        body = GetComponent<Rigidbody2D>();
        hinge.target = Manager.cookie.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        timebetween += Time.deltaTime;
        Debug.Log(timebetween);
        if (timebetween > 3f)
        {
            timebetween = 0f;
        }
        else if (timebetween > 0.2f)
        {
            hinge.enabled = false;
        }
        else
        {
            hinge.target = Manager.cookie.transform.position;
            hinge.enabled = true;
        }
        bool gothit = false;
        foreach (Transform child in transform)
        {
            //cooldown = true;
            //Debug.Log("Child Name: " + child.gameObject.name);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(child.transform.position, 0.6f);

            foreach (Collider2D hit in hitColliders)
            {
                if (hit.gameObject.transform.parent && hit.gameObject.transform.parent.name == "cookie" && (body.linearVelocity.magnitude > destroyvelocity || body.angularVelocity / 50 > destroyvelocity || body.angularVelocity / 50 < -destroyvelocity))
                {
                    gothit = true;
                    Debug.Log("go");
                    //Debug.Log(body.angularVelocity);
                    //hit.gameObject.transform.position = new Vector2(10, 10);
                    StartCoroutine(pickaxescript.NewParticle(hit.gameObject));
                }
            }
        }
        if (gothit)
        {
            foreach (Transform child in Manager.cookie.transform)
            {
                //bool connected = false;
                int totalconnected = 0;
                Collider2D[] hitColliders = Physics2D.OverlapBoxAll(child.transform.position, new Vector2(0.2f, 0.2f), 0f);

                foreach (Collider2D hit in hitColliders)
                {
                    if (hit.gameObject.transform.parent && hit.gameObject.transform.parent.name == "cookie")
                    {
                        totalconnected += 1;
                        //connected = true;
                    }
                }
                Debug.Log(totalconnected);
                if (totalconnected < 3)
                {
                    StartCoroutine(pickaxescript.NewParticle(child.gameObject));
                }
            }
        }
    }
}
