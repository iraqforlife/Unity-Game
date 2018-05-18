using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public int _Player = 1; // 1 = x ; 2 = o
    public int _TurnCount = 0;
    public bool _GameStarted = false;
    public int[] _MarkedGrid = new int[9]; // to check winner
    public Button[] _Spaces = new Button[9];
    // Use this for initialization
    void Start()
    {
        //disable all grid
        for (int i = 0; i < _MarkedGrid.Length; i++)
            _MarkedGrid[i] = 0;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void playTurn(int spaceIndex)
    {
        Text space=_Spaces[spaceIndex].GetComponentInChildren(typeof(Text), true) as Text;
        _MarkedGrid[spaceIndex] = _Player;
        if (_Player == 1)
        {
            _Player = 2;
            space.text = "X";
        }
        else
        {
            _Player = 1;
            space.text = "O";
        }
       _Spaces[spaceIndex].interactable = false;
        _TurnCount++;
    }
}