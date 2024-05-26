using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pindahscene : MonoBehaviour
{
    public void PindahKeLevel1()
    {
        SceneManager.LoadScene("level1");
    }

    public void PindahKeInformasi()
    {
        SceneManager.LoadScene("informasi");
    }
    public void PindahKeMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
