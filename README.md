# Holographic Keyboard issues

## Context

This sample helps you reproduce issues encountered when dealing with the Holographic (native) keyboard provided by UWP, in the context of Unity.

### Issues encountered

On Mixed Reality occluded headsets, usually (out of the box) the keyboard has a very very odd behavior in Unity.

No-one wants this behavior. However from a technical standpoint it is easily understandable. The current behavior is based on touch-input-first approach, as the `TouchScreenKeyboard` called by `InputField`, well, is designed for touch. The keyboard appears on focus and disappears on focus lost. In VR, focus callback is when the cursor hovers the `InputField` and onfocus when the cursor stops hovering.

By having a look at the source code of `InputField` it is possible to fix that behavior.

## Video content

Behavior of original and "fixed" input field 

- on VR headsets: https://drive.google.com/file/d/1WXOzHuE__TCqqI8nSetGC0xkoazZ4rsr/view?usp=sharing
- on HoloLens: https://drive.google.com/file/d/1lWBdOPvj1c6VNdl0SjxVxM-Uwr1cUz8r/view?usp=sharing