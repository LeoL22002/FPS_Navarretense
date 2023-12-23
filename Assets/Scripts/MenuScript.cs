using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Jugar() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Salir() {
        Application.Quit();
    }
    public void VolverJugar() {
        SceneManager.LoadScene(1);
    }
}
