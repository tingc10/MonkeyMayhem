using UnityEngine;
using System.Collections;

public class ReloadScript : MonoBehaviour {

	public void ReloadLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
