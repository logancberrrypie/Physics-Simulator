using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInfo : MonoBehaviour {

    public int index;
    public Vector3 Velocity;
    public bool hasCollided;

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
        if (hasCollided == false)
        {
            other.gameObject.GetComponent<ParticleInfo>().hasCollided = true;
            hasCollided = true;
            int index = other.GetComponent<ParticleInfo>().index;
            Debug.Log("Triggered");
            Debug.Log(other.GetComponent<ParticleInfo>().Velocity);
            Debug.Log(gameObject.GetComponent<ParticleInfo>().Velocity);


            New__CalculateCollision(gameObject.GetComponent<ParticleInfo>(), other.GetComponent<ParticleInfo>());
            Debug.Log("---------------------");
            Debug.Log(gameObject.GetComponent<ParticleInfo>().Velocity.x);
            Debug.Log(gameObject.GetComponent<ParticleInfo>().Velocity.y);
            Debug.Log(gameObject.GetComponent<ParticleInfo>().Velocity.z);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<ParticleInfo>().hasCollided = false;
        hasCollided = false;
    }
    public static void New__CalculateCollision(ParticleInfo first, ParticleInfo second)
    {
        //Getting unit direction vector
        Vector3 deltaPosition = first.transform.position - second.transform.position;

        Vector3 unitDirection = (deltaPosition) / (MyMaths.Vector_Magnitude(deltaPosition));

        Debug.Log(deltaPosition);
        Debug.Log(unitDirection);

        //Getting velocity parrlele and perpendicular before colllisiosn
        Vector3 FirstParrelleVelocity = CalculateParrelelVelocity(first.Velocity, unitDirection);
        Vector3 FirstPerpendicularVelocity = first.Velocity - FirstParrelleVelocity;

        Debug.Log(FirstParrelleVelocity);
        Debug.Log(FirstPerpendicularVelocity);

        Vector3 SecondParrelleVelocity = CalculateParrelelVelocity(second.Velocity, unitDirection);
        Vector3 SecondPerpendicularVelocity = second.Velocity - SecondParrelleVelocity;

        Debug.Log(SecondParrelleVelocity);
        Debug.Log(SecondPerpendicularVelocity);

        float first_e = Particle.Instances[first.index].Restitution;
        float second_e = Particle.Instances[second.index].Restitution;
        float e = first_e * second_e;

        Debug.Log(first_e);
        Debug.Log(second_e);
        Debug.Log(e);


        float m = Particle.Instances[first.index].Mass;
        float M = Particle.Instances[second.index].Mass;

        Debug.Log(m);
        Debug.Log(M);


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
