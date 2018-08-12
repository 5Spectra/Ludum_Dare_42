using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour {
    // 0=Plebe  1=Normal  2=Nobre  

    public int bodyAtual = -1;

    public int bodyforreputation = 6;

    private int reputation;
    public int Reputation
    {
        get { return reputation; }
        set
        {
            reputation = value;
            txtReputation.text = string.Format("Reputation: {0}", reputation);
        }
    }

    private int bodyCount;
    public int BodyCount
    {
        get { return bodyCount; }
        set
        {
            bodyCount = value;
            txtBody.text = string.Format("Gravestones: {0}", bodyCount);
            bodyChange();
        }
    }

    private int money = 100;
    public int Money {
        get { return money; }
        set {
            money = value;
            txtMoney.text = string.Format("Money: {0}", money);
        }
    }

    private float tempo;
    public float Tempo
    {
        get { return tempo; }
        set
        {
            tempo = value;
            txtTimer.text = string.Format("Time: {0}", tempo.ToString("#0.0"));
        }
    }

    public bool isCarry;
    public bool isOUT;

    public GameObject playerIN, playerOUT;
    public GameObject playerIconOUT;

    public GameObject menuPause;
    public GameObject[] pause_confirm = new GameObject[2];

    public TMP_Text txtMoney, txtBody, txtReputation, txtTimer;

    public bool start;

    public GameObject endGame;
    public GameObject controlsShow;

    public EventSystem eventos;
    public GameObject firstPause;

    bool isGamePaused;

    //Game Preços Gerais para fácil acesso
    [HideInInspector]
    public int[] newCovas = { 45, 90, 160 };//plebe | normal | nobre

    [HideInInspector]
    public int[] bodyFire = { 22, 45, 80 };//plebe | normal | nobre

    public int[,] bodyGold =   {   //min max
                            { 15, 30 } , // plebe
                            { 35, 55 } , // normal
                            { 60, 100} // nobre
                        };

    public int[,] repRate = {
                                { 100, 101}, //0
                                { 80, 95}, //1
                                { 70, 90}, //2
                                { 60, 85}, //3
                                { 50, 80}, //4
                                { 45, 80}, //5
                                { 40, 75}, //6
                                { 35, 75}, //7
                                { 30, 70}, //8
                                { 25, 65}, //9
                                { 20, 60} //10
    };

    public AudioClip[] loopMusics;
    public AudioClip[] soundEffects;
    /* 
    0= Contruir cova (som de batida de martelo)
    1= Ampliar cova (som de cavar?)
    2= Pegar Corpo (som de ziper)
    3= Colocar corpo na cova (som de algo caindo)
    4= Abrir fechar porta (som de porta rangendo / batida na madeira)
    5= Som de incinerar corpo ("fumm")
    6= Cancelar
    */

    public AudioSource music, sfx;
    float audioTamanho;
    int currentLoop = 0;

    public void Play_SFX(int sound) {
            sfx.clip = soundEffects[sound];
            sfx.Play();
    }

    void Start()
    {
        StartCoroutine(LoopMusics());
    }

    private void Update()
    {
        if (start == true)
        {
            if (Tempo <= 0)
                endGame.SetActive(true);
            else
                Tempo -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q))
            Money += 100;
        if (Input.GetKeyDown(KeyCode.Q))
            BodyCount += bodyforreputation;

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused == false)
                Pause();
            else
                Resume();
        }
    }

    IEnumerator LoopMusics()
    {
        while (true)
        {
            music.clip = loopMusics[currentLoop];
            audioTamanho = loopMusics[currentLoop].length;
            music.Play();
            yield return new WaitUntil(() => audioTamanho == music.time);
            currentLoop += 1;
            if (currentLoop > loopMusics.Length - 1)
                currentLoop = 0;
        }
    }

    void bodyChange() {

        for (int i = bodyforreputation; i < (bodyforreputation * 10 + 1); i += bodyforreputation)
        {
            if (BodyCount >= i) {
                Reputation = Mathf.FloorToInt(i / bodyforreputation);
            }
        }
    }

    public void Resume()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        menuPause.SetActive(false);
        pause_confirm[0].SetActive(false);
        pause_confirm[1].SetActive(false);
        controlsShow.SetActive(false);
    }

    public void Pause()
    {
        eventos.SetSelectedGameObject(firstPause, null);
        isGamePaused = true;
        Time.timeScale = 0f;
        menuPause.SetActive(true);
    }

    public void Change_Scene(string cena)
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(cena);
    }

    public void sair()
    {
        Application.Quit();
    }
}
