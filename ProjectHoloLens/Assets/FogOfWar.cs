using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{


    public GameObject matFogOfWar;
    public Transform matPlayer;
    public LayerMask matPogLayer;
    public float matRadius = 7f;

    private float matRadiusSqu 
    { 
    get { 
            return matRadius * matRadius; 
        } 
    }

    private Mesh matmesh;
    private Vector3[] matvertices;
    private Color[] matcolours;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray(transform.position, matPlayer.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, matPogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < matvertices.Length; i++)
            {
                Vector3 v = matFogOfWar.transform.TransformPoint(matvertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < matRadiusSqu)
                {
                    float alpha = Mathf.Min(matcolours[i].a, dist / matRadiusSqu);
                    matcolours[i].a = alpha;
                }
            }
            UpdateColor();
        }
    }

    void Initialize()
    {
        matmesh = matFogOfWar.GetComponent<MeshFilter>().mesh;
        matvertices = matmesh.vertices;
        matcolours = new Color[matvertices.Length];
        for (int i = 0; i < matcolours.Length; i++)
        {
            matcolours[i] = Color.black;
        }
        UpdateColor();
    }

    public void UpdateColor()
    {
        matmesh.colors = matcolours;
    }
}
