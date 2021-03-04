using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManagerTest : MonoBehaviour
{
    public int populationSize = 100;
    public float trainingDuration = 30;
    public float mutationRate = 5;


    public Agent agentPrefab;
    public Transform agentGroup;

    Agent agent;
    List<Agent> agents = new List<Agent>();

    public CameraController cameraController;

    public int generationCount = 0;

    void Start()
    {
        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        NewGeneration();
        Focus();

        yield return new WaitForSeconds(trainingDuration);


        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        NewGeneration();
        Focus();

        yield return new WaitForSeconds(trainingDuration);

        StartCoroutine(Loop());
    }

    void NewGeneration()
    {
        agents.Sort();
        AddOrRemoveAgents();
        Mutate();
        ResetAgents();
        SetColors();
        generationCount++;
    }

    void AddOrRemoveAgents()
    {
        if (agents.Count != populationSize)
        {
            int dif = populationSize - agents.Count;

            if (dif > 0)
            {
                for (int i = 0; i < dif; i++)
                {
                    AddAgents();
                }
            }
            else
            {
                for (int i = 0; i < -dif; i++)
                {
                    RemoveAgents();
                }
            }
        }
    }

    void AddAgents()
    {
        agent = Instantiate(agentPrefab, Vector3.zero, Quaternion.identity, agentGroup);
        agent.net = new NeuralNetwork(agent.net.layers);

        agents.Add(agent);
    }

    void RemoveAgents()
    {
        Destroy(agents[agents.Count - 1].transform);
        agents.RemoveAt(agents.Count - 1);
    }

    void Focus()
    {
        cameraController.target = (agents[0].transform);
    }

    void Mutate()
    {
        for (int i = agents.Count/2; i < agents.Count; i++)
        {
            agents[i].net.CopyNet(agents[i - (agents.Count / 2)].net);
            agents[i].net.Mutate(mutationRate);
            agents[i].SetMutatedColor();
        }
    }

    void ResetAgents()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].ResetAgent();        
        }
    }

    void SetColors()
    {
        for (int i = 1; i < agents.Count/2; i++)
        {
            agents[i].SetDefaultColor();
        }

        agents[0].SetFirstColor();
    }

    public void End()
    {
        StopAllCoroutines();
        StartCoroutine(Loop());
    }

    public void ResetNets()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].net = new NeuralNetwork(agent.net.layers);
        }

        End();
    }
}
