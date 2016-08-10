using UnityEngine;
using System.Collections;

public class SluiceItems: MonoBehaviour
{
	
	
	private GameObject player1;
//	PlayerData pd;
	Inventory inv;
	bool playerInZone;
	
	void Awake ()
	{
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		player1 = GameObject.Find ("Player1");
		
	//	player3 = GameObject.Find ("FPSController3");
	}
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject == player1)
		{
			playerInZone = true;
		}
	}
	
	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject == player1)
		{
			playerInZone = false;
		}
	}

	void ItemPickup()
	{
		if (!playerInZone)
		{
			return;
		}
		
		if (Input.GetButtonDown ("LMB"))
		{
			if(!inv.draggingItemBool && inv.HitObjectCheck())
			{
				if (inv.HitObjectCheck().collider.gameObject.name == "Pillow") {
					inv.addItem(2);
				}
				
				if (inv.HitObjectCheck().collider.gameObject.name == "Scissors") {
					inv.addItem(5);
				}
				
				if (inv.HitObjectCheck().collider.gameObject.name == "Scalpel") {
					inv.addItem(3);
				}
				
				if (inv.HitObjectCheck().collider.gameObject.name == "Defibrillator") {
					inv.addItem(4);
				}
				
				if (inv.HitObjectCheck().collider.gameObject.name == "Syringe") {
					inv.addItem(6);
				}
			}
		}	
	}
	
	
	


	void Update()
	{
		ItemPickup ();
				
	}

}