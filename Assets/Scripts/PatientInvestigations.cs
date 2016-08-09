using UnityEngine;
using System.Collections;

public class PatientInvestigations : MonoBehaviour {

	PatientData pd;
	Inventory inv;
	UIManager uim;
	public Transform bloodTransform;
	public GameObject bloodsamplePrefab;
	
		
	void Awake ()
	{
		pd = this.gameObject.GetComponent<PatientData> ();
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		bloodTransform = GameObject.FindGameObjectWithTag ("Blood").transform;
		uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();
	}
	
	public void ObtainBloodSample() // code 6 = empty blood syringe
	{
		if (!inv.checkHasItem(6)) // if empty syringe not in any slot of hotbar, return
		{
			return;
		}
		else if (uim.HotkeyPress() >= 0)
		{


				if(inv.Items[uim.HotkeyPress()].itemID == 6)
				{
	
					inv.Items[uim.HotkeyPress()] = new Item (); // sets the empty syringe slot to empty
					
					GameObject bloodGameobject = (GameObject)Instantiate(bloodsamplePrefab);
					bloodGameobject.transform.SetParent(bloodTransform, false); //this.gameObject.transform
					bloodGameobject.name = "BloodSample" + pd.visitorNumber;
					Item item = new Item(-1, "Blood", "Utility", "Some thick red stuff.", pd.visitorNumber, bloodGameobject);
					
					inv.Items[uim.HotkeyPress()] = item; // sets the empty slot into a blood filled syringe
					
					
					
					BloodSample bs = bloodGameobject.GetComponent<BloodSample>();
					bs.visitorID = pd.visitorNumber;
					bs.visitorName = pd.patientName;
					bs.hospitalID = pd.patientHospitalNumber;
					bs.bloodresult = pd.patientBlood;
					bs.itemdata = item;
				}
//			}
			


			
//			inv.Items[0] = new Item (); //set hotbar slot 0 to empty
//			
//			GameObject bloodGameobject = (GameObject)Instantiate(bloodsamplePrefab);
//			bloodGameobject.transform.SetParent(bloodTransform, false); //this.gameObject.transform
//			bloodGameobject.name = "BloodSample" + pd.visitorNumber;
//			Item item = new Item(-1, "Blood", "Utility", "Some thick red stuff.", pd.visitorNumber, bloodGameobject);
//			
//			inv.Items[0] = item;
//			
//
//			
//			BloodSample bs = bloodGameobject.GetComponent<BloodSample>();
//			bs.visitorID = pd.visitorNumber;
//			bs.visitorName = pd.patientName;
//			bs.hospitalID = pd.patientHospitalNumber;
//			bs.bloodresult = pd.patientBlood;
//			bs.itemdata = item;
		}
	}


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
}
