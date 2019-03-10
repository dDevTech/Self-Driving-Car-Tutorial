using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMov : MonoBehaviour {
    private float vyRot = 0;
    public float speedLinear = 0.2f;
    public float speedRotation = 0.5f;
    private float vz;
    private float acceleration;
    private float increaseAcc =5;
    public bool activeAcceleration = true;
	// Use this for initialization
	void Start () {
        vz = speedLinear;
	}
	
	// Update is called once per frame
	void Update () {
        float time = Time.deltaTime;
        transform.position += transform.forward * (vz * time + 0.5f * acceleration * time * time);
        transform.Rotate(new Vector3(0, vyRot, 0));
	}
    public float getAcceleration()
    {
        return acceleration;
    }
    public void updateMovement(List<float> outputs)
    {
        if (outputs[0] * 2 > 1f)
        {
            vyRot = (outputs[0] * 2 - 1) * speedRotation * Time.deltaTime;
        }
        else
        {
            vyRot = -(outputs[0] * 2) * speedRotation * Time.deltaTime;
        }
       
       
        if (outputs[1] * 2 > 1f)
        {
            acceleration = (outputs[1] * 2 - 1) * increaseAcc;
        }
        else
        {
            acceleration = -outputs[1] * 2 * increaseAcc;
        }
    }
}
