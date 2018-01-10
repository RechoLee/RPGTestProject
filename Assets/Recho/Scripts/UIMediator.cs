using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作为button注册的中介者，用于不同UI之间的通信
/// </summary>
public class UIMediator : MonoBehaviour
{
    //startpanel的tween
    public TweenScale startPanelTween;

    public TweenScale loginPanelTween;

    public TweenScale registerPanelTween;

    public TweenScale serverPanelTween;

    public TweenPosition characterPanel;

    public TweenPosition characterSelectPanel;

    private static string userName;
    private static string pwd;

    public UIInput userName_login;
    public UIInput pwd_login;
    public UIInput userName_register;
    public UIInput pwd_register;
    public UIInput repwd_register;

    public UILabel username_start;
    public UILabel server_start;

    public UIButton serverItemBtn;

    public UILabel characterName;
    public UILabel showCharacterName;


    public GameObject[] characterArray;
    public GameObject[] showCharArray;
    GameObject selectedGO;

    /// <summary>
    /// 服务器的grid
    /// </summary>
    public UIGrid serverGrid;
    //火爆的服务器
    public GameObject hotServer;
    //流畅的服务器
    public GameObject normalServer;

    private bool isInitServer = false;

    public void OnStartGameClick()
    {
        //StartPanel隐藏
        startPanelTween.PlayReverse();

        StartCoroutine(HidePanel(startPanelTween.gameObject));

        //characterPanel显示
        characterPanel.gameObject.SetActive(true);
        characterPanel.PlayForward();

    }


    public void OnStartClick()
    {
        //StartPanel隐藏
        startPanelTween.PlayReverse();

        StartCoroutine(HidePanel(startPanelTween.gameObject));


        //LoginPanel显示
        loginPanelTween.gameObject.SetActive(true);
        loginPanelTween.PlayForward();
    }

    IEnumerator HidePanel(GameObject obj)
    {
        yield return new WaitForSeconds(0.4f);
        obj.SetActive(false);
    }


    /// <summary>
    /// 响应Login关闭按钮的点击事件
    /// </summary>
    public void OnCloseClick_Login()
    {
        loginPanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginPanelTween.gameObject));

        startPanelTween.gameObject.SetActive(true);
        startPanelTween.PlayForward();
    }

    /// <summary>
    /// 响应Register关闭按钮的点击事件
    /// </summary>
    public void OnCloseClick_Register()
    {
        registerPanelTween.PlayReverse();
        StartCoroutine(HidePanel(registerPanelTween.gameObject));

        startPanelTween.gameObject.SetActive(true);
        startPanelTween.PlayForward();
    }


    /// <summary>
    /// Login界面的登录按钮的点击事件
    /// </summary>
    public void OnLoginClick_Login()
    {
        //将注册信息存储
        userName = userName_login.value;
        pwd = userName_login.value;
        username_start.text = userName;


        //负责当前界面的关闭，目标界面的显示
        loginPanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginPanelTween.gameObject));

        startPanelTween.gameObject.SetActive(true);
        startPanelTween.PlayForward();
    }


    public void OnRegisterClick_login()
    {
        //负责当前界面的关闭，目标界面的显示
        loginPanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginPanelTween.gameObject));

        registerPanelTween.gameObject.SetActive(true);
        registerPanelTween.PlayForward();

        
    }

    public void OnCancelClick_register()
    {
        //返回login界面
        registerPanelTween.PlayReverse();
        StartCoroutine(HidePanel(registerPanelTween.gameObject));

        loginPanelTween.gameObject.SetActive(true);
        loginPanelTween.PlayForward();
        

    }


    /// <summary>
    /// 注册并登陆按钮的事件
    /// </summary>
    public void OnRegisterAndLogin_register()
    {
        //储存注册信息
        userName = userName_register.value;
        pwd = pwd_register.value;
        username_start.text = userName;


        //切换界面
        registerPanelTween.PlayReverse();
        StartCoroutine(HidePanel(registerPanelTween.gameObject));

        startPanelTween.gameObject.SetActive(true);
        startPanelTween.PlayForward();

    }

    /// <summary>
    /// 选择服务器按钮
    /// </summary>
    public void OnServerPanelClick()
    {
        //初始化服务器列表
        InitServer();

        startPanelTween.PlayReverse();
        StartCoroutine(HidePanel(startPanelTween.gameObject));

        serverPanelTween.gameObject.SetActive(true);
        serverPanelTween.PlayForward();
    }

    /// <summary>
    /// 初始化服务器列表
    /// </summary>
    public void InitServer()
    {
        if (isInitServer)
            return;
        GameObject obj;
        
        for(int i=1;i<=20;i++)
        {
            float x =Random.Range(0f, 100f);
            if (x > 50)
                obj = NGUITools.AddChild(serverGrid.gameObject, hotServer);
            else
                obj = NGUITools.AddChild(serverGrid.gameObject,normalServer);

            serverGrid.AddChild(obj.transform);
            //获取UIbutton 注册事件
       
            UILabel label = obj.GetComponentInChildren<UILabel>();
            label.text = i + "区 花果山水帘洞";
        }
        
        

        isInitServer =true;
    }

    public void OnSelectedServerListClick()
    {
        Debug.Log("ssssssssssssssssssss");
        //serverItemBtn.GetComponentInChildren<UILabel>().text = uILabel.text;
        UIScrollView ss;
        UIGrid uIGrid;
        
    }




    public void OnSetServerClick()
    {
        string info = serverItemBtn.GetComponentInChildren<UILabel>().text;

        serverPanelTween.PlayReverse();
        StartCoroutine(HidePanel(serverPanelTween.gameObject));

        startPanelTween.gameObject.SetActive(true);
        startPanelTween.PlayForward();

        //设置选择的服务器
        server_start.text = info;
    }

    public void UpdateServerList(GameObject go)
    {
        //更新选中的服务器信息
        string serverInfo = go.GetComponentInChildren<UILabel>().text;

        serverItemBtn.GetComponentInChildren<UILabel>().text = serverInfo;

    }


    /// <summary>
    /// 选中了角色
    /// </summary>
    public void OnSelectedCharacter(GameObject go)
    {
        if (selectedGO != null && selectedGO.name.Equals(go.name))
            return;
        if (selectedGO != null)
            iTween.ScaleTo(selectedGO,new Vector3(1,1,1),0.3f);
        iTween.ScaleTo(go, new Vector3(1.3f, 1.3f, 1.3f), 0.3f);
        selectedGO = go;
    }

    /// <summary>
    /// 返回到更换角色界面
    /// </summary>
    public void OnClickCharSelectBack()
    {
        characterSelectPanel.PlayReverse();
        StartCoroutine(HidePanel(characterSelectPanel.gameObject));

        characterPanel.gameObject.SetActive(true);
        characterPanel.PlayForward();
    }


    /// <summary>
    /// 点击确认按钮
    /// </summary>
    public void OnClickCharSelectSure()
    {
        //处理一些选择的信息
        OnClickCharSelectBack();
        int index=0;
        if(selectedGO!=null)
        {
            for (int i = 0; i <characterArray.Length; i++)
            {
                if (selectedGO.name.Equals(characterArray[i].name))
                {
                    index = i;
                    break;
                }
            }
        }

        for (int i = 0; i < showCharArray.Length; i++)
        {
            showCharArray[i].SetActive(false);
        }
        showCharArray[index].SetActive(true);

        if(!string.IsNullOrEmpty(characterName.text))
            showCharacterName.text = characterName.text;

    }

    public void ChangeCharacterBtn()
    {
        characterPanel.PlayReverse();
        StartCoroutine(HidePanel(characterPanel.gameObject));

        characterSelectPanel.gameObject.SetActive(true);
        characterSelectPanel.PlayForward();
        
    }
}
