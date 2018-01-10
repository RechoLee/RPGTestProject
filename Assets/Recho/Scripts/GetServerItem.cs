using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetServerItem : MonoBehaviour {


    /// <summary>
    /// 当鼠标抬起的时候，发送选中的物体到Mediator脚本处理
    /// </summary>
    /// <param name="isPress"></param>
    void OnPress(bool isPress)
    {
        if (!isPress)
            GameObject.Find("UIMediatorObj").GetComponent<UIMediator>().UpdateServerList(this.gameObject);
    }
}
