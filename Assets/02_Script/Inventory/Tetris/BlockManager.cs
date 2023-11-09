using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager Instance;

    private TetrisImg _selectBlock;             //«ˆ¿Á º±≈√µ» ∫Ì∑œ

    public TetrisImg[] test;

    private void Awake()
    {
        #region ΩÃ±€≈Ê
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(transform);
            Debug.LogError($"{transform} : BlockManager is Multiply running!");
        }
        #endregion
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateBlock(test[Random.Range(0,test.Length - 1)]);
        }

        MoveBlock();
    }

    public void MoveBlock()
    {
        if(_selectBlock != null)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {

            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {

            }
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                _selectBlock.RotateImage();
            }
        }
    }

    public void CreateBlock(TetrisImg tetrisImg)
    {
        //_selectBlock = Instantiate(tetrisImg, transform);
    }
}