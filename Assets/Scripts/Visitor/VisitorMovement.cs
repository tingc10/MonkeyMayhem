﻿using UnityEngine;
using System.Collections;

public enum Awareness{
	Oblivious,
	Cautious,
	Terrified
};

public class VisitorMovement : MonoBehaviour {
	public Awareness currentAwareness;
	public float fleeingDistance = 10f;
	public float cautionDuration = 1f;
	public float defaultSpeed = 3.5f;
	public float terrifiedSpeed = 6f;

	Rigidbody visitorRigidBody;
	Transform player;
	NavMeshAgent nav;
	Animator anim;
	VisitorHealth visitorHealth;
	WanderingAI visitorWander;
	float timer;
	Vector3 runTo;


	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		visitorHealth = GetComponent<VisitorHealth> ();
		visitorRigidBody = GetComponent<Rigidbody> ();
		visitorWander = GetComponent<WanderingAI> ();

		visitorWander.enabled = false;
		nav = GetComponent<NavMeshAgent> ();
		nav.speed = defaultSpeed;
		anim = GetComponent<Animator> ();
		anim.SetBool ("IsWalking", true);
	}

	void Update() {
		if (!visitorHealth.hasFainted) {
			if (nav.enabled == false) {
				nav.enabled = true;
			}
			switch(currentAwareness) {
				case Awareness.Oblivious:

					visitorWander.enabled = true;

					break;
				case Awareness.Cautious:
					visitorWander.enabled = false;
					// Cautiousness makes visitor run away as long as monkey is within trigger
					timer += Time.deltaTime;
					if (timer >= cautionDuration) {
						currentAwareness = Awareness.Oblivious;
					}

					RunAway ();
					break;
				case Awareness.Terrified:
					visitorWander.enabled = false;
					
					RunAway ();
					break;
				default:
					Debug.Log ("Unidentified Awareness");
					break;
			}
			if (isIdle ()) {
				anim.SetBool ("IsWalking", false);
			} else {
				anim.SetBool ("IsWalking", true);
			}
		} else {
			nav.enabled = false;
		}

	}

	void RunAway() {
		// Reset Cautiousness level
		//		timer = 0f;
		//		currentAwareness = Awareness.Cautious;
		// get the direction vector from the player to the visitor
		Vector3 direction = (transform.position - player.position).normalized;
		runTo = transform.position + direction*fleeingDistance;
		nav.SetDestination (runTo);

	}


	bool isIdle () {
		// Check if we've reached the destination
		if (!nav.pathPending)
		{
			if (nav.remainingDistance <= nav.stoppingDistance)
			{
				if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void LootResponse(Vector3 appliedForce) {
		visitorRigidBody.AddForce (appliedForce, ForceMode.Impulse);
		currentAwareness = Awareness.Terrified;
		nav.speed = terrifiedSpeed;

	}

	public void ElevateAwareness () {
		
		if (currentAwareness == Awareness.Oblivious) {
			currentAwareness = Awareness.Cautious;
		}
		timer = 0f;
	}

		
}