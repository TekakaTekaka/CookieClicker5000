using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

public class pickaxescript : MonoBehaviour
{
    TargetJoint2D hinge;
    Rigidbody2D body;

    public float checkradius = 0.5f;

    [SerializeField] gamemanagerscript Manager;

    //bool cooldown = false;
    readonly float destroyvelocity = 20f;
    Vector3 mousepos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hinge = GetComponent<TargetJoint2D>();
        body = GetComponent<Rigidbody2D>();
        //body.centerOfMass = new Vector2(5, 5);
    }

    public void ResetParticle(GameObject particle, SpriteRenderer sr, Rigidbody2D hitbody, Color oldcolor, Vector2 oldpos, quaternion oldrot)
    {
        sr.color = new Color(oldcolor.r, oldcolor.g, oldcolor.b, 0f);
        hitbody.bodyType = RigidbodyType2D.Static;
        hitbody.simulated = false;
        particle.transform.position = oldpos;
        particle.transform.rotation = oldrot;
    }

    public IEnumerator NewParticle(GameObject particle)
    {
        Vector2 oldpos = particle.transform.position;
        quaternion oldrot = particle.transform.rotation;
        Rigidbody2D hitbody = particle.GetComponent<Rigidbody2D>();
        SpriteRenderer sr = particle.GetComponent<SpriteRenderer>();
        Color oldcolor = sr.color;

        if (hitbody.bodyType == RigidbodyType2D.Dynamic)
        {
            yield break;
        }

        particle.transform.SetParent(Manager.particlefolder.transform, true);
        hitbody.bodyType = RigidbodyType2D.Dynamic;
        //hit.gameObject.layer = LayerMask.NameToLayer("cookie");
        hitbody.linearVelocity = new Vector2(UnityEngine.Random.Range(10, 20), UnityEngine.Random.Range(10, 20));

        for (int i = 0; i < 5; i++)
        {
            if (i == 1)
            {
                particle.gameObject.layer = LayerMask.NameToLayer("cookie");
            }
            yield return new WaitForSeconds(0.5f);
            if (Manager.cookie.transform.childCount == 0)
            {
                ResetParticle(particle, sr, hitbody, oldcolor, oldpos, oldrot);
                yield break;
            }
        }

        var trans = 1f;
        sr.color = new Color(1f, 1f, 1f, trans);
        while (trans > 0f)
        {
            trans -= 0.1f;
            if (trans < 0f)
            {
                trans = 0f;
            }
            sr.color = new Color(1f, 1f, 1f, trans);
            yield return new WaitForSeconds(0.01f);
        }
        ResetParticle(particle, sr, hitbody, oldcolor, oldpos, oldrot);
        //Destroy(particle);
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(body.angularVelocity / 50);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        //body.linearVelocity = new Vector3(0, 0, 0);
        
        hinge.target =  worldPos;//new Vector2(mousepos.x, mousepos.y)
        //body.centerOfMass = new Vector2(5, 5);
        //if (cooldown)
        //{
        //    cooldown = false;
        //    return;
        //}
        bool gothit = false;
        foreach (Transform child in transform)
        {
            //cooldown = true;
            //Debug.Log("Child Name: " + child.gameObject.name);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(child.transform.position, checkradius);

            foreach (Collider2D hit in hitColliders)
            {
                if (hit.gameObject.transform.parent && hit.gameObject.transform.parent.name == "cookie" && (body.linearVelocity.magnitude > destroyvelocity || body.angularVelocity / 50 > destroyvelocity || body.angularVelocity / 50 < -destroyvelocity))
                {
                    gothit = true;
                    //Debug.Log(body.angularVelocity);
                    //hit.gameObject.transform.position = new Vector2(10, 10);
                    StartCoroutine(NewParticle(hit.gameObject));
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
                //Debug.Log(totalconnected);
                if (totalconnected < 3 || Manager.cookie.transform.childCount < 6)
                {
                    StartCoroutine(NewParticle(child.gameObject));
                }
            }
        }

    }
}
