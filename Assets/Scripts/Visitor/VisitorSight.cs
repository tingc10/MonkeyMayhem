using UnityEngine;
using System.Collections;

public class VisitorSight : MonoBehaviour {
	public bool playerInSight;
	public float fieldOfViewAngle = 110f;


	GameObject player;
	VisitorMovement movement;


	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		movement = GetComponent<VisitorMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Called once per frame for every collider/rigidbody that is touching collider
	void OnTriggerStay (Collider other) {

		// if a player enters the trigger sphere
		if (other.gameObject == player) {
			playerInSight = false;

			// Create a vector from the visitor to the player
			// and store the angle between it and forward (vector)
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

			// if the angle between forward and where the player is is less than
			// half the angle of view
			if (angle < fieldOfViewAngle * 0.5f || movement.currentAwareness == Awareness.Cautious) {
				movement.ElevateAwareness ();
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject == player) {

		}
	}
}
