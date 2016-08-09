using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pharmacy : MonoBehaviour {

	
	GameObject player;
	bool playerInZone;
	Inventory inventory;
	public UIManager uim;


	void Awake ()
	{
		player = GameObject.Find("Player1");
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
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
		inventory.addItem(7);
		uim.ClosePharmacy();
	}
	
	public void Red2()
	{
		inventory.addItem(8);
		uim.ClosePharmacy();
	}
	
	public void Red3()
	{
		inventory.addItem(9);
		uim.ClosePharmacy();
	}
	
	public void Blue1()
	{
		inventory.addItem(10);
		uim.ClosePharmacy();
	}
	
	public void Blue2()
	{
		inventory.addItem(11);
		uim.ClosePharmacy();
	}
	
	public void Blue3()
	{
		inventory.addItem(12);
		uim.ClosePharmacy();
	}
	
	public void Purple1()
	{
		inventory.addItem(13);
		uim.ClosePharmacy();
	}
	
	public void Purple2()
	{
		inventory.addItem(14);
		uim.ClosePharmacy();
	}
	
	public void Purple3()
	{
		inventory.addItem(15);
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
		if(playerInZone)
		{
			if(Input.GetButtonDown ("LMB")) //&& !medicalRecord.gameObject.activeSelf
			{
				OpenPharmacy();			
			}
			
		}
	}
	
}
