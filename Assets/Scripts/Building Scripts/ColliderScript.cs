using UnityEngine;
using System.Collections;

public class ColliderScript : MonoBehaviour {

    void OnMouseDown()
    {
        if(GetComponent<BuildingScript>().currentState != gameObject.GetComponent<PositionScript>())
        {
            SpriteRenderer sprite = GetComponentsInChildren<SpriteRenderer>()[1];
            sprite.enabled = !sprite.enabled;
        }
    }
}
