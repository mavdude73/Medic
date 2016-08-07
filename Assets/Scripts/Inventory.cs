using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {


	public List<GameObject> Slots = new List<GameObject> ();
	public List<Item> Items = new List<Item> ();
	public GameObject slots;
	public GameObject draggedItemGameobject;
	public bool draggingItem = false;
	public Item draggedItem;
	public int draggedItemSlotOrigin;
	ItemDatabase db;
	UIManager uim;
	int hotkey;	
//	UIManager uim;
//	int x = -160;
	public int slotCount = 1;
//	int x = 0;
//	int y = 0;

	void CreateHotbar()
	{
		int x = (-slotCount + 1) * 40;
		int slotAmount = 0;
//		for (int i = 0; i < 1; i++)
//		{}
		for(int k = 0; k < slotCount; k++)
		{
			GameObject slot = (GameObject)Instantiate(slots);
			Slots.Add(slot);
			Items.Add(new Item());
			slot.transform.SetParent(this.gameObject.transform, false);
			slot.name = "Slot" + k;
			slot.GetComponent<RectTransform>().localPosition = new Vector3(x,0,0);
			slot.GetComponent<SlotManager>().slotNumber = slotAmount;
			x = x + 80;
			slotAmount++;
		}
	}



	// Use this for initialization
	void Awake ()
		{
			db = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase>();
			uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();
			
			CreateHotbar ();
			addItem (0);
		}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Delete))
		{
			deleteItem0();
		}
		
		if(draggingItem)
		{
			Vector3 posi = (Input.mousePosition - GameObject.FindGameObjectWithTag("UIManager").GetComponent<RectTransform>().localPosition);
			draggedItemGameobject.GetComponent<RectTransform>().localPosition = new Vector3 (posi.x + 15, posi.y - 15, posi.z);
		}
	}

	
	public void showDraggedItem(Item item, int slotnumber)
	{
		draggedItemSlotOrigin = slotnumber;
		draggedItemGameobject.SetActive(true);
		draggedItem = item;
		draggingItem = true;
		draggedItemGameobject.GetComponent<Image>().sprite = item.itemIcon;
	}
	
	public void closeDraggedItem()
	{
		draggingItem = false;
		draggedItemGameobject.SetActive(false);
	}
	

	public bool checkHasItem (int itemid)
	{
		for (int i = 0; i < Items.Count; i++)
		{
			if (Items [i].itemID == itemid)
			{
				return true;
			}

		}
		return false;
		
	}


	public void addItem(int itemid)
	{
		for (int i = 0; i < db.items.Count; i++)
		{
			if(db.items[i].itemID == itemid)
			{
				Item item = db.items[i];
				addItemIfEmpty(item);
				break;
			}
		}
	}


	public void addItemIfEmpty(Item item)
	{
		for (int k = 0; k < Items.Count; k++)
		{
			if(Items[k].itemName == null)
			{
				Items[k] = item;
				break;
			}
		}
	}
	
	public void deleteItem0()
	{
		if(Items[uim.HotkeyPress()].itemObj != null)
		{
			Destroy(Items[uim.HotkeyPress()].itemObj);
		}
		Items[uim.HotkeyPress()] = new Item();
	}
	

}
