using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField]
    private int id;

    private HintsManager hintsManager;

    [SerializeField]
    private Piece piece;

    private BoxCollider2D boxCollider;

    public void Init(int id, HintsManager hintsManager)
    {
        this.id = id;
        this.hintsManager = hintsManager;
        RectTransform rt = GetComponent<RectTransform>();
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
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
        float distance = Vector2.Distance(collision.transform.position, transform.position);
        if (distance < 2 && piece.id == id)
        {
            piece.SetConnected(this);
        }
        else if (piece.connected)
        {
            piece.SetDisconnected();
        }
    }

    public void FinishConnect()
    {
        hintsManager.ShowNextHint(id);
        GetComponent<Image>().enabled = false;
        boxCollider.enabled = false;
    }
}
