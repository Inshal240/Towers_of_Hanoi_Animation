using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    private Queue<string> Solution;
    public Base Base1;
    public Base Base2;
    public Base Base3;
    private bool completed = false;
    private string currentStep = "";
	// Use this for initialization
	void Start () 
    {
        createSolution();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!Global.DiskMoving )
        {
            if (Solution.Count > 0)
            {
                string nextStep = Solution.Dequeue();

                currentStep = "Currently moving from " + nextStep[0] + " to " + nextStep[2];

                Base src = getBaseFromChar(nextStep[0]),
                    dest = getBaseFromChar(nextStep[2]);

                Disk toMove = src.Disks.Pop();
                src.AvailablePos.Push(toMove.transform.position);

                toMove.Destination = dest.AvailablePos.Pop();
                dest.Disks.Push(toMove);
            }
            else
            {
                completed = true;
            }
        }
	}

    void OnGUI()
    {
        int width = Screen.width;
        int height = Screen.height;
        Vector3 cameraPos = GameObject.Find("Main Camera").transform.position;

        GUILayout.BeginArea(new Rect(0, 0, width, height));
        if (completed)
        {   
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Box("All disks moved to final stack");
            if (GUILayout.Button("Back to mainscreen"))
            {
                Application.LoadLevel(0);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();            
        }
        else 
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.Box("Disk velocity");
            Global.DiskVelocity = GUILayout.HorizontalSlider(Global.DiskVelocity, 0.01f, 0.5f);
            GUILayout.FlexibleSpace();
            GUILayout.Box("Move Camera");
            cameraPos.z = GUILayout.VerticalSlider(cameraPos.z, -10, -100);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.Box(currentStep);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

        }
        GUILayout.EndArea();

        GameObject.Find("Main Camera").transform.position = cameraPos;
    }

    private void createSolution()
    {
        Solution = new Queue<string>();
        int n = Global.NumberOfDisks;

        // Wrapper for the stacks TowerOfHanoi Function
        StackWrapper<int> A = new StackWrapper<int>("A");
        StackWrapper<int> B = new StackWrapper<int>("B");
        StackWrapper<int> C = new StackWrapper<int>("C");

        for (int i = 0; i < n; i++)
        {
            A.WrappedStack.Push(i);
        }

        TowerOfHanoi<int>(n, A, B, C);
    }

    private void TowerOfHanoi<T>(int N, StackWrapper<T> Beg, StackWrapper<T> Aux, StackWrapper<T> End)
    {
        // Base Case
        if (N == 1)
        {
            End.WrappedStack.Push( Beg.WrappedStack.Pop() );
            Solution.Enqueue(Beg.Name + " " + End.Name);
        }

        // Recursive Case
        else
        {
            TowerOfHanoi(N - 1, Beg, End, Aux);

            End.WrappedStack.Push( Beg.WrappedStack.Pop() );
            Solution.Enqueue(Beg.Name + " " + End.Name);

            TowerOfHanoi(N - 1, Aux, Beg, End);
        }
    }

    private Base getBaseFromChar(char x)
    {
        switch (x)
        {
            case 'A':
                return Base1;
                
            case 'B':
                return Base2;
                
            case 'C':
                return Base3;
                
            default:
                return (Base)null;
        }
    }
}

/// <summary>
/// Created for the sole purpose of keeping track of the moves. The "Name" field helps
/// accomplish the task.
/// </summary>
/// <typeparam name="T">Type of objects contained in the stack</typeparam>
class StackWrapper<T>
{
    public string Name;
    public Stack<T> WrappedStack;

    public StackWrapper(string name) 
    {
        Name = name;
        WrappedStack = new Stack<T>();
    }
}