using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class SlideBar
    : MonoBehaviour,
        IPointerDownHandler,
        IDragHandler,
        IPointerUpHandler,
        IBeginDragHandler
{
    public Vector2 mousePos;
    public Vector2 newMousePos;
    public bool dragPiece;
    public Piece piece;
    private Canvas canvas;
    private Transform canvasTransform;
    private Transform contentBox;
    private ScrollRect scrollRect;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasTransform = canvas.transform;
        contentBox = GetComponentInChildren<ContentSizeFitter>().transform;
        scrollRect = GetComponent<ScrollRect>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Получаем все объекты, с которыми произошло взаимодействие (нажатие).
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            // Проверяем, является ли объект дочерним элементом ScrollView.
            if (result.gameObject.transform.IsChildOf(transform))
            {
                piece = result.gameObject.GetComponent<Piece>();
                mousePos = eventData.position;
                break;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragPiece)
        {
            piece.MoveTo(eventData.delta / canvas.scaleFactor);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        newMousePos = eventData.position;
        float xMove = Mathf.Abs(newMousePos.x - mousePos.x);
        float yMove = Mathf.Abs(newMousePos.y - mousePos.y);
        if (yMove > xMove)
        {
            dragPiece = true;
            piece.SetGrabed(canvasTransform);
            scrollRect.enabled = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragPiece = false;
        piece.SetDropped(contentBox);
        scrollRect.enabled = true;
    }
}
