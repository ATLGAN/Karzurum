using UnityEditor;
using UnityEngine;

public class CrashDamager : MonoBehaviour
{
    public MinMaxFloat damageRange;
    public MinMaxFloat contactForceRange;

    public float contactForce { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        Damagable damagable = collision.collider.gameObject.GetComponent<Damagable>();

        if (damagable != null)
        {
            contactForce = (collision.impulse / Time.fixedDeltaTime).magnitude;

            if (contactForce >= contactForceRange.MinValue && contactForce <= contactForceRange.MaxValue)
            {
                float perForForce = Mathf.InverseLerp(contactForceRange.MinValue, contactForceRange.MaxValue, contactForce);
                float damageAmount = Mathf.Lerp(damageRange.MinValue, damageRange.MaxValue, perForForce);

                damagable.GiveDamage(damageAmount);

                Debug.Log("Contact Force: " + contactForce);
            }
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(CrashDamager))]
public class CrashDamagerEditor : Editor
{
    CrashDamager script { get => target as CrashDamager; }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawMinMax("Damage Range", ref script.damageRange.MinValue, ref script.damageRange.MaxValue, 0, 100);
        DrawMinMax("Contact Force Range", ref script.contactForceRange.MinValue, ref script.contactForceRange.MaxValue, 100, 5000);
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(script);
        }
    }
    void DrawMinMax(string _header, ref float _minVal, ref float _MaxValue, float _minLimit, float _maxLimit)
    {
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.MinMaxSlider(_header, ref _minVal, ref _MaxValue, _minLimit, _maxLimit);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Min: " + _minVal.ToString(),GUILayout.Width(250));
        EditorGUILayout.LabelField("Max: " + _MaxValue.ToString());
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
}
#endif

