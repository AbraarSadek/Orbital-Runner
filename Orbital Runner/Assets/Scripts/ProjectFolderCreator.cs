using UnityEngine;
using UnityEditor;
using System.IO; // Required for directory operations

/*
 * Purpose: This Script Creates A Set of Default Folders In The Unity Project When Executed.
 * 
 * Created By: Abraar Sadek
 * Date Created: 06/10/2024
 * 
 * Modified By: Abraar Sadek
 * Date Last Modified: 06/10/2024
 * 
 */

//ProjectFolderCreator Class - Creates A Set of Default Folders In The Unity Project
public class ProjectFolderCreator : EditorWindow {

    //String Array Named folders Containing The Default Folder Names
    private static string[] folders = new string[] {
        
        "Art",
        "Art/Materials",
        "Art/Textures",
        "Art/Models",
        "Audio",
        "Prefabs",
        "Scenes",
        "Scripts",
        "Scripts/UI",
        "Scripts/Gameplay",
        "UI",
        "Animation"

    }; //End of String Array

    //ShowWindow Method - That Will Add The Create Default Project Folders Option To The Tools Menu 
    [MenuItem("Tools/Create Default Project Folders")]
    public static void ShowWindow() {
        GetWindow<ProjectFolderCreator>("Project Folder Creator");
    } //End of ShowWindow Method

    //OnGUI Method - That Will Create A Button To Execute The CreateProjectFolders Method
    void OnGUI() {

        GUILayout.Label("Create Default Project Folders", EditorStyles.boldLabel); //Label For The Editor Window

        //If-Statement - That Will Execute The CreateProjectFolders Method When The Button Is Clicked
        if (GUILayout.Button("Create Folders")) {
            CreateProjectFolders();
        } //End of If Statement

    } //End of OnGUI Method

    //CreateProjectFolders Method - That Will Create The Default Folders In The Unity Project
    private static void CreateProjectFolders() {

        //Foreach Loop - That Will Iterate Through The String Array And Create Each Folder If It Does Not Already Exist
        foreach (string folder in folders) {

            string path = "Assets/" + folder; //Construct The Full Path For The Folder

            //If-Else Statement - That Will Check If The Folder Already Exists, If Not It Will Create It
            if (!AssetDatabase.IsValidFolder(path)) {
                AssetDatabase.CreateFolder("Assets", folder); //Create The Folder
                Debug.Log($"Created folder: {path}"); //Debug Log To Confirm Folder Creation
            } else {
                Debug.LogWarning($"Folder already exists: {path}"); //Debug Warning If The Folder Already Exists
            } //End of If-Else Statement

        } //End of Foreach Loop

        AssetDatabase.Refresh(); //Refresh the Asset Database to show newly created folders
        Debug.Log("Default project folders created successfully!"); //Debug Log To Confirm Completion of Folder Creation

    } //End of CreateProjectFolders Method

} //End of ProjectFolderCreator Class