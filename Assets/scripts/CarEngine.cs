using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 45f;

    private List<Transform> nodes;
    private int currentNode = 0;


    public WheelCollider FRWheel;
    public WheelCollider FLWheel;

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
        ApplySteer();
        Drive();
        CheckWaypointDistance();
	}

    void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        FLWheel.steerAngle = newSteer;
        FRWheel.steerAngle = newSteer;
    }

    void Drive()
    {
        FLWheel.motorTorque = 2000f;
        FRWheel.motorTorque = 2000f;
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
