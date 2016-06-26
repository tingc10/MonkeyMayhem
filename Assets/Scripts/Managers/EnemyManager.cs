using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
	public int maxInstantiations = 3;
	public static int numEnemiesActive = 0;

	/**
	 *  Checks if the target is on navmesh
	 */
	bool isTargetOnNav(Vector3 target) {
		NavMeshHit hit;
		NavMesh.SamplePosition (target, out hit, 2f, 1 << NavMesh.GetAreaFromName("Walkable"));
//		Debug.Log(" myNavHit = " + hit + " myNavHit.position = " + hit.position + " target = " + target);
		if (hit.position.x == Mathf.Infinity && 
			hit.position.y == Mathf.Infinity && 
			hit.position.z == Mathf.Infinity) {
			return false;
		}
		return true;
	}

    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
		numEnemiesActive = 0;
    }


    void Spawn ()
    {
		if(playerHealth.currentHealth <= 0f || (numEnemiesActive+1) > maxInstantiations)
        {
            return;
        }
		numEnemiesActive++;
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Transform spawnPoint = spawnPoints [spawnPointIndex];

		if (isTargetOnNav(spawnPoint.position)) {
			Instantiate (enemy, spawnPoint.position, spawnPoint.rotation);
		}
        
    }
}
