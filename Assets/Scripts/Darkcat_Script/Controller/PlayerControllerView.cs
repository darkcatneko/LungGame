using Gamemanager;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerControllerView : MonoBehaviour
{
    void OnMove(InputValue inputValue)
    {
        var message = inputValue.Get<Vector2>();
        Vector2 moveDirection = new Vector2(message.x, 1f).normalized;
        GameManager.Instance.MainGameEvent.Send(new PlayerMoveCommand() {Input = moveDirection });                                                                
    }
    void OnBackToTitle()
    {
        GameManager.Instance.MainGameEvent.Send(new BackToTitleCmd());
    }

}
