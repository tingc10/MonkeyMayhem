using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.05f;
    public int energyDrain = 1;
	public int escapeThreshold = 5;			// how much is required for monkey to resist in order to escape


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
	PlayerMovement playerMovement;
    EnemyHealth enemyHealth;
	EnemyMovement enemyMovement;
    bool playerInRange;
    float timer;
	int numResistance = 0;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
		playerMovement = player.GetComponent <PlayerMovement> ();
        enemyHealth = GetComponent<EnemyHealth>();
		enemyMovement = GetComponent<EnemyMovement> ();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0 && enemyMovement.isStunned == false)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

		if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (energyDrain);
			playerMovement.ConstrainMovement ();

        }
    }
}
