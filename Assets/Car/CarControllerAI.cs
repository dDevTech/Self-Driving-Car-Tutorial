using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerAI : MonoBehaviour {
    public List<GameObject> cars;
    public int population = 20;
    public int generation = 0;
    public GameObject car;
    [HideInInspector]
    public DNA winner;
    public DNA secWinner;
    private int carsCreated = 0;
    // Use this for initialization
    void Start () {
        newPopulation();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public List<GameObject> getCars()
    {
        return cars;
    }
    public void newPopulation()
    {
        cars = new List<GameObject>();
        for(int i = 0; i < population; i++)
        {
            GameObject carObj = (Instantiate(car));
            cars.Add(carObj);
            carObj.GetComponent<Car>().Initialize();
        }
        generation++;
        Debug.Log(generation);
    }
    public void newPopulation(bool geneticManipulation)
    {
        if (geneticManipulation)
        {
            Debug.Log("aa");
            cars = new List<GameObject>();
            for(int i = 0; i < population; i++)
            {
                DNA dna = winner.crossover(secWinner);
                DNA mutated = dna.mutate();
                GameObject carObj = Instantiate(car);
                cars.Add(carObj);
                carObj.GetComponent<Car>().Initialize(mutated);
            }
        }
        generation++;
        carsCreated = 0;
        GameObject.Find("Camera").GetComponent<CameraMovement>().Follow(cars[0]);
    }
    public void restartGeneration()
    {
        cars.Clear();
        newPopulation();
    }
}
