using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public bool isCaptured;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidBody;

//	int floorMask;
//	float camRayLength = 100f;

	void Awake() {
//		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidBody = GetComponent<Rigidbody> ();
		isCaptured = false;

	}
	void FixedUpdate() {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");


		if (isCaptured == false) {
			MoveAndTurn (h, v);
		}


		Animating (h, v);
	}

	public void ConstrainMovement() {
		if (isCaptured == false) {
			isCaptured = true;
		}
	}

	void MoveAndTurn (float h, float v) {
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidBody.MovePosition (transform.position + movement);

		// Rotate towards walking direction
		if (movement != Vector3.zero) {
//			Quaternion newRotation = Quaternion.LookRotation (movement);

			//get the angle between transform.forward and target delta
			float angleDiff = Vector3.Angle(transform.forward, movement);

			// get its cross product, which is the axis of rotation to
			// get from one vector to the other
			Vector3 cross = Vector3.Cross(transform.forward, movement);
			playerRigidBody.AddTorque (cross * angleDiff);


		}



	}



	void Turning() {
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


	}

	void Animating(float h, float v) {
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}
}
