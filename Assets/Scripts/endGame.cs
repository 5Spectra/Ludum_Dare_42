using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour {

    public GameObject cam;
    public TMP_Text E_txt, R_txt;

    public GameObject[] Destroer;

    float piscaTime = 1f;

    public float speed = 5f;

	void OnEnable () {

        for (int i = 0; i < Destroer.Length; i++)
            Destroy(Destroer[i]);

        cam.SetActive(true);
        StartCoroutine(pisca_pisca(E_txt,'E', "go to menu"));
        StartCoroutine(pisca_pisca(R_txt,'R', "try again"));
    }

    void Update()
    {
        float move = speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
        {
            if (cam.transform.position.y > -35.1f)
                cam.transform.position += Vector3.down * move;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (cam.transform.position.y < 22)
                cam.transform.position += Vector3.up * move;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Menu");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }
    }

    IEnumerator pisca_pisca(TMP_Text txt, char letra, string end) {
        float mutate;

        while (true)
        {
            piscaTime = 1.2f;
            mutate = Random.Range(.8f, 1);
            piscaTime *= mutate; 

            txt.text = string.Empty;

            yield return new WaitForSeconds(piscaTime);

            txt.text = string.Format ("Press {0} to {1}", letra, end);

            yield return new WaitForSeconds(piscaTime);
        }
    }
}
