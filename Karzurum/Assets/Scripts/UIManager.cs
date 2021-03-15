using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image crosshair;
    [SerializeField] TMP_Text tooltipText;

    public Color defaultCrosshairColor;

    private void Start()
    {
        tooltipText.enabled = false;
    }
    public void SetActiveCrosshair(bool _state)
    {
        crosshair.enabled = _state;
    }
    public void ChangeCrosshairColor(Color _color)
    {
        crosshair.color = _color;
    }
    public void SetDefaultCrosshairColor()
    {
        crosshair.color = defaultCrosshairColor;
    }
    public void UpdateTooltipMessage(string text)
    {
        if (text == "")
            tooltipText.enabled = false;
        else
            tooltipText.enabled = true;

        tooltipText.text = text;
    }
}
