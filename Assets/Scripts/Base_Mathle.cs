using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Base_Mathle : MonoBehaviour
{

    // Num array with solutions
    public int [] sequence = new int[6];

    // Array for keeping track of most complete answer
    public int [] playerSequence = new int[6];

    // Array for the board
    int [,] intBoard = new int[6,6];

    [SerializeField]
    public GameObject [] Row0, Row1, Row2, Row3, Row4, Row5;
    public GameObject[][] BOARD = new GameObject[6][];
    

    private GameObject cell1, cell2, cell3;
    private Image image1, image2, image3;
    private InputField field1, field2, field3;
    private string opStr1, opStr2;
    public int currentRow = 0;     // keep track of what row player is on
    EventSystem system;
    public Button checkSol;
    string currentDate; // stores current date as string

    //Error Message
    public Color targetColor = new Color(1, 1, 1, 0);
    public Image ImageToFade;

    //Animations
    private GameObject Col0, Col1, Col2, Col3, Col4, Col5;
    private Animator col0, col1, col2, col3, col4, col5;

    // Start is called before the first frame update
    void Start()
    {
        
        currentDate = System.DateTime.Now.ToString("yyyyMMdd");

        system = EventSystem.current;
        checkSol.onClick.AddListener(checkSolPressed);
        // check date/time, turn into int, pass into createSequence as parameter
        int date = System.Int32.Parse(System.DateTime.Now.ToString("yyyyMMdd"));
        createSequence(date);
        while (sequence[0] == sequence[1]) {
            createSequence(date);
        }

        print("Sequence.length = " + sequence.Length);

        fillFirstRow();
        printBoard();
        printSequence();

        // fillBoard();
        // print(BOARD[0][0]);

        
    }

    // Update is called once per frame
    void Update()
    {
        string updatedDate = System.DateTime.Now.ToString("yyyyMMdd");
        if (currentDate != updatedDate) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void createSequence(int dateSeed) {
        // 0 add, 1 sub, 2 mult
        // sequence op num2 op num3
        
        Random.InitState(dateSeed);
        int op1 = Random.Range(0,3);
        int op2 = Random.Range(0,3);

        if (op1 == 2 && op2 == 2) {
            op1 = Random.Range(0,2);
        }

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
    }
    public enum SolutionCode : int
        {
            Invalid = 0,    // not all cells are filled
            Continue = 1,   // incorrect answer, not last row
            WinGame = 2,    // correct answer
            LoseGame = 3    // on last row, incorrect answer
        };
    void checkSolPressed() {

        // store text information into array
        for (int i = 0; i < sequence.Length; i++) {
            // print(GameObject.Find("R" + (currentRow).ToString() + "C" + (i).ToString()).GetComponentInChildren<InputField>().text);
            //print(GameObject.Find("R" + (currentRow).ToString() + "C" + (i).ToString()).GetComponentInChildren<InputField>().text == "");
            if (currentRow < 6){
                GameObject thisCell = GameObject.Find("R" + (currentRow).ToString() + "C" + (i).ToString());
                intBoard[currentRow, i] = (thisCell.GetComponentInChildren<InputField>().text != "" && thisCell.GetComponentInChildren<InputField>().text != "-") ? System.Int32.Parse(thisCell.GetComponentInChildren<InputField>().text) : 0;
            }
        }




        switch (checkSolution()) {
            case SolutionCode.Invalid:
                print("row is not fully filled");
                //Error Message
                ImageToFade.color = new Color(1, 1, 1, 1);
                PlayAnimation();
                StartCoroutine(LerpFunction(targetColor, 1.5f));

                // play noise?
                // animation?
                // popup?
                break;
            case SolutionCode.Continue:
                print("continuing game");
                checkRow();
                nextRow();
                break;
            case SolutionCode.WinGame:
                checkRow();
                print("you win");
                break;
            case SolutionCode.LoseGame:
                checkRow();
                print("you lose");
                break;
        }
    }

    void PlayAnimation(){
        Col0 = GameObject.Find("R" + (currentRow).ToString() + "C0");
        Col1 = GameObject.Find("R" + (currentRow).ToString() + "C1");
        Col2 = GameObject.Find("R" + (currentRow).ToString() + "C2");
        Col3 = GameObject.Find("R" + (currentRow).ToString() + "C3");
        Col4 = GameObject.Find("R" + (currentRow).ToString() + "C4");
        Col5 = GameObject.Find("R" + (currentRow).ToString() + "C5");

        col0 = Col0.GetComponent<Animator>();
        col0.SetBool("Error", true);
        col0.Play("CellShake");

        col1 = Col1.GetComponent<Animator>();
        col1.SetBool("Error", true);
        col1.Play("CellShake");

        col2 = Col2.GetComponent<Animator>();
        col2.SetBool("Error", true);
        col2.Play("CellShake");

        col3 = Col3.GetComponent<Animator>();
        col3.SetBool("Error", true);
        col3.Play("CellShake");

        col4 = Col4.GetComponent<Animator>();
        col4.SetBool("Error", true);
        col4.Play("CellShake");

        col5 = Col5.GetComponent<Animator>();
        col5.SetBool("Error", true);
        col5.Play("CellShake");



        StartCoroutine(Wait(col0));
        StartCoroutine(Wait(col1));
        StartCoroutine(Wait(col2));
        StartCoroutine(Wait(col3));
        StartCoroutine(Wait(col4));
        StartCoroutine(Wait(col5));
        

        
        
        

    }

   IEnumerator Wait(Animator col)
   {
       yield return new WaitForSeconds(.5f);
       col.SetBool("Error", false);
   }

    public SolutionCode checkSolution() {

        if (!rowFilled()) {
            return SolutionCode.Invalid;
        }

        int [] currentSequence = new int[sequence.Length];
        for (int i = 0; i < sequence.Length; i++) {
            currentSequence[i] = intBoard[currentRow, i];
        }
        if (Enumerable.SequenceEqual(sequence, currentSequence)) {
            return SolutionCode.WinGame;
        }

        if (currentRow >= 5) {
            return SolutionCode.LoseGame;
        }

        return SolutionCode.Continue;

    }

    // function to check whether every cell in a row has text in it
    bool rowFilled() {
        if (currentRow < 6){
            for (int i = 0; i < sequence.Length; i++) {
                if (intBoard[currentRow, i] == 0 && GameObject.Find("R" + (currentRow).ToString() + "C" + (i).ToString()).GetComponentInChildren<InputField>().text != "0") {
                    return false;
                }
            }
            return true;
        } else{
            Debug.Log("Game Over");
            return false;
        }
    }

    void checkRow() {
        for (int i = 0; i < sequence.Length; i++) {
            StartCoroutine(isNumCorrect(currentRow, i));
        }

    }

    void nextRow() {
        currentRow++;
        if (currentRow < 6){
            fillRow();  
        }
              
    }

    bool isCorrect(int col) {
        return GameObject.Find("R" + (currentRow).ToString() + "C" + (col).ToString()).GetComponentInChildren<InputField>().text == sequence[col].ToString();
    }

    IEnumerator isNumCorrect(int row, int col) {
        GameObject thisCell = GameObject.Find("R" + (row).ToString() + "C" + (col).ToString());
        image1 = thisCell.GetComponent<Image>();
        field1 = thisCell.transform.GetChild(0).GetComponent<InputField>();

        // if number is correct, make green and store into player sequence object
        if (intBoard[row,col] == sequence[col]) {
            // do flip animation?

            // change color to green
            image1.color = Color.black;
            field1.image.color = Color.green;
            field1.text = sequence[col].ToString();

        } else if (intBoard[row,col] < sequence[col]) {
            // do flip animation?

            // change color to red
            image1.color = Color.black;
            field1.image.color = Color.red;
            field1.text = intBoard[row,col].ToString();

        } else {
            // do flip animation?

            // change color to blue
            image1.color = Color.black;
            field1.image.color = Color.blue;
            field1.text = intBoard[row,col].ToString();

        }
        field1.interactable = false;
        yield return new WaitForSeconds(.2f);
    }

    public void fillBoard() {
        for (int i = 0; i < sequence.Length; i++) {
            for (int j = 0; j < 6; j++) {
                switch(i) {
                    case 0:
                        BOARD[i][j] = Row0[j];
                        break;
                    case 1:
                        BOARD[i][j] = Row1[j];
                        break;
                    case 2:
                        BOARD[i][j] = Row2[j];
                        break;
                    case 3:
                        BOARD[i][j] = Row3[j];
                        break;
                    case 4:
                        BOARD[i][j] = Row4[j];
                        break;
                    case 5:
                        BOARD[i][j] = Row5[j];
                        break;
                }
            }
        }
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

        for (int i = 1; i < sequence.Length; i++) {

            switch(op1) {
                case 0:
                    sequence[i] = sequence[i-1] + num2;
                    break;
                case 1:
                    sequence[i] = sequence[i-1] - num2;
                    break;
                case 2:
                    sequence[i] = sequence[i-1] * num2;
                    break;
            }

            switch(op2) {
                case 0:
                    sequence[i] = sequence[i] + num3;
                    break;
                case 1:
                    sequence[i] = sequence[i] - num3;
                    break;
                case 2:
                    sequence[i] = sequence[i] * num3;
                    break;
            }

        }
        return;
    }

    public void fillRow() {
        for (int i = 0; i < sequence.Length; i++) {
            GameObject thisCell = GameObject.Find("R" + (currentRow).ToString() + "C" + (i).ToString());
            GameObject prevCell = GameObject.Find("R" + (currentRow - 1).ToString() + "C" + (i).ToString());

            // if (prevCell.GetComponent<Image>().color == Color.green) {
            //     thisCell.GetComponent<Image>().color = prevCell.GetComponent<Image>().color;
            //     thisCell.GetComponentInChildren<InputField>().text = prevCell.GetComponentInChildren<InputField>().text;
            // } else {
            //     thisCell.GetComponentInChildren<InputField>().interactable = true;
            // }

            if (prevCell.GetComponentInChildren<InputField>().image.color == Color.green) {
                thisCell.GetComponentInChildren<InputField>().image.color = prevCell.GetComponentInChildren<InputField>().image.color;
                thisCell.GetComponentInChildren<InputField>().text = prevCell.GetComponentInChildren<InputField>().text;
            } else {
                thisCell.GetComponentInChildren<InputField>().interactable = true;
            }


        }
    }

    public void fillFirstRow(){
        int num1 = Random.Range(0,sequence.Length);
        int num2 = Random.Range(0,sequence.Length);

        while(num2 == num1){
            num2 = Random.Range(0,sequence.Length);
        }

        playerSequence[num1] = sequence[num1];
        playerSequence[num2] = sequence[num2];

        print(num1 + ", " + num2);


        // changed to only first row
        intBoard[0,num1] = sequence[num1];

        //print("R" + (i+1).ToString() + "C" + num1.ToString());
        cell1 = GameObject.Find("R0C" + (num1).ToString());
        image1 = cell1.GetComponent<Image>();
        image1.color = new Color(0, 0, 0, 0);
        field1 = cell1.transform.GetChild(0).GetComponent<InputField>();
        field1.text = sequence[num1].ToString();
        field1.image.color = new Color (0, 1, 0, 1);
        field1.interactable = false;


        intBoard[0,num2] = sequence[num2];

        //print("R" + (i+1).ToString() + "C" + num2.ToString());
        cell2 = GameObject.Find("R0C" + (num2).ToString());
        image2 = cell2.GetComponent<Image>();
        image2.color = new Color(0, 0, 0, 0);
        field2 = cell2.transform.GetChild(0).GetComponent<InputField>();
        field2.text = sequence[num2].ToString();
        field2.image.color = new Color (0, 1, 0, 1);
        field2.interactable = false;
    }

    public void printBoard(){
    
        for(int i = 0; i < 6; i++){
            string str = "";
            for(int j = 0; j < 6; j++){
                str += intBoard[i,j] + " ";
            }
            Debug.Log(str);
        }
    }

    public void printSequence(){
        string str = "";
        for(int j = 0; j < sequence.Length; j++){
            str += sequence[j] + " ";
        }
        Debug.Log(str);
    }

    //Error Message Lerp
    IEnumerator LerpFunction(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = ImageToFade.color;
        while (time < duration)
        {
            ImageToFade.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        ImageToFade.color = endValue;
    }
}
