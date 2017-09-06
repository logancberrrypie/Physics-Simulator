using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateController : MonoBehaviour {

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

    public static float _Radius;
    public static float Radius
    {
        set { _Radius = MyMaths.Magnitude(value); }
        get { return _Radius; }
    }

    public GameObject Prefab_Sphere;

    public void SimulateControl(Particle values)
    {
        NEW__OnSimulateClicked();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float deltaT = Time.fixedDeltaTime;
        if (isSimulating)
        {
            for (int i =0;i<ParticleInstances.Count;i++)
            {
                UpdateVelocity(deltaT, i);
                CheckCollisions(ParticleInstances[i], deltaT, i);
                Debug.Log(ParticleInstances[i].GetComponent<Rigidbody>().velocity.y);
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

    private void CheckCollisions(GameObject gameObject, float deltaT, int i)
    {
        //throw new NotImplementedException();
    }

    private void UpdateVelocity(float deltaT, int i)
    {
        Vector3 acceleration = new Vector3(
            Particle.Instances[i].Acceleration[0],
            Particle.Instances[i].Acceleration[1],
            Particle.Instances[i].Acceleration[2]);
        //V = u + at
        ParticleInstances[i].GetComponent<Rigidbody>().velocity += deltaT * acceleration;
    }

    public static void NEW__OnSimulateClicked()
    {
        ParticleInstances = new List<GameObject>();
        string tag = "Simulation";
        DestroyObjectsWithTag(tag);

        maxTime = 0;
        GetSimulationSpeed();
        GetRadius();

        for (int i = 0;i<Particle.Instances.Count;i++)
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

    private static void GetRadius()
    {
        string tmp = UiController.instances.InputField_Radius.text;
        //Radius cannot be NULL and cannot be 0
        if (tmp == "" || Radius == '0')
        {
            //Default radius size is 1
            Radius = 1;
        }
        else
        {
            Radius = float.Parse(UiController.instances.InputField_Radius.text);
        }
    }

    private static void InstatiateParticle(int i)
    {
        Vector3 Position = new Vector3(
            Particle.Instances[i].Position[0],
            Particle.Instances[i].Position[1],
            Particle.Instances[i].Position[2]);

        ParticleInstances.Add(Instantiate(Resources.Load("Sphere"), Position, Quaternion.identity) as GameObject);

        Vector3 Velocity = new Vector3(
            Particle.Instances[i].InitialVelociy[0],
            Particle.Instances[i].InitialVelociy[1],
            Particle.Instances[i].InitialVelociy[2]);
        ParticleInstances[i].GetComponent<Rigidbody>().velocity = Velocity;

        ParticleInstances[i].transform.localScale = new Vector3(
            Radius,
            Radius,
            Radius);
    }

    private static void GetSimulationSpeed()
    {
        SimulationSpeed = UiController.instances.SliderSimulationSpeed.value;
    }

    public static void DestroyObjectsWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        for (int i =0;i<gameObjects.Length;i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
