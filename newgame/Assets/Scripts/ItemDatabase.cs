using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();
	public GameObject empty;


	// Use this for initialization
	void Start () {
		items.Add (new Item (0, "Potion", "Utility", "Vitalise your energy", -1, empty));
		items.Add (new Item (1, "PotionPlus", "Utility", "Max your energy", -1, empty));
		items.Add (new Item (2, "Pillow", "Utility", "Soft head cushion. Do not inhale.", -1, empty));
		items.Add (new Item (3, "Scalpel", "Utility", "Precise cutting instrument. Sharp.", -1, empty));
		items.Add (new Item (4, "Defibrillator", "Utility", "Delivers high voltage. Shocking.", -1, empty));
		items.Add (new Item (5, "Scissors", "Utility", "Pair of blades. Do not run with.", -1, empty));
		items.Add (new Item (6, "Syringe", "Utility", "Fluid not included.", -1, empty));
		
		items.Add (new Item (7, "RedPotion", "Drug", "Amoxcitol", -1, empty));
		items.Add (new Item (8, "RedPotion", "Drug", "Placebolin", -1, empty));
		items.Add (new Item (9, "RedPotion", "Drug", "Derpcillium", -1, empty));
		items.Add (new Item (10, "BluePotion", "Drug", "Hypnotol", -1, empty));
		items.Add (new Item (11, "BluePotion", "Drug", "Adamaxine", -1, empty));
		items.Add (new Item (12, "BluePotion", "Drug", "Hydromax", -1, empty));
		items.Add (new Item (13, "PurplePotion", "Drug", "Tixabrufen", -1, empty));
		items.Add (new Item (14, "PurplePotion", "Drug", "Synapticol", -1, empty));
		items.Add (new Item (15, "PurplePotion", "Drug", "Pseudodrine", -1, empty));
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
