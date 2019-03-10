using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public GameObject FirstCar;
    public float speedRotCamera = 0.2f;
    public float keyRotSpeedCamera = 500f;
    private float rotCamera = 0;
    public float timeAnimation = 1; //In seconds

    public GameObject follow;
    public Vector3 initialPosition;
    public float initialTime;
	// Use this for initialization
	void Start () {
		List<GameObject>cars = GameObject.Find("CarController").GetComponent<CarControllerAI>().getCars();
        follow = cars[Random.Range(0, cars.Count - 1)];
        initialPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (follow != null)
        {
            float timePassed = (Time.time - initialTime);
            float proportion = timePassed / timeAnimation;
            Vector3 currentPosition;
            if (proportion < 1)
            {
                currentPosition = Vector3.Lerp(initialPosition,follow.transform.position,proportion);
            }
            else
            {
                currentPosition = follow.transform.position;
            }
            transform.position = new Vector3(currentPosition.x, currentPosition.y + 11.21f, currentPosition.z - 17.91f);
            transform.LookAt(currentPosition);
            transform.Translate(Vector3.right * Time.deltaTime * rotCamera * 5);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            List<GameObject> cars = GameObject.Find("CarController").GetComponent<CarControllerAI>().getCars();
            int index = cars.IndexOf(follow);
            if(index == cars.Count - 1)
            {
                index = 0;
            }
            else
            {
                index += 1;
            }
            Follow(cars[index]);
        }
	}
    public void Follow(GameObject newFollow)
    {
        initialPosition = follow.transform.position;
        initialTime = Time.time;
        follow = newFollow;
        
    }
    public void UnFollow()
    {
        follow = null;
    }
    public GameObject getFollowing()
    {
        return follow;
    }
}
