using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] private Text colleccionables;
    [SerializeField] public GameObject menu;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject pause;
    public AudioSource reproductor;
    public AudioClip sonido;
    public int coltotales = 0;
    public int colEncontrados = 0;
    void Start()
    {
        colleccionables.text = colEncontrados + "/" + coltotales;
        menu.SetActive(false);
        win.SetActive(false);
        lose.SetActive(false);
        pause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
            pause.SetActive(!pause.activeSelf);
            Time.timeScale = (menu.activeSelf) ? 0 : 1;
            Cursor.lockState = (menu.activeSelf) ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
    public void Perder()
    {
        Debug.Log("perdiste");
        menu.SetActive(true);
        lose.SetActive(true);
        Time.timeScale = (menu.activeSelf) ? 0 : 1;
        Cursor.lockState = (menu.activeSelf) ? CursorLockMode.None : CursorLockMode.Locked;
    }
    public void Ganar()
    {
        Debug.Log("ganaste");
        menu.SetActive(true);
        win.SetActive(true);
        Time.timeScale = (menu.activeSelf) ? 0 : 1;
        Cursor.lockState = (menu.activeSelf) ? CursorLockMode.None : CursorLockMode.Locked;
    }
    public void EncontrarCol()
    {
        reproductor.clip = sonido;
        reproductor.Play();
        colEncontrados++;
        colleccionables.text = colEncontrados + "/" + coltotales;
        if (colEncontrados == coltotales) Ganar();
    }
}
