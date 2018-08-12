using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void Change_Scene(string cena) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(cena);
    }

    public void sair() {
        Application.Quit();
    }
}
