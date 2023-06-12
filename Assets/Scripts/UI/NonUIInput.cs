using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public static class NonUIInput
{
    // Check if is currently focused on input field
    public static bool IsEditingInputField => EventSystem.current.currentSelectedGameObject?.TryGetComponent(out TMP_InputField _) ?? false;
    // conditional layers over UnityEngine.Input.GetKey methods
    public static bool GetKeyDown(KeyCode key) => IsEditingInputField ? false : Input.GetKeyDown(key);
    public static bool GetKeyUp(KeyCode key) => IsEditingInputField ? false : Input.GetKeyUp(key);
    public static bool GetKey(KeyCode key) => IsEditingInputField ? false : Input.GetKey(key);
}
