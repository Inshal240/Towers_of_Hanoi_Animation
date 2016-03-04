using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    float maxRadius;

    // Use this for initialization
	void Start () 
    {
        maxRadius = Global.MinRadius;
        
        for (int i = 0; i < Global.NumberOfDisks; i++)
        {
            maxRadius += (i + 1) * Global.RadiusDiff;
        }
        
        // Create the bases
        for (int i = 0; i < 3; i++)
        {
            createBase("Stack " + (i + 1).ToString(), maxRadius,
                new Vector3((maxRadius + Global.DistanceBetweenBases) * (i - 1), 0, 0));
        }

        StartCoroutine(createRest(1));
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    void createBase(string name, float radius, Vector3 position)
    {
        var item = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        item.AddComponent<Base>();
        item.GetComponent<Base>().ChangeRadius(radius);
        item.renderer.material = Resources.Load("m_palmbark") as Material;
        item.name = name;
        item.transform.position = position;
    }

    void createDisk(string name, float radius, Base initialBase)
    {
        Vector3 position = initialBase.AvailablePos.Pop();
        var item = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        item.AddComponent<Disk>();
        item.GetComponent<Disk>().ChangeRadius(radius);
        item.name = name;
        item.renderer.material = Resources.Load("m_disks") as Material;
        item.transform.position = position;
        initialBase.Disks.Push(item.GetComponent<Disk>());
    }

    IEnumerator createRest(float duration)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
            yield return 0;

        var stack1 = GameObject.Find("Stack 1").GetComponent<Base>();

        for (int i = 0; i < Global.NumberOfDisks; i++)
        {
            createDisk("Disk " + (Global.NumberOfDisks - i).ToString(), maxRadius - Global.RadiusDiff * (i + 1),
                stack1);
        }

        var controller = new GameObject("GameController");
        controller.AddComponent<GameController>();
        var controlComp = controller.GetComponent<GameController>();
        controlComp.Base1 = stack1;
        controlComp.Base2 = GameObject.Find("Stack 2").GetComponent<Base>();
        controlComp.Base3 = GameObject.Find("Stack 3").GetComponent<Base>();

        DestroyImmediate(gameObject);
    }
}
