using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator
{
    // create texture from 1D color map
    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    // Create texture from 2D height map
    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        // Get width and length of noise map
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        // Initailize color for the map, color map is 1D map
        Color[] colorMap = new Color[width * height];

        // Loop through map
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Use 2 color black and white to display the different
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }
        return TextureFromColorMap(colorMap, width, height);
    } 
}
