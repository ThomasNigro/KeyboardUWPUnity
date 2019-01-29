using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardTests : InputField
{
    private bool activateHoloLensKeyboardOnNextUpdate;

    // OnPointerClick is ran before OnSelect
    public override void OnPointerClick(PointerEventData eventData)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WSAPlayerX86:
            case RuntimePlatform.WSAPlayerX64:
            case RuntimePlatform.WSAPlayerARM:
                if (this.m_Keyboard == null || !this.m_Keyboard.active)
                {
                    base.OnPointerClick(eventData);
                    HoloLensCheck();
                }
                else
                {
                    base.OnDeselect(null);
                }
                break;
            default:
                base.OnPointerClick(eventData);
                break;
        }
    }
    protected override void LateUpdate()
    {
        if (activateHoloLensKeyboardOnNextUpdate)
        {
            Debug.Log("Activating HoloLens keyboard manually");
            if (!isFocused)
            {
                Debug.Log("Activating because it's not focused");
                ActivateHoloLensKeyboard();
                activateHoloLensKeyboardOnNextUpdate = false;
                return;
            }

            activateHoloLensKeyboardOnNextUpdate = false;
        }
        base.LateUpdate();
    }

    private void ActivateHoloLensKeyboard()
    {
        this.m_Keyboard = (inputType == InputType.Password) ?
            TouchScreenKeyboard.Open(m_Text, keyboardType, false, multiLine, true, false, "", characterLimit) :
            TouchScreenKeyboard.Open(m_Text, keyboardType, inputType == InputType.AutoCorrect, multiLine, false, false, "", characterLimit);
    }

    private void HoloLensCheck()
    {
        if (!TouchScreenKeyboard.isSupported)
        {
            Debug.Log(@"Running on Windows Mixed Reality but Touch Screen Keyboard isn't supported.
                            Probably running on, you know, that little thing called HoloLens.");
            activateHoloLensKeyboardOnNextUpdate = true;
        }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WSAPlayerX86:
            case RuntimePlatform.WSAPlayerX64:
            case RuntimePlatform.WSAPlayerARM:
                if (this.m_Keyboard != null && this.m_Keyboard.active)
                {
                    base.OnSelect(eventData);
                    HoloLensCheck();
                }
                break;
            default:
                base.OnSelect(eventData);
                break;
        }
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WSAPlayerX86:
            case RuntimePlatform.WSAPlayerX64:
            case RuntimePlatform.WSAPlayerARM:
                return;
            default:
                base.OnDeselect(eventData);
                break;
        }
    }

    public void DeselectForReal()
    {
        base.OnDeselect(null);
    }
}