﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public static List<Particle> Instances = new List<Particle>();
    public Particle beforeSimulation;


    public Vector3 Displacement = new Vector3();
    public Vector3 InitialVelociy = new Vector3();
    public Vector3 FinalVelocity = new Vector3();
    public Vector3 Acceleration = new Vector3();
    public float Time;
    public Vector3 Position = new Vector3();
    public bool canCollide;
    public bool HasGravity;


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

    private float mass = 1;
    public float Mass
    {
        get { return mass; }
        set { mass = MyMaths.Magnitude(value); }
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

    public Particle()
    {
        //Do nothing init
    }
    public Particle(Particle copy)
    {
        Displacement = copy.Displacement;
        InitialVelociy = copy.InitialVelociy;
        FinalVelocity = copy.FinalVelocity;
        Acceleration = copy.Acceleration;
        Time = copy.Time;
        Position = copy.Position;
        canCollide = copy.canCollide;
        HasGravity = copy.HasGravity;
        Key = copy.Key;
        inValidInput = copy.inValidInput;
        _NumberOfInputs = copy.GetNumberOfInputs();
        mass = copy.mass;
        radius = copy.radius;
        restitution = copy.restitution;
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