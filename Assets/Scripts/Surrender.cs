using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surrender : MonoBehaviour {

    public GameObject endGame;

    public GameObject playerIcon;

    public GameObject obj_Confirm;

    bool inSpace, confirmation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inSpace == true)
        {
            if (confirmation == false)
            {
                confirmation = true;
                obj_Confirm.SetActive(true);
            }
            else if (confirmation == true)
            {
                endGame.SetActive(true);
            }
        }
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
        confirmation = false;
        obj_Confirm.SetActive(false);
    }
}
