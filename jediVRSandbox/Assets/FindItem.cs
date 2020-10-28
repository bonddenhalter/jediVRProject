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
		//https://answers.unity.com/questions/1329858/unity-gameobject-to-sprite-ui-image.html
		if(itemIcon == null){
			//itemIcon = 
		}
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
