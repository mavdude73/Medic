using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DroppedItem : MonoBehaviour
{

	public Item item;
	Inventory inv;

	
	void Awake()
	{
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
	}


	public void PickUpItem(string name, bool isMouse, int hotkey)
	{

		if (name == this.gameObject.name)
		{
			if(!item.itemName.Contains("spill"))
			{
				if(inv.AddItemIfEmpty(item))
				{
					Destroy(this.gameObject);
				}
			}
			else if(item.itemName.Contains("spill"))
			{
				if(!isMouse && inv.Items[hotkey].itemName == "Mop")
				{
					Destroy(this.gameObject);
				}
				else if(isMouse && inv.draggedItem.itemName == "Mop")
				{
					Destroy(this.gameObject);
				}
			}
		}
	}


}
