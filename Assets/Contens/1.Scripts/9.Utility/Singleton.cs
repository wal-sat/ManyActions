using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	public static T _instance = default;

	protected virtual void Awake()
	{
		if (_instance)
		{
			Destroy(this);
			Debug.LogWarning($"{typeof(T)}のインスタンスは既に生成されています");

			return;
		}

		_instance = this as T;

		this.gameObject.transform.parent = null;
        DontDestroyOnLoad(this.gameObject);
	}
	protected virtual void OnDestroy()
	{
		if (ReferenceEquals(this, _instance))
			_instance = null;
	}
}
