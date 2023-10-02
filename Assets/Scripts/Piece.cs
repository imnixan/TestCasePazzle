using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Piece : MonoBehaviour
{
    public bool connected;
    public int id;
    private Image image;
    private RectTransform rt;
    private BoxCollider2D boxCollider;
    private Hint hint;

    public void Init(int id, Sprite sprite)
    {
        this.id = id;
        image = GetComponent<Image>();
        image.sprite = sprite;
        image.SetNativeSize();
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
        image.DOColor(new Color(1, 1, 1, 0.7f), StaticData.AnimationSpeed);
        boxCollider.enabled = true;
    }

    public void SetConnected(Hint hint)
    {
        connected = true;
        this.hint = hint;
        image.DOColor(new Color(1, 1, 1, 1), StaticData.AnimationSpeed);
    }

    public void SetDisconnected()
    {
        connected = false;
        hint = null;
        image.DOColor(new Color(1, 1, 1, 0.5f), StaticData.AnimationSpeed);
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
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchorMin = rt.anchorMax;
            rt.DOAnchorPos(Vector2.zero, StaticData.AnimationSpeed);
        }
        else
        {
            transform.SetParent(slideBar);
            image.DOColor(new Color(1, 1, 1, 1), StaticData.AnimationSpeed);
        }
        rt.localScale = new Vector2(1, 1);
    }
}
