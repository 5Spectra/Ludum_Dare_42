using UnityEngine;
using System.Collections;

public class BodyDoor : MonoBehaviour {

    [SerializeField]
    GameManager GM;

    [SerializeField]
    float waitTime = 1f;

    public GameObject camIN, camOUT;
    public GameObject PortaIN, PortaOUT;
    GameObject PlayerIN, PlayerOUT;

    [Tooltip("Está indo de dentro para fora?")]
    public bool INtoOUT;

    bool inSpace, wait_next;

    public GameObject playerIcon;

    void Update()
    {
        PlayerIN = GM.playerIN;
        PlayerOUT = GM.playerOUT;

        if (Input.GetKeyDown(KeyCode.E) && inSpace == true)
        {
            if (wait_next == false)
            {
                StartCoroutine(Wait_seconds(waitTime));
            }
        }
    }

    IEnumerator Wait_seconds(float sec) {
        wait_next = true;
        GM.Play_SFX(4);
        yield return new WaitForSeconds(sec);

        set_IN(!INtoOUT);
        set_OUT(INtoOUT);
        GM.isOUT = INtoOUT;

        wait_next = false;
    }

    void set_IN(bool tf)
    {
        PlayerIN.SetActive(tf);
        camIN.SetActive(tf);
        PortaIN.SetActive(tf);
    }

    void set_OUT(bool tf)
    {
        PlayerOUT.SetActive(tf);
        camOUT.SetActive(tf);
        PortaOUT.SetActive(tf);
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
