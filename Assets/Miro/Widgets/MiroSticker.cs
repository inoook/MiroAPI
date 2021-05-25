using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Miro;

public class MiroSticker : MiroWidget
{
    [SerializeField] Text text = null;
    [SerializeField] Image bgImage = null;

    public override void SetData(WidgetRead widget, string boardId)
    {
        base.SetData(widget, boardId);

        if (text != null)
        {
            text.text = widget.text;
        }

        var rectTrans = this.gameObject.GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(widget.width, widget.height);

        Color bgColor = Color.white;
        if (ColorUtility.TryParseHtmlString(widget.style.backgroundColor, out bgColor))
        {
            bgImage.color = bgColor;
        }
    }
}
