using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class PatientZone : MonoBehaviour {
	
	PatientData pd;
	PatientInvestigations pi;
	PatientTreatment pt;
	UIManager uim;
	Inventory inv;
	GameObject player;
	bool playerInZone;
	
	
	void Awake()
	{
		player = GameObject.Find("Player1");
		pd = this.gameObject.GetComponent<PatientData> ();
		pi = this.gameObject.GetComponent<PatientInvestigations> ();
		pt = this.gameObject.GetComponent<PatientTreatment> ();
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();

	}
	
		
	
	void OpenMedicalRecord()
	{
		if(!Input.GetButtonDown ("LMB"))
		{
			return;
		}
		else if(!inv.draggingItemBool && inv.HitSpecificObject("Patientsprite"))
		{
			uim.medicalPages[0].SetActive(true);
	
			uim.stickyLabels[0].GetComponent<Text>().text = pd.patientLabel;
			uim.stickyLabels[1].GetComponent<Text>().text = pd.patientLabel;
			uim.stickyLabels[2].GetComponent<Text>().text = pd.patientLabel;
			uim.stickyLabels[3].GetComponent<Text>().text = pd.patientLabel;
			uim.currentTreatmentLabel.GetComponent<Text>().text = pd.currentTreatment;
		}
	
	}
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject == player)
		{
			playerInZone = true;
//			Debug.Log(pd.patientName);
//			patientData = this.gameObject.GetComponentInParent<PatientData>();
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
		if(playerInZone && !pd.patientDead)
		{
			pi.ObtainBloodSample();
			pt.AdministerTreatment();
			OpenMedicalRecord();
		}
		
//		if(Input.GetKeyDown(KeyCode.Delete))
//		{
//			if(inv.Items[0].itemObj != null)
//			{
//				Destroy(inv.Items[0].itemObj);
//			}
//			inv.Items[0] = new Item();
//		}
		
		
	}
	
	
}

