using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Covas : MonoBehaviour {
    // 0=Plebe, 1=Normal, 2=Nobre

    GameManager GM;
    Player playerOUT;
    GameObject playerIcon;

    public float showTime;

    public GameObject UIbar;
    public GameObject[] border = new GameObject[2];

    public TMP_Text txtGetMoney, txtGraveNum;

    bool inSpace, slcMode, slcOPC;

    [Tooltip("Classe social do corpo")]
    public int covaClasse;

    [Tooltip("Qual o preço inicial e por uso?")]
    public int prices, price_pUSE;
 
    int enterrados;

    int maxCap = 2;
    private int MaxCap {
        get { return maxCap; }
        set {
            maxCap = value;
            txtGraveNum.text = string.Format("{0} / {1}", enterrados, maxCap);
        }
    }


    void Start () {
        GM = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerOUT = GM.playerOUT.GetComponent<Player>();
        playerIcon = GM.playerIconOUT;

        UIbar.SetActive(false);
        StartCoroutine(Show_inMoney(string.Format("- {0}", GM.newCovas[covaClasse])));
        txtGraveNum.text = string.Format("{0} / {1}", enterrados, maxCap);
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.E) && inSpace == true && slcMode == false)
        {
            if (GM.isCarry == true && enterrados != maxCap && GM.bodyAtual == covaClasse)
            {
                int x = get_money_death(GM.bodyGold[GM.bodyAtual, 0], GM.bodyGold[GM.bodyAtual, 1]);

                StartCoroutine(Show_inMoney(string.Format("+ {0}", x)));

                enterrados += 1;
                txtGraveNum.text = string.Format("{0} / {1}", enterrados, maxCap);

                GM.Money += x;
                GM.BodyCount += 1;
                GM.isCarry = false;
                GM.bodyAtual = -1;

                GM.Play_SFX(3);
            }
            else
            {
                if (UIbar.activeInHierarchy == false)
                {
                    UIbar.SetActive(true);
                    playerOUT.enabled = false;
                    slc_border(false);
                    slcMode = true;
                }
            }
        }
        else if (slcMode == true) {

            if (Input.GetKeyDown(KeyCode.A)) {
                slc_border(true);
                txtGetMoney.text = string.Format("Cost: {0}", prices);
            }

            if (Input.GetKeyDown(KeyCode.D)){
                slc_border(false);
                txtGetMoney.text = string.Empty;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                bool buyed = false;

                if (slcOPC == true) {
                    if (GM.Money > prices)
                    {
                        StartCoroutine(Show_inMoney(string.Format("- {0}", prices)));
                        GM.Money -= prices;
                        prices += price_pUSE;
                        MaxCap += 1;
                        GM.Play_SFX(1);
                        buyed = true;
                    }
                    else
                        StartCoroutine(Show_inMoney("Out of money"));
                }
                else
                    txtGetMoney.text = string.Empty;

                UIbar.SetActive(false);
                playerOUT.enabled = true;
                slcMode = false;
                if (buyed == false) GM.Play_SFX(6);
            }
        }
    }

    void slc_border(bool tf) {
        border[0].SetActive(tf);
        border[1].SetActive(!tf);

        slcOPC = tf;
    }

    IEnumerator Show_inMoney(string texto) {

        txtGetMoney.text = texto;

        yield return new WaitForSeconds(showTime);

        txtGetMoney.text = string.Empty;
    }

    int get_money_death(int min, int max)
    {
        max += 1;
        int rand = Random.Range(min, max);

        return rand;
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
