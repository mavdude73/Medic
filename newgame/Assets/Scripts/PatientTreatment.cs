using UnityEngine;
using System.Collections;

public class PatientTreatment : MonoBehaviour {
	
	PlayerData playerData;
	PatientData pd;
	Inventory inv;
	
	void Awake ()
	{
		playerData = GameObject.Find("Player1").GetComponent<PlayerData>();
		pd = this.gameObject.GetComponent<PatientData> ();
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
	}
	
	public void HasTreatment()
	{
		
		if(inv.Items[0].itemType == "Drug")
		{
			pd.currentTreatment = inv.Items[0].itemDesc;
			inv.deleteItem0();
			
			
			if (pd.currentTreatment == "Expired")
			{
				Debug.Log ("Drug has expired - no effect");
			}
			
			else if (pd.currentTreatment == pd.correctTreatment)
			{
				Debug.Log ("You healed me");
			}
			
			else if (pd.currentTreatment != pd.correctTreatment)
			{
				pd.health--;
				CheckHealth();
				Debug.Log ("Wrong drug - you monster");
			}
			
		}
		else
		{
			
			if (inv.Items[0].itemName == "Pillow")
			{
				pd.health--;
				CheckHealth();
			}
			
			
		}
	
		
		
	}
	
	void CheckHealth()
	{
		if(pd.health == 1)
		{
			Debug.Log("Health minus 1");
		}
		else if(pd.health == 0)
		{
			pd.patientDead = true;
			playerData.harmed++;
			Debug.Log ("Patient dead");
		}
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

