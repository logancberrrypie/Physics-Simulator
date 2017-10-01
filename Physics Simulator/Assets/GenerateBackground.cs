using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBackground : MonoBehaviour {

    
    public GameObject[] prefabs = new GameObject[2];


    //Number of additional 4x4 squares added to a X , Y and Z side
    private int offset = 2;
    private int sizeOfSprite = 4;

    private Vector3 min = new Vector3();
    private Vector3 max = new Vector3();

    private Vector3 NumberOfInstatiates = new Vector3();

    public void CreateBackground()
    {
        prefabs[0] = Resources.Load("4x4 Black") as GameObject;
        prefabs[1] = Resources.Load("4x4 White") as GameObject;
        string tag = "Background_Prefab";
        SimulateController.DestroyObjectsWithTag(tag);

        GetMin(0);
        GetMin(1);
        GetMin(2);
        GetMax(0);
        GetMax(1);
        GetMax(2);
        CalculateNumberOfinstatiates();

        Debug.Log("Number of instances");
        Debug.Log(NumberOfInstatiates);

        Debug.Log(min[0]);
        Debug.Log(min[1]);
        Debug.Log(min[2]);

        Debug.Log(max[0]);
        Debug.Log(max[1]);
        Debug.Log(max[2]);

        InstatiatePrefabs();
    }

    private void InstatiatePrefabs()
    {
        int n = 0;
        for (int i =0;i<NumberOfInstatiates.x;i++)
        {
            for (int j =0;j<NumberOfInstatiates.y;j++)
            {
                int value = (int)(0.5f * (1 + Mathf.Pow(-1, j-1 + n)));
                Vector3 Position = new Vector3(
                    min.x - sizeOfSprite + sizeOfSprite * i,
                    min.y - sizeOfSprite + sizeOfSprite * j,
                    0.0f);
                Debug.Log(Position);
                Instantiate(prefabs[value], Position, Quaternion.identity);
            }
            n++;
        }
    }

    private void CalculateNumberOfinstatiates()
    {
        Vector3 temp = ((max - min) / sizeOfSprite) + Vector3.one * offset;
        temp = MyMaths.Vector_Ceil(temp);
        for (int i =0;i<3;i++)
        {
            NumberOfInstatiates[i] = MyMaths.Magnitude(temp[i]);
        }

    }
    private void GetMin(int n)
    {
        float InitialPos;
        float finalPos;
        for (int i =0;i<Particle.Instances.Count;i++)
        {
            InitialPos = Particle.Instances[i].Position[n];
            finalPos = InitialPos + Particle.Instances[i].Displacement[n];
            if (InitialPos < min[n])
            {
                min[n] = InitialPos;
            }
            if (finalPos < min[n])
            {
                min[n] = finalPos;
            }
        }
    }
    private void GetMax(int n)
    {
        float InitialPos;
        float finalPos;
        for (int i = 0; i < Particle.Instances.Count; i++)
        {
            InitialPos = Particle.Instances[i].Position[n];
            finalPos = InitialPos + Particle.Instances[i].Displacement[n];
            Debug.Log("Final pos : " + finalPos);
            if (InitialPos > max[n])
            {
                max[n] = InitialPos;
            }
            if (finalPos > max[n])
            {
                max[n] = finalPos;
            }
        }
    }

}
