using UnityEngine;
using System.Collections;

public class RaycasterForTyper : MonoBehaviour {
	[SerializeField] private bool ShowDebugRay;
	public LayerMask m_InclusionLayers;
	private ButtonUnit CurrentButton;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ShowDebugRay) {
			Debug.DrawRay (gameObject.transform.position, gameObject.transform.forward * 5f, Color.green, 1f);
		}
			
		RaycastHit hit;
		var NewRay = new Ray (gameObject.transform.position, gameObject.transform.forward);
		if (Physics.Raycast (NewRay, out hit, Mathf.Infinity,m_InclusionLayers)) {
			if (hit.collider.GetComponent<ButtonUnit> ()) {
				CurrentButton = hit.collider.GetComponent<ButtonUnit> ();
//				Debug.Log (CurrentButton.theKeyName);
				CurrentButton.Hover();
			}
				
		} 
	}
}
