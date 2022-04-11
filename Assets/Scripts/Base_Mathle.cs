using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base_Mathle : MonoBehaviour
{

    // Num array with solutions
    int [] sequence = new int[7];

    // Array for the board
    int [,] board = new int[6,7];

    private GameObject cell1, cell2, cell3;
    private Image image1, image2, image3;
    private InputField field1, field2, field3;
    private string opStr1, opStr2;


    // Start is called before the first frame update
    void Start()
    {
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

        fillBoard();
        printBoard();
        printSequence();


    }

    // Update is called once per frame
    void Update()
    {
        
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

    // HOW TO CHECK FOR VALID NUMBERS
    // string s1 = "0";
    // bool isNumber = int.TryParse(s1, out int n);
    // print(isNumber);

    public void fillBoard(){
        int num1 = Random.Range(0,7);
        int num2 = Random.Range(0,7);

        while(num2 == num1){
            num2 = Random.Range(0,7);
        }

        int num3 = Random.Range(0,7);
        while(num3 == num1 || num3 == num2){
            num3 = Random.Range(0,7);
        }

        print(num1 + ", " + num2 + ", " + num3);


        for(int i = 0; i < 6; i++){
            board[i,num1] = sequence[num1];

            //print("R" + (i+1).ToString() + "C" + num1.ToString());
            cell1 = GameObject.Find("R" + (i+1).ToString() + "C" + (num1).ToString());
            image1 = cell1.GetComponent<Image>();
            image1.color = new Color(0, 1, 0, 1);
            field1 = cell1.transform.GetChild(0).GetComponent<InputField>();
            field1.text = sequence[num1].ToString();
            field1.interactable = false;


            board[i,num2] = sequence[num2];

            //print("R" + (i+1).ToString() + "C" + num2.ToString());
            cell2 = GameObject.Find("R" + (i+1).ToString() + "C" + (num2).ToString());
            image2 = cell2.GetComponent<Image>();
            image2.color = new Color(0, 1, 0, 1);
            field2 = cell2.transform.GetChild(0).GetComponent<InputField>();
            field2.text = sequence[num2].ToString();
            field2.interactable = false;

            board[i,num3] = sequence[num3];

            //print("R" + (i+1).ToString() + "C" + num3.ToString());
            cell3 = GameObject.Find("R" + (i+1).ToString() + "C" + (num3).ToString());
            image3 = cell3.GetComponent<Image>();
            image3.color = new Color(0, 1, 0, 1);
            field3 = cell3.transform.GetChild(0).GetComponent<InputField>();
            field3.text = sequence[num3].ToString();
            field3.interactable = false;
        }
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
