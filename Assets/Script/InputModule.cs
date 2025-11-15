using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

public class InputModule : MonoBehaviour
{
    [Header("操作ボタン")]
    [SerializeField] private InputAction _actionOK;
    [SerializeField] private InputAction _actionNG;
    [SerializeField] private InputAction _actionRight;
    [SerializeField] private InputAction _actionLeft;

    /// <summary>
    /// ボタン押下イベント
    /// </summary>
    public event Action OnOKPressed;
    public event Action OnNGPressed;
    public event Action OnRightPressed;
    public event Action OnLeftPressed;

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

    private void OnPerformRight(InputAction.CallbackContext context)
    {
        OnRightPressed.Invoke();
    }

    private void OnPerformLeft(InputAction.CallbackContext context)
    {
        OnLeftPressed.Invoke();
    }
}