using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public GameObject UIPanel;
    public string mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false; 
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
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            UIPanel.SetActive(true);
            Time.timeScale = 1f;
        }

    }

    public void QuitToMainMenu()
    {
        Destroy(GameObject.FindWithTag("GameManager"));
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;

    }

}
