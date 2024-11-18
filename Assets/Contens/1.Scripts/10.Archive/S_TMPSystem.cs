using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public enum Language
{
    Japanese,
    English
}

public class S_TMPSystem : Singleton<S_TMPSystem>
{
    [SerializeField] TMP_FontAsset[] fontAssets;

    Action<TMP_FontAsset> _onChangeFont;
    Action<Language> _onChangeLanguage;

    private int _useFontIndex = 0;
    public int UseFontIndex
    {
        get => _useFontIndex;
        set
        {
            _onChangeFont?.Invoke(fontAssets[value]);
            _useFontIndex = value;
        }
    }

    private Language _useLanguage = Language.Japanese;
    public Language UseLanguage
    {
        get => _useLanguage;
        set
        {
            _onChangeLanguage?.Invoke(value);
            _useLanguage = value;
        }
    }

    public void Subscribe(Action<TMP_FontAsset> onChangeFont, Action<Language> onChangeLanguage)
    {
        _onChangeFont += onChangeFont;
        _onChangeLanguage += onChangeLanguage;
        onChangeFont?.Invoke(fontAssets[_useFontIndex]);
        onChangeLanguage?.Invoke(_useLanguage);
    }
    
    public void UnSubscribe(Action<TMP_FontAsset> onChangeFont, Action<Language> onChangeLanguage)
    {
        _onChangeFont -= onChangeFont;
        _onChangeLanguage -= onChangeLanguage;
    }

    [Button]
    void ChangeLanguage()
    {
        if (UseLanguage == Language.Japanese)
        {
            UseLanguage = Language.English;
        }
        else
        {
            UseLanguage = Language.Japanese;
        }
    }
    [Button]
    void ChangeFont()
    {
        int useFont = UseFontIndex;
        useFont = (useFont + 1) % fontAssets.Length;
        UseFontIndex = useFont;
    }
}
