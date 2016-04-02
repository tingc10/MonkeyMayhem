using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;

	RaycastHit[] oldHits;

	Vector3 offset;

	void Start(){
		offset = transform.position - target.position;
	}
		
	void FixedUpdate() {
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}

	void Update() {
		RaycastHit[] hits;
		hits = Physics.RaycastAll(transform.position, transform.forward, Vector3.Distance(transform.position, target.position) - 5.0f);
		for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits[i];
			Renderer rend = hit.transform.GetComponent<Renderer>();

			if (rend) {
				// Change the material of all hit colliders
				// to use a transparent shader.
//				rend.material.shader = Shader.Find("Transparent/Diffuse");
//				Color tempColor = rend.material.color;
//				tempColor.a = 0.3F;
//				rend.material.color = tempColor;


				AutoTransparent AT = rend.GetComponent<AutoTransparent>();
				if (AT == null) // if no script is attached, attach one
				{
					AT = rend.gameObject.AddComponent<AutoTransparent>();
				}
				AT.BeTransparent(); // get called every frame to reset the falloff
			}
		}
	}
}
