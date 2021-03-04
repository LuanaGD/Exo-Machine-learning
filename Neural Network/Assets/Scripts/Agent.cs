using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Agent : MonoBehaviour, IComparable<Agent>
{
    public NeuralNetwork net;

    public CarController carController;

    public Material firstMat;
    public Material defaultMat;
    public Material mutatedMat;

    public MeshRenderer mapFeedbackRenderer;

    public float fitness;
    public float distanceTraveled;

    public float[] inputs;
    public bool turnCheck;


    public Transform nextCheckpoint;
    public float nextCheckpointDist;

    public Rigidbody rb;

    public float rayRange;

    public LayerMask layerMask;

    public void ResetAgent()
    {
        fitness = 0;
        distanceTraveled = 0;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        inputs = new float[net.layers[0]];

        carController.Reset();

        nextCheckpoint = CheckpointManager.instance.firstCheckpoint;
        nextCheckpointDist = (transform.position - nextCheckpoint.position).magnitude;
    }

    public void FixedUpdate()
    {
        InputUpdate();
        OutputUpdate();
        FitnessUpdate();
    }

    void InputUpdate()
    {
        inputs[0] = RaySensor(transform.position + Vector3.up * 0.2f, transform.forward, 4f);
        inputs[1] = RaySensor(transform.position + Vector3.up * 0.2f, transform.right, 1.5f);
        inputs[2] = RaySensor(transform.position + Vector3.up * 0.2f, -transform.right, 1.5f);
        inputs[3] = RaySensor(transform.position + Vector3.up * 0.2f, transform.forward + transform.right, 2f);
        inputs[4] = RaySensor(transform.position + Vector3.up * 0.2f, transform.forward - transform.right, 2f);

        inputs[5] = (float)Math.Tanh(rb.velocity.magnitude * 0.05f);
        inputs[6] = (float)Math.Tanh(rb.angularVelocity.y * 0.1f);
        inputs[7] = 1;
    }

    RaycastHit hit;


    float RaySensor(Vector3 pos, Vector3 direction, float length)
    {
        if(Physics.Raycast(pos, direction, out hit, length*rayRange, layerMask))
        {
            Debug.DrawRay(pos, direction * hit.distance, Color.Lerp(Color.red, Color.green, (rayRange * length - hit.distance) / (rayRange * length)));

            return (rayRange*length - hit.distance)/(rayRange*length);
        }
        else
        {
            Debug.DrawRay(pos, direction * rayRange * length, Color.red);
        }

        return 0;
    }

    void OutputUpdate()
    {
        net.FeedForward(inputs);

        carController.horizontalInput = net.neurons[net.layers.Length - 1][0];
        carController.verticalInput = net.neurons[net.layers.Length - 1][1];
    }

    float tempDistance;
    void FitnessUpdate()
    {
        tempDistance = distanceTraveled + (nextCheckpointDist - (transform.position - nextCheckpoint.position).magnitude);

        if(fitness < tempDistance)
        {
            fitness = tempDistance;
        }
    }


    public void CheckpointReached(Transform checkpoint)
    {
        distanceTraveled += nextCheckpointDist;
        nextCheckpoint = checkpoint;
        nextCheckpointDist = (transform.position - nextCheckpoint.position).magnitude;
    }

    public void SetFirstColor()
    {
        GetComponent<MeshRenderer>().material = firstMat;
        mapFeedbackRenderer.material = firstMat;
    }

    public void SetDefaultColor()
    {
        GetComponent<MeshRenderer>().material = defaultMat;
        mapFeedbackRenderer.material = defaultMat;
    }

    public void SetMutatedColor()
    {
        GetComponent<MeshRenderer>().material = mutatedMat;
        mapFeedbackRenderer.material = mutatedMat;
    }

    public int CompareTo(Agent other)
    {
        if(fitness < other.fitness)
        {
            return 1;
        }
        if (fitness > other.fitness)
        {
            return -1;
        }

        return 0;

    }

}
