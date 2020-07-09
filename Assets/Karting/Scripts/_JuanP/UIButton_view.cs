using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;


public class UIButton_view :Selectable,IPointerClickHandler {
	//Button btn;

	[System.Serializable]
	public class UIButtonEvents_smodel {
		
		public UnityEngine.Events.UnityEvent onPressDown;
		public UnityEngine.Events.UnityEvent onPressUp;
		//public UnityEngine.Events.UnityEvent onPressHold;
		/*public UnityEventFloat onPressHold;

		[System.Serializable]
		public class UnityEventFloat:UnityEngine.Events.UnityEvent<float> {

		}*/
	}
	//---------------------------------------------------------------------------------------------------------Variables
	/*[SerializeField] 
	bool m_interactable = true;*/

	//Tween
	[Header("ButtonTween")]
	[SerializeField] 
	RectTransform m_targetForTween;
	[SerializeField] 
	Vector3 m_normalScale = Vector3.one;
	[SerializeField] 
	Vector3 m_pressedScale = new Vector3(0.9f, 0.9f, 0.9f);
	//[SerializeField, Tooltip("Time values must be between 0 and 1. Where t=0 is normal state and t=1 is pressed state. value=0 is normalScale and value=1 is pressedScale")]
	[SerializeField, Tooltip("Time values must be between 0 and 1.\n-Horizontal axis [0,1]=[normal,pressed]\n-Vertical axis [0,1]=[normalScale,pressedScale]")] 
	AnimationCurve m_toPressedCurve = new AnimationCurve(
		                                  new Keyframe(0f, 0f, 3.041004f, 3.041004f),
		                                  new Keyframe(1f, 1f, 0f, 0f)
	                                  );
	[SerializeField, Tooltip("Time values must be between 0 and 1.\n-Horizontal axis [0,1]=[normal,pressed]\n-Vertical axis [0,1]=[normalScale,pressedScale]")] 
	AnimationCurve m_toNormalCurve = new AnimationCurve(
		                                 new Keyframe(0f, 0f, -22.19325f, -22.19325f),
		                                 new Keyframe(0.1440796f, -1.11788f, 0.4271598f, 0.4271598f),
		                                 new Keyframe(0.396396f, 0.4520959f, 3.072056f, 3.072056f),
		                                 new Keyframe(1f, 1f, 0.1609337f, 0.1609337f)
	                                 );
	[SerializeField] 
	float m_pressDuration = 0.1f;
	[SerializeField] 
	float m_restoreDuration = 0.2f;
	[SerializeField,Range(0, 1)] 
	float m_pressPersentForRestoreAnim = 0.2f;


	//Events
	[Space(6)]
	[SerializeField] 
	UnityEvent m_onClick;
	[SerializeField] 
	UIButtonEvents_smodel m_otherEvents;


	//Internal
	bool m_isPressed;
	bool m_isClicking;
	//Tween
	//[SerializeField,Range(-0.3f, 1.3f)]
	float m_currTweenPercent;
	bool m_oldIsForwardFade;




	//Component
	EventTrigger m_eventTrigger;

	//----------------------------------------------------------------------------------------------------------Accesors

	RectTransform targetForTween {
		get {
			if (m_targetForTween == null)
				m_targetForTween = transform.GetComponent<RectTransform>();
			return m_targetForTween;
		}
	}

	public EventTrigger eventTrigger {
		get { 
			if (m_eventTrigger == null) {
				m_eventTrigger = GetComponent<EventTrigger>();
			}
			return m_eventTrigger;
		}	
	}

	public UnityEvent onClick {	get { return m_onClick; } }

	public UnityEvent onPressDown {	get { return m_otherEvents.onPressDown; } }

	public UnityEvent onPressUp {	get { return m_otherEvents.onPressUp; } }

	//------------------------------------------------------------------------------------------------Inherited Accesors
	
	//---------------------------------------------------------------------------------------------------------Dalegates
	
	//--------------------------------------------------------------------------------------------------------Validation
	
	//--------------------------------------------------------------------------------------------------------Initialize

	void Awake() {
		
	}

	void Start() {
		
	}

	void OnEnable() {
		m_isPressed = false;
		m_isClicking = false;
		m_oldIsForwardFade = false;
		SetNormalState();
	}
	
	//-----------------------------------------------------------------------------------------------------------Updates
	
	//void Update() {
		
	//}

	//------------------------------------------------------------------------------------------------Internal functions

	IEnumerator c_tweenFadeCorout;

	float TweenCurve(float t, bool forwardFade) {
		if (forwardFade)
			return  m_toPressedCurve.Evaluate(t);
		else
			return  m_toNormalCurve.Evaluate(t);
	}

	IEnumerator TweenFade(bool isForwardFade, float duration) {

		//Initialize your things
		bool wasLastIsForward = m_oldIsForwardFade;
		m_oldIsForwardFade = isForwardFade;
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

			if (fixDuration >= duration * m_pressPersentForRestoreAnim) {
				//Lerp with curve for current requested tween direction
				lerpT = TweenCurve(t, isForwardFade);
			} else {
				//Lerp with curve for previous unfinished tween direction
				lerpT = TweenCurve(t, wasLastIsForward);
			}
			//Apply t parameter: Curve.Evaluate(t);  parameter*t; param+=t*rate;
			targetForTween.localScale = Vector3.LerpUnclamped(m_normalScale, m_pressedScale, lerpT);

			elapsedTime += Time.unscaledDeltaTime;
			yield return null;
		}
		//Last frame for coorutine execute the last thing
		if (isForwardFade) {
			targetForTween.localScale = m_pressedScale;
			//targetForTween.localScale = m_normalScale;
		} else {
			targetForTween.localScale = m_normalScale;
		}

		//if (onComplete != null) onComplete();

	}

	void PlayTween(bool forward) {
		if (!isActiveAndEnabled)
			return;
		if (c_tweenFadeCorout != null)
			StopCoroutine(c_tweenFadeCorout);
		if (forward) {
			c_tweenFadeCorout = TweenFade(forward, m_pressDuration);
		} else {
			c_tweenFadeCorout = TweenFade(forward, m_restoreDuration);
		}
		StartCoroutine(c_tweenFadeCorout);
	}

	void SetNormalState() {
		targetForTween.localScale = m_normalScale;
		m_currTweenPercent = 0;
	}


	public void SetInteractable(bool enable) {
		bool m_isPressed = false;
		bool m_isClicking = false;
		//Tween
		//[SerializeField,Range(-0.3f, 1.3f)]
		//float m_currTweenPercent = 0;
		//bool m_oldForwardFade = 0;
		//targetForTween.localScale = m_normalScale;
	}

	//--------------------------------------------------------------------------------------------------Public functions


	///Add entry callback to event trigger
	public void AddListenerToEventTrigger(EventTriggerType eventType, UnityAction<PointerEventData> call) {
		if (call == null) {
			return;
		}
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = eventType;//.PointerUp;
		entry.callback.AddListener((data) => {
			if (call != null)
				call((PointerEventData)data);
		});
		eventTrigger.triggers.Add(entry);
	}

	///Add entry callback to event trigger
	public void AddListenerToEventTrigger(EventTriggerType eventType, UnityAction call) {
		if (call == null) {
			return;
		}
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = eventType;//.PointerUp;
		entry.callback.AddListener((data) => {
			if (call != null)
				call();
		});
		eventTrigger.triggers.Add(entry);
	}



	//------------------------------------------------------------------------------------------Interface implementation

	void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
		if (!interactable)
			return;
		m_isClicking = false;	
		m_isPressed = false;
	
		m_onClick.Invoke();
		//Debug.Log("onClick", this);
		//m_otherEvents.onPressUp.Invoke();
		PlayTween(m_isPressed);

		//Debug.Log("OnPointerClick", this);
	}

	//-----------------------------------------------------------------------------------------------Inherited functions
	bool m_isFocus;

	public override void OnPointerDown(PointerEventData eventData) {
		if (!interactable)
			return;
		base.OnPointerDown(eventData);

		m_isClicking = true;	
		m_isPressed = true;
		m_otherEvents.onPressDown.Invoke();
		//Debug.Log("1 onPressDown", this);

		PlayTween(m_isPressed);

		//Debug.Log("OnPointerDown", this);
	}

	public override void OnPointerEnter(PointerEventData eventData) {
		if (!interactable)
			return;
		base.OnPointerEnter(eventData);

		if (m_isClicking) {	
			if (!m_isPressed) {
				m_isPressed = true;
				PlayTween(m_isPressed);
				//Debug.Log("2 onPressDown", this);
				m_otherEvents.onPressDown.Invoke();
			}
		}
		//Debug.Log("OnPointerEnter", this);
	}

	public override void OnPointerExit(PointerEventData eventData) {
		if (!interactable)
			return;
		base.OnPointerExit(eventData);

		if (m_isClicking) {
			m_isPressed = false;
			m_otherEvents.onPressUp.Invoke();
			//Debug.Log("onPressUp out", this);
			//PlayTween(m_isPressed);
			SetNormalState();
		}
		//Debug.Log("OnPointerExit", this);
	}

	public override void OnPointerUp(PointerEventData eventData) {
		if (!interactable)
			return;
		base.OnPointerUp(eventData);
		if (m_isClicking) {
			m_isPressed = false;
			m_otherEvents.onPressUp.Invoke();
			//Debug.Log("onPressUp out", this);
			PlayTween(m_isPressed);
			//SetNormalState();
		}

		m_isClicking = false;	
		m_isPressed = false;
		//Debug.Log("OnPointerUp " + m_isFocus, this);
	}


	public override void OnSelect(BaseEventData eventData) {
		base.OnSelect(eventData);
		//m_isFocus = true;
		//Debug.Log("OnSelect", this);
	}

	public override void OnDeselect(BaseEventData eventData) {
		base.OnDeselect(eventData);
		m_isFocus = false;
		//if (!interactable) return;
		if (m_isClicking) {
			m_otherEvents.onPressUp.Invoke();
			//Debug.Log("onPressUp out", this);
			SetNormalState();
		}
		m_isClicking = false;	
		m_isPressed = false;
		//Debug.Log("OnDeselect", this);
	}

	//-------------------------------------------------------------------------------------------------Disable & Destroy

	void OnDisable() {
		if (c_tweenFadeCorout != null)
			StopCoroutine(c_tweenFadeCorout);
	}

}
