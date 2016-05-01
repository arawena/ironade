using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GridControllerScript : MonoBehaviour {

	private Material lineMaterial;

	void Awake() {
		lineMaterial = new Material(Shader.Find("Custom/wireFrameShader"));
		lineMaterial.SetInt ("_ZWrite", 1);
		Debug.Log (transform.position.z);
	}

	void DrawLine(Vector3 point1, Vector3 point2) {
		GL.PushMatrix ();
		GL.LoadIdentity ();
		GL.MultMatrix (transform.localToWorldMatrix);
		GL.Begin(GL.LINES);

		GL.Color(new Color(lineMaterial.color.r,lineMaterial.color.g, lineMaterial.color.b, lineMaterial.color.a));
		GL.Vertex(point1);
		GL.Vertex(point2);
		GL.End();
		GL.PopMatrix ();
	}

	private void OnRenderObject() {
		GridOperations gridOps = GridOperations.sharedInstance;
		lineMaterial.SetPass(0);
		for (float i = gridOps.topCoordinate; i > gridOps.topCoordinate - gridOps.mapHeight; i -= gridOps.cellHeight) {
			DrawLine (new Vector3(gridOps.leftCoordinate, i,  transform.position.z), 
				new Vector3(gridOps.leftCoordinate+gridOps.mapWidth, i, transform.position.z));
		} 

		for (float i = gridOps.leftCoordinate; i < gridOps.leftCoordinate + gridOps.mapWidth; i += gridOps.cellWidth) {
			DrawLine (new Vector3(i,gridOps.topCoordinate, transform.position.z), 
				new Vector3(i,gridOps.topCoordinate-gridOps.mapHeight, transform.position.z));
		}
	}
}
