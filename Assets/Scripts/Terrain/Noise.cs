using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise
{
   public static float[,] GenerateNoiseMap (int mapWidth, int mapHeight, int seed, float scale,
                                            int octaves, float persistance, float lancunarity, Vector2 offset)
    {
        // Initialize 2D noise map
        float[,] noiseMap = new float[mapWidth, mapHeight];


        // Use seed to generate the same map
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        // Force the scale never under 0
        if (scale <= 0)
        {
            scale = 0.001f;
        }

        // Initialize max and min noise height to keep track later
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        // Initialize half width and height of map
        float halfWidth = mapWidth / 2;
        float halfHeight = mapHeight / 2;

        // loop through noise map
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                // Loop through octaves
                for (int i = 0; i < octaves; i++)
                {
                    // calculate sample of (x, y)
                    // calculate with half width and height so when scale the map it still at the center of the map
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;      // scale can not under 0
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    // calculate perlin noise value, return between 0 and 1
                    // (* 2 - 1) so that the result can be negative
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lancunarity;
                }

                // Keep track max and min noise height
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                } else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                // set noise of coordinates (x,y) to the noise height
                noiseMap[x, y] = noiseHeight;
            }
        }

        // Normalize so that noise can be beetween 0 and 1
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        // return 2D noise map
        return noiseMap;
    }
}
