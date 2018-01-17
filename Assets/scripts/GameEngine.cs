using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEngine : MonoBehaviour {

    public GameObject carPrefab;
    public Transform node1;
    public Transform node2;
    public Transform node3;
    public Transform node4;

    float speed = 1000f;
    public int count;

    // Use this for initialization
    void Start () {

        StartCoroutine(generateCars());

    }

    IEnumerator generateCars ()
    {
        System.Random rnd = new System.Random();

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(3.0f);

            int index = rnd.Next(2, 5);
            Transform startNode = this.PickStartingNode(index);

            GameObject car = Instantiate(carPrefab, startNode.position, startNode.rotation);
            
            car.GetComponent<CarEngine>().force = speed;
            car.GetComponent<CarEngine>().Drive();

            count++;
        }
    }


	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        
    }

    Transform PickStartingNode(int index)
    {
        switch(index)
        {
            case 1: return node1;
            case 2: return node2;
            case 3: return node3;
            case 4: return node4;
            default: return node1;
        }
    }
}
