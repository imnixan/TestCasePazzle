using System.Collections;
using UnityEngine;

public class Hint : MonoBehaviour
{
    private int id;
    private HintsManager hintsManager;
    private Piece piece;

    public void Init(int id, HintsManager hintsManager)
    {
        this.id = id;
        this.hintsManager = hintsManager;
        RectTransform rt = GetComponent<RectTransform>();
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.size = rt.sizeDelta;
        boxCollider.isTrigger = true;

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        piece = collision.gameObject.GetComponent<Piece>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        piece = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Vector2.Distance(collision.transform.position, transform.position) < 2)
        {
            piece.SetConnected(this);
        }
        piece.SetDisconnected();
    }

    public void FinishConnect()
    {
        hintsManager.ShowNextHint(id);
    }
}
