using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {

    public State currentState;
	
	void Update () {
        if (currentState && currentState.nextState)
        {
            currentState.enabled = false;
            State nextState = currentState.nextState;
            nextState.enabled = true;
            currentState.nextState = null;
            currentState = nextState;
        }
    }
}
