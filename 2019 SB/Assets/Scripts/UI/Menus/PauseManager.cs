using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public GameObject UIPanel;
    public GameObject gm;
    public string mainMenu;
    public FloatValue gameSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //gameSpeed = 1f;
        isPaused = false;
        gm = GameObject.FindWithTag("GameManager");
        gameSpeed = gm.GetComponent<GameManager>().GameSpeed;
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
            ChangePause();
        }
        
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            UIPanel.SetActive(false);
            pausePanel.SetActive(true);
            gameSpeed.RuntimeValue = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            UIPanel.SetActive(true);
            gameSpeed.RuntimeValue = 1f;
        }

    }

    public void QuitToMainMenu()
    {
        Destroy(GameObject.FindWithTag("GameManager"));
        SceneManager.LoadScene(mainMenu);

    }

}
