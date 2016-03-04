using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : MonoBehaviour {

    public Stack<Vector3> AvailablePos;
    public Stack<Disk> Disks;
    
	// Use this for initialization
	void Start () 
    {
        // Initializations
        AvailablePos = new Stack<Vector3>();
        Disks = new Stack<Disk>();
        // Setting up the peg
        createPosInStack();
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    private void createPosInStack()
    {
        Vector3 basePos = transform.position;

        int n = Global.NumberOfDisks;

        for (int i = 0; i < n; i++)
        {
            Vector3 freePos = basePos +
                (new Vector3(0, transform.localScale.y * (n - i) * 2, 0));

            AvailablePos.Push(freePos);
        }
    }

    public void ChangeRadius(float radius)
    {
        radius = radius + Global.DiskBaseDiff;
        Vector3 scale = transform.localScale;
        scale.x = radius;
        scale.z = radius;
        scale.y = Global.DiskHeight();
        transform.localScale = scale;
    }
}
