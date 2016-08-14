using UnityEngine;
using System.Collections;

public class SluiceItems : MonoBehaviour
{
	Inventory inv;
	
	public SluiceItems()
	{
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
	}

	public void ItemPickup(string name)
	{
		if (name == "Pillow") 
		{
			inv.AddItem(2);
		}
					
		if (name == "Scalpel")
		{
			inv.AddItem(3);
		}
		
		if (name == "Defibrillator") 
		{
			inv.AddItem(4);
		}
		
		if (name == "Scissors")
		{
			inv.AddItem(5);
		}
		
		if (name == "Syringe")
		{
			inv.AddItem(6);
		}
	}
}