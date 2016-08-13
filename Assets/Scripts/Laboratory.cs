using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Laboratory : MonoBehaviour {
	
	GameObject player;
	bool playerInZone;
	public int bloodAnalysisTimer;
	Inventory inv;
	UIManager uim;
	public string allBloodResults;
	
	public List<string> bloodList = new List<string>();
	
	public Queue<GameObject> bloodQueue = new Queue<GameObject>();

	void Awake ()
	{
		uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		player = GameObject.Find("Player1");
		StartCoroutine(AnalyseBlood(0f));
	}
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject == player)
		{
			playerInZone = true;
		}
	}
	
	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject == player)
		{
			playerInZone = false;
		}
	}
	
	bool PlayerInZone() {return playerInZone;}
	
	void CheckForSamples ()
	{
		if(uim.HotkeyPress() >= 0 && inv.Items[uim.HotkeyPress()].itemName == "Blood")
		{
		
			bloodQueue.Enqueue(inv.Items[uim.HotkeyPress()].itemObj);
			inv.Items[uim.HotkeyPress()] = new Item();
//			Debug.Log (inv.Items[0].itemObj.GetComponent<BloodSample>().bloodresult);
//			Debug.Log(inv.Items[0].itemObj + " added to queue");
		}
		else if(Input.GetButtonDown("LMB") && inv.draggedItem.itemName == "Blood")
		{
			bloodQueue.Enqueue(inv.draggedItem.itemObj);
//			inv.draggedItem = new Item();
			inv.CloseDraggedItem();
		}
	}
	
	IEnumerator AnalyseBlood (float delay)
	{
		if(bloodQueue.Count > 0)
		{
			yield return new WaitForSeconds(bloodAnalysisTimer);
			BloodSample bs = bloodQueue.Peek().GetComponent<BloodSample>();
			string analysis = bs.visitorName + " (" + bs.hospitalID + "): " + bs.bloodresult + "\n";
			bloodList.Add (analysis);
			Destroy(bloodQueue.Dequeue());
//			bloodQueue.Dequeue();
			
			allBloodResults = "Blood Results: " + "\n";
			if(bloodList.Count > 8)
			{
				bloodList.RemoveAt(0);
//				bloodList.Clear();
			}
			for(int i = bloodList.Count-1; 0 <= i; i--)
			{
				allBloodResults = allBloodResults + bloodList[i];
			}
			uim.computerScreenText.GetComponent<Text>().text = allBloodResults;
		}
		yield return new WaitForSeconds(delay);
		StartCoroutine(AnalyseBlood(10f));
	}
	
	
	

	void Update()
	{
		if(playerInZone)
		{
				CheckForSamples();
		}
	}
	
	
}
