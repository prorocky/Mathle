using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Base_Mathle : MonoBehaviour
{

    // Num array with solutions
    int [] sequence = new int[7];

    // Array for keeping track of most complete answer
    int [] playerSequence = new int[7];

    // Array for the board
    int [,] board = new int[6,7];

    private GameObject cell1, cell2, cell3;
    private Image image1, image2, image3;
    private InputField field1, field2, field3;
    private string opStr1, opStr2;
    private int currentRow = 0;     // keep track of what row player is on
    EventSystem system;


    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
        // 0 add, 1 sub, 2 mult
        // sequence op num2 op num3
        int op1 = Random.Range(0,3);
        int op2 = Random.Range(0,3);

        int num1 = Random.Range(0,10);
        int num2, num3;

        switch (op1) {
            case 2:
                num2 = Random.Range(1, 4);
                break;
            default:
                num2 = Random.Range(0, 10);
                break;
        }

        switch (op2) {
            case 2:
                num3 = Random.Range(1, 4);
                break;
            default:
                num3 = Random.Range(0, 10);
                break;
        }

        getSolution(op1,op2,num1,num2,num3);

        fillFirstRow();
        printBoard();
        printSequence();

        string str = "";
        for(int j = 0; j < 7; j++){
            str += playerSequence[j] + " ";
        }
        Debug.Log(str);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void testCell() {
        GameObject row1 = GameObject.Find("Row" + (1).ToString());

        
    }

    public enum SolutionCode : int
    {
        GameOver = 1,
        Continue = 2
    };
    public SolutionCode checkSolution() {

        if (Enumerable.SequenceEqual(sequence, playerSequence)) {
            // function / code to end game here
            return SolutionCode.GameOver;
        }

        // check current sequence and solution one by one
        for (int i = 0; i < 7; i++) {
            isNumCorrect(currentRow, i);
        }
        return SolutionCode.Continue;

    }

    IEnumerator isNumCorrect(int row, int col) {
        GameObject thisCell = GameObject.Find("R" + (row).ToString() + "C" + (col).ToString());
        image1 = thisCell.GetComponent<Image>();
        field1 = thisCell.transform.GetChild(0).GetComponent<InputField>();

        // if number is correct, make green and store into player sequence object
        if (board[row,col] == sequence[col]) {
            // do flip animation?

            // change color to green
            image1.color = new Color(0, 1, 0, 1);
            field1.text = sequence[col].ToString();

        } else if (board[row,col] < sequence[col]) {
            // do flip animation?

            // change color to red
            image1.color = Color.red;
            field1.text = board[row,col].ToString();

        } else {
            // do flip animation?

            // change color to blue
            image1.color = Color.blue;
            field1.text = board[row,col].ToString();

        }
        field1.interactable = false;
        yield return new WaitForSeconds(1);
    }

    public void getSolution(int op1, int op2, int num1, int num2, int num3){
        sequence[0] = num1;

        switch(op1){
            case 0:
                opStr1 = "+";
                break;
            case 1:
                opStr1 = "-";
                break;
            case 2:
                opStr1 = "*";
                break;
        }

        switch(op2){
            case 0:
                opStr2 = "+";
                break;
            case 1:
                opStr2 = "-";
                break;
            case 2:
                opStr2 = "*";
                break;
        }

        Debug.Log("formula: "+ "num" + " " + opStr1 + " " + num2 + " " +  opStr2 + " " +  num3);


        switch(op1) {
            case 0:
                switch(op2) {
                    case 0:
                        for (int i = 1; i < 7; i++) {
                            sequence[i] = (sequence[i-1] + num2) + num3;
                        }
                        break;
                    case 1:
                        for (int i = 1; i < 7; i++) {
                            sequence[i] = (sequence[i-1] + num2) - num3;
                        }
                        break;
                    case 2:
                        for (int i = 1; i < 7; i++) {
                            sequence[i] = (sequence[i-1] + num2) * num3;
                        }
                        break;
                }
            break;

            case 1:
                switch(op2) {
                    case 0:
                        for (int i = 1; i < 7; i++) {
                            sequence[i] = (sequence[i-1] - num2) + num3;
                        }
                        break;
                    case 1:
                        for (int i = 1; i < 7; i++) {
                            sequence[i] = (sequence[i-1] - num2) - num3;
                        }
                        break;
                    case 2:
                        for (int i = 1; i < 7; i++) {
                            sequence[i] = (sequence[i-1] - num2) * num3;
                        }
                        break;
                }
            break;

            case 2:
                switch(op2) {
                    case 0:
                        for (int i = 1; i < 7; i++) {
                            sequence[i] = (sequence[i-1] * num2) + num3;
                        }
                        break;
                    case 1:
                        for (int i = 1; i < 7; i++) {
                            sequence[i] = (sequence[i-1] * num2) - num3;
                        }
                        break;
                    case 2:
                        for (int i = 1; i < 7; i++) {
                            sequence[i] = (sequence[i-1] * num2) * num3;
                        }
                        break;
                }
            break;
        }

        return;
    }

    public void fillRow() {
        for (int i = 0; i < 7; i++) {
            GameObject thisCell = GameObject.Find("R" + (currentRow).ToString() + "C" + (i).ToString());
            GameObject prevCell = GameObject.Find("R" + (currentRow - 1).ToString() + "C" + (i).ToString());

            thisCell.GetComponent<Image>().color = prevCell.GetComponent<Image>().color;
            thisCell.GetComponentInChildren<InputField>().text = prevCell.GetComponentInChildren<InputField>().text;
            if (thisCell.GetComponent<Image>().color != Color.green) {
                thisCell.GetComponentInChildren<InputField>().interactable = true;
            }
        }
    }

    public void fillFirstRow(){
        int num1 = Random.Range(0,7);
        int num2 = Random.Range(0,7);

        while(num2 == num1){
            num2 = Random.Range(0,7);
        }

        int num3 = Random.Range(0,7);
        while(num3 == num1 || num3 == num2){
            num3 = Random.Range(0,7);
        }

        playerSequence[num1] = sequence[num1];
        playerSequence[num2] = sequence[num2];
        playerSequence[num3] = sequence[num3];

        print(num1 + ", " + num2 + ", " + num3);


        // changed to only first row
        board[0,num1] = sequence[num1];

        //print("R" + (i+1).ToString() + "C" + num1.ToString());
        cell1 = GameObject.Find("R0C" + (num1).ToString());
        image1 = cell1.GetComponent<Image>();
        image1.color = new Color(0, 1, 0, 1);
        field1 = cell1.transform.GetChild(0).GetComponent<InputField>();
        field1.text = sequence[num1].ToString();
        field1.interactable = false;


        board[0,num2] = sequence[num2];

        //print("R" + (i+1).ToString() + "C" + num2.ToString());
        cell2 = GameObject.Find("R0C" + (num2).ToString());
        image2 = cell2.GetComponent<Image>();
        image2.color = new Color(0, 1, 0, 1);
        field2 = cell2.transform.GetChild(0).GetComponent<InputField>();
        field2.text = sequence[num2].ToString();
        field2.interactable = false;

        board[0,num3] = sequence[num3];

        //print("R" + (i+1).ToString() + "C" + num3.ToString());
        cell3 = GameObject.Find("R0C" + (num3).ToString());
        image3 = cell3.GetComponent<Image>();
        image3.color = new Color(0, 1, 0, 1);
        field3 = cell3.transform.GetChild(0).GetComponent<InputField>();
        field3.text = sequence[num3].ToString();
        field3.interactable = false;
    }

    public void printBoard(){
    
        for(int i = 0; i < 6; i++){
            string str = "";
            for(int j = 0; j < 7; j++){
                str += board[i,j] + " ";
            }
            Debug.Log(str);
        }
    }

    public void printSequence(){
        string str = "";
        for(int j = 0; j < 7; j++){
            str += sequence[j] + " ";
        }
        Debug.Log(str);
    }
}
