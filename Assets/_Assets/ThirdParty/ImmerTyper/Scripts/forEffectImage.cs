using UnityEngine;
using System.Collections;

using UnityEngine.UI;
public class forEffectImage : MonoBehaviour {
	private Image _Image;
	public Sprite[] AlternativeSprites;
	private Vector3 NormalLocalScale;
	private int SpriteIndex=0;
	public float PreWaitTime=0f;
	void Awake()
	{
		NormalLocalScale = gameObject.transform.localScale;	
		_Image = gameObject.GetComponent<Image> ();
		_Image.sprite = AlternativeSprites [0];
		gameObject.transform.localScale = NormalLocalScale*0.7f;
		Color theColor = _Image.color;
		theColor.a = 0f;
		_Image.color = theColor;
	}

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds (PreWaitTime);
		StartCoroutine ("EffectManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator EffectManager()
	{
		gameObject.transform.localScale = NormalLocalScale*0.7f;
		Color theColor = _Image.color;
		theColor.a = 0f;
		_Image.color = theColor;

		while (gameObject.transform.localScale.magnitude < NormalLocalScale.magnitude) {
			float Delta = Time.deltaTime*1f;
			gameObject.transform.localScale += new Vector3 (Delta, Delta, Delta);

			theColor.a += Time.deltaTime*1.5f;
			_Image.color = theColor;

			yield return new WaitForFixedUpdate ();
		}

		yield return new WaitForSeconds (1.0f);

		while (theColor.a > 0f) {
			theColor.a -= Time.deltaTime*1.5f;
			_Image.color = theColor;
			yield return new WaitForFixedUpdate ();
		}

		SpriteIndex += 1;
		if (SpriteIndex >= AlternativeSprites.Length) {
			SpriteIndex = 0;
		}

		_Image.sprite=AlternativeSprites[SpriteIndex];
		
		yield return new WaitForSeconds (1.5f);
		StartCoroutine ("EffectManager");
	}
}
