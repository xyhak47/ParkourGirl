using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
public class KeyBoardManager : MonoBehaviour {
	public static KeyBoardManager _instance;
	[Header("最大字符数")]
	[Range(1,20)]
	public int MaxCharNum=7;
	[Header("限定时间")]
	public int TimeToGo=30;
	private AudioSource mAudio;
	public AudioClip[] mAudioClips;
	public Sprite[] Letter_Sprites_Default;
	public Sprite[] Letter_Sprites_Hover;
	public Sprite[] Letter_Sprites_Confirm;
	public Text Content_Text;
	public Text Timer_Text;
	public char[] Content_CharArray;
	public string Content_String;
	private bool KeyBoardLock = false;
	public List<char> Content_CharList=new List<char>();
	private Transform TargetTransf;
	public static KeyBoardManager Instance
	{
		get{return _instance ?? (_instance = new KeyBoardManager ());}
	}

	void Awake()
	{
		_instance = this;
		Content_String = "IM";
		Content_CharArray.Initialize ();
		mAudio = gameObject.GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		Content_Text.text = "player";
		Timer_Text.text = TimeToGo.ToString ();
		StartCoroutine ("ClockTicking");
		TargetTransf = GameObject.Find ("CameraController").transform;
	}

	void Update()
	{
		if (TargetTransf)
			gameObject.transform.forward = TargetTransf.forward;
	}

	IEnumerator ClockTicking()
	{
		yield return new WaitForSeconds (1.0f);
		TimeToGo -= 1;
		Timer_Text.text = TimeToGo.ToString ();
		if (TimeToGo <= 0) {
			TimeToGo = 0;
			TyperConfirm();
		} else {
			StartCoroutine ("ClockTicking");
		}
	}

	public void ContentProccessor(ButtonUnit.KeyName theKeyName)
	{//按键处理方法
		if (KeyBoardLock)
			return;
		
		if (theKeyName == ButtonUnit.KeyName.BackSpace) {
			Debug.Log ("backspace");
			if (Content_CharList.Count <= 0) {
				PlaySound (4);
				return;
			}
			PlaySound (2);
			Content_CharList.RemoveAt (Content_CharList.Count - 1);
			Content_String=new string(Content_CharList.ToArray());
			//Debug.Log (Content_String);
			Content_Text.text = Content_String.ToString ();
		} else if (theKeyName == ButtonUnit.KeyName.Enter) {
			//Debug.Log ("enter");
			 TyperConfirm();
		} else {
			if (Content_CharList.Count >= MaxCharNum) {
				PlaySound (4);
				return;
			}
			foreach (char c in theKeyName.ToString().ToCharArray()) {
				//Debug.Log (c.ToString ());
				Content_CharList.Add (c);
			}
			PlaySound (1);
			Content_String=new string(Content_CharList.ToArray());
//			Debug.Log (Content_String);
			Content_Text.text = Content_String.ToString ();
		}
	}

	void TyperConfirm()
	{//当按下确认键时调用该方法
		KeyBoardLock = true;
		PlaySound (3);
		StopCoroutine ("ClockTicking");

        /*****Add by xyh,2016.8.10 *****/
        UIController.Instance.AddNewRank(Content_Text.text);
        UIController.Instance.ShowUIRank();
        /******************************/
	}

	public void PlaySound(int Index)
	{//音效播放
		mAudio.PlayOneShot (mAudioClips [Index], 0.8f);
	}
}
