using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {

	public float wanderDistance;
	public float wanderTimer;


	private Transform target;
	private NavMeshAgent agent;
	private float timer;
	private Vector3 newDest;

	// Use this for initialization
	void OnEnable () {
		agent = GetComponent<NavMeshAgent> ();
		timer = wanderTimer;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= wanderTimer) {
			newDest = RandomNavSphere(transform.position, wanderDistance, -1);
			agent.SetDestination(newDest);
			timer = 0;
		}
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
		Vector3 randDirection = Random.insideUnitSphere * dist;

		randDirection += origin;

		NavMeshHit navHit;

		NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);

		return navHit.position;
	}

	public Vector3 NewDestination () {
		return newDest;
	}
}
