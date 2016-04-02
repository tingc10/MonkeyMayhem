using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float stunLength = 1.5f;
	public bool isStunned = false;

	Transform player;
    PlayerHealth playerHealth;
	PlayerMovement playerMovement;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
	Animator anim;
	float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerMovement = player.GetComponent<PlayerMovement> ();
        playerHealth = player.GetComponent <PlayerHealth> ();

		anim = GetComponent<Animator> ();
		enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();

		anim.SetBool ("isWalking", true);
    }

	public void Stun ()
	{
		playerMovement.isCaptured = false;
		nav.enabled = false;
		isStunned = true;
		timer = 0f;
		anim.SetBool ("isWalking", false);
	}
    void Update ()
    {
		
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
			if (nav.enabled == false) {
				timer += Time.deltaTime;
				if (timer >= stunLength) {
					nav.enabled = true;
					isStunned = false;
					anim.SetBool ("isWalking", true);
				}
			} else {
				nav.SetDestination (player.position);
			}

        }
        else
        {
            nav.enabled = false;
        }
    }
}
