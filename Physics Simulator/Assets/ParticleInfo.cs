using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInfo : MonoBehaviour {

    public int index;
    public Vector3 Velocity;
    public bool hasCollided;

    public float GraviationalConstant = 6.6740831f * Mathf.Pow(10, -11); 

    private void Update()
    {
        if (SimulateController.isSimulating == true)
        {
            //Puts change of time in terms of the simulation slider(static variable)
            float deltaT = Time.fixedDeltaTime * SimulateController.SimulationSpeed;
            UpdateVelocity(deltaT);
            if (Particle.Instances[index].HasGravity == true)
            {
                Debug.Log("Gravity ran");
                UpdateGravity(deltaT);
            }
            Move(deltaT);
        }
        else
        {
            
            //Velocity = Vector3.zero;
        }
    }

    private void UpdateGravity(float deltaT)
    {
        for (int i = 0; i < Particle.Instances.Count; i++)
        {
            if (i != index && Particle.Instances[i].HasGravity == true)
            {
                Vector3 radius = gameObject.transform.position - SimulateController.ParticleInstances[i].transform.position;

                float sizeOfRadiusSquared = Mathf.Pow(MyMaths.Vector_Magnitude(radius), 2);
                if (sizeOfRadiusSquared > 0)
                {
                    Vector3 acceleration = radius.normalized * GraviationalConstant * Particle.Instances[index].Mass / sizeOfRadiusSquared;
                    Vector3 additionalVelocity = acceleration * deltaT;
                    SimulateController.ParticleInstances[i].GetComponent<ParticleInfo>().Velocity += additionalVelocity;
                    Debug.Log("Additional velocity : " + additionalVelocity);
                }
            }
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
        int otherIndex = other.gameObject.GetComponent<ParticleInfo>().index;
        if (hasCollided == false && other.gameObject.tag == "Simulation" && Particle.Instances[otherIndex].canCollide == true && Particle.Instances[index].canCollide == true)
        {
            other.gameObject.GetComponent<ParticleInfo>().hasCollided = true;
            hasCollided = true;
            int index = other.GetComponent<ParticleInfo>().index;
            New__CalculateCollision(gameObject.GetComponent<ParticleInfo>(), other.GetComponent<ParticleInfo>());
        }

    }
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<ParticleInfo>().hasCollided = false;
        hasCollided = false;
    }

#region Momentum Convervation methods

    public static void New__CalculateCollision(ParticleInfo first, ParticleInfo second)
    {
        //Getting unit direction vector
        Vector3 deltaPosition = first.transform.position - second.transform.position;

        Vector3 unitDirection = (deltaPosition) / (MyMaths.Vector_Magnitude(deltaPosition));

        //Getting velocity parrlele and perpendicular before colllisiosn
        Vector3 FirstParrelleVelocity = CalculateParrelelVelocity(first.Velocity, unitDirection);
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

#endregion
}
