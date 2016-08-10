using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pharmacy : MonoBehaviour {

	
	GameObject player;
	bool playerInZone;
	Inventory inv;
	public UIManager uim;


	void Awake ()
	{
		player = GameObject.Find("Player1");
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
	}
	
	void OpenPharmacy()
	{
		if (!uim.pharmacyMenu.activeSelf)
		{
			uim.pharmacyMenu.SetActive(true);
		} 
	}

	public void Red1()
	{
		inv.addItem(7);
		uim.ClosePharmacy();
	}
	
	public void Red2()
	{
		inv.addItem(8);
		uim.ClosePharmacy();
	}
	
	public void Red3()
	{
		inv.addItem(9);
		uim.ClosePharmacy();
	}
	
	public void Blue1()
	{
		inv.addItem(10);
		uim.ClosePharmacy();
	}
	
	public void Blue2()
	{
		inv.addItem(11);
		uim.ClosePharmacy();
	}
	
	public void Blue3()
	{
		inv.addItem(12);
		uim.ClosePharmacy();
	}
	
	public void Purple1()
	{
		inv.addItem(13);
		uim.ClosePharmacy();
	}
	
	public void Purple2()
	{
		inv.addItem(14);
		uim.ClosePharmacy();
	}
	
	public void Purple3()
	{
		inv.addItem(15);
		uim.ClosePharmacy();
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
		if(!playerInZone)
		{
			return;
		}
		else if(inv.HitSpecificObject("PharmacyCollider") && Input.GetButtonDown ("LMB")) //&& !medicalRecord.gameObject.activeSelf
		{
			OpenPharmacy();			
		}
			
	}

	
}
