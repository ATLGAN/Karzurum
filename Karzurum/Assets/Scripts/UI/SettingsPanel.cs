using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    private void Start()
    {
        
    }
    public void Pressed_ExitButton()
    {
        gameObject.SetActive(false);
    }
    public void ChangedQuailtyDropdown(int value)
    {
        QualitySettings.SetQualityLevel(value);


    }
}
