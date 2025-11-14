using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputModule : MonoBehaviour
{
    [SerializeField] private InputAction _actionOK;
    [SerializeField] private InputAction _actionNG;

    public static event Action OnOKPressed;
    public static event Action OnNGPressed;

    // 有効化
    private void OnEnable()
    {
        // Actionのコールバックを登録
        _actionOK.performed += OnPerformedOK;
        _actionNG.performed += OnPerformedNG;
        // InputActionを有効化
        _actionOK?.Enable();
        _actionNG?.Enable();
    }

    // 無効化
    private void OnDisable()
    {
        // Actionのコールバックを解除
        _actionOK.performed -= OnPerformedOK;
        _actionNG.performed -= OnPerformedNG;
        // Actionを無効化する必要がある
        _actionOK?.Disable();
        _actionNG?.Disable();
    }

    // コールバックを受け取ったときの処理
    private void OnPerformedOK(InputAction.CallbackContext context)
    {
        OnOKPressed.Invoke();
    }

    private void OnPerformedNG(InputAction.CallbackContext context)
    {
        OnNGPressed.Invoke();
    }
}
