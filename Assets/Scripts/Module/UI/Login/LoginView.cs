using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginView : MonoBehaviour
{
    #region 登录主界面
    [Header("登录主界面")]
    public Transform loginMainTran;
    public void InitView()
    {
        loginMainTran = GameObject.Find("UI/LoginPanel").GetComponent<Transform>() ;
        InitLoginView();
        InitRegisterView();
        InitForgetView();

        AddEventHandler();
    }
    private void AddEventHandler()
    {
        AddLoginEventHandler();
        AddRegisterEventHandler();
        AddForgetEventHandler();
    }

    public void ShowView()
    {
        loginMainTran.gameObject.SetActive(true);
    }
    public void HideView()
    {
        loginMainTran.gameObject.SetActive(false);
    }
    #endregion

    #region 登录界面
    [Header("登录界面")]
    public Image loginPanel;
    public TMP_InputField loginIdInput;
    public TMP_InputField loginPwInput;
    public Button loginBtn;
    public Button loginRegBtn;
    public Button loginForgetBtn;
    private void InitLoginView()
    {
        loginPanel = GameObject.Find("UI/LoginPanel/LoginContent").GetComponent<Image>();
        loginIdInput = GameObject.Find("UI/LoginPanel/LoginContent/Content/InputContent/IdInputContentOutLine/IdInputContent/InputField (TMP)").GetComponent<TMP_InputField>();
        loginPwInput = GameObject.Find("UI/LoginPanel/LoginContent/Content/InputContent/PasswordInputContentOutLine/PasswordInputContent/InputField (TMP)").GetComponent<TMP_InputField>();
        loginBtn = GameObject.Find("UI/LoginPanel/LoginContent/Content/LoginBtn").GetComponent<Button>();
        loginRegBtn = GameObject.Find("UI/LoginPanel/LoginContent/Content/RegisterBtn").GetComponent<Button>();
        loginForgetBtn = GameObject.Find("UI/LoginPanel/LoginContent/Content/ForgetBtn").GetComponent<Button>();
    }
    public void ShowLoginView()
    {
        loginPanel.gameObject.SetActive(true);
        registerPanel.gameObject.SetActive(false);
        forgetPanel.gameObject.SetActive(false);
        ResetLoginView();
    }
    private void AddLoginEventHandler()
    {
        loginRegBtn.onClick.AddListener(ShowRegisterView);
        loginForgetBtn.onClick.AddListener(ShowForgetView);
    }
    private void ResetLoginView()
    {
        loginIdInput.text = "";
        loginPwInput.text = "";
    }
    #endregion

    #region 注册界面
    [Header("注册界面")]
    public Image registerPanel;
    public TMP_InputField registerMailInput;
    public TMP_InputField registerNameInput;
    public TMP_InputField registerKeyInput;
    public TMP_InputField registerPasswordInput;
    public TMP_InputField registerIdentigyInput;
    public Button registerGetIdentifyBtn;
    public Button registerBtn;
    public Button registerReLoginBtn;
    private void InitRegisterView()
    {
        registerPanel = GameObject.Find("UI/LoginPanel/RegisterContent").GetComponent<Image>();
        registerMailInput = GameObject.Find("UI/LoginPanel/RegisterContent/Content/InputContent/MailInputContentOutLine/MailInputContent/InputField (TMP)").GetComponent<TMP_InputField>();
        registerNameInput = GameObject.Find("UI/LoginPanel/RegisterContent/Content/InputContent/NameInputContentOutLine/NameInputContent/InputField (TMP)").GetComponent<TMP_InputField>();
        registerKeyInput = GameObject.Find("UI/LoginPanel/RegisterContent/Content/InputContent/KeyInputContentOutLine/KeyInputContent/InputField (TMP)").GetComponent<TMP_InputField>();
        registerPasswordInput = GameObject.Find("UI/LoginPanel/RegisterContent/Content/InputContent/PasswordInputContentOutLine/PasswordInputContent/InputField (TMP)").GetComponent<TMP_InputField>();
        registerIdentigyInput = GameObject.Find("UI/LoginPanel/RegisterContent/Content/InputContent/IdentifyNumInputPanel/IdentifyNumInputOutLine/IdentifyNumInput/InputField (TMP)").GetComponent<TMP_InputField>();
        registerGetIdentifyBtn = GameObject.Find("UI/LoginPanel/RegisterContent/Content/InputContent/IdentifyNumInputPanel/IdentifyButtonOutLine/Button").GetComponent<Button>();
        registerReLoginBtn = GameObject.Find("UI/LoginPanel/RegisterContent/Content/ReturnLoginBtn").GetComponent<Button>();
        registerBtn = GameObject.Find("UI/LoginPanel/RegisterContent/Content/RegisterBtn").GetComponent<Button>();
    }
    public void ShowRegisterView()
    {
        loginPanel.gameObject.SetActive(false);
        registerPanel.gameObject.SetActive(true);
        forgetPanel.gameObject.SetActive(false);
        ResetRegisterView();
    }
    private void AddRegisterEventHandler()
    {
        registerReLoginBtn.onClick.AddListener(ShowLoginView);
    }
    private void ResetRegisterView()
    {
        registerMailInput.text = "";
        registerNameInput.text = "";
        registerKeyInput.text = "";
        registerPasswordInput.text = "";
        registerIdentigyInput.text = "";
    }
    #endregion

    #region 找回密码界面
    [Header("找回密码界面")]
    public Image forgetPanel;
    public TMP_InputField forgetMailInput;
    public TMP_InputField forgetKeyInput;
    public TMP_InputField forgetPasswordInput;
    public TMP_InputField forgetIdentifyInput;
    public Button forgetGetIdentifyBtn;
    public Button forgetReGetBtn;
    public Button forgetReLoginBtn;
    private void InitForgetView()
    {
        forgetPanel = GameObject.Find("UI/LoginPanel/ForgetContent").GetComponent<Image>();
        forgetMailInput = GameObject.Find("UI/LoginPanel/ForgetContent/Content/InputContent/MailInputContentOutLine/MailInputContent/InputField (TMP)").GetComponent<TMP_InputField>();
        forgetKeyInput  = GameObject.Find("UI/LoginPanel/ForgetContent/Content/InputContent/KeyInputContentOutLine/KeyInputContent/InputField (TMP)").GetComponent<TMP_InputField>();
        forgetPasswordInput = GameObject.Find("UI/LoginPanel/ForgetContent/Content/InputContent/PasswordInputContentOutLine/PasswordInputContent/InputField (TMP)").GetComponent<TMP_InputField>();
        forgetIdentifyInput = GameObject.Find("UI/LoginPanel/ForgetContent/Content/InputContent/IdentifyNumInputPanel/IdentifyNumInputOutLine/IdentifyNumInput/InputField (TMP)").GetComponent<TMP_InputField>();
        forgetGetIdentifyBtn = GameObject.Find("UI/LoginPanel/ForgetContent/Content/InputContent/IdentifyNumInputPanel/IdentifyButtonOutLine/Button").GetComponent<Button>();
        forgetReGetBtn = GameObject.Find("UI/LoginPanel/ForgetContent/Content/ReGetBtn").GetComponent<Button>();
        forgetReLoginBtn = GameObject.Find("UI/LoginPanel/ForgetContent/Content/ReturnLoginBtn").GetComponent<Button>();
    }
    public void ShowForgetView()
    {
        loginPanel.gameObject.SetActive(false);
        registerPanel.gameObject.SetActive(false);
        forgetPanel.gameObject.SetActive(true);
        ResetForgetView();
    }
    private void AddForgetEventHandler()
    {
        forgetReLoginBtn.onClick.AddListener(ShowLoginView);
    }
    private void ResetForgetView()
    {
        forgetMailInput.text = "";
        forgetKeyInput.text = "";
        forgetPasswordInput.text = "";
        forgetIdentifyInput.text = "";
    }
    #endregion
}
