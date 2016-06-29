using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public GameObject target;

    public float speed;
    public float damage;

    private float threshold;
    private Vector3 travelDistance;
    private float totalTime = 0;

    private const int lifeTime = 3;

	void Start () {
        threshold = ((target.transform.position - transform.position) * 0.2F).magnitude;
        travelDistance = (target.transform.position - transform.position) * (GridOperations.sharedInstance.cellWidth * speed);
    }
	
	void Update () {
        if (target)
        {
            transform.position += travelDistance * Time.deltaTime;
            if (Vector3.Distance(transform.position, target.transform.position) < threshold)
            {
                FarmerControllerScript properties = target.GetComponent<FarmerControllerScript>();
                properties.GetHit(damage);

            }
        } else
        {
            DestroyObject(gameObject);
        }

        totalTime += Time.deltaTime;
        if(totalTime>lifeTime)
        {
            DestroyObject(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("farmer"))
        {
            FarmerControllerScript properties = other.gameObject.GetComponent<FarmerControllerScript>();
            properties.GetHit(damage);
            DestroyObject(gameObject);
        }
    }
}
