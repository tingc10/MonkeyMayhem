using UnityEngine;
using System.Collections;

public class AutoTransparent : MonoBehaviour {

	private Shader m_OldShader = null;
	private Color m_OldColor = Color.black;
	private float m_Transparency = 0.3f;
	private const float m_TargetTransparancy = 0.3f;
	private const float m_FallOff = 0.1f; // returns to 100% in 0.1 sec

	public void BeTransparent()
	{
		// reset the transparency;
		m_Transparency = m_TargetTransparancy;
		if (m_OldShader == null)
		{
			Renderer rend = transform.GetComponent<Renderer> ();
			// Save the current shader
			m_OldShader = rend.material.shader;
			m_OldColor  = rend.material.color;
			rend.material.color = new Color(255, 0, 0, 0.8f);
			rend.material.shader = Shader.Find ("Mobile/Particles/Additive");
		}
	}
	void Update()
	{
		Renderer rend = transform.GetComponent<Renderer> ();
		if (m_Transparency < 1.0f)
		{
			Color C = rend.material.color;
			C.a = m_Transparency;
			rend.material.color = C;
		}
		else
		{
			// Reset the shader
			rend.material.shader = m_OldShader;
			rend.material.color = m_OldColor;
			// And remove this script
			Destroy(this);
		}
		m_Transparency += ((1.0f-m_TargetTransparancy)*Time.deltaTime) / m_FallOff;
	}
}
