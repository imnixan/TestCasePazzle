using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour
{
    public int id;
    private Image image;
    private RectTransform rt;
    private bool connected;
    private BoxCollider2D boxCollider;
    private Hint hint;

    public void Init(int id, Sprite sprite)
    {
        this.id = id;
        image = GetComponent<Image>();
        image.sprite = sprite;
        rt = GetComponent<RectTransform>();
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(image.mainTexture.width, image.mainTexture.height);
        boxCollider.enabled = false;
    }

    public void SetGrabed(Transform canvasTransform)
    {
        connected = false;
        image.SetNativeSize();
        transform.SetParent(canvasTransform);
        //boxCollider.size = rt.sizeDelta / 2;
        boxCollider.enabled = true;
    }

    public void SetConnected(Hint hint)
    {
        connected = true;
        this.hint = hint;
    }

    public void SetDisconnected()
    {
        connected = false;
        hint = null;
    }

    public void MoveTo(Vector2 pos)
    {
        rt.anchoredPosition += pos;
    }

    public void SetDropped(Transform slideBar)
    {
        boxCollider.enabled = false;
        if (connected)
        {
            transform.SetParent(hint.transform);
            hint.FinishConnect();
            rt.anchoredPosition = Vector2.zero;
        }
        else
        {
            transform.SetParent(slideBar);
        }
    }
}
