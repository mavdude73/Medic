using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Laboratory : MonoBehaviour {
	
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
		StartCoroutine(AnalyseBlood(0f));
	}


	public void CheckForSamples (bool inLaboratory, bool isMouse, int hotkey)
	{
		if(inLaboratory)
		{
			if(!isMouse && inv.Items[hotkey].itemName == "Blood")
			{
				bloodQueue.Enqueue(inv.Items[hotkey].itemObj);
				inv.Items[hotkey] = new Item();
			}
			else if(isMouse && inv.draggedItem.itemName == "Blood")
			{
				bloodQueue.Enqueue(inv.draggedItem.itemObj);
				inv.CloseDraggedItem();
			}
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

	}
	
	
}
