using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEngine;

public class OpenAndStartScenesOnEditor
{
    static string dataPath = Application.dataPath;
    static string path = dataPath + @"/SCS_Sim/_Data/Scenarios/BHL/ScenarioDatas/";

    //START SCENES
    [MenuItem("Tools/Start Scene")]
    public static void StartWithTutorialScene() =>              StartGame("MainMenu");

    //OPEN SCENES
    [MenuItem("Tools/Open Scene/Game Scene")]
    public static void OpenConsructorScene() =>                 OpenScene("GameScene");
    [MenuItem("Tools/Open Scene/Main Menu")]
    public static void OpenMainScene() => OpenScene("MainMenu");

    //FUNCTIONS
    private static bool OpenScene(string scene)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {            
            EditorSceneManager.OpenScene("Assets/Scenes/" + scene + ".unity");
            return true;
        }
        else
            return false;
    }
    public static void StartGame(string name)
    {
        if(OpenScene(name))
            EditorApplication.isPlaying = true;
    }
}
