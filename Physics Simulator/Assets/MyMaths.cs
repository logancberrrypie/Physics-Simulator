using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMaths : MonoBehaviour
{

    public static float Magnitude(float value)
    {
        if (value < 0)
        {
            return -value;
        }
        else
        {
            return value;
        }
    }
    public static float SquareRoot(float x)
    {
        if (x <= 0)
        {
            return 0;
        }
        //Arbitary first guess
        float guess = 1.0f;
        float current = 2.0f;
        while (guess != current)
        {
            guess = current;
            current = (float)(0.5 * (guess + x / guess));
        }
        return current;
    }
    public static float Clamp(float value, float min, float max)
    {
        if (min > max)
        {
            float temp = min;
            min = max;
            max = temp;
        }
        if (value > max)
        {
            value = max;
        }
        else if (value < min)
        {
            value = min;
        }
        return value;
    }
    public static float Vector_Magnitude(List<float> a)
    {
        float SquaredMagnitude = 0;
        for (int i = 0; i < a.Count; i++)
        {
            SquaredMagnitude += Mathf.Pow(a[i], 2);
        }
        return SquareRoot(SquaredMagnitude);
    }
    public static float DotProduct_Value(List<float> a, List<float> b)
    {
        if (a.Count != b.Count)
        {
            return 0;
        }
        else
        {
            float counter = 0;
            for (int i = 0; i < a.Count; i++)
            {
                counter += a[i] * b[i];
            }
            return counter;
        }
    }
    public static float DotProduct_Angle(List<float> a, List<float> b)
    {
        if (a.Count != b.Count)
        {
            return 0;
        }
        else
        {
            float Sinangle = Vector_Magnitude(a) * Vector_Magnitude(b) / DotProduct_Value(a, b);
            //arcsin means sin^-1 so inverse of sin
            return Mathf.Asin(Sinangle);
        }
    }
}