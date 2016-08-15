using UnityEngine;
using System.Collections;

public class PharmacySponsors 
{
	public int sponsorID;
	public string sponsorName;
	public string sponsorIconName;
	public Color32 sponsorColor32;


	public PharmacySponsors(int newsponsorID, string newsponsorName, string newsponsorIconName, Color32 newsponsorColor32)
	{
		sponsorID = newsponsorID;
		sponsorName = newsponsorName;
		sponsorIconName = newsponsorIconName;
		sponsorColor32 = newsponsorColor32;
	}
	

}
