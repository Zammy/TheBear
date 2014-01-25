using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	public Sprite sprite_DarkTree;

	public GameObject theBear;

	public float speed = 0.4f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			var tree = new GameObject();

			tree.transform.Translate(new Vector3(0f, 15.75f - Random.Range(0f, 0.5f), -1.25f + Random.Range(0f, 0.5f)));

			tree.transform.Rotate(0,0,Random.Range(-5f, 5f));
			tree.transform.RotateAround(new Vector3(0,0,0), Vector3.forward, Random.Range(0f, 360f));

			var spriteRenderer = tree.AddComponent<SpriteRenderer>();
			spriteRenderer.sprite = (Sprite)Instantiate(this.sprite_DarkTree);

			spriteRenderer.transform.parent = this.gameObject.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow)) {
			this.theBear.transform.localScale = new Vector3(1, 1, 1);
			this.transform.Rotate(Vector3.forward, this.speed);
		} else if(Input.GetKey(KeyCode.LeftArrow)) {
			this.theBear.transform.localScale = new Vector3(-1, 1, 1);
			this.transform.Rotate(Vector3.forward, -this.speed);
		}
	}
}