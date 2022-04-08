using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Mathle : MonoBehaviour
{

    // Num array with solutions
    int [] sequence = new int[7];

    // Array for the board
    int [,] board = new int[6,7];

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
        Debug.Log("formula: "+ num1 + " " + op1 + " " + num2 + " " +  op2 + " " +  num3);

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

    public void fillBoard(){
        int num1 = Random.Range(0,8);
        int num2 = Random.Range(0,8);

        while(num2 == num1){
            num2 = Random.Range(0,8);
        }

        int num3 = Random.Range(0,8);
        while(num3 == num1 || num3 == num2){
            num3 = Random.Range(0,8);
        }


        for(int i = 0; i < 6; i++){
            board[i,num1] = sequence[num1];
            board[i,num2] = sequence[num2];
            board[i,num3] = sequence[num3];
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
