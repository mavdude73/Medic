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
		if(!inv.mouseOverHotbar && !pd.treatmentInProgress && !pd.patientDead)
		{
		
			if(uim.HotkeyPress() >= 0 && inv.Items[uim.HotkeyPress()].itemType == "Drug")
			{
				StartCoroutine(DrugEffect(inv.Items[uim.HotkeyPress()].itemDesc));
				inv.Items[uim.HotkeyPress()] = new Item ();
			}
			else if(Input.GetButtonDown("LMB") && inv.draggedItem.itemType == "Drug")
			{
				StartCoroutine(DrugEffect(inv.draggedItem.itemDesc));
				inv.closeDraggedItem();
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
	
	public IEnumerator DrugEffect(string treatment)
	{
		if(!pd.patientCured)
		{
			pd.treatmentInProgress = true;
			int timer = 5;
			yield return new WaitForSeconds(timer);
			if(!pd.patientDead)
			{
				pd.treatmentInProgress = false;
				for(int i = 0; i < pd.treatments.Count; i++)
				{
					if(treatment == pd.treatments[i])
					{
						Debug.Log("Treatment successful");
						
						pd.TreatmentProgress(treatment, "successful");
						pd.treatments.RemoveAt(i);
						return true;
					}
				}
				if(treatment == "Expired")
				{
					Debug.Log ("Expired medicine");
					pd.TreatmentProgress(treatment, "expired");

				}
				else
				{
					pd.TreatmentProgress(treatment, "failed");
					pd.health--;
					CheckHealth();
				}
			}
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

