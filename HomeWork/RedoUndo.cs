using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedoUndoComponents : MonoBehaviour
{  
    [NonSerialized]
    public float Speed = 3.0f;

    [SerializeField]
    public float Speed2 = 3.0f;
    
    private Stack<Vector3> position_stack = new Stack<Vector3>(); 
    private Stack<Vector3> redo_stack = new Stack<Vector3>();
    void Update()
    {
        Movements();
        Undo();
        Redo();
    }

    void Movements()
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
            position_stack.Push(transform.position);
            redo_stack.Clear();
        }
        
        transform.position += movePos.normalized * (Speed2 * Time.deltaTime);
    }

    void Undo()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (position_stack.TryPop(out Vector3 newPosition))
            {
                redo_stack.Push(transform.position);
                transform.position = newPosition;
            }
        }
    }

    void Redo()
    {
         if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R)) 
         {
             if (redo_stack.TryPop(out Vector3 newPosition))
             {
                 position_stack.Push(transform.position);
                 transform.position = newPosition;
             }
         }
    }
    
}