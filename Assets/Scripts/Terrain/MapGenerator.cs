using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColorMap, Mesh, FallOff};
    public DrawMode drawMode;

    public NoiseData noiseData;
    public TerrainData terrainData;

    public const int mapChunkSize = 241;
    [Range(0, 5)]
    public int levelOfDetail;

    public bool autoUpdate;

    public TerrainType[] regions;

    float[,] fallOffMap;

    private void Awake()
    {
        fallOffMap = FalloffGenerator.GenerateFallOffMap(mapChunkSize);
    }

    void OnValuesUpdated()
    {
        if (!Application.isPlaying)
        {
            DrawMap();
        }
    }

    
    public void DrawMap()
    {
        // Generate map
        MapData mapData = GenerateDataMap();

        MapDisplay display = FindObjectOfType<MapDisplay>();

        // Noise map
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
        }
        else if (drawMode == DrawMode.ColorMap)       // Color Map
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(mapData.colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)       // Mesh map
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, terrainData.meshHeightMutiplier, terrainData.meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(mapData.colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.FallOff)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFallOffMap(mapChunkSize)));
        }
    }

    MapData GenerateDataMap()
    {
        // Call noise script to generate noise map
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lancunarity, noiseData.offset);

        // Initailize the 1D color map with the size of the map
        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        // Loop through map
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                // Apply Falloff map if enable
                if (terrainData.useFallOff)
                {
                    noiseMap[x, y] = noiseMap[x, y] - fallOffMap[x, y];
                }

                // Get height of the coordinate
                float currentHeight = noiseMap[x, y];
                // Loop through regions
                for (int i = 0; i < regions.Length; i++)
                {
                    // Set the color of the coordinate by the color of region
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        return new MapData(noiseMap, colorMap);
    }

    // Validate the stat
    private void OnValidate()
    {
        if (terrainData != null)
        {
            terrainData.OnValuesUpdated -= OnValuesUpdated;
            terrainData.OnValuesUpdated += OnValuesUpdated;
        }
        if (noiseData != null)
        {
            noiseData.OnValuesUpdated -= OnValuesUpdated;
            noiseData.OnValuesUpdated += OnValuesUpdated;
        }

        // Use falloff map in editor
        fallOffMap = FalloffGenerator.GenerateFallOffMap(mapChunkSize);
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}

public struct MapData
{
    public readonly float[,] heightMap;
    public readonly Color[] colorMap;

    public MapData(float[,] heightMap, Color[] colorMap)
    {
        this.heightMap = heightMap;
        this.colorMap = colorMap;
    }
}
