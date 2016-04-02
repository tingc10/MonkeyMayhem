﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public float pounceSpeed = 7f;
	public bool isCaptured;
	public float jumpForce = 5f;
	public float pounceBoost = 1.5f;

	Vector3 movement;
	Vector3 movementTrajectory;
	Animator anim;
//	CapsuleCollider capsuleCollider;
	Rigidbody playerRigidBody;
	PlayerAction playerAction;
	bool shouldPounce = false;
	bool isGrounded = true;
//	PlayerHealth playerHealth;
//	bool shiftUp = false;
//	GameObject latchCharacter;
//	HingeJoint hingeJoint;
//	float camRayLength = 100f;

	void Awake() {
		
		anim = GetComponent<Animator> ();
		playerRigidBody = GetComponent<Rigidbody> ();
//		playerHealth = GetComponent<PlayerHealth> ();
		playerAction = GetComponent<PlayerAction>();
		isCaptured = false;

//		capsuleCollider = GetComponent<CapsuleCollider> ();
//		distToGround = capsuleCollider.bounds.extents.y;
	}
	void FixedUpdate() {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		if (isCaptured == false) {
				
			MoveAndTurn (h, v);
			Animating (h, v);
		}
			

	}

	public void ConstrainMovement() {
		if (isCaptured == false) {
			isCaptured = true;
		}
	}

	public void PounceForward() {
		if (isGrounded) {
			shouldPounce = true;

		}
	}

	void MovePlayer(float h, float v) {
		if (isGrounded) {
			movement.Set (h, 0f, v);

		} else {
			movement.Set (movementTrajectory.x, 0f, movementTrajectory.z);
		}
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidBody.MovePosition (transform.position + movement);
	}

	void MoveAndTurn (float h, float v) {
		
//		playerRigidBody.velocity = new Vector3(0,10,0);

		if (shouldPounce) {
			Debug.Log (playerRigidBody.useGravity);
			playerRigidBody.AddForce (new Vector3 (pounceBoost * h, jumpForce, pounceBoost * v), ForceMode.Impulse);
			shouldPounce = false;
			movementTrajectory.Set (h, 0f, v);
		}

		MovePlayer (h, v);
		

		// Rotate towards walking direction
		if (movement != Vector3.zero && isGrounded) {
//			Quaternion newRotation = Quaternion.LookRotation (movement);

			//get the angle between transform.forward and target delta
			float angleDiff = Vector3.Angle(transform.forward, movement);

			// get its cross product, which is the axis of rotation to
			// get from one vector to the other
			Vector3 cross = Vector3.Cross(transform.forward, movement);
			playerRigidBody.AddTorque (cross * angleDiff);


		}



	}

//	void LootVisitor (GameObject colObject) {
//		
//		Rigidbody visitorRigidBody = colObject.GetComponent<Rigidbody>();
//		VisitorMovement visitorMovement = colObject.GetComponent<VisitorMovement> ();
////		VisitorMeta visitorMeta = colObject.GetComponent<VisitorMeta> ();
//		VisitorHealth visitorHealth = colObject.GetComponent<VisitorHealth>();
//
//		// push the visitor with current force
//		float energyReturn = visitorHealth.VisitorAttacked();
//
//		Vector3 currentForce = playerRigidBody.velocity;
//		visitorRigidBody.AddForce (currentForce, ForceMode.Impulse);
//		playerHealth.Eat (energyReturn);
//	}
//
	void OnCollisionEnter (Collision colInfo) {
		string objectTag = colInfo.gameObject.tag;
		if (!isGrounded && objectTag != "Floor") {
			movementTrajectory.Set (0, 0, 0);

		}

		if (objectTag == "Visitor") {
			playerAction.LootVisitor (colInfo.gameObject);

		}


		if (objectTag == "Floor") {
			isGrounded = true;

		}

	}

	void OnCollisionExit (Collision colInfo) {
//		if (latchCharacter) {
//			latchCharacter = null;
//		}
		if (colInfo.gameObject.tag == "Floor") {
			isGrounded = false;
		}

	}

	void Animating(float h, float v) {
		bool walking = h != 0f || v != 0f;
		if (!isGrounded) {
			anim.SetBool ("IsWalking", false);

		} else {
			anim.SetBool ("IsWalking", walking);
		}

	}
}



//	void Turning() {
/* Example code - mouse to rotate */
//		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
//		RaycastHit floorHit;
//
//		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
//			Vector3 playerToMouse = floorHit.point - transform.position;
//			playerToMouse.y = 0f;
//
//			// stores rotation
//			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
//			playerRigidBody.MoveRotation (newRotation);
//		}


//	}