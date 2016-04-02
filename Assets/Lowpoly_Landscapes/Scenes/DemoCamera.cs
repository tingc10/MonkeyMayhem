using UnityEngine;
using System.Collections;

public class DemoCamera : MonoBehaviour
{

	public Vector3 move = Vector3.zero;

	void Update()
	{ transform.Translate(move); }

}
