//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2015 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the object's alpha. Works with both UI widgets as well as renderers.
/// </summary>

[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
	[Range(0f, 1f)] public float from = 1f;
	[Range(0f, 1f)] public float to = 1f;

	bool mCached = false;
	UIRect mRect;
	Material mMat;
	SpriteRenderer mSr;

	[System.Obsolete("Use 'value' instead")]
	public float alpha { get { return this.value; } set { this.value = value; } }

	void Cache ()
	{
		mCached = true;
		mRect = GetComponent<UIRect>();
		mSr = GetComponent<SpriteRenderer>();

		if (mRect == null && mSr == null)
		{
			Renderer ren = GetComponent<Renderer>();
			if (ren != null) mMat = ren.material;
			if (mMat == null) mRect = GetComponentInChildren<UIRect>();
		}
	}

	/// <summary>
	/// Tween's current value.
	/// </summary>

	public float value
	{
		get
		{
			if (!mCached) Cache();
			if (mRect != null) return mRect.alpha;
			if (mSr != null) return mSr.color.a;
			return mMat != null ? mMat.color.a : 1f;
		}
		set
		{
			if (!mCached) Cache();

			if (mRect != null)
			{
				mRect.alpha = value;
			}
			else if (mSr != null)
			{
				Color c = mSr.color;
				c.a = value;
				mSr.color = c;
			}
			else if (mMat != null)
			{
				Color c = mMat.color;
				c.a = value;
				mMat.color = c;
			}
		}
	}

	/// <summary>
	/// Tween the value.
	/// </summary>

	protected override void OnUpdate (float factor, bool isFinished) { value = Mathf.Lerp(from, to, factor); }

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	public void Begin(float duration, float alpha )
	{
		this.from = value;
		this.to = alpha;

		if (duration <= 0f)
		{
			this.Sample(1f, true);
			this.enabled = false;
		} else {
			this.enabled = true;
		}
	}

	static public TweenAlpha Begin (GameObject go, float duration, float alpha)
	{
		TweenAlpha comp = UITweener.Begin<TweenAlpha>(go, duration);
		comp.from = comp.value;
		comp.to = alpha;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}

	static public TweenAlpha Begin (GameObject go, float duration, float from_alpha, float to_alpha)
	{
		TweenAlpha comp = UITweener.Begin<TweenAlpha>(go, duration);
		comp.from = from_alpha;
		comp.to = to_alpha;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}

	public override void SetStartToCurrentValue () { from = value; }
	public override void SetEndToCurrentValue () { to = value; }
}
/*
public class TweenAlphaForTMPro : UITweener
{
	[Range(0f, 1f)] public float from = 1f;
	[Range(0f, 1f)] public float to = 1f;

	bool mCached = false;
	TMPro.TextMeshPro mTextMeshPro;

	[System.Obsolete("Use 'value' instead")]
	public float alpha { get { return this.value; } set { this.value = value; } }

	void Cache ()
	{
		mCached = true;
		mTextMeshPro = GetComponent<TMPro.TextMeshPro>();
	}

	/// <summary>
	/// Tween's current value.
	/// </summary>

	public float value
	{
		get
		{
			if (!mCached) Cache();
			return mTextMeshPro != null ? mTextMeshPro.color.a : 1f;
		}
		set
		{
			if (!mCached) Cache();

			if (mTextMeshPro != null)
			{
				Color c = mTextMeshPro.color;
				c.a = value;
				mTextMeshPro.color = c;
			}
		}
	}

	/// <summary>
	/// Tween the value.
	/// </summary>

	protected override void OnUpdate (float factor, bool isFinished) { value = Mathf.Lerp(from, to, factor); }

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenAlphaForTMPro Begin (GameObject go, float duration, float alpha)
	{
		TweenAlphaForTMPro comp = UITweener.Begin<TweenAlphaForTMPro>(go, duration);
		comp.from = comp.value;
		comp.to = alpha;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}

	static public TweenAlphaForTMPro Begin( TweenAlphaForTMPro comp, float duration, float alpha )
	{
		// from parent...
		//comp.mStarted = false;
		comp.duration = duration;
		comp.tweenFactor = 0f;
		//comp.mAmountPerDelta = Mathf.Abs(comp.amountPerDelta);
		comp.style = Style.Once;
		comp.animationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
		comp.eventReceiver = null;
		comp.callWhenFinished = null;
		comp.enabled = true;

		//
		comp.from = comp.value;
		comp.to = alpha;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}

	public override void SetStartToCurrentValue () { from = value; }
	public override void SetEndToCurrentValue () { to = value; }
}
*/