using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{

    public enum DrawMode {
        NoiseMap,
        ColourMap,
        Mesh };

    public DrawMode drawMode;


    const int MapChunk = 241;
    [Range(0, 6)]
    public int DetailLevel;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public float MeshHeightMutli;
    public AnimationCurve MeshCurve;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;

    public void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(MapChunk, MapChunk, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[MapChunk * MapChunk];
        for (int y = 0; y < MapChunk; y++)
        {
            for (int x = 0; x < MapChunk; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * MapChunk + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGeneration.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGeneration.TextureFromColourMap(colourMap, MapChunk, MapChunk));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerate.GenerateTerrainMesh(noiseMap, MeshHeightMutli, MeshCurve, DetailLevel), TextureGeneration.TextureFromColourMap(colourMap, MapChunk, MapChunk));
        }
    }

    void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}


