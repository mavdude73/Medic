using UnityEngine;
using System.Collections;

public class SluiceItems: MonoBehaviour
{
	Inventory inv;
	PlayerController pc;
	
	void Awake ()
	{
		pc = GameObject.Find ("Player1").GetComponent<PlayerController>();
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
	}

	void ItemPickup()
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
	
	
	


	void Update()
	{
		ItemPickup ();
				
	}

}