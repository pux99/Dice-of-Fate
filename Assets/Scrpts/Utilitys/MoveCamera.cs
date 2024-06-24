using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private GameObject boardGame;
    [SerializeField] private GameObject diceGame;
    public BoardManager boardManager;
    public bool toBoard;
    public bool toDice;
    public UnityEvent inPositionDice;
    public UnityEvent inPositionBoard;

    void Update()
    {
        if (toDice)
        {
            if (this.transform.position != diceGame.transform.position || transform.rotation != diceGame.transform.rotation)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, diceGame.transform.position, 40 * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, diceGame.transform.rotation, 40 * Time.deltaTime);
            }
            else
            {
                toDice = false;
                inPositionDice.Invoke();
            }
        }

        if (toBoard) {
            if (this.transform.position != boardGame.transform.position || transform.rotation != boardGame.transform.rotation)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, boardGame.transform.position, 20 * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, boardGame.transform.rotation, 20 * Time.deltaTime);
            }
            else
            {
                inPositionBoard.Invoke();
                toBoard = false;
            }
        }
    }
    public void MoveToDice() { toDice = true; }
    public void MoveToBoard() {  toBoard = true; }

}
