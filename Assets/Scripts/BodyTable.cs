using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTable : MonoBehaviour {
    // 0=Plebe  1=Normal  2=Nobre  

    //scripts referencais
    [SerializeField]
    GameManager GM;

    public GameObject playerIcon, spawnPoint;

    bool isSpawn, inSpace;

    int actualBody;

    //locais pegas
    [SerializeField]
    GameObject[] bodyPreFab = new GameObject[3];
    GameObject spawned;

    //variaveis chances
    int[] porcent = new int[2];

    void Update()
    {

        if (isSpawn == false && GM.isCarry == false)
        {
            actualBody = chose_rng_Body();
            spawned = Instantiate(bodyPreFab[actualBody], spawnPoint.transform.position, Quaternion.identity, transform.parent);
            isSpawn = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && isSpawn == true && GM.isCarry == false && inSpace == true)
        {
            GM.bodyAtual = actualBody;
            GM.isCarry = true;
            isSpawn = false;
            Destroy(spawned);

            if (GM.start == false)
            {
                GM.start = true;
                GM.controlsShow.SetActive(false);
            }

            GM.Play_SFX(2);
            GM.Tempo = 30;
        }

    }

    int chose_rng_Body()
    {
        int x = GM.Reputation;

        porcent[0] = GM.repRate[x, 0];
        porcent[1] = GM.repRate[x, 1];

        int rand = Random.Range(0, 101);

        //print(porcent[0] + " " + porcent[1] + " "+ " " + rand);

        if (rand <= porcent[0])
            rand = 0;
        else if (rand > porcent[0] && rand <= porcent[1])
            rand = 1;
        else if (rand > porcent[1])
            rand = 2;
        else
            print(string.Format("erro{0}", rand));

        return rand;
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (playerIcon.activeInHierarchy == false && GM.isCarry == false)
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
