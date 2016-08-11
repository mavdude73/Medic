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
		if(!inv.mouseOverHotbar)
		{
		
			if(uim.HotkeyPress() >= 0 && inv.Items[uim.HotkeyPress()].itemType == "Drug")
			{
				pd.currentTreatment = inv.Items[uim.HotkeyPress()].itemDesc;
				inv.Items[uim.HotkeyPress()] = new Item ();
				DrugEffect();
			}
			else if(Input.GetButtonDown("LMB") && inv.draggedItem.itemType == "Drug")
			{
				pd.currentTreatment = inv.draggedItem.itemDesc;
				inv.closeDraggedItem();
				DrugEffect();
			}
			
			if (uim.HotkeyPress() >= 0 && inv.Items[uim.HotkeyPress()].itemName == "Pillow")
			{
				pd.health--;
				CheckHealth();
			}
			else if (Input.GetButtonDown("LMB") && inv.draggedItem.itemName == "Pillow")
			{
				pd.health--;
				CheckHealth();
			}
			
		}
			
	}
	
	void DrugEffect()
	{
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
		
		CheckHealth();
	}
	
	void CheckHealth()
	{
		if(pd.health == 1)
		{
			Debug.Log("1 health remaining");
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

