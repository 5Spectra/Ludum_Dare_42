using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Incinerator : MonoBehaviour {
    // 0=Plebe  1=Normal  2=Nobre  

    //scripts referencais
    [SerializeField]
    GameManager GM;

    [SerializeField]
    float showTime = 2;

    public GameObject playerIcon;
    public TMP_Text txtGetMoney;

    bool isSpawn, inSpace;

    int actualBody;

    void Start () {
        txtGetMoney.text = string.Empty;
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.E) && GM.isCarry == true && inSpace == true)
        {
            if (GM.Money > GM.bodyFire[GM.bodyAtual])
            {
                GM.Money -= GM.bodyFire[GM.bodyAtual];
                StartCoroutine(Show_inMoney(string.Format("{0} {1}", '-', GM.bodyFire[GM.bodyAtual])));
                GM.isCarry = false;
                GM.bodyAtual = -1;
                GM.Play_SFX(5);
            }
            else
            {
                StartCoroutine(Show_inMoney("Out of money"));
                GM.Play_SFX(6);
            }
        }
    }

    int get_money_death(int min, int max)
    {
        max += 1;
        int rand = Random.Range(min, max);

        return rand;
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
