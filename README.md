# Holographic Keyboard issues

This sample helps you reproduce issues encountered when dealing with the Holographic (native) keyboard provided by UWP, in the context of Unity.

On Mixed Reality occluded headsets, usually (out of the box) the keyboard has a very very odd behavior in Unity. If you have an InputField in the scene, it behaves like this:

[insert gif]

No-one wants this behavior. However from a technical standpoint it is easily understandable. The current behavior is based on touch-input-first approach, as the `TouchScreenKeyboard` called by `InputField`, well, is designed for touch. The keyboard appears on focus and disappears on focus lost. In VR, focus callback is when the cursor hovers the `InputField` and onfocus when the cursor stops hovering.

By having a look at the source code of `InputField` it is possible to fix that behavior.