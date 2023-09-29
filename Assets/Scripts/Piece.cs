using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    private bool dragged;
    private Vector2 startPos;
    private RectTransform rt;
    private Transform t;
    private Image image;
    private Vector2 mousePos;
    private Camera camera;
    private float minYPos;

    private void Start()
    {
        image = GetComponent<Image>();
        rt = GetComponent<RectTransform>();
        camera = Camera.main;
        t = transform;
        minYPos = t.position.y + 2;
    }

    private void OnMouseDown()
    {
        startPos = rt.anchoredPosition;
        dragged = true;
        image.color = Color.red;
    }

    private void OnMouseUp()
    {
        image.color = Color.white;
        dragged = false;
        rt.anchoredPosition = startPos;
    }

    private void Update()
    {
        if (dragged)
        {
            mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.y >= minYPos)
            {
                t.position = mousePos;
            }
            else
            {
                rt.anchoredPosition = startPos;
            }
        }
    }
}
