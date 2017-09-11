using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        CalculateCollision(gameObject, collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        CalculateCollision(gameObject, other.gameObject);
    }

    public static void CalculateCollision(GameObject firstObject, GameObject secondObject)
    {
        int firstIndex = firstObject.GetComponent<ParticleInfomation>().index;
        Particle first = Particle.Instances[firstIndex];
        Vector3 firstVelocity = SimulateController.ParticleInstances[firstIndex].GetComponent<Rigidbody>().velocity;

        int secondIndex = secondObject.GetComponent<ParticleInfomation>().index;
        Particle second = Particle.Instances[secondIndex];
        Vector3 secondVelocity = SimulateController.ParticleInstances[secondIndex].GetComponent<Rigidbody>().velocity;

        //Caclculates the line of centres where velocity changes.
        Vector3 unitDirection = (first.Position - second.Position) / MyMaths.Vector_Magnitude(first.Position - second.Position);

    //Getting the velocity components
        Vector3 FirstParrelel = CalculateParrelelVelocity(first.FinalVelocity, unitDirection);
        Vector3 FirstPerpendicular = firstVelocity - FirstParrelel;

        Vector3 SecondParrelel = CalculateParrelelVelocity(secondVelocity, unitDirection);
        Vector3 SecondPerpendicular = secondVelocity - SecondParrelel;

        float e = first.Restitution * second.Restitution;

        firstVelocity = CalculateAfterVelocityFirst(first.Mass, second.Mass, FirstParrelel, SecondParrelel, e) + FirstPerpendicular;
        SimulateController.ParticleInstances[firstIndex].GetComponent<Rigidbody>().velocity = firstVelocity;

        secondVelocity = CalculateAfterVelocitySecond(first.Mass, second.Mass, FirstParrelel, SecondParrelel, e) + SecondPerpendicular;
        SimulateController.ParticleInstances[secondIndex].GetComponent<Rigidbody>().velocity = secondVelocity;


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
