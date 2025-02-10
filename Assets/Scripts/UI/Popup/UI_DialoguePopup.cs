using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// TODO : Major task : Build a csv,json,xml for dialogues 

public class UI_DialoguePopup : UI_Popup
{
    enum Texts
    {
        DialogueText,
    }


    enum Buttons
    {
        BackGroundButton,  // Dialog continue button
    }

    string _text;
    const float START_SECOND_PER_CHAR = 0.05f;
    float _secondPerCharacter = START_SECOND_PER_CHAR;
    Action _onTextEndCallback;
    int _index = 0;
    Coroutine _coShowText;
    Button _buttons;
    TMP_Text _dialogueText;
    int _index2 = 10000;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        //GetText((int)Texts.DialogueText).text = "Test";
        GetButton((int)Buttons.BackGroundButton).gameObject.BindEvent(OnClickText);
        GetText((int)Texts.DialogueText).gameObject.BindEvent(OnClickText);

        // RefreshUI();

        ShowText(Managers.Data.Texts[_index2++].eng, onTextEndCallback);

        return true;
    }

    void OnClickText()
    {
        Debug.Log("OnClickText");



        if (_index >= _text.Length)
        {
            if (_coShowText != null)
            {
                StopCoroutine(_coShowText);
                _coShowText = null;
            }

            _onTextEndCallback?.Invoke();
        }
        else
        {
            _secondPerCharacter = Math.Max(0.0001f, 0);
        }
    }

    void onTextEndCallback()
    {
        Debug.Log("showtext : " + _index2);

        if (_index2 > 10021)
        {
            _index2 = 10000;
            Managers.UI.ClosePopupUI(this);
            return;
        }

        ShowText(Managers.Data.Texts[_index2++].eng, onTextEndCallback);
    }


    public void ShowText(string text, Action onTextEndCallback = null)
    {
        // sound 

        Debug.Log("Show text " + text);
        _text = text;
        _onTextEndCallback = onTextEndCallback;
        _index = 0;
        _secondPerCharacter = 0.05f;

        //var dialogueText = GetText((int)Texts.DialogueText);
        //if (dialogueText == null)
        //{
        //    Debug.LogError("ShowText: DialogueText is null. Check BindText or UI setup.");
        //    return;
        //}

        //Debug.Log($"ShowText: Displaying text: {_text}");
        // StartCoroutine(CoShowText());


        if (_coShowText != null)
        {
            StopCoroutine(_coShowText);
            _coShowText = null;
        }

        _coShowText = StartCoroutine(CoShowText());
    }


    // Text Output
    IEnumerator CoShowText()
    {

        while (true)
        {
            if (_index >= _text.Length)
            {
                GetText((int)Texts.DialogueText).text = _text;

                yield return new WaitForSeconds(0.5f);
                _onTextEndCallback?.Invoke();
                break;
            }

            _index++;
            string text = _text.Substring(0, _index);
            //Debug.Log($"CoShowText: Substring(0, {_index}) = {text}");
            GetText((int)Texts.DialogueText).text = text; // null
            yield return new WaitForSeconds(_secondPerCharacter);
        }
    }


    public void SetInfo(int start, int end)
    {
        if (_init == false)
            return;

        Debug.Log("set info");

        ShowText(Managers.Data.Texts[start].eng, onTextEndCallback: () =>
        {
            Debug.Log(start);
            //Managers.UI.ClosePopupUI(this);
            if (start == end)
            {
                Managers.UI.ClosePopupUI(this);
                return;
            }

            SetInfo(start + 1, end);
        });

        //RefreshUI();
    }

    void RefreshUI()
    {
        if (_init == false)
            return;

    }

}
