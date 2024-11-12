using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPController : MonoBehaviour
{
    [SerializeField] private string _japaneseText;
    [SerializeField] private string _englishText;

    private TMP_Text textMeshPro;

    Action<TMP_FontAsset> _onChangeFont;
    Action<Language> _onChangeLanguage;

    void Awake()
    {
        textMeshPro = GetComponent<TMP_Text>();
    }

    void Start()
    {
        _onChangeFont = font => textMeshPro.font = font;
        _onChangeLanguage = SwitchText;
        S_TMPSystem._instance.Subscribe(_onChangeFont, _onChangeLanguage);
    }

    void OnDestroy()
    {
        if (S_TMPSystem._instance != null)
        {
            S_TMPSystem._instance.UnSubscribe(_onChangeFont, _onChangeLanguage);
        }
    }

    public void SetText(string japaneseText, string englishText)
    {
        _japaneseText = japaneseText;
        _englishText = englishText;
        SwitchText(S_TMPSystem._instance.UseLanguage);
    }

    void SwitchText(Language language)
    {
        switch (language)
        {
            case Language.Japanese:
                textMeshPro.text = _japaneseText;
                break;
            case Language.English:
                textMeshPro.text = _englishText;
                break;
        } 
        
    }
}
