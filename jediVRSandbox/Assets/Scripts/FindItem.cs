using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	DESCRIPTION: This script is for collected items to be placed in a player's inventory.
	COMPATIBLE SCRIPTS: Inventory.cs
	DIRECTIONS:	Place this script on any object that needs to be collected.
		If desired, provide an itemIcon so that it is displayed in the UI
		inventory.
	PROJECTED CHANGES: It currently doesn't check which object it's colliding
		with. This will change so that the player (or a player's children)
		will be the only objects that trigger the collection of an item.
*/

//Script based on various tutorials and code 
// from Unity Documentation and Unity Forums
public class FindItem : MonoBehaviour
{
	public Sprite itemIcon; //input image
	private bool found = false;
	private Inventory inventory;
	private GameObject player;
		
	void Start(){
		player = GameObject.Find("OVRPlayerController");
		inventory = player.GetComponent<Inventory>();
	}
	
	void OnTriggerEnter(Collider col){
		//if(col.gameObject.name == player.name && !found){
			found = true;
			Debug.Log("hit");
			//Put object into player's inventory
			GameObject item = this.gameObject;
			inventory.AddItem(item);
		//}
	}	
}
