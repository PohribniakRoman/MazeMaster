using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;



public class MazeCell : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private GameObject _frontWall;

    [SerializeField]
    private GameObject _backWall;

    [SerializeField]
    private GameObject _unvisitedBlock;
    [SerializeField]
    private GameObject _trapBlock;
    [SerializeField]
    private GameObject _exitBlock;
    private List<char> activeWalls = new List<char> { 'r', 'l', 'f', 'b' };
    public bool IsVisited { get; private set; }

    public void PlaceExit()
    {
        GameObject current = null;
        Vector3 positionJustify = new Vector3(-0.1f, 0, 0);
        Vector3 rotationJustify = new Vector3(0, 90, 0);
        switch (activeWalls[0])
        {
            case 'r':
                current = _rightWall;
                break;
            case 'l':
                current = _leftWall;
                positionJustify = new Vector3(0.1f, 0, 0);
                break;
            case 'b':
                current = _backWall;
                positionJustify = new Vector3(0, 0, 0.1f);
                rotationJustify = new Vector3(0, 0, 0);
                break;
            case 'f':
                current = _frontWall;
                rotationJustify = new Vector3(0, 0, 0);
                positionJustify = new Vector3(0, 0, -0.1f);
                break;
        }
        if (current)
        {
            Instantiate(_exitBlock, current.transform.position + positionJustify, Quaternion.Euler(rotationJustify));
        }
    }

    public void Visit()
    {
        IsVisited = true;
        _unvisitedBlock.SetActive(false);
        TryRemoveTrap();

    }

    public void TryRemoveTrap()
    {
        if (_trapBlock != null)
        {
            float chance = Random.Range(0f, 1f);
            if (chance <= 0.9f)
            {
                Destroy(_trapBlock);
            }
        }
    }

    void FilterActiveWalls(char side)
    {
        activeWalls = activeWalls.Where((obj) => obj != side).ToList();
    }

    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
        FilterActiveWalls('l');
    }

    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
        FilterActiveWalls('r');
    }

    public void ClearFrontWall()
    {
        _frontWall.SetActive(false);
        FilterActiveWalls('f');
    }

    public void ClearBackWall()
    {
        _backWall.SetActive(false);
        FilterActiveWalls('b');
    }
}

