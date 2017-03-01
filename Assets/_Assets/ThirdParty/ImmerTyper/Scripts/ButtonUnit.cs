using UnityEngine;
using System.Collections;

using UnityEngine.UI;
public class ButtonUnit : MonoBehaviour {
	public enum KeyName{
		A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,
		BackSpace,
		Enter
	}
	public KeyName theKeyName;
	private bool IsHover = false;
	private float HoverTime=0f;
	private float ConfirmTime = 0f;
	private Sprite Sprite_Default;
	private Sprite Sprite_Hover;
	private Sprite Sprite_Confirm;
	private Image _Image;

	void Awake()
	{
		_Image = gameObject.GetComponent<Image> ();
	}
	// Use this for initialization
	void Start () {
//		Debug.Log (theKeyName.ToString().GetHashCode());
		switch (theKeyName) {
		default:
			Sprite_Default=KeyBoardManager._instance.Letter_Sprites_Default[theKeyName.ToString().GetHashCode()-65];
			Sprite_Hover=KeyBoardManager._instance.Letter_Sprites_Hover[theKeyName.ToString().GetHashCode()-65];
			Sprite_Confirm=KeyBoardManager._instance.Letter_Sprites_Confirm[theKeyName.ToString().GetHashCode()-65];
			break;
		case KeyName.BackSpace:
			Sprite_Default=KeyBoardManager._instance.Letter_Sprites_Default[26];
			Sprite_Hover=KeyBoardManager._instance.Letter_Sprites_Hover[26];
			Sprite_Confirm=KeyBoardManager._instance.Letter_Sprites_Confirm[26];
			break;
		case KeyName.Enter:
			Sprite_Default=KeyBoardManager._instance.Letter_Sprites_Default[27];
			Sprite_Hover=KeyBoardManager._instance.Letter_Sprites_Hover[27];
			Sprite_Confirm=KeyBoardManager._instance.Letter_Sprites_Confirm[27];
			break;
		}
		_Image.sprite = Sprite_Default;
	}
	
	// Update is called once per frame
	void Update () {
		if (HoverTime > 0f) {
			HoverTime -= Time.deltaTime;
			if (Input.GetButtonDown ("Fire1")||Input.GetButtonDown ("Fire2")) {
				SubmitValue ();
			}
		} else {
			HoverTime = 0f;
			if(ConfirmTime<=0)
			_Image.sprite = Sprite_Default;
		}

		if (ConfirmTime > 0f) {
			ConfirmTime -= Time.deltaTime;
			_Image.sprite = Sprite_Confirm;
		} else {
			ConfirmTime = 0f;
			if(HoverTime<=0)
			_Image.sprite = Sprite_Default;
		}
	}

	public void Hover()
	{//当该键为焦点时
//		Debug.Log (theKeyName + "    Hover");
		if (HoverTime > 0f) {
			HoverTime += Time.deltaTime;
		} else {
			HoverTime = 0.1f;
			KeyBoardManager._instance.PlaySound (0);
			_Image.sprite = Sprite_Hover;
		}
	}

	public void SubmitValue()
	{//当按下该键时
		KeyBoardManager._instance.ContentProccessor (theKeyName);
		ConfirmTime = 0.5f;
	}
}
