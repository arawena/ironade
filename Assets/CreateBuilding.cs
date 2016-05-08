using UnityEngine;
using System.Collections;

public class CreateBuilding : MonoBehaviour {
	public Sprite[] buildingImages;
	public GameObject buildingPrefab;

	public void Init(int spriteIndex) {
		GameObject building = Instantiate (buildingPrefab);
		building.GetComponent<SpriteRenderer> ().sprite = buildingImages[spriteIndex];
	}
}
