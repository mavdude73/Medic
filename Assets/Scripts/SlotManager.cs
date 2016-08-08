using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler {

	public int slotNumber;
	public Item item;
	Image itemImage;
	Inventory inv;


	void Start ()
	{
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		itemImage = gameObject.transform.GetChild (0).GetComponent<Image> ();
	}
	
	void Update ()
	{
		SlotUpdater ();
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
		if (inv.Items[slotNumber].itemName == null && inv.draggingItemBool)
		{
			inv.Items[slotNumber] = inv.draggedItem;
			inv.closeDraggedItem();
		}
		else if(inv.Items[slotNumber].itemName != null && inv.draggingItemBool)
		{
			inv.Items[inv.draggedItemSlotOrigin] = inv.Items[slotNumber];
			inv.Items[slotNumber] = inv.draggedItem;
			inv.closeDraggedItem();
		}
	}
	
	public void OnPointerEnter(PointerEventData data)
	{
//		Debug.Log ("Entered");
	}
	
	public void OnPointerExit(PointerEventData data)
	{
		//		Debug.Log ("Entered");
	}
	
	public void OnDrag(PointerEventData data)
	{
		if (inv.Items[slotNumber].itemName != null)
		{
			inv.showDraggedItem(inv.Items[slotNumber], slotNumber);
			inv.Items[slotNumber] = new Item();
		}
	}



}
