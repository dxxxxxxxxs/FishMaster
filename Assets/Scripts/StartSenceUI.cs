using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSenceUI : MonoBehaviour
{
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("gold");
        PlayerPrefs.DeleteKey("lv");
        PlayerPrefs.DeleteKey("scd");
        PlayerPrefs.DeleteKey("bcd");
        PlayerPrefs.DeleteKey("exp");
        SceneManager.LoadScene("Main");
    }
    public void OldGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
