using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Miro;

public class MiroShape : MiroWidget
{
    [SerializeField] Text text = null;

    public override void SetData(WidgetRead widget, string boardId)
    {
        base.SetData(widget, boardId);

        if (text != null)
        {
            text.text = widget.text;
        }
    }
}
