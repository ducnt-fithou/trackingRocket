using UnityEngine;
using System.Collections;

public class HelicopterRocket : MonoBehaviour {
	Transform target;
	Vector3 targetDir;
	public float speed=100;
	public float rotateSpeed=2;

	public float distanceToIgnoreTarget=100;
	bool targetIgnored=false;
	bool readyToDestroy=false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			return;
		target = GameObject.FindGameObjectWithTag("Player").transform;
		targetDir =  target.position-  transform.position ;
		float rotationZ = Mathf.Atan2(targetDir.y,targetDir.x) * Mathf.Rad2Deg;

		if(GameController.instance.isAlive)

		{        
			if (transform.position.y > target.position.y -distanceToIgnoreTarget && !targetIgnored) {
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.AngleAxis (rotationZ + 90, Vector3.forward), rotateSpeed * Time.deltaTime);
				transform.Translate (Vector3.down * speed);
			} else  {
				targetIgnored = true;
				transform.Translate (Vector3.down * speed);
				if (!readyToDestroy) {
					readyToDestroy = true;
					Destroy (gameObject,3f);
				}
			}
		} 	else if (!GameController.instance.isAlive ){
			transform.Translate(Vector3.down*speed);
			if (!readyToDestroy) {
				readyToDestroy = true;
				Destroy (gameObject,3f);
			}
		}

	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			gameObject.GetComponent<EnemyAttribute> ().collideWithPlayer ();
			//coll.gameObject.GetComponent<PlayerController> ().takeHit (coll.gameObject.GetComponent<EnemyAttribute> ().collideDamage);
			Destroy (gameObject);
		}

	}




}
