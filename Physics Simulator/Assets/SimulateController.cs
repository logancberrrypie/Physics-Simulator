using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateController : MonoBehaviour
{
    #region TODO
    //Contioue simulation by clicking play
    //Adding pause
    //Adding restart simulation
    //Simulation Speed
    //Gravity in simulation
    //Can collide toggle
    //Colisions in simulations
    //Changing time element on UI as simulation occuirs
    //Updating values of Suvat while simulation is running then resetting them
    //Scrolling backgound
    //Simulation graphs
    




    public static List<GameObject> ParticleInstances = new List<GameObject>();
    public static bool isSimulating;

    private static float simulationTime;
    private static float _maxTime;
    private static float maxTime
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
        float deltaT = Time.fixedDeltaTime;
        if (isSimulating)
        {
            for (int i = 0; i < ParticleInstances.Count; i++)
            {
                UpdateVelocity(deltaT, i);
                //CheckCollisions(ParticleInstances[i], deltaT, i);
            }
            if (simulationTime >= maxTime)
            {
                isSimulating = false;
                //Resetting velocity
                for (int i = 0; i < ParticleInstances.Count; i++)
                {
                    ParticleInstances[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
            simulationTime += deltaT;
        }
    }

    public static void OnSimulateClicked()
    {
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
        ParticleInstances[index].GetComponent<Rigidbody>().velocity = Velocity;
        ParticleInstances[index].GetComponent<ParticleInfo>().index = index;


        float Radius = Particle.Instances[index].Radius;

        ParticleInstances[index].transform.localScale = new Vector3(
            Radius,
            Radius,
            Radius);
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

    private void UpdateVelocity(float deltaT, int index)
    {
        Vector3 acceleration = new Vector3(
            Particle.Instances[index].Acceleration[0],
            Particle.Instances[index].Acceleration[1],
            Particle.Instances[index].Acceleration[2]);
        //V = u + at
        ParticleInstances[index].GetComponent<Rigidbody>().velocity += deltaT * acceleration;
    }
}
