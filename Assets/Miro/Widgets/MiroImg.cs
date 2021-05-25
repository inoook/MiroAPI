using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miro;

public class MiroImg : MiroWidget
{
    [SerializeField] ImgLoader imgLoader = null;

    public override void SetData(WidgetRead widget, string boardId)
    {
        base.SetData(widget, boardId);

        string _url = "https://miro.com/api/v1/boards/" + boardId + "/picture?etag=" + widget.id;
        StartCoroutine(imgLoader.LoadTexture(_url));
    }
}
