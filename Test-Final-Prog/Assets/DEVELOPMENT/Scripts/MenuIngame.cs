using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuIngame : MonoBehaviour
{
    public Slider Vol;
    void Start() => Vol.value = PlayerPrefs.GetFloat("vol", 1);

    public void restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }
    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void ChangeVolume()
    {
        AudioListener.volume = Vol.value;
        PlayerPrefs.SetFloat("vol", Vol.value);
    }
}
