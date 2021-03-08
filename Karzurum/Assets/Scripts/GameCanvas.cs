using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvas : MonoBehaviour
{
    public string gameScene;

    public void Pressed_StartButton()
    {
        SceneManager.LoadScene(gameScene);
    }
    public void Pressed_SettingsButton()
    {

    }
    public void Pressed_CreditButton()
    {

    }
    public void Pressed_ExitButton()
    {

    }
}
