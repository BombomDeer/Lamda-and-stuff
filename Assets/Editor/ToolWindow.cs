using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;//needed for EditorWindow
using System.IO;

//put this script in the Editor folder
public class ToolWindow : EditorWindow//needs UnityEditor
{
    string strFilename;
    [MenuItem("Custom/Save/GameData")]// this is important. whenever somethings in [] google it to see what it does
    static void Init()
    {
        ToolWindow window = (ToolWindow)EditorWindow.GetWindow(typeof(ToolWindow));
        window.Show();
    }

    void OnGUI()//we need this
    {//there are GUI related functions that we should google apparently
        GUILayout.Label("Fill out filename and press");//we need the label
        strFilename = EditorGUILayout.TextField("FileName: ", strFilename);//어떤 파일 이름을 만들고 싶을때
        if(GUILayout.Button("Save"))//it makes a button in the editor window
        {
            string data = string.Empty;
            GameObject[] building=
                GameObject.FindGameObjectsWithTag("Building");

            data += building[0].name;
            data += ", ";
            data += building[0].transform.position.x.ToString();
            data += ", ";
            data += building[0].transform.position.y.ToString();
            data += ", ";
            data += building[0].transform.position.z.ToString();

            string directory = Application.dataPath;

            EditorUtility.SaveFilePanel("Save Building Info", directory, strFilename, "csv");//windows settings is causing this to not work properly on the computer i used to write this code
            //EditorUtility.SaveFolderPanel;

            if (strFilename.Equals(""))//reference 비교는 하지마라
                return;

            FileStream f = new FileStream(strFilename, FileMode.Create, FileAccess.Write);//needs Systems.IO
            StreamWriter writer = new StreamWriter(f, System.Text.Encoding.UTF8);
            writer.WriteLine(data);
            writer.Close();//closing file
        }
    }
}
