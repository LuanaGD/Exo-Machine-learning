﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Manager : MonoBehaviour
{
    public int populationSize = 100;
    public float trainingDuration = 30;
    public float mutationRate = 5;

    public Agent agentPrefab;
    public Transform agentGroup;

    Agent agent;
    List<Agent> agents = new List<Agent>();

    public CameraController cameraController;
    public TriggerManager triggerManager;

    public int generationCount = 0;

    void Start()
    {
        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        NewGeneration();
        InitNeuralNetworkViewer();
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
        NeuralNetworkViewer.instance.agent = agents[0];
        NeuralNetworkViewer.instance.RefreshAxon();
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



    public void Save()
    {
        List<NeuralNetwork> nets = new List<NeuralNetwork>();

        for (int i = 0; i < agents.Count; i++)
        {
            nets.Add(agents[i].net);
        }

        DataManager.instance.Save(nets);
    }




    public void Load()
    {
        Data data = DataManager.instance.Load();

        if(data != null)
        {
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].net = data.nets[i];
            }
        }

        End();
    }



    void InitNeuralNetworkViewer()
    {
        NeuralNetworkViewer.instance.Init(agents[0]);
    }

}

