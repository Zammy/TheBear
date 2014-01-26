using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class FlowerData {
	public int slot;
	public float anglePos;
	public GameObject gameObj;
}

public class Main : MonoBehaviour {

	public Sprite sprite_DarkTree;
	public Sprite sprite_WhiteFlower;

	public GameObject theBear;

	public float SPEED = 0.4f;

	private const float FLOWER_ANGULAR_SPACE = 11.25f;
	private List<FlowerData> flowers = new List<FlowerData>();

	private void GenerateTrees() 
	{
		float angle = 0;
		var numTrees = Random.Range(8, 10);
		for (int i = 0; i < numTrees; i++)
		{
			angle += Random.Range(300f, 500f) / numTrees;

			var tree = new GameObject();
			tree.name = "Foreground Tree";
			tree.transform.Translate(new Vector3(0f, 15.75f - Random.Range(0f, 0.5f), -1.25f + Random.Range(0f, 0.5f)));
			
			tree.transform.Rotate(0,0,Random.Range(-5f, 5f));
			tree.transform.RotateAround(new Vector3(0,0,0), Vector3.forward, angle);
			
			var spriteRenderer = tree.AddComponent<SpriteRenderer>();
			spriteRenderer.sprite = (Sprite)Instantiate(this.sprite_DarkTree);
			
			spriteRenderer.transform.parent = this.gameObject.transform;
		}
	}

	private int FlowerSlotFromPos(float angularPos) 
	{
		if (this.theBear.transform.localScale.x < 0) {
			angularPos -= FLOWER_ANGULAR_SPACE/2;
		}

		return Mathf.RoundToInt(angularPos / FLOWER_ANGULAR_SPACE);
	}

	private float FlowerPosFromSlot(int slot) 
	{
		return FLOWER_ANGULAR_SPACE * slot;
	}

	private void AddFlowerAt(int slot) 
	{
		var angularPos = this.FlowerPosFromSlot (slot);
		Debug.Log ("Tree at slot " + slot + " pos " + angularPos);
		var flower = new GameObject();
		flower.transform.Translate(new Vector3(0f, 10f, -.5f));
		flower.transform.RotateAround (new Vector3 (0, 0, 0), Vector3.forward, -angularPos);

		var rotation = this.gameObject.transform.rotation;

		this.gameObject.transform.rotation = Quaternion.identity;
		flower.transform.parent = this.gameObject.transform;

		this.gameObject.transform.rotation = rotation;


		flower.name = "Flower";

		var flowerData = new FlowerData () 
		{ 
			slot = slot,
			anglePos = angularPos,
			gameObj = flower
		};

		var spriteRenderer = flower.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = (Sprite)Instantiate (this.sprite_WhiteFlower);

		this.flowers.Add(flowerData);
	}

	// Use this for initialization
	void Start () 
	{
		this.GenerateTrees ();
//		for (int i = 0; i < 32; i++) {
//			int slot = i;
//			this.AddFlowerAt(slot);
//		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.RightArrow)) 
		{
			this.theBear.transform.localScale = new Vector3(1, 1, 1);
			this.transform.Rotate(Vector3.forward, this.SPEED);
		} 
		else if(Input.GetKey(KeyCode.LeftArrow)) 
		{
			this.theBear.transform.localScale = new Vector3(-1, 1, 1);
			this.transform.Rotate(Vector3.forward, -this.SPEED);
		}

		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			var pos = this.transform.rotation.eulerAngles.z;
			int slot = this.FlowerSlotFromPos (pos);
			var flowerData = this.flowers.Find (f => f.slot == slot);
			if (flowerData == null) 
			{
				this.AddFlowerAt(slot);
			} 
			else 
			{
				Destroy(flowerData.gameObj);
				this.flowers.Remove (flowerData);
			}
		}
	}
}