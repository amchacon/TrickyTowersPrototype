using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public GameConfig gameConfig;
    [Header("Key Bindings")]
    public KeyBinding MoveLeft;
    public KeyBinding MoveRight;
    public KeyBinding Rotate;
    public KeyBinding Dash;

    [Header("Key Press Events")]
    public GameEvent eventMoveLeft;
    public GameEvent eventMoveRight;
    public GameEvent eventRotate;
    public GameEvent eventDash;

    void Update()
    {
        if (gameConfig.gameMode == GameConfig.GameMode.Versus) return;

        if (Input.GetKeyDown(MoveLeft.value))
        {
            eventMoveLeft.Raise();
        }

        if (Input.GetKeyDown(MoveRight.value))
        {
            eventMoveRight.Raise();
        }

        if (Input.GetKeyDown(Rotate.value))
        {
            eventRotate.Raise();
        }

        if (Input.GetKeyDown(Dash.value))
        {
            eventDash.Raise();
        }
    }
}