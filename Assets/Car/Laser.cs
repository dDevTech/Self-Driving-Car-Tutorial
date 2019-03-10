using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public Color laserColor = new Color(0, 1, 0, 0.5f);
    public float distanceLaser = 50;
    public float finalLength = 0.02f;
    public float initialLength = 0.02f;

    private Vector3 positionLaser;
    private LineRenderer lineRenderer;
    private float distance = 0;
	// Use this for initialization
	void Start () {
        distance = distanceLaser;
        positionLaser = new Vector3(0, 0, finalLength);
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
        lineRenderer.startWidth = initialLength;
        lineRenderer.endWidth = finalLength;
        lineRenderer.positionCount = 2;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 finalPoint = transform.position + transform.forward * distanceLaser;
        RaycastHit collisionPoint;
        if(Physics.Raycast(transform.position,transform.forward,out collisionPoint, distanceLaser))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, collisionPoint.point);
            distance = collisionPoint.distance;

        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, finalPoint);
            distance = distanceLaser;
        }
	}
    public float getDistance()
    {
   
        return distance/distanceLaser;
    }
}
