﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowSetting : MonoBehaviour
{
    #region 窗口透明化显示
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern int SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int cx, int cy,
        int uFlags);

    [DllImport("user32.dll")]
    static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);

    [DllImport("User32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    const int GWL_STYLE = -16;
    const int GWL_EXSTYLE = -20;
    const uint WS_POPUP = 0x80000000;
    const uint WS_VISIBLE = 0x10000000;

    const uint WS_EX_TOPMOST = 0x00000008;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    const int SWP_FRAMECHANGED = 0x0020;
    const int SWP_SHOWWINDOW = 0x0040;
    const int LWA_ALPHA = 2;
    const int LWA_COLORKEY = 2;

    private IntPtr HWND_TOPMOST = new IntPtr(-1);
    private IntPtr HWND_TOP = new IntPtr(0);

    private IntPtr _hwnd;


    void Start()
    {
#if !UNITY_EDITOR
        MARGINS margins = new MARGINS() { cxLeftWidth = -1 };
        _hwnd = GetActiveWindow();
        int fWidth = Screen.width;
        int fHeight = Screen.height;
        SetWindowLong(_hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);

        //鼠标穿透
        //SetWindowLong(_hwnd, GWL_EXSTYLE, WS_EX_TOPMOST | WS_EX_LAYERED | WS_EX_TRANSPARENT);

        DwmExtendFrameIntoClientArea(_hwnd, ref margins);
        SetWindowPos(_hwnd, HWND_TOP , 0, 0, fWidth, fHeight, SWP_FRAMECHANGED | SWP_SHOWWINDOW);

        ShowWindowAsync(_hwnd, 3); //在没有反应应用程序的情况下，强制窗口显示    // SW_SHOWMAXIMIZED(3)
#endif
    }

    #endregion

    #region 窗口最小化设置
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    const int SW_SHOWMINIMIZED = 2; //{最小化, 激活}  

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                ShowWindow(GetForegroundWindow(), SW_SHOWMINIMIZED);
            }
        }
    }
    #endregion
}
