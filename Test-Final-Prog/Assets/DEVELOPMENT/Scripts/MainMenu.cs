using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Slider Vol;
    public void Jugarbtn()
    {
        SceneManager.LoadScene("Game");
    }
    public void Salirbtn()
    {
        Application.Quit();
    }
    public void ChangeVolume()
    {
        AudioListener.volume = Vol.value;
        PlayerPrefs.SetFloat("vol", Vol.value);

    }
}
