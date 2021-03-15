using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public enum BlendMode { Alpha = 0, Premultiply = 1, Additive = 2, Multiply = 3 }
public enum SurfaceType { Alpha = 0, Transparent = 1 }
public enum ValidateType { NullReference, ArgNullReference, Exception}
public class UtilitiesMethods
{
    public static GameObject GetNearestObject(Vector3 center, GameObject[] gameObjects)
    {
        if (gameObjects == null) return null;

        GameObject nearObject = null;
        float nearDistance = 99;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            float dist = Vector3.Distance(center, gameObjects[i].transform.position);
            if (dist < nearDistance)
            {
                nearObject = gameObjects[i];
                nearDistance = dist;
            }
        }
        return nearObject;
    }
    public static Mesh CreateViewCone(float aAngle, float aDistance, int aConeResolution = 30)
    {
        Vector3[] verts = new Vector3[aConeResolution + 1];
        Vector3[] normals = new Vector3[verts.Length];
        int[] tris = new int[aConeResolution * 3];
        Vector3 a = Quaternion.Euler(-aAngle / 2, 0, 0) * Vector3.forward * aDistance;
        Vector3 n = Quaternion.Euler(-aAngle / 2, 0, 0) * Vector3.up;
        Quaternion step = Quaternion.Euler(0, 0, 360f / aConeResolution);
        verts[0] = Vector3.zero;
        normals[0] = Vector3.back;
        for(int i = 0; i < aConeResolution; i++)
        {
            normals[i + 1] = n;
            verts[i + 1] = a;
            a = step * a;
            n = step * n;
            tris[i * 3] = 0;
            tris[i * 3 + 1] = (i + 1) % aConeResolution + 1;
            tris[i * 3 + 2] = i + 1;
        }
        Mesh m = new Mesh();
        m.vertices = verts;
        m.normals = normals;
        m.triangles = tris;
        m.RecalculateBounds();
        return m;
    }
    public static byte[] BinarySerialize(object graph)
    {
        using(var stream = new MemoryStream())
        {
            var formatter = new BinaryFormatter();

            formatter.Serialize(stream, graph);

            return stream.ToArray();
        }
    }
    public static object BinaryDeserialize(byte[] buffer)
    {
        using(var stream = new MemoryStream(buffer))
        {
            var formatter = new BinaryFormatter();

            return formatter.Deserialize(stream);
        }
    }
    public static DateTime RandomDate(int startYear = 1995)
    {
        DateTime start = new DateTime(startYear, 1, 1);
        System.Random gen = new System.Random();
        int range = (DateTime.Now - start).Days;
        return start.AddDays(gen.Next(range));
    }
    public static void FixNameForClone(UnityEngine.GameObject obj)
    {
        string newName = obj.name.Remove(obj.name.Length - 7);
        obj.name = newName;
    }
    public static string ConvertSecondsToFormattedTimeString(float seconds, TimeFormat resultFormat = TimeFormat.MmSs)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string format = "";
        switch (resultFormat)
        {
            case TimeFormat.HhMmSs:
                format = @"hh\:mm\:ss";
                break;
            case TimeFormat.MmSs:
                format = @"mm\:ss";
                break;
            default:
                break;
        }
        return time.ToString(format);
    }
    public static void SetActivityObjects(bool state, params GameObject[] objs)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i] != null)
                objs[i].gameObject.SetActive(state);
        }
    }
    public static void GenerateValidate<TVariable, TCompare>(TVariable obj = default(TVariable), TCompare cmp = default(TCompare), string message = "", ValidateType type = ValidateType.Exception, bool lookAtState = true)
    {        
        bool result = false;
        if (obj is Enum)
        {
            Enum v = obj as Enum;
            Enum c = cmp as Enum;
            int a = v.CompareTo(c);
            result = a == 0;            
        }        
        else if(obj is object)
        {
            object variable = obj;
            object compare = cmp;
            result = variable.Equals(compare);         
        }                
        if (result == lookAtState)
        {
            switch (type)
            {
                case ValidateType.NullReference:                                        
                    throw new NullReferenceException(message);                    
                case ValidateType.ArgNullReference:
                    throw new ArgumentNullException(message);
                case ValidateType.Exception:
                    throw new Exception(message);                                    
            }         
        }
    }
}
public static class ExtensionMethods
{
    private static System.Random mRandom = new System.Random();
    private static string URP_STD_SHADER_NAME = "Universal Render Pipeline/Lit";

    #region Material
    public static void SetColor(this Material material, Color color)
    {
        if(material.shader.name == URP_STD_SHADER_NAME)
        {
            material.SetColor("_BaseColor", color);
        }
    }
    public static void SetColor(this Material material, float r, float g, float b, float a)
    {
        if(material.shader.name == URP_STD_SHADER_NAME)
        {
            material.SetColor("_BaseColor", new Color(r, g, b, a));
        }
    }
    public static Color GetColor(this Material material)
    {
        if(material.shader.name == URP_STD_SHADER_NAME)
        {
            return material.GetColor("_BaseColor");
        }
        return new Color(1, 1, 1, 1);
    }
    public static void SetSurfaceType(this Material material, SurfaceType surfaceType)
    {
        if(material.shader.name == URP_STD_SHADER_NAME)
        {
            material.SetFloat("_Surface", (int)surfaceType);
        }
        else
        {
            throw new Exception("Unspported Material!");
        }
    }
    public static SurfaceType GetSurfaceType(this Material material)
    {
        if(material.shader.name == URP_STD_SHADER_NAME)
        {
            return (SurfaceType)(int)material.GetFloat("_Surface");
        }
        else
        {
            throw new Exception("Unspported Material!");
        }
    }
    public static void SetBlendingMode(this Material material, BlendMode blendMode)
    {
        if(material.shader.name == URP_STD_SHADER_NAME)
        {
            if(material.GetFloat("_Surface") == (int)SurfaceType.Transparent)
            {
                material.SetFloat("_Blend", (int)blendMode);
            }
        }
        else
        {
            throw new Exception("Unspported Material!");
        }
    }
    public static BlendMode GetBlendingMode(this Material material)
    {
        if(material.shader.name == URP_STD_SHADER_NAME)
        {
            if(material.GetFloat("_Surface") == (int)SurfaceType.Transparent)
            {
                return (BlendMode)(int)material.GetFloat("_Blend");
            }
        }
        else
        {
            throw new Exception("Unspported Material!");
        }
        return BlendMode.Alpha;
    }
    public static void SetTextureOffset(this Material material, Vector2 value)
    {
        if(material.shader.name == URP_STD_SHADER_NAME)
        {
            material.SetTextureOffset("_BaseMap", value);
        }
        else
        {
            throw new Exception("Unspported Material!");
        }
    }
    public static Vector2 GetTextureOffset(this Material material)
    {
        if(material.shader.name == URP_STD_SHADER_NAME)
        {
            return material.GetTextureOffset("_BaseMap");
        }
        else
        {
            throw new Exception("Unspported Material!");
        }
    }
    #endregion
    #region Button
    public static void ChangeName(this Button button, string name)
    {
        if (button.transform.childCount > 0)
        {
            if (button.transform.GetChild(0).GetComponent<TMP_Text>())
            {
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = name;
            }
            else if (button.transform.GetChild(0).GetComponent<Text>())
            {
                button.transform.GetChild(0).GetComponent<Text>().text = name;
            }
        }
    }
    public static void ChangeImage(this Button button, Sprite sprite)
    {
        if (button.GetComponent<Image>())
        {
            if (sprite != null)
                button.GetComponent<Image>().sprite = sprite;
        }
    }
    public static void ChangeTextColor(this Button button, Color color)
    {
        if (button.transform.childCount > 0)
        {
            if (button.transform.GetChild(0).GetComponent<TMP_Text>())
            {
                button.transform.GetChild(0).GetComponent<TMP_Text>().color = color;
            }
            else if (button.transform.GetChild(0).GetComponent<Text>())
            {
                button.transform.GetChild(0).GetComponent<Text>().color = color;
            }
        }
    }
    #endregion
    #region List
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while(n > 1)
        {
            n--;
            int k = mRandom.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    #endregion
}
public enum TimeFormat
{
    HhMmSs,
    MmSs
}
