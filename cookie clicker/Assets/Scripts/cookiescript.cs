using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class cookiescript : MonoBehaviour
{

    bool fullyDestroyed = false;

    [SerializeField] gamemanagerscript Manager;

    GameObject particlefolder;
    TMP_Text cookieText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particlefolder = Manager.particlefolder;
        cookieText = Manager.cookieText;
        //clone.transform.position = transform.position;
    }

    void ResetParticle(Transform child)
    {
        Rigidbody2D hitbody = child.gameObject.GetComponent<Rigidbody2D>();
        child.gameObject.layer = LayerMask.NameToLayer("Default");
        hitbody.simulated = true;
        SpriteRenderer sr = child.gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
    }

    IEnumerator FinishCookie()
    {
        yield return new WaitForSeconds(1f);
        List<Transform> childlist = new List<Transform>();

        foreach (Transform child in particlefolder.transform)
        {
            ResetParticle(child);
            childlist.Add(child);
        }
        foreach (Transform child in transform)
        {
            ResetParticle(child);
            childlist.Add(child);
        }

        foreach (Transform child in childlist)
        {
            child.SetParent(transform, true);
        }
        fullyDestroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0 && fullyDestroyed == false)
        {
            Debug.Log("destroyed");
            fullyDestroyed = true;
            Manager.cookies += 1f;
            Manager.UpdateCookies();

            StartCoroutine(FinishCookie());
        }

    }
}
