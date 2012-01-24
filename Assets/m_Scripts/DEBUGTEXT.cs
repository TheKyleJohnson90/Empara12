using UnityEngine;
using System.Collections;

public class DEBUGTEXT : MonoBehaviour {
	private bool show = true;

void Update () {
	if (Input.GetKeyDown("0"))
		show = !show;
	if (show)
	{
		if (Time.timeScale != 0.0)
		{
			double fps =1.0 / Time.deltaTime;
			guiText.text = fps.ToString();
		}
		else
		{
			guiText.text = "0";
		}
	}
	else
	{
		guiText.text = "";	
	}
}

//@script RequireComponent(GUIText)
}