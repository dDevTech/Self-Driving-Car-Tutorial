using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour {
    public Color colorBeam = new Color(0, 255, 0, 0.5f);
    public int distanceLaser = 20;
    public static int lasers = 5;
    public static int view = 120; //In degrees
    public float height = 0;


    private int count;
    private GameObject[] laserObjects;

    // Use this for initialization
    void Start () {
        count = view / (lasers - 1);
        laserObjects = new GameObject[lasers];

        for(int i = 0;i< lasers; i++)
        {
            float currentDegree = count * i - view / 2;
            GameObject obj = new GameObject();
            Laser laser=obj.AddComponent<Laser>();
            laser.finalLength = 0.02f;
            laser.laserColor = colorBeam;
            laser.distanceLaser = distanceLaser;
           
            laser.transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);

            laserObjects[i] = obj;
            laserObjects[i].transform.Rotate(new Vector3(0,currentDegree,0));
            obj.transform.SetParent(transform);
           
        }
	}
    public float[] getDistances()
    {
        float[] lasers = new float[laserObjects.Length];
        for(int i = 0; i < laserObjects.Length; i++)
        {

            lasers[i] = laserObjects[i].GetComponent<Laser>().getDistance();
        }
        return lasers;
    }
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject obj in laserObjects)
        {
            obj.transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        }
	}
}
