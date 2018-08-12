using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class createCova : MonoBehaviour {

    GameManager GM;
    Player playerOUT;

    public int numTerreno;
    public SpriteRenderer spriteR;
    [Tooltip("O sprite 4 é o padrao")]
    public Sprite[] sprites = new Sprite[4];

    public float showTime = 2;

    public GameObject terainPreFab;
    public GameObject[] covaPreFab = new GameObject[3];

    public GameObject UIbar;
    public GameObject[] borders = new GameObject[4];
    GameObject playerIcon;

    public TMP_Text txtGetMoney;

    public int distCovas = 8; 

    bool inSpace, slcMode, buyed; 
    int slcOPC;

    void Start () {
        txtGetMoney.text = string.Empty;
        UIbar.SetActive(false);

        GM = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerOUT = GM.playerOUT.GetComponent<Player>();
        playerIcon = GM.playerIconOUT;
    }

    void Update () {

        if (inSpace == true)
        if (Input.GetKeyDown(KeyCode.E) && slcMode == false)
        {
                UIbar.SetActive(true);
                playerOUT.enabled = false;
                slcOPC = 4;
                slc_boarder();
                slcMode = true;
        }
        else if (slcMode == true)
        {
            if (Input.GetKeyDown(KeyCode.A)) {
                if (slcOPC > 1){
                    slcOPC--;
                    slc_boarder();
                }                   
            }

            if (Input.GetKeyDown(KeyCode.D)) {
                if (slcOPC < 4) {
                    slcOPC++;
                    slc_boarder();
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (slcOPC != 4)
                {
                        if (GM.Money >= GM.newCovas[slcOPC - 1])
                        {
                            StartCoroutine(Show_inMoney(string.Format("- {0}", GM.newCovas[slcOPC - 1])));
                            GM.Money -= GM.newCovas[slcOPC - 1];
                            buyed = true;
                        }
                        else
                        {
                            StartCoroutine(Show_inMoney("Out of money"));
                            spriteR.sprite = sprites[3];
                        }
                }
                else
                   txtGetMoney.text = string.Empty;

                UIbar.SetActive(false);
                playerOUT.enabled = true;
                slcMode = false;

                if (buyed == true)
                {
                    GM.Play_SFX(0);

                    GameObject ints = Instantiate(covaPreFab[slcOPC-1], transform.position, Quaternion.identity, transform.parent);
                    ints.name = string.Format("Covas_{0}", numTerreno);

                    if ((numTerreno + 3) < 21) {
                        ints = Instantiate(terainPreFab, new Vector3(transform.position.x, transform.position.y - distCovas, 0), Quaternion.identity, transform.parent);
                        ints.name = string.Format("Terain_{0}", numTerreno + 3); ;
                        createCova cC = ints.GetComponent<createCova>();
                        cC.numTerreno = numTerreno + 3;
                        cC.terainPreFab = terainPreFab;
                    }

                    Destroy(gameObject);
                }
                else
                    GM.Play_SFX(6);
             }
        }
    }

    void slc_boarder()
    {
        for (int i = 1; i < 5; i++)
        {
            if (slcOPC == i) {
                borders[i - 1].SetActive(true);

                spriteR.sprite = sprites[i - 1];

                if (i == 4)
                    txtGetMoney.text = string.Empty;
                else
                    txtGetMoney.text = string.Format("Cost: {0}", GM.newCovas[slcOPC - 1]);
            }
            else
                borders[i - 1].SetActive(false);
        }
    }

    IEnumerator Show_inMoney(string texto)
    {

        txtGetMoney.text = texto;

        yield return new WaitForSeconds(showTime);

        txtGetMoney.text = string.Empty;
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (playerIcon.activeInHierarchy == false)
            playerIcon.SetActive(true);
        inSpace = true;
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (playerIcon.activeInHierarchy == true)
            playerIcon.SetActive(false);
        inSpace = false;
    }
}
