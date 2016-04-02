using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public bool isCaptured;
	public float gravity = 20.0F;
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidBody;

	Vector2 touchOrigin = -Vector2.one;
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
		//Check if Input has registered more than zero touches
//		float h = 0f;
//		float v = 0f;
//		if (Input.touchCount > 0)
//		{
//			//Store the first touch detected.
//			Touch myTouch = Input.touches[Input.touches.Length - 1];
//			if (myTouch.phase == TouchPhase.Began) {
//				touchOrigin = myTouch.position;
//			} else if (myTouch.phase == TouchPhase.Moved) {
//				h = myTouch.position.x;
//				v = myTouch.position.y;
//			}
//
//			//Check if the phase of that touch equals Began
//			if (myTouch.phase == TouchPhase.Began)
//			{
//				//If so, set touchOrigin to the position of that touch
//				touchOrigin = myTouch.position;
//			}
//
//			//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
//			else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
//			{
//				//Set touchEnd to equal the position of this touch
//				Vector2 touchEnd = myTouch.position;
//
//				//Calculate the difference between the beginning and end of the touch on the x axis.
//				float x = touchEnd.x - touchOrigin.x;
//
//				//Calculate the difference between the beginning and end of the touch on the y axis.
//				float y = touchEnd.y - touchOrigin.y;
//
//				//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
//				touchOrigin.x = -1;
//
//				//Check if the difference along the x axis is greater than the difference along the y axis.
//				if (Mathf.Abs(x) > Mathf.Abs(y))
//					//If x is greater than zero, set horizontal to 1, otherwise set it to -1
//					horizontal = x > 0 ? 1 : -1;
//				else
//					//If y is greater than zero, set horizontal to 1, otherwise set it to -1
//					vertical = y > 0 ? 1 : -1;
//			}
//		}

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
		movement.y -= gravity * Time.deltaTime;
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
