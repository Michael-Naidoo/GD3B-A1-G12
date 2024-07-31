using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    public Sprite cursorTexture; // There is no sprite for now, can be added later.
    public Vector2 hotSpot = Vector2.zero;  // Position from the top left of the screen.
    public CursorMode cursorMode = CursorMode.Auto;  // "Whether to render this cursor as a hardware cursor on supported platforms, or force software cursor." - From UnityDocs


    private void Start()
    {
        // Sets the cursor texture, position and mode (hardware/software)/
        Cursor.SetCursor(cursorTexture.texture, hotSpot, cursorMode);
    }
}
