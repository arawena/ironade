using UnityEngine;
using System.Collections;

public class RangeScript : MonoBehaviour {
    SpriteRenderer mRenderer;

    public void RescaleSprite()
    {
        mRenderer = GetComponent<SpriteRenderer>();
        BuildingProperties properties = GetComponentInParent<BuildingProperties>();
  
        transform.localPosition -= new Vector3(0, GridOperations.sharedInstance.cellHeight * properties.noOfCellsHigh/5.0f , 0);

        float mSpriteWidth = mRenderer.sprite.bounds.size.x;
        float mSpriteHeight = mRenderer.sprite.bounds.size.y;

        float scaleH = (properties.rangeRadius * 2 * GridOperations.sharedInstance.cellWidth) / mSpriteWidth;
        float scaleV = (properties.rangeRadius * 2 * GridOperations.sharedInstance.cellHeight) / mSpriteHeight;

        float parentX = transform.parent.localScale.x;
        float parentY = transform.parent.localScale.y;

        transform.localScale = new Vector3(scaleH/parentX, scaleV/parentY, 1.0f);
    }
}
