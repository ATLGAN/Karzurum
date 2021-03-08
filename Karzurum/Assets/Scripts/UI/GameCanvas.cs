using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvas : MonoBehaviour
{
    public string gameScene;

    public SettingsPanel settingsPanel;
    public GameObject creditPanel;

    private void OnEnable()
    {
        settingsPanel.gameObject.SetActive(false);
        creditPanel.gameObject.SetActive(false);
    }
    public void Pressed_StartButton()
    {
        SceneManager.LoadScene(gameScene);
    }
    public void Pressed_SettingsButton()
    {
        settingsPanel.gameObject.SetActive(true);
    }
    public void Pressed_CreditButton()
    {
        creditPanel.SetActive(true);
    }
    public void Pressed_ExitButton()
    {
        Application.Quit();
    }
}
