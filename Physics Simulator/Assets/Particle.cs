using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public static List<Particle> Instances = new List<Particle>();

    public GameObject ParticleObject;

    public float[] Displacement = new float[3];
    public float[] InitialVelociy = new float[3];
    public float[] FinalVelocity = new float[3];
    public float[] Acceleration = new float[3];
    public float Time;
    public float[] Position = new float[3];


    public string[] Key = new string[3] { "00000", "00000", "00000" };

    public bool[] inValidInput = new bool[3] { false, false, false };


    //Property variable link
    private int[] _NumberOfInputs = new int[3];
    public int[] GetNumberOfInputs()
    {
        updateNumberOfInputs();
        return _NumberOfInputs;
    }
    public void SetNumberOfInputs(int index, int value)
    {
        _NumberOfInputs[index] = value;
    }

    private float mass;
    public float Mass
    {
        get { return mass; }
        set
        {
            if (value >= 0)
            {
                mass = value;
            }
            else
            {
                mass = -value;
            }
        }
    }

    private float restitution;
    public float Restitution
    {
        get { return restitution; }
        set
        {
            restitution = value;
            MyMaths.Clamp(restitution, 0, 1);
        }
    }

    private float radius;
    public float Radius
    {
        get { return radius; }
        set { radius = MyMaths.Magnitude(value); }
    }



    private void updateNumberOfInputs()
    {
        for (int i = 0; i < 3; i++)
        {
            int NumInputs = 0;
            for (int j = 0; j < Key[i].Length; j++)
            {
                if ((Key[i])[j] == '1')
                {
                    NumInputs++;
                }
            }
            SetNumberOfInputs(i, NumInputs);
        }
    }
}