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
    private Vector2 sizeInScroll,
        nativeSize;

    public void Init(int id, Sprite sprite, float contentBoxHeight)
    {
        this.id = id;
        image = GetComponent<Image>();
        image.sprite = sprite;
        image.SetNativeSize();
        rt = GetComponent<RectTransform>();

        nativeSize = rt.sizeDelta;
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = nativeSize;
        boxCollider.enabled = false;

        float newHeight = Mathf.Min(contentBoxHeight, nativeSize.y);
        float scale = newHeight / nativeSize.y;
        // Применяем масштабирование
        sizeInScroll = new Vector2(nativeSize.x * scale, newHeight);
    }

    public void SetGrabed(Transform canvasTransform)
    {
        connected = false;
        image.SetNativeSize();
        transform.SetParent(canvasTransform);
        image.DOColor(new Color(1, 1, 1, 0.7f), StaticData.AnimationLength);
        rt.DOSizeDelta(nativeSize, StaticData.AnimationLength);
        boxCollider.enabled = true;
    }

    public void SetConnected(Hint hint)
    {
        connected = true;
        this.hint = hint;
        image.DOColor(new Color(1, 1, 1, 1), StaticData.AnimationLength);
    }

    public void SetDisconnected()
    {
        connected = false;
        hint = null;
        image.DOColor(new Color(1, 1, 1, 0.5f), StaticData.AnimationLength);
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
            rt.DOAnchorPos(Vector2.zero, StaticData.AnimationLength);
        }
        else
        {
            transform.SetParent(slideBar);
            image.DOColor(new Color(1, 1, 1, 1), StaticData.AnimationLength);
            rt.DOSizeDelta(sizeInScroll, StaticData.AnimationLength);
        }
        rt.localScale = new Vector2(1, 1);
    }
}
