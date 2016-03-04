using UnityEngine;
using System.Collections;

public class Disk : MonoBehaviour {

    private bool moving;
    private bool rising;
    public Vector3 Destination;
    public float Radius;

	// Use this for initialization
	void Start () 
    {
        Destination = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (transform.position != Destination && !Global.DiskMoving)
        {
            Global.DiskMoving = true;
            moving = true;
            rising = true;
        }

        if (moving && Global.DiskMoving)
        {
            Vector3 currentPos = transform.position;
            
            if (rising)
            {
                currentPos.y += Global.DiskVelocity;
                rising = (currentPos.y > Global.DiskRaiseHeight()) ? false : true;
            }
            else if (currentPos.x != Destination.x)
            {
                int direction = (Destination.x - currentPos.x > 0) ? 1 : -1;

                currentPos.x += Global.DiskVelocity * direction;

                // Force them to be equal
                if (Mathf.Abs(Destination.x - currentPos.x) < 0.3)
                {
                    currentPos.x = Destination.x;
                }

            }
            else if (currentPos.y > Destination.y)
            {
                currentPos.y -= Global.DiskVelocity;
                
                // Force them to be equal
                if (currentPos.y < Destination.y)
                {
                    currentPos.y = Destination.y;
                }
            }

            moving = (currentPos == Destination) ? false : true;
            Global.DiskMoving = (currentPos == Destination) ? false : true;
            
            transform.position = currentPos;
        }
	}

    public void ChangeRadius(float radius)
    {
        this.Radius = radius;
        Vector3 scale = transform.localScale;
        scale.x = radius;
        scale.z = radius;
        scale.y = Global.DiskHeight();
        transform.localScale = scale;
    }
}
