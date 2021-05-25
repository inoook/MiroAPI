using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Miro;

// https://developers.miro.com/reference
public class MiroREST : MonoBehaviour
{
    readonly string url = "https://api.miro.com/v1/boards";

    [SerializeField] MiroSetting setting = null;

    [Header("Post")]
    [SerializeField] CreateBoard createBoard = null;
    [SerializeField] CardWidget widget = null;

    // response
    [Header("Get (Response)")]
    [SerializeField] AllWidgets allWidgets = null;

    REST rest = null;

    public MiroSetting Setting
    {
        get => setting;
        set => setting = value;
    }

    private void Start()
    {
        rest = new REST();
    }

    string Authenticate()
    {
        string auth = setting.token;
        auth = "Bearer " + auth;
        return auth;
    }

    [ContextMenu("CreateBoard")]
    void CreateBoardAct()
    {
        StartCoroutine(Create(createBoard));
    }

    IEnumerator Create(CreateBoard createBoard)
    {
        Debug.Log("Create");
        string jsonStr = JsonUtility.ToJson(createBoard);
        string auth = Authenticate();
        Debug.Log(auth);
        yield return rest.Post(url, auth, jsonStr);
    }

    // BOARD CONTENT (Widget)

    [ContextMenu("CreateWidget")]
    void CreateBoardSticker()
    {
        StartCoroutine(CreateWidget(setting.boardId, widget));
    }

    IEnumerator CreateWidget(string boardId, Widget widget)
    {
        Debug.Log("CreateWidget");
        string jsonStr = JsonUtility.ToJson(widget);

        string auth = Authenticate();
        string widgetUrl = url + "/" + boardId + "/widgets";

        yield return rest.Post(widgetUrl, auth, jsonStr);
    }
    //
    [ContextMenu("UpdateWidget")]
    void _UpdateWidget()
    {
        StartCoroutine(UpdateWidget(setting.boardId, widget));
    }

    IEnumerator UpdateWidget(string boardId, Widget widget)
    {
        Debug.Log("UpdateWidget");
        string jsonStr = JsonUtility.ToJson(widget);

        string auth = Authenticate();
        string widgetUrl = url + "/" + boardId + "/widgets/" + widgetId;

        yield return rest.Post(widgetUrl, auth, jsonStr);
    }

    [Header("Patch")]
    [SerializeField] WidgetPos pos;
    [SerializeField] string widgetId;

    [ContextMenu("_UpdateWidgetPos 位置移動")]
    void _UpdateWidgetPos()
    {
        StartCoroutine(UpdateWidgetPos(setting.boardId, widgetId, pos));
    }

    IEnumerator UpdateWidgetPos(string boardId, string widgetId, WidgetPos pos)
    {
        string jsonStr = JsonUtility.ToJson(pos);

        string auth = Authenticate();
        string widgetUrl = url + "/" + boardId + "/widgets/" + widgetId;

        yield return rest.Patch(widgetUrl, auth, jsonStr);
    }
    //
    [ContextMenu("_DeleteWidgetPos")]
    void _DeleteWidgetPos()
    {
        StartCoroutine(DeleteWidget(setting.boardId, widgetId));
    }

    IEnumerator DeleteWidget(string boardId, string widgetId)
    {
        string auth = Authenticate();
        string widgetUrl = url + "/" + boardId + "/widgets/" + widgetId;

        yield return rest.Delete(widgetUrl, auth);
    }

    //
    // 配置されているWidgets取得
    [ContextMenu("_ListAllWidgets 配置されているWidget情報取得")]
    public void _ListAllWidgets(Action<AllWidgets> callback)
    {
        StartCoroutine(ListAllWidgets(setting.boardId, callback));
    }

    IEnumerator ListAllWidgets(string boardId, Action<AllWidgets> callback)
    {
        Debug.Log("ListAllWidgets");

        string auth = Authenticate();
        string widgetUrl = url + "/" + boardId + "/widgets/";

        yield return rest.Get(widgetUrl, auth, (s) =>
        {
            Debug.LogWarning(s.downloadHandler.text);
            allWidgets = JsonUtility.FromJson<AllWidgets>(s.downloadHandler.text);
            //
            callback?.Invoke(allWidgets);
            //Create(allWidgets);
        });
    }

    //
    //※画像の取得は非公開ではあるが、
    //https://miro.com/api/v1/boards/{board_Id}/picture?etag={image_id}
    //で取得可能か？
    [Header("Widgetの画像のロード")]
    [SerializeField] string imageId = "";
    [SerializeField] RawImage rawImage = null;
    [ContextMenu("_LoadImage")]
    void _LoadImage()
    {
        StartCoroutine(LoadImage(setting.boardId, imageId));
    }

    IEnumerator LoadImage(string boardId, string imageId)
    {
        string _url = "https://miro.com/api/v1/boards/" + boardId + "/picture?etag=" + imageId;

        yield return rest.GetImage(_url, (s) =>
        {
            Debug.LogWarning(s.downloadHandler.text);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(s.downloadHandler.data);

            rawImage.texture = texture;
        });
    }

    [SerializeField] Picture picture = null;
    // PICTURES
    // ボードやユーザーのプロフィール写真
    [ContextMenu("_GetPicture")]
    void _GetPicture()
    {
        StartCoroutine(GetPicture(setting.boardId));
    }

    IEnumerator GetPicture(string boardId)
    {
        Debug.Log("ListAllWidgets");

        string auth = Authenticate();
        string url = "https://api.miro.com/v1";
        string _url = url + "/boards/" + boardId + "/picture";
        Debug.Log(_url);
        yield return rest.Get(_url, auth, (s) =>
        {
            Debug.LogWarning(s.downloadHandler.text);

            picture = JsonUtility.FromJson<Picture>(s.downloadHandler.text);
        });
    }

    // USERS


    // -----
    //Debug
#if false
    [SerializeField] MiroCanvas miroCanvas = null;
    void Create(AllWidgets allWidgets)
    {
        miroCanvas.Create(allWidgets, setting.boardId);
    }


    private void Update()
    {
        if (allWidgets != null)
        {
            foreach (var o in allWidgets.data)
            {
                if (o.type == "paint")
                {
                    DrawPoints(new Vector2(o.x, o.y), o.points);
                }
            }
        }
    }

    void DrawPoints(Vector2 center, Vector2[] points)
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector2 p0 = points[i] + center;
            p0.y *= -1;
            Vector2 p1 = points[i + 1] + center;
            p1.y *= -1;
            Debug.DrawLine(p0, p1);
        }
    }

    private void OnDrawGizmos()
    {
        if (allWidgets != null)
        {
            foreach (var o in allWidgets.data)
            {
                Vector3 pos = new Vector3(o.x, -o.y, 0);
                Gizmos.DrawWireSphere(pos, 2.5f);
                GizmosExtra.DrawLabel(pos, o.ToString(), Color.red, 12);
            }
        }
    }

#endif
}
