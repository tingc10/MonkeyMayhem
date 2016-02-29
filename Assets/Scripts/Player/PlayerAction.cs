using UnityEngine;
using System.Collections;

public class PlayerAction : MonoBehaviour {

//	public int damagePerShot = 20;
	public float timeBetweenBullets = 0.1f;
//	public float range = 100f;

	GameObject capturer;
	GameObject curCapturer;
	PlayerMovement playerMovement;
	EnemyHealth curEnemyHealth;
//	EnemyAttack capturer;

	float timer;
//	int escapeThreshold = 5;	// number of times required to resist to escape
//	int numResist = 0;
//	Ray shootRay;
//	RaycastHit shootHit;
//	int shootableMask;
//	ParticleSystem gunParticles;
//	LineRenderer gunLine;
//	AudioSource gunAudio;
//	Light gunLight;
//	float effectsDisplayTime = 0.2f;


	void Awake ()
	{

		playerMovement = GetComponent <PlayerMovement> ();

//		shootableMask = LayerMask.GetMask ("Shootable");
//		gunParticles = GetComponent<ParticleSystem> ();
//		gunLine = GetComponent <LineRenderer> ();
//		gunAudio = GetComponent<AudioSource> ();
//		gunLight = GetComponent<Light> ();
	}


	void Update ()
	{
		timer += Time.deltaTime;

		if(Input.GetMouseButtonDown (0) && timer >= timeBetweenBullets && Time.timeScale != 0)
		{
//			Debug.Log ("hi");
			Action ();
		}

//		if(timer >= timeBetweenBullets * effectsDisplayTime)
//		{
//			DisableEffects ();
//		}
	}
	void OnTriggerEnter (Collider other) {
		Debug.Log (capturer);

		if (other.gameObject.tag == "Capturer") {
			Debug.Log ("HELLO");
			curCapturer = other.gameObject;
			curEnemyHealth = curCapturer.GetComponent <EnemyHealth> ();
		}
	}


	public void DisableEffects ()
	{
//		gunLine.enabled = false;
//		gunLight.enabled = false;
	}



	void Action ()
	{
		timer = 0f;


		if (playerMovement.isCaptured == true) {
			
			curEnemyHealth.GetWhacked ();
		}
//		gunAudio.Play ();
//
//		gunLight.enabled = true;
//
//		gunParticles.Stop ();
//		gunParticles.Play ();
//
//		gunLine.enabled = true;
//		gunLine.SetPosition (0, transform.position);
//
//		shootRay.origin = transform.position;
//		shootRay.direction = transform.forward;

//		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
//		{
//			EnemyHealth curEnemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
//			if(curEnemyHealth != null)
//			{
//				curEnemyHealth.TakeDamage (damagePerShot, shootHit.point);
//			}
//			gunLine.SetPosition (1, shootHit.point);
//		}
//		else
//		{
//			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
//		}
	}
}
