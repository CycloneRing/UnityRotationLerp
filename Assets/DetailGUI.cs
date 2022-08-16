using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailGUI : MonoBehaviour
{
    public Vector2 padding = new Vector2();

    void OnGUI()
    {
        GUI.skin.label.fontSize = 21;
        var rotation = this.transform.localEulerAngles;
        var text = $"[ {rotation.x:F3} , {rotation.y:F3} , {rotation.z:F3}]";
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        var textSize = GUI.skin.label.CalcSize(new GUIContent(text));
        GUI.Label(new Rect(position.x + padding.x, Screen.height - position.y + padding.y, textSize.x, textSize.y), text);
    }
}
