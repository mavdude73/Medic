using UnityEngine;
using UnityEngine.UI;
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
		if (uim.HotkeyPress() >= 0 && inv.Items[uim.HotkeyPress()].itemID == 6)
		{
			inv.Items[uim.HotkeyPress()] = new Item (); 								// sets the empty syringe slot to empty	
			inv.Items[uim.HotkeyPress()] = GenerateSampleFunction(); 					// sets the empty slot into a blood filled syringe				
		}
		else if (Input.GetButtonDown("LMB") && inv.draggedItem.itemID == 6)
		{
			inv.draggedItem = new Item (); 												// sets the empty syringe slot to empty	
			inv.draggedItem = GenerateSampleFunction(); 								// sets the empty slot into a blood filled syringe
			inv.draggedItemGameobject.GetComponent<Image>().sprite = inv.draggedItem.itemIcon;
		}
	}

	Item GenerateSampleFunction()
	{
		GameObject bloodGameobject = (GameObject)Instantiate(bloodsamplePrefab);
		bloodGameobject.transform.SetParent(bloodTransform, false); //this.gameObject.transform
		bloodGameobject.name = "BloodSample" + pd.visitorNumber;
		Item item = new Item(-1, "Blood", "Sample", "Some thick red stuff.", pd.visitorNumber, bloodGameobject);
		
		BloodSample bs = bloodGameobject.GetComponent<BloodSample>();
		bs.visitorID = pd.visitorNumber;
		bs.visitorName = pd.patientName;
		bs.hospitalID = pd.patientHospitalNumber;
		bs.bloodresult = pd.patientBlood;
		bs.itemdata = item;
		
		return item;
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
