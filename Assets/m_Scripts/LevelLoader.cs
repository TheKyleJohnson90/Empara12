using UnityEngine;
using System.Collections;
//using a staticfunction
// LevelLoader.LoadingLevel( level, Texture, time)

public class LevelLoader : MonoBehaviour {
	static void LoadingLevel (GameObject level,Texture2D fadeTexture,float fadeLength)
	{
		if (fadeTexture == null){
			//LoadingLevel(level, Color.white, fadeLength);
		}
		
		GameObject fade = new GameObject ("Fade");
		fade.AddComponent("LevelLoader");
		fade.AddComponent<GUITexture>();
		fade.transform.position = new Vector3(0.5f, 0.5f, 1000f);
		fade.guiTexture.texture = fadeTexture;
		fade.GetComponent<LevelLoader>().DoFade(level, fadeLength, false);
		
	}
	void DoFade (GameObject level,float fadeLength,bool destroyTexture )
	{
		// Dont destroy the fade game object during level load
		DontDestroyOnLoad(this.gameObject);
		//guiTexture.color.a = 0f;
		float time = 0.0f;
		while (time < fadeLength)
		{
			time += Time.deltaTime;
			//guiTexture.color.a = Mathf.InverseLerp(0.0, fadeLength, time);//replace this funciton with own
			return;//maybe break
		}
		//guiTexture.color.a = 1;
		return;// maybe break

		// Complete the fade out 
		//Application.LoadLevel(level);
		//reset player position?
		//????????????//
		
		// Fade texture out
		time = 0.0f;
		while (time < fadeLength)
		{
			time += Time.deltaTime;
			//guiTexture.color.a = Mathf.InverseLerp(fadeLength, 0.0f, time);//replace this funcitonwith own
			return;//maybe break
		}
		//guiTexture.color.a = 0;
		return;//maybe break

		Destroy (gameObject);

		if (destroyTexture)
			Destroy (guiTexture.texture);
			
	}
}