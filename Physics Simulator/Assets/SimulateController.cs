using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateController : MonoBehaviour {

    public static bool isSimulating;

    public GameObject Prefab_Sphere;

    public void SimulateControl(Particle values)
    {
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
        //throw new NotImplementedException();
    }

    private static void CheckCollisions()
    {
        //Checking position of all Particles
        //throw new NotImplementedException();
    }
}
