using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 1;
    public AudioClip deathClip;
	public int escapeThreshold = 5;


	GameObject player;
//	PlayerMovement playerMovement;
	EnemyMovement enemyMovement;
    Animator anim;
    AudioSource enemyAudio;
//    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
	int amountResistance;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
//		player = GameObject.FindGameObjectWithTag ("Player");
//		playerMovement = player.GetComponent <PlayerMovement> ();
		enemyMovement = GetComponent<EnemyMovement>();
		anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
//        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
		amountResistance = 0;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


//    public void TakeDamage (int amount, Vector3 hitPoint)
//    {
//        if(isDead)
//            return;
//
//        enemyAudio.Play ();
//
//        currentHealth -= amount;
//            
//        hitParticles.transform.position = hitPoint;
//        hitParticles.Play();
//
//        if(currentHealth <= 0)
//        {
//            Death ();
//        }
//    }
	public void GetWhacked () {
		
		amountResistance++;
		Debug.Log (amountResistance);
		if (amountResistance >= escapeThreshold) {
			amountResistance = 0;
//			playerMovement.isCaptured = false;
			enemyMovement.Stun ();
		}

	}

    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
		ScoreManager.score += scoreValue;
        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


//    public void StartSinking ()
//    {
//        GetComponent <NavMeshAgent> ().enabled = false;
//        GetComponent <Rigidbody> ().isKinematic = true;
//        isSinking = true;
//		Debug.Log (scoreValue);
//
//        ScoreManager.score += scoreValue;
//		Debug.Log (ScoreManager.score);
//        Destroy (gameObject, 2f);
//    }
}
