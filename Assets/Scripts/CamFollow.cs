using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    public Transform alvo;

    public float Speed;

    Vector3 position = Vector3.zero;

    void FixedUpdate()
    {
        //float x = alvo.transform.localPosition.x;
        float y = alvo.transform.localPosition.y;

        //if (x < -11     || x > 11      || 
        if (y > 35.5f   || y < -38 )return;

        position = Vector3.Lerp(transform.position, alvo.position, Speed * Time.deltaTime);

        position.z = -10f;

        transform.position = position;
    }
}
