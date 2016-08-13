using UnityEngine;
using System.Collections;

public class SluiceItems: MonoBehaviour
{
	
	
	private GameObject player1;
//	PlayerData pd;
	Inventory inv;
	PlayerController pc;
	bool playerInZone;
	
	void Awake ()
	{
		pc = GameObject.Find ("Player1").GetComponent<PlayerController>();
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
		if (playerInZone)
		{
			if(!pc.itemOnCursor)
			{
				if (pc.rayHitobject == "Pillow") 
				{
					inv.AddItem(2);
				}
							
				if (pc.rayHitobject == "Scalpel")
				{
					inv.AddItem(3);
				}
				
				if (pc.rayHitobject == "Defibrillator") 
				{
					inv.AddItem(4);
				}
				
				if (pc.rayHitobject == "Scissors")
				{
					inv.AddItem(5);
				}
				
				if (pc.rayHitobject == "Syringe")
				{
					inv.AddItem(6);
				}
			}	
		}
	}
	
	
	


	void Update()
	{
		ItemPickup ();
				
	}

}