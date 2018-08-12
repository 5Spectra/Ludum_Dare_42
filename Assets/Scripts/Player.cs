using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // 0=Plebe  1=Normal  2=Nobre 

    [SerializeField]
    GameManager GM;

    int carryingBody = -1;

    [SerializeField]
    Sprite[] carryingBody_Image = new Sprite[3];
    public SpriteRenderer body;
    SpriteRenderer selfRender;
    Animator anim;

    [SerializeField]
    float velocidade = 2;

    void Start () {
        body.enabled = false;
        selfRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
	
	void Update () {

        float speed = velocidade * Time.deltaTime;

        if (carryingBody != GM.bodyAtual) {
            carryingBody = GM.bodyAtual;

            //print(carryingBody);

            if (carryingBody == -1)
                body.enabled = false;
            else
                body.enabled = true;

            if (carryingBody == 0)
                body.sprite = carryingBody_Image[carryingBody];
            else if (carryingBody == 1)
                body.sprite = carryingBody_Image[carryingBody];
            else if (carryingBody == 2)
                body.sprite = carryingBody_Image[carryingBody];
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = 0;
        bool last = selfRender.flipX;

        if (GM.isOUT == true)
            y = Input.GetAxisRaw("Vertical");

        if (x > 0 && last == false)
            selfRender.flipX = true;
        else if (x < 0 && last == true)
            selfRender.flipX = false;

        if (x != 0 || y != 0)
        {
            anim.enabled = true;
        }
        else
        {
            anim.Play("Main", 0, 0f);
            transform.rotation = Quaternion.identity;
            anim.enabled = false;
        }

        transform.position += new Vector3(x, y, 0) * speed;
    }
}
