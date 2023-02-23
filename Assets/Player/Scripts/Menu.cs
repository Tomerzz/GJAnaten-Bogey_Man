using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject canvasPause;
    public Slider soundSlider;
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        canvasPause.SetActive(false);

        soundSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Exit"))
        {
            PauseGame();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        canvasPause.SetActive(true);
    }

    public void ResumeGame()
    {
        canvasPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void SoundModif()
    {
        PlayerPrefs.SetFloat("Volume", soundSlider.value);
        mixer.SetFloat("Volume", Mathf.Log10(soundSlider.value) * 20);
    }
}
