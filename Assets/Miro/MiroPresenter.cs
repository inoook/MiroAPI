using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miro;

public class MiroPresenter : MonoBehaviour
{
    [SerializeField] string settingFile = ".json";

    [SerializeField] MiroREST miroREST = null;
    [SerializeField] MiroCanvas miroCanvas = null;

    // Start is called before the first frame update
    void Awake()
    {
        string path = System.IO.Path.Combine(Application.dataPath, "../../", settingFile);
        Debug.Log("path: "+ path);
        string jsonStr = FileUtils.Read(path);

        miroREST.Setting = JsonUtility.FromJson<MiroSetting>(jsonStr);
    }

    [ContextMenu("WriteSetting")]
    void WriteSetting()
    {
        string path = System.IO.Path.Combine(Application.dataPath, "../../", settingFile);
        string jsonStr = JsonUtility.ToJson(miroREST.Setting, true);
        FileUtils.Write(path, jsonStr);
    }


    void ListAllWidgets()
    {
        miroREST._ListAllWidgets((widgets) => {
            miroCanvas.Create(widgets, miroREST.Setting.boardId);
        });
    }


    [SerializeField] Rect drawRect = new Rect(10,10,100,100);
    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);
        if (GUILayout.Button("ListAllWidgets"))
        {
            ListAllWidgets();
        }
        GUILayout.EndArea();
    }
}
