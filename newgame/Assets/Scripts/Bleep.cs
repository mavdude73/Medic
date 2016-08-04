using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bleep : MonoBehaviour {

	
	GameObject player;
	bool playerInZone;
	public UIManager uim;
	public PlayerData pd;
	bool bleepAnswered;


	void Start ()
	{
		player = GameObject.Find("Player1");
		StartCoroutine(MatronBleep(3f));
	}
	

	public IEnumerator MatronBleep (float delay)
	{
		yield return new WaitForSeconds(delay);
//		bool hasBleep = true;
		if(pd.hasBleep)
		{
			uim.bleepAlert.SetActive(true);
			bleepAnswered = false;
			Debug.Log("ANSWER YOUR BLEEP!!");
			yield return new WaitForSeconds(10);
			bleepOutcome();
		}
		else 
		{
			StartCoroutine(MatronBleep(3f));
		}
//		yield return new WaitForSeconds(delay);
//		StartCoroutine(MatronBleep(20f));
	}
	
	void bleepOutcome()
	{
		if(!bleepAnswered)
		{
			Debug.Log("Bleep unanswered");
			bleepResponded();
			StartCoroutine(MatronBleep(5f));
		}
		else if(bleepAnswered)
		{
			Debug.Log("Good job slave");
			StartCoroutine(MatronBleep(15f));
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
