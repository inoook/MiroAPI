using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[RequireComponent(typeof(RawImage))]
public class ImgLoader : MonoBehaviour
{
    [SerializeField] RawImage img = null;

    public IEnumerator LoadTexture(string path, System.Action<Texture2D> onLoadComplete = null)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.LogWarning(uwr.error + " / " + path);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                img.texture = texture;
                img.SetNativeSize();

                if (onLoadComplete != null)
                {
                    onLoadComplete(texture);
                }
            }
        }
    }

    public void Dispose()
    {
        StopAllCoroutines();
    }

}
