using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{

    public float bSpeed = 100f;
    public float time = 3f;

    public GameObject effect;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(pickup(other));
        }
    }


    IEnumerator pickup(Collider Player)
    {
        PlayerCon stats = Player.GetComponent<PlayerCon>();
        stats.speed += bSpeed;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(time);

        stats.speed -= bSpeed;

        Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
