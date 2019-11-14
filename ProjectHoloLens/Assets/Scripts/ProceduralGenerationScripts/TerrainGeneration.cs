using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public GameObject tree;
    public GameObject worldItems;

    public float minTSize;
    public float maxTSize;

    public float minISize;
    public float maxISize;


    public Texture2D noiseImage;

    //Enviroment Size
    public float forestSize;
    public float worldItemSize;

    //Density For the Enviroment Items
    public float treeDensity;
    private float baseDensity = 5.0f;
    public float itemDensity;
    private float itemBaseDensity = 5.0f;

    // Use this for initialization
    void Start()
    {
        Generate();
    }

    public void Generate()
    {

        for (int y = 0; y < forestSize; y++)
        {

            for (int x = 0; x < forestSize; x++)
            {

                float chance = noiseImage.GetPixel(x, y).r / (baseDensity / treeDensity);

                if (TreePlace(chance))
                {
                    float size = Random.Range(minTSize, maxTSize);

                    GameObject newTree = Instantiate(tree);
                    newTree.transform.localScale = Vector3.one * size;
                    newTree.transform.position = new Vector3(x, 0, y);
                    newTree.transform.parent = transform;
                }
            }
        }


        for (int y = 0; y < worldItemSize; y++)
        {

            for (int x = 0; x < worldItemSize; x++)
            {

                float chance = noiseImage.GetPixel(x, y).r / (baseDensity / treeDensity);

                if (ItemPlace(chance))
                {
                    float size = Random.Range(minISize, maxISize);

                    GameObject NewItem = Instantiate(worldItems);
                    NewItem.transform.localScale = Vector3.one * size;
                    NewItem.transform.position = new Vector3(x, 0, y);
                    NewItem.transform.parent = transform;
                }
            }
        }
    }

    //Returns true or false given some chance from 0 to 1
    public bool TreePlace(float chance)
    {
        if (Random.Range(0.0f, 1.0f) <= chance)
        {
            return true;
        }
        return false;
    }

    public bool ItemPlace(float chance)
    {
        if (Random.Range(0.0f, 1.0f) <= chance)
        {
            return true;
        }
        return false;
    }

}
