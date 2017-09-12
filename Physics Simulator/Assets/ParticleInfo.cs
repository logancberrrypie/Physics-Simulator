using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInfo : MonoBehaviour {

    public int index;
    public Vector3 Velocity;

    private void Update()
    {
        if (SimulateController.isSimulating == true)
        {
            float deltaT = Time.fixedDeltaTime;
            UpdateVelocity(deltaT);
            Move(deltaT);
        }
        else
        {
            Velocity = Vector3.zero;
        }
    }

    private void Move(float deltaT)
    {
        //Moving by s = vt
        gameObject.transform.position += Velocity * deltaT;
    }

    private void UpdateVelocity(float deltaT)
    {
        //Using equation V = u + at
        Velocity = Velocity + Particle.Instances[index].Acceleration * deltaT; 
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        New__CalculateCollision(gameObject.GetComponent<ParticleInfo>(), gameObject.GetComponent<ParticleInfo>());
        Debug.Log(gameObject.GetComponent<ParticleInfo>().Velocity.x);
        Debug.Log(gameObject.GetComponent<ParticleInfo>().Velocity.y);
        Debug.Log(gameObject.GetComponent<ParticleInfo>().Velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.tag == "Simulation")
        {
            New__CalculateCollision(gameObject.GetComponent<ParticleInfo>(), gameObject.GetComponent<ParticleInfo>());
            Debug.Log(gameObject.GetComponent<ParticleInfo>().Velocity.x);
            Debug.Log(gameObject.GetComponent<ParticleInfo>().Velocity.y);
            Debug.Log(gameObject.GetComponent<ParticleInfo>().Velocity.z);
        }
    }

    public static void New__CalculateCollision(ParticleInfo first, ParticleInfo second)
    {
        //Getting unit direction vector
        Vector3 deltaPosition = first.transform.position - second.transform.position;
        Vector3 unitDirection = (deltaPosition) / (MyMaths.Vector_Magnitude(deltaPosition));

        //Getting velocity parrlele and perpendicular before colllisiosn
        Vector3 FirstParrelleVelocity = CalculateParrelelVelocity(second.Velocity, unitDirection);
        Vector3 FirstPerpendicularVelocity = first.Velocity - FirstParrelleVelocity;

        Vector3 SecondParrelleVelocity = CalculateParrelelVelocity(second.Velocity, unitDirection);
        Vector3 SecondPerpendicularVelocity = second.Velocity - SecondParrelleVelocity;

        float first_e = Particle.Instances[first.index].Restitution;
        float second_e = Particle.Instances[second.index].Restitution;
        float e = first_e * second_e;


        float m = Particle.Instances[first.index].Mass;
        float M = Particle.Instances[second.index].Mass;


        first.Velocity = CalculateAfterVelocityFirst(m, M, FirstParrelleVelocity, SecondParrelleVelocity, e) + FirstPerpendicularVelocity;
        second.Velocity = CalculateAfterVelocitySecond(m, M, FirstParrelleVelocity, SecondParrelleVelocity, e) + FirstPerpendicularVelocity;
    }

    public static Vector3 CalculateParrelelVelocity(Vector3 velocity, Vector3 unitDirection)
    {
        //returns a vector of the parrelel velocity
        return (MyMaths.DotProduct_Value(velocity, unitDirection) * unitDirection);
    }

    public static Vector3 CalculateAfterVelocityFirst(float m, float M, Vector3 v, Vector3 u, float e)
    {
        Vector3 topQuotient = (m * v) + (M * u) + (M * e * u) - (M * e * v);
        float bottomQuotient = m + M;
        return (1 / bottomQuotient) * topQuotient;
    }

    public static Vector3 CalculateAfterVelocitySecond(float m, float M, Vector3 v, Vector3 u, float e)
    {
        Vector3 topQuotient = (m * v) + (M * u) + (m * e * v) - (m * e * u);
        float bottomQuotient = m + M;
        return (1 / bottomQuotient) * topQuotient;
    }





}
