using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();
	public GameObject empty;


	// Use this for initialization
	void Start () {
		items.Add (new Item (0, "Bloodspill", "Spill", "Spilt mess on the floor", -1, empty));
		items.Add (new Item (1, "Urinespill", "Spill", "Spilt mess on the floor", -1, empty));
		items.Add (new Item (2, "Pillow", "Utility", "Soft head cushion. Do not inhale.", -1, empty));
		items.Add (new Item (3, "Scalpel", "Utility", "Precise cutting instrument. Sharp.", -1, empty));
		items.Add (new Item (4, "Defibrillator", "Utility", "Delivers high voltage. Shocking.", -1, empty));
		items.Add (new Item (5, "Scissors", "Utility", "Pair of blades. Do not run with.", -1, empty));
		items.Add (new Item (6, "Syringe", "Utility", "Fluid not included.", -1, empty));

		// reserve IDs 30 onwards for custom drugs
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
