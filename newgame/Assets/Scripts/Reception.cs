using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Reception : MonoBehaviour {

	
	GameObject player;
	bool playerInZone;
	public UIManager uim;
	
	bool bleepAnswered = true;


	void Start ()
	{
		player = GameObject.Find("Player1");
		StartCoroutine(MatronBleep(2f));
	}
	

	public IEnumerator MatronBleep (float delay)
	{
		yield return new WaitForSeconds(delay);
		bool hasBleep = true;
		if(hasBleep)
		{
			uim.bleepAlert.SetActive(true);
			bleepAnswered = false;
			Debug.Log("ANSWER YOUR BLEEP!!");
			yield return new WaitForSeconds(10);
			bleepOutcome();
		}
//		yield return new WaitForSeconds(delay);
//		StartCoroutine(MatronBleep(20f));
	}
	
	void bleepOutcome()
	{
		if(!bleepAnswered)
		{
			Debug.Log("YOU SUCK. YOU DIDNT ANSWER");
			bleepResponded();
			StartCoroutine(MatronBleep(4f));
		}
		else if(bleepAnswered)
		{
			Debug.Log("GOOD JOB SLAVE");
			StartCoroutine(MatronBleep(8f));
		}
	}
	
	void bleepResponded()
	{
		bleepAnswered = true;
		uim.bleepAlert.SetActive(false);
	}

	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject == player)
		{
			playerInZone = true;
		}
	}
	
	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject == player)
		{
			playerInZone = false;
		}
	}
	
	bool PlayerInZone() {return playerInZone;}
	
	void Update()
	{
		if(playerInZone && !bleepAnswered)
		{
			if(Input.GetMouseButtonDown (0)) //&& !medicalRecord.gameObject.activeSelf
			{
				bleepResponded();			
			}
			
		}
	}
	
}
