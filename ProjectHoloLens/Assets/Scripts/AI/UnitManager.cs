using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager unitManager;
    public bool test;
    public List<GameObject> playerUnits = new List<GameObject>();
    public List<GameObject> enemyUnits = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (unitManager == null)
        {
            DontDestroyOnLoad(gameObject);
            unitManager = this;
        }
        else if (unitManager != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame

}
