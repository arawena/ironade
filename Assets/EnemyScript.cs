using UnityEngine;
using System.Collections;

public class EnemyScript : StateMachine {

	void Start () {
        currentState = gameObject.GetComponent<CalculatePathScript>();
	}
}
