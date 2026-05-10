using UnityEngine;

public class DirectionalBillboardSprite : MonoBehaviour
{
    [Header("References")]
    public Transform characterRoot;
    public Transform spritePivot;
    public SpriteRenderer spriteRenderer;

    [Header("Sprites")]
    public Sprite frontSprite;
    public Sprite halfTurnSprite;
    public Sprite sideSprite;
    public Sprite backSprite;

    [Header("Camera")]
    public Transform playerCamera;

    void Start()
    {
        if (playerCamera == null && Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }

        if (characterRoot == null)
        {
            characterRoot = transform;
        }

        if (spritePivot == null)
        {
            spritePivot = spriteRenderer.transform;
        }
    }

    void LateUpdate()
    {
        if (playerCamera == null || spriteRenderer == null)
            return;

        //-------------------------------------------------
        // 1. BILLBOARD ONLY THE SPRITE
        //-------------------------------------------------

        Vector3 camForward = playerCamera.forward;
        camForward.y = 0f;

        spritePivot.forward = camForward;

        //-------------------------------------------------
        // 2. DETERMINE VIEW ANGLE
        //-------------------------------------------------

        Vector3 toCamera =
            playerCamera.position - characterRoot.position;

        toCamera.y = 0f;
        toCamera.Normalize();

        Vector3 npcForward = characterRoot.forward;
        npcForward.y = 0f;
        npcForward.Normalize();

        float angle = Vector3.SignedAngle(
            npcForward,
            toCamera,
            Vector3.up
        );

        float absAngle = Mathf.Abs(angle);

        // LEFT / RIGHT detection
        bool leftSide = angle < 0f;

        //-------------------------------------------------
        // 3. SELECT SPRITE
        //-------------------------------------------------

        // FRONT
        if (absAngle <= 45f)
        {
            spriteRenderer.sprite = frontSprite;
            spriteRenderer.flipX = false;
        }
        // HALF TURN
        else if (absAngle <= 85f)
        {
            spriteRenderer.sprite = halfTurnSprite;

            // FLIPPED
            spriteRenderer.flipX = !leftSide;
        }
        // SIDE
        else if (absAngle <= 160f)
        {
            spriteRenderer.sprite = sideSprite;

            // FLIPPED
            spriteRenderer.flipX = !leftSide;
        }
        // BACK
        else
        {
            spriteRenderer.sprite = backSprite;
            spriteRenderer.flipX = false;
        }
    }
}