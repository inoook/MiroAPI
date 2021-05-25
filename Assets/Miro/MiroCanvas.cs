using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miro;

public class MiroCanvas : MonoBehaviour
{
    [SerializeField] Transform canvasTrans = null;
    //
    [SerializeField] MiroImg imgPrafab = null;
    [SerializeField] MiroSticker stickerPrafab = null;
    [SerializeField] MiroLine linePrafab = null;
    [SerializeField] MiroText textPrafab = null;
    [SerializeField] MiroShape shapePrafab = null;
    [SerializeField] MiroPaint paintPrafab = null;
    [SerializeField] MiroCard cardPrafab = null;
    [SerializeField] MiroKanban kanbanPrafab = null;
    [SerializeField] MiroWidget widgetPrafab = null;

    //

    public void Create(AllWidgets allWidgets, string boardId)
    {
        for (int i = 0; i < allWidgets.data.Length; i++)
        {
            WidgetRead widget = allWidgets.data[i];
            string type = widget.type;
            MiroWidget w = null;
            if (type == "image")
            {
                w = GameObject.Instantiate<MiroImg>(imgPrafab);
            }
            else if (type == "sticker")
            {
                w = GameObject.Instantiate<MiroSticker>(stickerPrafab);
            }
            else if (type == "line")
            {
                w = GameObject.Instantiate<MiroLine>(linePrafab);
            }
            else if (type == "text")
            {
                w = GameObject.Instantiate<MiroText>(textPrafab);
            }
            else if (type == "shape")
            {
                w = GameObject.Instantiate<MiroShape>(shapePrafab);
            }
            else if (type == "paint")
            {
                w = GameObject.Instantiate<MiroPaint>(paintPrafab);
            }
            else if (type == "card")
            {
                w = GameObject.Instantiate<MiroCard>(cardPrafab);
            }
            else if (type == "kanban")
            {
                w = GameObject.Instantiate<MiroKanban>(kanbanPrafab);
            }
            else
            {
                w = GameObject.Instantiate<MiroWidget>(widgetPrafab);
            }

            w.transform.SetParent(canvasTrans);
            w.SetData(widget, boardId);
        }
    }
}
