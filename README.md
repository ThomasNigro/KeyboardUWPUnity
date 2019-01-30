# Holographic Keyboard issues

## Context

This sample helps you reproduce issues encountered when dealing with the Holographic (native) keyboard provided by UWP, in the context of Unity.

### Issues encountered

#### Input mapping
On Mixed Reality occluded headsets, usually (out of the box) the keyboard has a very very odd behavior in Unity.

No-one wants this behavior. However from a technical standpoint it is easily understandable. The current behavior is based on touch-input-first approach, as the `TouchScreenKeyboard` called by `InputField`, well, is designed for touch. The keyboard appears on focus and disappears on focus lost. In VR, focus callback is when the cursor hovers the `InputField` and onfocus when the cursor stops hovering.

By having a look at the source code of `InputField` it is possible to fix that behavior.

Note that on HoloLens, `TouchScreenKeyboard.isSupported` returns false, which prevents the keyboard from being opened, unlike MR occluded headsets. To fix that, we need to override some specific `InputField` methods to handle the keyboard opening ourselves.

I built a tiny custom `InputField` class which tentatively fix all the issues mentioned above while keeping the original behavior on other platforms such as iOS and Android. The class is found here: `Assets/KeyboardTest`.

#### Broken keyboard layout

During initialisation of the keyboard (aka in the first couple seconds), notice the keyboard layout is broken. Fortunately, on MR occluded headsets, the layout gets fixed as soon as the transition-in animation ends. However, on HoloLens, the layout remains broken in very odd ways. You can notice in the video below that even the broken layout varies, it's not always the same broken layout that appears.

There's no easy fix on the developer side for this one unfortunately, only a user fix: a tap to change the mode (special characters or digits layout) then a switch back to the alphabetical layout fixes the layout.

## Video content

Behavior of original and "fixed" input field 

- on VR headsets: https://drive.google.com/file/d/1WXOzHuE__TCqqI8nSetGC0xkoazZ4rsr/view?usp=sharing
- on HoloLens: https://drive.google.com/file/d/1lWBdOPvj1c6VNdl0SjxVxM-Uwr1cUz8r/view?usp=sharing
