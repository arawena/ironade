using UnityEngine;
using System.Collections;

public class GridSettingsScript : MonoBehaviour {

	void Start () {
		Renderer mRenderer = gameObject.GetComponent<Renderer> ();
		Material mMaterial = mRenderer.material;
		mMaterial.SetFloat ("_GridSpacingX",GridOperations.sharedInstance.cellWidth);
		mMaterial.SetFloat ("_GridSpacingY",GridOperations.sharedInstance.cellHeight);
	}

}
