using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PushAway : MonoBehaviour
{
    [SerializeField]
    Vector3 centre;
    [SerializeField]
    float radius = 3;
    Vector3 direction;
    public int myDamage = 20;
    EnemyUnit enemyUnit;
    Vector3 goal;
    public Button button;
   
    
   public void Explosion()
    {
        button.interactable = false;
        centre = gameObject.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(centre, radius);
        foreach (Collider collider in hitColliders)
        {

            if(collider.gameObject.CompareTag("Enemy"))
            {
                enemyUnit = collider.GetComponent<EnemyUnit>();
                enemyUnit.TakeDamage(myDamage);
                direction = (enemyUnit.transform.position - gameObject.transform.position).normalized;
                goal = enemyUnit.transform.position + direction * 4;
                StartCoroutine(MoveToPosition(enemyUnit.transform, goal));
                
            }          
        }
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 0.5f;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}