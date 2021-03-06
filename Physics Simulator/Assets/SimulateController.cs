﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateController : MonoBehaviour
{
    #region TODO

    //Before release

    //Scrolling backgound
    //Simulation graphs
    //Message box


    //After release

    //Adding files
    //particle colors
    //updated UI (black and proffesional)
    //move camera in 3d (lock into screen and move with mouse to look around the press esc to unlock mouse)
    //Draw movements



    #endregion




    public static List<GameObject> ParticleInstances = new List<GameObject>();
    public static bool isSimulating;

    public static float simulationTime;
    private static float _maxTime;
    public static float maxTime
    {
        get { return _maxTime; }
        set
        {
            _maxTime = MyMaths.Magnitude(value);
        }
    }
    public static float _SimulationSpeed;
    public static float SimulationSpeed
    {
        get { return _SimulationSpeed; }
        set
        {
            _SimulationSpeed = MyMaths.Magnitude(value);
        }
    }

    void Update()
    {
        int particleSelected = UiController.instances.DropBoxParticle.value;
        GetSimulationSpeed();
        float deltaT = Time.fixedDeltaTime * SimulationSpeed;
        if (isSimulating)
        {
            if (simulationTime >= maxTime)
            {
                isSimulating = false;
            }
            UiController.instances.UpdateUI(Particle.Instances[particleSelected]);
            SetParticleTimes();
            simulationTime += deltaT;

        }
    }

    private static void SetParticleTimes()
    {
        for (int i =0;i<Particle.Instances.Count;i++)
        {
            Particle.Instances[i].Time = simulationTime;
        }
    }

    public static void OnSimulateClicked()
    {
        GenerateBackground generator = new GenerateBackground();
        generator.CreateBackground();

        ParticleInstances = new List<GameObject>();
        string tag = "Simulation";
        DestroyObjectsWithTag(tag);

        maxTime = 0;
        //Simulation speed of ingame time
        GetSimulationSpeed();
        for (int i = 0; i < Particle.Instances.Count; i++)
        {
            InstatiateParticle(i);
            if (Particle.Instances[i].Time > maxTime)
            {
                maxTime = Particle.Instances[i].Time;
            }
        }
        isSimulating = true;
        simulationTime = 0;
    }

    private static void InstatiateParticle(int index)
    {

        Vector3 Position = getPosition(index);
        Vector3 Velocity = getVelocity(index);

        ParticleInstances.Add(Instantiate(Resources.Load("Sphere"), Position, Quaternion.identity) as GameObject);
        ParticleInstances[index].GetComponent<ParticleInfo>().Velocity = Velocity;
        ParticleInstances[index].GetComponent<ParticleInfo>().index = index;


        float Radius = Particle.Instances[index].Radius;

        ParticleInstances[index].transform.localScale = new Vector3(
            Radius,
            Radius,
            Radius);

        Particle.Instances[index].beforeSimulation = new Particle(Particle.Instances[index]);
    }

    private static Vector3 getPosition(int index)
    {
        Vector3 Position = new Vector3(
            Particle.Instances[index].Position[0],
            Particle.Instances[index].Position[1],
            Particle.Instances[index].Position[2]);
        return Position;
    }
    private static Vector3 getVelocity(int index)
    {
        Vector3 Velocity = new Vector3(
            Particle.Instances[index].InitialVelociy[0],
            Particle.Instances[index].InitialVelociy[1],
            Particle.Instances[index].InitialVelociy[2]);
        return Velocity;
    }


    public static void DestroyObjectsWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
    private static void GetSimulationSpeed()
    {
        SimulationSpeed = UiController.instances.SliderSimulationSpeed.value;
    }
}
