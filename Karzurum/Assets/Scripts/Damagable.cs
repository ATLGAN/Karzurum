using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public bool deadSelf;

    public float maxHealt;
    public float healt;

    public UnityEvent OnDead;

    public bool changeColor;
    public MeshRenderer renderer;
    public Color switchToColor;

    Color defautlColor;

    Material mat;

    private void OnEnable()
    {
        if (changeColor)
        {
            mat = renderer.material;
            defautlColor = mat.GetColor("_BaseColor");
        }
    }
    private void Start()
    {
        if(healt > maxHealt)
        {
            healt = maxHealt;
        }
    }
    public void HealtBoost(float _amount)
    {
        float newHealt = healt + _amount;
        if(newHealt < maxHealt && newHealt > healt)
        {
            healt = newHealt;
        }
        else
        {
            healt = maxHealt;
        }
    }
    public void GiveDamage(float _amount)
    {
        float newHealt = healt - _amount;
        if(newHealt <= 0)
        {
            healt = 0;
            if(deadSelf)
            {
                Destroy(gameObject);
            }
            else
            {
                if(OnDead.GetPersistentEventCount() > 0)
                {
                    OnDead.Invoke();
                }
            }
        }
        else
        {
            healt = newHealt;

            if (changeColor)
            {
                float t = Mathf.InverseLerp(maxHealt, 0, newHealt);
                Color currentColor = Color.Lerp(defautlColor, switchToColor, t);
                mat.SetColor("_BaseColor", currentColor);

                renderer.material = mat;
            }
        }
    }
}
