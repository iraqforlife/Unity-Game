using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GamePlay : MonoBehaviour
{
    public int _Player = 1; // 1 = x ; 2 = o
    public int _TurnCount = 1;
    public bool _GameStarted = false;
    public int[] _MarkedGrid = new int[9]; // to check winner
    public Button[] _Spaces = new Button[9];
    public Text _TurnDescription;
    public GameObject _GameGridPanel;
    public GameObject _GameStartPanel;
    public Dropdown _SelectPlayer;
    // Use this for initialization
    void Start()
    {
        _TurnDescription.enabled = false;
        _SelectPlayer.value = 0;
        _GameGridPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void PlayTurn(int spaceIndex)
    {
        Text space = _Spaces[spaceIndex].GetComponentInChildren(typeof(Text), true) as Text;
        _MarkedGrid[spaceIndex] = _Player;
        //disable the cell
        _Spaces[spaceIndex].interactable = false;
        _TurnCount++;
        //check if the game is finished
        if (_TurnCount > 3 && GameOver())
        {
            EditorUtility.DisplayDialog("Game Over", "Player " + _Player + " has won the game.", "Peace bro");
            reset();
        }

        if (_TurnCount > 9)
        {
            EditorUtility.DisplayDialog("Game Over", "No one has won the game.", "Peace bro");
            reset();
        }
        else
        {
            //change player
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
            UpdatePlayerDescriptionText();
        }
    }
    private bool GameOver()
    {
        //hard coded winner condition
        return
            (_Player * 3 == _MarkedGrid[0] + _MarkedGrid[1] + _MarkedGrid[2] && _MarkedGrid[2] != 0 && _MarkedGrid[1] != 0 && _MarkedGrid[0] != 0)
            || (_Player * 3 == _MarkedGrid[3] + _MarkedGrid[4] + _MarkedGrid[5] && _MarkedGrid[4] != 0 && _MarkedGrid[3] != 0 && _MarkedGrid[5] != 0)
            || (_Player * 3 == _MarkedGrid[6] + _MarkedGrid[7] + _MarkedGrid[8] && _MarkedGrid[6] != 0 && _MarkedGrid[7] != 0 && _MarkedGrid[8] != 0)
            || (_Player * 3 == _MarkedGrid[0] + _MarkedGrid[3] + _MarkedGrid[6] && _MarkedGrid[6] != 0 && _MarkedGrid[3] != 0 && _MarkedGrid[0] != 0)
            || (_Player * 3 == _MarkedGrid[1] + _MarkedGrid[4] + _MarkedGrid[7] && _MarkedGrid[1] != 0 && _MarkedGrid[4] != 0 && _MarkedGrid[7] != 0)
            || (_Player * 3 == _MarkedGrid[2] + _MarkedGrid[5] + _MarkedGrid[8] && _MarkedGrid[2] != 0 && _MarkedGrid[5] != 0 && _MarkedGrid[8] != 0)
            || (_Player * 3 == _MarkedGrid[0] + _MarkedGrid[4] + _MarkedGrid[8] && _MarkedGrid[0] != 0 && _MarkedGrid[4] != 0 && _MarkedGrid[8] != 0)
            || (_Player * 3 == _MarkedGrid[2] + _MarkedGrid[4] + _MarkedGrid[6] && _MarkedGrid[2] != 0 && _MarkedGrid[4] != 0 && _MarkedGrid[6] != 0)
            ;
    }
    private void reset()
    {
        _TurnCount = 1;
        _GameStartPanel.SetActive(true);
        _GameGridPanel.SetActive(false);
        _TurnDescription.enabled = false;
        //disable all grid
        for (int i = 0; i < _MarkedGrid.Length; i++)
            _MarkedGrid[i] = 0;
        //reset buttons text 
        for (int i = 0; i < _Spaces.Length; i++)
        {
            Text space = _Spaces[i].GetComponentInChildren(typeof(Text), true) as Text;
            space.text = string.Empty;
            _Spaces[i].interactable = true;
        }
    }
    private void UpdatePlayerDescriptionText()
    {
        _TurnDescription.text = "Player " + _Player + " turns to play";
    }
    public void StartGameButton()
    {
        _Player = _SelectPlayer.value + 1;
        reset();
        UpdatePlayerDescriptionText();
        _TurnDescription.enabled = true;
        _GameStartPanel.SetActive(false);
        _GameGridPanel.SetActive(true);

    }
}