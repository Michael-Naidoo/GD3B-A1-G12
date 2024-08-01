using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorLock : MonoBehaviour
{
    [SerializeField] private BoxCollider2D restrictedZone;


    private void Awake()
    {
        // Hides the cursor on Awake.
        Cursor.visible = false;
    }
    private void Update()
    {
        // Gets cursor postition.
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
        cursorPosition.x = Mathf.Clamp(cursorPosition.x, restrictedZone.bounds.min.x, restrictedZone.bounds.max.x);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, restrictedZone.bounds.min.y, restrictedZone.bounds.max.y);

        // Makes the player postition the cursor postition (essentially moving the player with the cursor).
        transform.position = cursorPosition;
    }
}
