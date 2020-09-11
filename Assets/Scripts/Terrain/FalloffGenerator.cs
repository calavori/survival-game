using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FalloffGenerator 
{
    public static float[,] GenerateFallOffMap(int size)
    {
        // Initialize map
        float[,] map = new float[size, size];

        // Loop through map
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                // Transform the coordinate of position to (-1;1)
                float x = i / (float)size * 2 - 1;
                float y = j / (float)size * 2 - 1;

                // Find out x or y nearest the edge of map
                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                map[i, j] = Evaluate(value);
            }
        }

        return map;
    }

    // Evalute for more black
    static float Evaluate(float value)
    {
        float a = 3;
        float b = 5f;

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}
