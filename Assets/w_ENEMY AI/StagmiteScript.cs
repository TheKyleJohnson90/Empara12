using UnityEngine;
using System.Collections;

public class StagmiteScript : MonoBehaviour {

	public GameObject enemy;

	public float timer = 0.0f;
	public bool monsterSpawn= false;
	public int count = 0;

	void Start () 
	{
	}

	void Update () 
	{
		if (monsterSpawn == true)
		{
			timer += Time.deltaTime;
			
			if (timer >= 2)
			{
				GameObject myPrefab = (GameObject)Instantiate(enemy);
				myPrefab.transform.position = transform.position;
				timer = 0;
				count++;
			}
			
			if (count == 5)
			{
				monsterSpawn = false;
			}
		}
	}
	
	void OnTriggerEnter(Collider col)
	{   	
		if (col.tag == "Player")   
		{
			monsterSpawn = true;
		}
	}
}
