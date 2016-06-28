using UnityEngine;
using System.Collections;

public class GameControllerScript : StateMachine {

	[SerializeField]
	private const string foregroundLevelName = "2 - Foreground";

	void Start () {
        currentState = gameObject.GetComponent<SetupScene>();
	}

}
