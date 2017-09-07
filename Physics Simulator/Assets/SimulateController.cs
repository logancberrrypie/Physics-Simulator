using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateController : MonoBehaviour {

    public static List<GameObject> ParticleInstances = new List<GameObject>();
    public static bool isSimulating;

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
        /*
        Vector3 Position = new Vector3(
            values.Position[0],
            values.Position[1],
            values.Position[2]);

        int ParticleSelected = UiController.instances.DropBoxParticle.value;
        Particle.Instances[ParticleSelected].ParticleObject = Instantiate(Resources.Load("Sphere"), Position, Quaternion.identity) as GameObject;
        Vector3 Velocity = new Vector3(
            values.FinalVelocity[0],
            values.FinalVelocity[1],
            values.FinalVelocity[2]);
        Particle.Instances[ParticleSelected].ParticleObject.GetComponent<Rigidbody>().velocity = Velocity;
        isSimulating = true;
        */
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isSimulating)
        {
            int ParticleSelected = UiController.instances.DropBoxParticle.value;
            UiController.instances.UpdateUI(Particle.Instances[ParticleSelected]);

            CheckCollisions();
            AccelerationControl();
        }



	}

    private static void AccelerationControl()
    {
        throw new NotImplementedException();
    }

    private static void CheckCollisions()
    {
        //Checking position of all Particles
        throw new NotImplementedException();
    }

    public static void NEW__OnSimulateClicked()
    {
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

        var temp = Instantiate(Resources.Load("Sphere"), Position, Quaternion.identity) as GameObject;

        Vector3 Velocity = new Vector3(
            Particle.Instances[i].FinalVelocity[0],
            Particle.Instances[i].FinalVelocity[1],
            Particle.Instances[i].FinalVelocity[2]);
        temp.GetComponent<Rigidbody>().velocity = Velocity;

        temp.transform.localScale = new Vector3(
            Radius,
            Radius,
            Radius);
        ParticleInstances.Add(temp);
    }

    private static void GetSimulationSpeed()
    {
        SimulationSpeed = UiController.instances.SliderSimulationSpeed.value;
    }

    private static void DestroyObjectsWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        for (int i =0;i<gameObjects.Length;i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
