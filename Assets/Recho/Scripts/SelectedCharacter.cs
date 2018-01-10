using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacter : MonoBehaviour {

    public void OnPress(bool isPress)
    {
        if (!isPress)
            GameObject.Find("UIMediatorObj").GetComponent<UIMediator>().OnSelectedCharacter(this.gameObject);
    }
}
