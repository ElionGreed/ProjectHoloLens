using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    public static UnitManager unitManager;
    public List<GameObject> playerUnits = new List<GameObject>(); //placeholder for more friendly units
    public List<GameObject> enemyUnits = new List<GameObject>();
    public GameObject pathfinding;
    public GameObject companion;
    public GameObject playerCharacter;
    public Button PushAway;
    public Button MovePlayer;

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
}
