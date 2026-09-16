using TMPro;
using UnityEngine;

public class gamemanagerscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float cookies;

    public TMP_Text cookieText;

    public GameObject cookie;

    public GameObject particlefolder;

    public void UpdateCookies()
    {
        cookieText.text = "Cookies: " + Mathf.Round(cookies);
    }

    void Start()
    {
        UpdateCookies();
    }

    // Update is called once per frame
}
