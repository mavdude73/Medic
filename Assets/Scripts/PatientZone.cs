using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class PatientZone : MonoBehaviour {
	
	PatientData pd;
	UIManager uim;
	GameObject player;
	
	
	void Awake()
	{
		player = GameObject.Find("Player1");
		pd = this.gameObject.GetComponent<PatientData> ();
		uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();

	}
	
		
	
	public void OpenMedicalRecord(GameObject obj, bool itemOnCursor)
	{
		if(gameObject == obj)
		{
			if(!itemOnCursor)
			{
				uim.medicalPages[0].SetActive(true);
		
				uim.stickyLabels[0].GetComponent<Text>().text = pd.patientLabel;
				uim.stickyLabels[1].GetComponent<Text>().text = pd.patientLabel;
				uim.stickyLabels[2].GetComponent<Text>().text = pd.patientLabel;
				uim.stickyLabels[3].GetComponent<Text>().text = pd.patientLabel;
				uim.seniorReviewText.GetComponent<Text>().text = pd.seniorReview;
				uim.treatmentHistoryLabel.GetComponent<Text>().text = pd.treatmentLog;

				if(pd.patientDead)
				{
					uim.deceasedStamp.SetActive(true);
				}
				else if (!pd.patientDead)
				{
					uim.deceasedStamp.SetActive(false);
				}

			}
		}
	}

	void CloseMedicalRecord()
	{
		if (uim.medicalPages[0].activeSelf)
		{
			uim.medicalPages[0].SetActive(false);
			uim.medicalPages[1].SetActive(false);
			uim.medicalPages[2].SetActive(false);
			uim.medicalPages[3].SetActive(false);
		}
	}

	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject == player)
		{
			pd.PlayerInZoneBool (true);
		}
	}
	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject == player)
		{
			pd.PlayerInZoneBool (false);
			CloseMedicalRecord();
		}
	}
	

	

	
	
	void Update()
	{
		
	}
	
	
}

