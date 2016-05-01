using UnityEngine;
using System.Collections;

public class SceneSetupScript : MonoBehaviour {
	public GameObject farmer;

	[SerializeField]
	private const string foregroundLevelName = "2 - Foreground";

	void Start () {
		(Instantiate (farmer, new Vector3 (0, 0, -3), Quaternion.identity) as GameObject).transform.parent = GameObject.Find(foregroundLevelName).transform;
	}
}
