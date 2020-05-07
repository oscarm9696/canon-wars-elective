using UnityEngine;
using System.Collections;

public class ShaderHandler : MonoBehaviour 
{
	//public int materialIndex = 0;
	public Vector3 shaderAnimationRate = new Vector3( 0.01f, 0.01f, 0.01f );
	public string textureName = "_MainTex";
	
	Vector3 offset = Vector3.zero;
	
	void LateUpdate() 
	{
		offset += ( shaderAnimationRate * Time.deltaTime );
		if( GetComponent<Renderer>().enabled )
		{
			GetComponent<Renderer>().sharedMaterial.SetTextureOffset( textureName, offset );
		}
	}
}