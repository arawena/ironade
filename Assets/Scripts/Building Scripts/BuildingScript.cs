using UnityEngine;
using System.Collections;
using System.Linq;

public class BuildingScript : StateMachine
{
	private SpriteRenderer mRenderer;

    void Start () {
		mRenderer = gameObject.GetComponent<SpriteRenderer> ();
		rescaleSprite ();
    
        currentState = gameObject.GetComponent<PositionScript>();
        currentState.enabled = true;
    }

    private void rescaleSprite() {
        BuildingProperties properties = this.gameObject.GetComponent<BuildingProperties>();
		float mSpriteWidth = mRenderer.sprite.bounds.size.x;
		float mSpriteHeight = mRenderer.sprite.bounds.size.y;

		float scaleH = (properties.baseCellsWidth * GridOperations.sharedInstance.cellWidth)/mSpriteWidth;
		float scaleV = ((properties.baseCellsHeight + properties.noOfCellsHigh) *GridOperations.sharedInstance.cellHeight)/mSpriteHeight;

		transform.localScale = new Vector3 (scaleH, scaleV, 1.0f);
	}
}
