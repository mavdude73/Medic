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
		if(!inv.mouseOverHotbar && !pd.treatmentInProgress)
		{
		
			if(uim.HotkeyPress() >= 0 && inv.Items[uim.HotkeyPress()].itemType == "Drug")
			{
				inv.Items[uim.HotkeyPress()] = new Item ();
				StartCoroutine(DrugEffect(inv.Items[uim.HotkeyPress()].itemDesc));
			}
			else if(Input.GetButtonDown("LMB") && inv.draggedItem.itemType == "Drug")
			{
				inv.closeDraggedItem();
				StartCoroutine(DrugEffect(inv.draggedItem.itemDesc));
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
	
	IEnumerator DrugEffect(string treatment)
	{
		pd.treatmentInProgress = true;
		yield return new WaitForSeconds(5);
		if(!pd.patientDead)
		{
			pd.treatmentInProgress = false;
			for(int i = 0; i < pd.treatment.Count; i++)
			{
				if(treatment == pd.treatment[i])
				{
					Debug.Log("Treatment successful");
					break;
				}
				if(treatment == "Expired")
				{
					Debug.Log ("Expired medicine");
					break;
				}
			}
			pd.health--;
			Debug.Log("WRONG TREATMENT");
			CheckHealth();
		}
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

