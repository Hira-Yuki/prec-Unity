using System.Collections.Generic;
using UnityEngine;

public class RedoUndoComponents : MonoBehaviour
{  
    private float speed = 3.0f;
    
    private readonly Stack<Vector3> _positionStack = new Stack<Vector3>(); 
    private readonly Stack<Vector3> _redoStack = new Stack<Vector3>();
    
    private void Update()
    {
        Movements();
        Undo();
        Redo();
    }

    private void Movements()
    {
        Vector3 movePos = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movePos += transform.forward;
        
        if (Input.GetKey(KeyCode.S))
            movePos -= transform.forward;
        
        if (Input.GetKey(KeyCode.A))
            movePos -= transform.right;
        
        if (Input.GetKey(KeyCode.D))
            movePos += transform.right;
        
        if (Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.D))
        {
            movePos = Vector3.zero;
            _positionStack.Push(transform.position);
            _redoStack.Clear();
            Debug.Log("새로운 언두 스택 저장 -> \n 리두 스택 리셋");
            Debug.Log($"언두에 저장된 스택 수: {_positionStack.Count}");
            Debug.Log($"리두에 저장된 스택 수: {_redoStack.Count}");
        }
        
        transform.position += movePos.normalized * (speed * Time.deltaTime);
    }

    private void Undo()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (!_positionStack.TryPop(out Vector3 newPosition)) return;
        _redoStack.Push(transform.position);
        transform.position = newPosition;
        Debug.Log("언두 실행 및 새로운 리두 스택 추가");
        Debug.Log($"언두에 저장된 스택 수: {_positionStack.Count}");
        Debug.Log($"리두에 저장된 스택 수: {_redoStack.Count}");
    }

    private void Redo()
    {
        if (!Input.GetKey(KeyCode.LeftControl) || !Input.GetKeyDown(KeyCode.R)) return;
        if (!_redoStack.TryPop(out Vector3 newPosition)) return;
        _positionStack.Push(transform.position);
        transform.position = newPosition;
        Debug.Log("리두 실행 및 새로운 언두 스택 추가");
        Debug.Log($"언두에 저장된 스택 수: {_positionStack.Count}");
        Debug.Log($"리두에 저장된 스택 수: {_redoStack.Count}");
    }
    
}
