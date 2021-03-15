using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsPanel : MonoBehaviour
{
    public TMP_Dropdown qualityDropDown;

    private void Start()
    {
        if (PlayerPrefs.HasKey(PlayerKeys.QuailtySettings))
        {
            if (qualityDropDown != null)
            {
                qualityDropDown.value = PlayerPrefs.GetInt(PlayerKeys.QuailtySettings);
            }
        }
    }
    public void Pressed_ExitButton()
    {
        gameObject.SetActive(false);
    }
    public void ChangedQuailtyDropdown(int value)
    {
        QualitySettings.SetQualityLevel(value);

        PlayerPrefs.SetInt(PlayerKeys.QuailtySettings, value);
    }
}
