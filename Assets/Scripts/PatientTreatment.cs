using UnityEngine;
using System.Collections;

public class PatientTreatment : MonoBehaviour {
	
	PlayerData playerData;
	PatientData pd;
	Inventory inv;
	UIManager uim;
	
	void Awake ()
	{
		playerData = GameObject.Find("Player1").GetComponent<PlayerData>();
		pd = this.gameObject.GetComponent<PatientData> ();
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();
	}
	
	public void AdministerTreatment()
	{
		
		if (uim.HotkeyPress() > inv.Items.Count)
		{
			return;
		}
		else if(inv.Items[uim.HotkeyPress()].itemType == "Drug")
		{
			pd.currentTreatment = inv.Items[uim.HotkeyPress()].itemDesc;
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
				Debug.Log ("Wrong drug - you monster");
			}
		}
		else if (inv.Items[uim.HotkeyPress()].itemName == "Pillow")
		{
			pd.health--;
		}
		
		CheckHealth();
			
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

