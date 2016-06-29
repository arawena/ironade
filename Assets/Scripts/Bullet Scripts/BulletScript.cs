using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{

    public GameObject target;

    public float speed;
    public float damage;

    private float totalTime = 0;
    private const int lifeTime = 3;

    void Start()
    {

    }

    void Update()
    {
        if (target)
        {
            Vector3 difference = target.transform.position - transform.position;
            Vector3 direction = difference / difference.magnitude;
            Vector3 scaledVector = new Vector3(direction.x * GridOperations.sharedInstance.cellWidth, direction.y * GridOperations.sharedInstance.cellHeight, direction.z);
            transform.position += scaledVector * speed * Time.deltaTime;
        }
        else
        {
            DestroyObject(gameObject);
        }

        totalTime += Time.deltaTime;
        if (totalTime > lifeTime)
        {
            DestroyObject(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            FarmerControllerScript properties = other.gameObject.GetComponent<FarmerControllerScript>();
            properties.GetHit(damage);
            DestroyObject(gameObject);
        }
    }
}
