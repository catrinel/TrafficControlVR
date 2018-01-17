using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarEngine : MonoBehaviour {

    public Transform path;

    private List<Transform> nodes;
    private int currentNode = 0;

    public WheelCollider FRWheel;
    public WheelCollider FLWheel;

    public WheelCollider BRWheel;
    public WheelCollider BLWheel;

    public bool isBraking = false;
    public float force;
    public float maxBreakTorque = 1000f;

	// Use this for initialization
	void Start () {
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
                nodes.Add(pathTransforms[i]);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //CheckWaypointDistance();
	}

    void OnMouseDown()
    {
        if (FLWheel.brakeTorque == 0)
        {
            Brake();
        }
        else
        {
            Drive();
        }
    }
    

    public void Drive()
    {

        BRWheel.brakeTorque = 0;
        BLWheel.brakeTorque = 0;

        FLWheel.motorTorque = force * GetComponent<Rigidbody>().mass;
        FRWheel.motorTorque = force * GetComponent<Rigidbody>().mass;
    }

    private void Brake()
    {
        FLWheel.motorTorque = 0;
        FRWheel.motorTorque = 0;

        BRWheel.brakeTorque = force * GetComponent<Rigidbody>().mass;
        BLWheel.brakeTorque = force * GetComponent<Rigidbody>().mass;
    }

    void CheckWaypointDistance()
    {
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 0.5f)
        {
            if(currentNode == nodes.Count - 1)
            {
                // destroy car
                return;
            }
            currentNode++;
        }
    }
}
