using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple {

    public Vector2Int gridPosition;
    public Transform transform;

    public GameObject gameObject;

    public virtual void AppleInit() {
            gameObject = new GameObject("Apple", typeof(SpriteRenderer));
            gameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.redApple;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            gameObject.transform.localScale = new Vector3(2, 2, 2);
            transform = gameObject.transform;    
    }
    
     void SetGridPosition(Vector2Int gridPosition)
        {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }


        public void changePosition()
        {
            transform.position = new Vector3Int((Random.Range(-14, 19)), (Random.Range(-9, 9)));
        }

}

public class GoldenApple : Apple {

    public override void AppleInit() {
        gameObject = new GameObject("GoldenApple", typeof(SpriteRenderer));
        gameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.goldenApple;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        gameObject.transform.localScale = new Vector3(2, 2, 2);
        transform = gameObject.transform;   
    }
}