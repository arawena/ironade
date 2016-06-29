using UnityEngine;
using System.Collections;

public class RangeColliderScript : MonoBehaviour
{

    GameObject enemy;

    void LockOnEnemy(Collider2D other)
    {
        if (!enemy && other.gameObject.CompareTag("farmer"))
        {
            enemy = other.gameObject;
            GetComponentInParent<AttackEnemies>().enemy = enemy;
            GetComponentInParent<BuildingScript>().currentState.nextState = GetComponentInParent<AttackEnemies>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        LockOnEnemy(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        enemy = null;
        GetComponentInParent<AttackEnemies>().enemy = enemy;
    }

    void OnTriggerStay2D(Collider2D other)
    {

        LockOnEnemy(other);
    }
}
