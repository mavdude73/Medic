using UnityEngine;
using System.Collections;

[System.Serializable]

public class Item {

	public int itemID;
	public string itemName;
	public string itemType;
	public string itemDesc;
	public int floorID;
//	public ItemType itemType;
	public GameObject itemObj;

	public Sprite itemIcon;
	public GameObject itemModel;

//	public enum ItemType
//	{
//		blood,
//		urine,
//		swab,
//		equipment
//	}

	public Item(int id, string name, string type, string desc, int floorid, GameObject obj)
	{
		itemID = id;
		itemName = name;
		itemType = type;
		itemDesc = desc;
		floorID = floorid;
		itemObj = obj;
		itemIcon = Resources.Load<Sprite> ("" + name);
		itemModel = Resources.Load<GameObject>("DroppedItem");

	}
	public Item(){}



}