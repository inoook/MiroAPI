using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Miro;

public class MiroWidget : MonoBehaviour
{
    [SerializeField] WidgetRead widget;

    public virtual void SetData(WidgetRead widget_, string boardI)
    {
        widget = widget_;
        this.gameObject.name = widget.type;
        
        this.transform.localScale = Vector3.one * (widget.scale == 0 ? 1 : widget.scale);
        this.transform.localEulerAngles = new Vector3(0, 0, -widget.rotation);

        var rectTrans = this.gameObject.GetComponent<RectTransform>();
        rectTrans.anchoredPosition = new Vector2(widget.x, -widget.y);
    }
}
