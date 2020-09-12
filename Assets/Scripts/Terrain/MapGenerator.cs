using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, Mesh, FallOff};
    public DrawMode drawMode;

    public NoiseData noiseData;
    public TerrainData terrainData;
    public TextureData textureData;

    public Material terrainMaterial;

    public const int mapChunkSize = 241;
    [Range(0, 5)]
    public int levelOfDetail;

    public bool autoUpdate;

    float[,] fallOffMap;

    void OnValuesUpdated()
    {
        if (!Application.isPlaying)
        {
            DrawMap();
        }
    }

    void OnTextureValuesUpdated()
    {
        textureData.ApplyToMaterial(terrainMaterial);
    }

    
    public void DrawMap()
    {
        // Generate map
        MapData mapData = GenerateMapData();

        MapDisplay display = FindObjectOfType<MapDisplay>();

        // Noise map
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
        }
        else if (drawMode == DrawMode.Mesh)       // Mesh map
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, terrainData.meshHeightMutiplier, terrainData.meshHeightCurve, levelOfDetail));
            textureData.ApplyToMaterial(terrainMaterial);
        }
        else if (drawMode == DrawMode.FallOff)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFallOffMap(mapChunkSize)));
        }
    }

    MapData GenerateMapData()
    {
        // Call noise script to generate noise map
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lancunarity, noiseData.offset);

        if (terrainData.useFallOff)
        {
            if (fallOffMap == null)
            {
                fallOffMap = FalloffGenerator.GenerateFallOffMap(mapChunkSize);
            }
        }

        // Loop through map
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                // Apply Falloff map if enable
                if (terrainData.useFallOff)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - fallOffMap[x, y]);
                }

                // Get height of the coordinate
                float currentHeight = noiseMap[x, y];
            }
        }
        // Update max and min height for terrain material
        textureData.UpdateMeshHeights(terrainMaterial, terrainData.minHeight, terrainData.maxHeight);

        return new MapData(noiseMap);
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
        if (textureData != null)
        {
            textureData.OnValuesUpdated -= OnTextureValuesUpdated;
            textureData.OnValuesUpdated += OnTextureValuesUpdated;
        }
    }
}

public struct MapData
{
    public readonly float[,] heightMap;

    public MapData(float[,] heightMap)
    {
        this.heightMap = heightMap;
    }
}
