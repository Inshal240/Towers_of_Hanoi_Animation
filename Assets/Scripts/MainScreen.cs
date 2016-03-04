using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

    string num;

	// Use this for initialization
	void Start () 
    {
        num = "";
	}
	
    void OnGUI()
    {
        int width = Screen.width;
        int height = Screen.height;
        Texture2D title = Resources.Load("Title") as Texture2D;

        GUILayout.BeginArea(new Rect(0, 0, width, height));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        GUILayout.Box(title);
        GUILayout.Box("Enter the number of disks you wish to start with");
        num = GUILayout.TextField(num);
        
        if (GUILayout.Button("Play animation"))
        {
            try
            {
                Global.NumberOfDisks = int.Parse(num);
                Application.LoadLevel(1);
            }
            catch
            {
                GUILayout.TextArea("Please enter a valid integer");
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
