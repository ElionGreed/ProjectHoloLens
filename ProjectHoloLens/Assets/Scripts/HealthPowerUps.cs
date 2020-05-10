using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public class HealthPowerUps : MonoBehaviour
{
    public float multi = 2f;
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
        Instantiate(effect, transform.position, transform.rotation);
        TestScriptHealthDeleteAfter stats = Player.GetComponent<TestScriptHealthDeleteAfter>();
        stats.health *= multi;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(time);

        stats.health /= multi;

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
