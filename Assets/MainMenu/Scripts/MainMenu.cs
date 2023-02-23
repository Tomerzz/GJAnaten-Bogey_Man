using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panelSettings;
    public GameObject panelCredits;

    public GameObject light;

    // Start is called before the first frame update
    void Start()
    {
        panelCredits.SetActive(false);
        panelSettings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        light.SetActive(false);
        SceneManager.LoadScene("TutorialScene");
    }

    public void ClosePanel()
    {
        panelSettings.SetActive(false);
        panelCredits.SetActive(false);
    }

    public void OpenSettings()
    {
        panelCredits.SetActive(false);
        panelSettings.SetActive(true);
    }

    public void OpenCredits()
    {
        panelSettings.SetActive(false);
        panelCredits.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
