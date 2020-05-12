using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public GameObject tree;
    public GameObject TreeTwo;
    public GameObject TreeThree;

    public float minTreeOneSize;
    public float maxTreeOneSize;

    public float minTreeTwoSize;
    public float maxTreeTwoSize;

    public float minTreeThreeSize;
    public float maxTreeThreeSize;

    public Texture2D noiseImage;

    public float TreeSizeOne;
    public float TreeSizeTwo;
    public float TreeSizeThree;


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

        for (int y = 0; y < TreeSizeOne; y++)
        {

            for (int x = 0; x < TreeSizeOne; x++)
            {

                float chance = noiseImage.GetPixel(x, y).r / (baseDensity / treeDensity);

                if (TreePlace(chance))
                {
                    float size = Random.Range(minTreeOneSize, maxTreeOneSize);

                    GameObject newTree = Instantiate(tree);
                    newTree.transform.localScale = Vector3.one * size;
                    newTree.transform.position = new Vector3(x, 0, y);
                    newTree.transform.parent = transform;
                }
            }
        }


        for (int y = 0; y < TreeSizeTwo; y++)
        {

            for (int x = 0; x < TreeSizeTwo; x++)
            {

                float chance = noiseImage.GetPixel(x, y).r / (baseDensity / treeDensity);

                if (ItemPlace(chance))
                {
                    float size = Random.Range(minTreeTwoSize, maxTreeTwoSize);

                    GameObject NewItem = Instantiate(TreeTwo);
                    NewItem.transform.localScale = Vector3.one * size;
                    NewItem.transform.position = new Vector3(x, 0, y);
                    NewItem.transform.parent = transform;
                }
            }
        }


        for (int y = 0; y < TreeSizeThree; y++)
        {

            for (int x = 0; x < TreeSizeThree; x++)
            {

                float chance = noiseImage.GetPixel(x, y).r / (baseDensity / treeDensity);

                if (ItemPlace(chance))
                {
                    float size = Random.Range(minTreeThreeSize, maxTreeThreeSize);

                    GameObject NewItem = Instantiate(TreeThree);
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
