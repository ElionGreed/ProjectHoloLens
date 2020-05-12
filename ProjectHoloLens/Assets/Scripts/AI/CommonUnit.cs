using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CommonUnit : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    public int myHealth;
    [SerializeField]
    public int mySpeed;
    [SerializeField]
    public int myDamage;
    public int maxHealth;
    Unit unit;
    public Image HPBar;
    public GameObject panel;




    // Start is called before the first frame update
    protected virtual void Start()
    {
     
        unit = gameObject.GetComponent<Unit>();
        unit.numOfMoves = mySpeed;
        anim = GetComponent<Animator>();
        anim.SetFloat("HP", myHealth);
        maxHealth = myHealth;
    }

 
 
    public void TakeDamage(int damage)
    {
        myHealth -= damage;
        Debug.Log(myHealth);
        anim.SetFloat("HP", myHealth);
        Die();
    }

   

    void Die()
    {
        if (myHealth <= 0)
        {
            UnitManager.unitManager.enemyUnits.Remove(gameObject);
            StartCoroutine(Wait());
            if(gameObject.tag == "Enemy")
            {
                GameControl.control.kills++;
            }
            if(gameObject.tag == "Player")
            {
                panel.SetActive(true);
            }
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        if (gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
