using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class storescript : MonoBehaviour
{
    public gamemanagerscript Manager;


    [SerializeField] int helperprice;
    [SerializeField] int strongerprice;
    [SerializeField] int biggerprice;

    [SerializeField] GameObject pickaxe;

    pickaxescript pickaxescript;
    TargetJoint2D target;
    SpriteRenderer renderer;

    [SerializeField] Sprite iron;
    [SerializeField] Sprite diamond;

    int pickaxelevel = 1;
    GameObject panel;
    [SerializeField] GameObject clonehelper;

    [SerializeField] Transform helperparent;

    TMP_Text helpertext;
    TMP_Text strongertext;
    TMP_Text biggertext;

    void Start()
    {
        panel = transform.Find("Panel").gameObject;
        helpertext = panel.transform.Find("helper").transform.Find("price").GetComponent<TMP_Text>();
        strongertext = panel.transform.Find("stronger").transform.Find("price").GetComponent<TMP_Text>();
        biggertext = panel.transform.Find("bigger").transform.Find("price").GetComponent<TMP_Text>();
        renderer = pickaxe.GetComponent<SpriteRenderer>();
        target = pickaxe.GetComponent<TargetJoint2D>();
        pickaxescript = pickaxe.GetComponent<pickaxescript>();

        helpertext.text = helperprice + " Cookies";
        strongertext.text = strongerprice + " Cookies";
        biggertext.text = biggerprice + " Cookies";
    }   

    public void StoreButton()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void HelperButton()
    {
        if (Manager.cookies >= helperprice)
        {
            Manager.cookies -= helperprice;
            Manager.UpdateCookies();
            helperprice *= 3;
            helpertext.text = helperprice + " Cookies";
            GameObject newhelper = Instantiate(clonehelper);
            newhelper.transform.SetParent(helperparent);
            newhelper.SetActive(true);
        }
    }

    public void StrongerButton()
    {
        if (Manager.cookies >= strongerprice)
        {
            Manager.cookies -= strongerprice;
            Manager.UpdateCookies();
            //strongerprice *= 3;
            pickaxelevel += 1;
            if (pickaxelevel == 2)
            {
                strongerprice = 20;
                renderer.sprite = iron;
                target.frequency = 1.5f;
                pickaxescript.checkradius = 0.6f;
                strongertext.text = strongerprice + " Cookies";
            }
            else if (pickaxelevel == 3)
            {
                renderer.sprite = diamond;
                target.frequency = 2f;
                pickaxescript.checkradius = 0.8f;
                strongerprice = 100000000;
                strongertext.text = "MAX";
            }
        }
    }


    public void Unstuck()
    {
        pickaxe.transform.position = new Vector2(7, 2);
    }
    public void BiggerButton()
    {
        //Debug.Log(Manager.cookies);
        if (Manager.cookies >= biggerprice)
        {
            Debug.Log(Manager.cookies);
            //SceneManager.LoadScene("EndScene");
        }
    }

    // Update is called once per frame
}
