using UnityEngine;
using System.Collections;

public class UITweenScale_view : MonoBehaviour {

	//---------------------------------------------------------------------------------------------------------Variables

	[SerializeField] Vector3 fromLocalScale = Vector3.zero;
	[SerializeField] Vector3 toLocalScale = Vector3.one;
	[SerializeField, Tooltip("Time values must be between 0 and 1.")]
	AnimationCurve m_tweenCurve = new AnimationCurve(
									  new Keyframe(0f, 0f, 0f, 0f),
									  new Keyframe(0.15f, 0f, 0f, 25.00001f),
									  new Keyframe(0.17f, 1f, 9.793964f, -0.02387369f),
									  new Keyframe(0.4011494f, 1f, 0f, 0f),
									  new Keyframe(1f, 0f, 0f, 0f)
								  );
	[SerializeField] float m_tweenDuration;

	//Tween
	float m_currTweenPercent;
	RectTransform m_rectTransform;

	//----------------------------------------------------------------------------------------------------------Accesors

	RectTransform rectTransform {
		get {
			if (m_rectTransform == null)
				m_rectTransform = transform.GetComponent<RectTransform>();
			return m_rectTransform;
		}
	}

	/// Playback percent. Set in order to set scale at desired percent of the tween curve. [0,1]
	public float playback {
		get { return m_currTweenPercent = Mathf.Clamp01(m_currTweenPercent); }
		set {
			m_currTweenPercent = Mathf.Clamp01(value);
			if (c_tweenFadeCorout != null)
				StopCoroutine(c_tweenFadeCorout);
			rectTransform.localScale = Vector3.LerpUnclamped(fromLocalScale, toLocalScale, m_tweenCurve.Evaluate(m_currTweenPercent));
		}
	}

	//---------------------------------------------------------------------------------------------------------Dalegates

	//--------------------------------------------------------------------------------------------------------Initialize

	//-----------------------------------------------------------------------------------------------------------Updates

	/*void Update() {
		if (Input.GetKeyDown(KeyCode.N)) {
			if (c_tweenFadeCorout != null)
				StopCoroutine(c_tweenFadeCorout);
			c_tweenFadeCorout = TweenFade(true, m_tweenDuration, null);
			StartCoroutine(c_tweenFadeCorout);
		}
		if (Input.GetKeyDown(KeyCode.M)) {
			if (c_tweenFadeCorout != null)
				StopCoroutine(c_tweenFadeCorout);
			c_tweenFadeCorout = TweenFade(false, m_tweenDuration, null);
			StartCoroutine(c_tweenFadeCorout);
		}
	}*/

	//------------------------------------------------------------------------------------------------Internal functions

	IEnumerator c_tweenFadeCorout;

	IEnumerator TweenFade(bool isForwardFade, float duration, System.Action onComplete = null) {
		//Initialize your things
		m_currTweenPercent = Mathf.Clamp01(m_currTweenPercent);
		float elapsedTime = 0;
		float t = 0;
		float lerpT = 0;
		float offset;
		float fixDuration;
		if (isForwardFade) {
			offset = m_currTweenPercent;
			fixDuration = (1 - m_currTweenPercent) * duration;
		} else {
			offset = 1 - m_currTweenPercent;
			fixDuration = (m_currTweenPercent) * duration;
		}
		//Debug.Log("Fix Duration:" + fixDuration + " currValue:" + m_currTweenPercent);
		if (fixDuration <= 0)
			yield break; //Break the coroutine
		while (elapsedTime <= fixDuration + Time.unscaledDeltaTime) {
			if (isForwardFade) {
				t = offset + elapsedTime / fixDuration;
			} else {
				t = 1 - (offset + elapsedTime / fixDuration);
			}
			m_currTweenPercent = t = Mathf.Clamp01(t);

			//Apply t parameter: Curve.Evaluate(t);  parameter*t; param+=t*rate;
			rectTransform.localScale = Vector3.LerpUnclamped(fromLocalScale, toLocalScale, m_tweenCurve.Evaluate(t));

			elapsedTime += Time.unscaledDeltaTime;
			yield return null;
		}
		if (isForwardFade) {
			rectTransform.localScale = toLocalScale;
		} else {
			rectTransform.localScale = fromLocalScale;
		}
		//Last frame for coorutine execute the last thing
		if (onComplete != null)
			onComplete();

	}
	//--------------------------------------------------------------------------------------------------Public functions
	public void PlayForward(System.Action onComplete = null) {
		if (!isActiveAndEnabled)
			return;
		if (c_tweenFadeCorout != null)
			StopCoroutine(c_tweenFadeCorout);
		c_tweenFadeCorout = TweenFade(true, m_tweenDuration, onComplete);
		StartCoroutine(c_tweenFadeCorout);
	}

	public void PlayReverse(System.Action onComplete = null) {
		if (!isActiveAndEnabled)
			return;
		if (c_tweenFadeCorout != null)
			StopCoroutine(c_tweenFadeCorout);
		c_tweenFadeCorout = TweenFade(false, m_tweenDuration, onComplete);
		StartCoroutine(c_tweenFadeCorout);
	}

	public void Invert() {
		Vector3 aux = toLocalScale;
		toLocalScale = fromLocalScale;
		fromLocalScale = aux;
	}

	public void Reset(bool setAtToScaleValue = false) {
		if (setAtToScaleValue)
			playback = 1;
		else
			playback = 0;
	}

	//----------------------------------------------------------------------------------------------

	void OnDisable() {
		if (c_tweenFadeCorout != null) StopCoroutine(c_tweenFadeCorout);
	}

}
