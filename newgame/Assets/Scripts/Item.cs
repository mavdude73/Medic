using UnityEngine;
using System.Collections;

public class Item {

	public int itemID;
	public string itemName;
	public string itemType;
	public string itemDesc;
	public int visitorID;
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

	public Item(int id, string name, string type, string desc, int patientid, GameObject obj)
	{
		itemID = id;
		itemName = name;
		itemType = type;
		itemDesc = desc;
		visitorID = patientid;
		itemObj = obj;
		itemIcon = Resources.Load<Sprite> ("" + name);

	}
	public Item(){}



}