using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler {

	public int slotNumber;
	public Item item;
	Image itemImage;
	Inventory inv;
	PlayerController pc;
	bool mouseOverHotbar = false;


	void Start ()
	{
		pc = GameObject.Find ("Player1").GetComponent<PlayerController>();
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		itemImage = gameObject.transform.GetChild (0).GetComponent<Image> ();
	}
	
	void Update ()
	{
		SlotUpdater ();
		
		if(!mouseOverHotbar)
		{
			inv.DropItem();
		}
		
	}

	void SlotUpdater()
	{
		if (inv.Items[slotNumber].itemName != null)
		{
			item = inv.Items[slotNumber];
			itemImage.enabled = true;
			itemImage.sprite = inv.Items[slotNumber].itemIcon;
		}
		else
		{
			itemImage.enabled = false;
		}
	}
	
	
	
	public void OnPointerDown(PointerEventData data)
	{
		if(data.button == PointerEventData.InputButton.Left)
		{
			if (inv.Items[slotNumber].itemName == null && pc.itemOnCursor)
			{
				inv.Items[slotNumber] = inv.draggedItem;
				inv.CloseDraggedItem();
			}
			else if(inv.Items[slotNumber].itemName != null && pc.itemOnCursor)
			{
				inv.Items[inv.draggedItemSlotOrigin] = inv.Items[slotNumber];
				inv.Items[slotNumber] = inv.draggedItem;
				inv.CloseDraggedItem();
			}
			else if(inv.Items[slotNumber].itemName != null && !pc.itemOnCursor)
			{
				inv.ShowDraggedItem(inv.Items[slotNumber], slotNumber);
				inv.Items[slotNumber] = new Item();
			}
		}
		
		if(data.button == PointerEventData.InputButton.Right)
		{
			if (!pc.itemOnCursor && inv.Items[slotNumber].itemName != null)
			{
				inv.ShowDraggedItem(inv.Items[slotNumber], slotNumber);
				inv.Items[slotNumber] = new Item();
				inv.DropItem();
			}
		}
	}
	
	public void OnPointerEnter(PointerEventData data)
	{
		mouseOverHotbar = true;
		inv.IsMouseOverHotbar(mouseOverHotbar);
	}
	
	public void OnPointerExit(PointerEventData data)
	{
		mouseOverHotbar = false;
		inv.IsMouseOverHotbar(mouseOverHotbar);
	}
	
	
	public void OnDrag(PointerEventData data)
	{
//		if (inv.Items[slotNumber].itemName != null)
//		{
//			inv.showDraggedItem(inv.Items[slotNumber], slotNumber);
//			inv.Items[slotNumber] = new Item();
//		}
	}



}
