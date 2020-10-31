using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script based on various tutorials and code 
// from Unity Documentation and Unity Forums
public class FindItem : MonoBehaviour
{
	public Sprite itemIcon; //input image
	private bool found = false;
	private Inventory inventory;
		
	void Start(){
		
	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "OVRPlayerController" && !found){
			found = true;
			
			//Put object into player's inventory
			inventory = col.gameObject.GetComponent<Inventory>();
			GameObject item = this.gameObject;
			inventory.AddItem(item);
		}
	}		
}
