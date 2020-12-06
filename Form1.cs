using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Calculator
{
    public partial class Form1 : Form
    {
        // Our global variables
        Double resultValue = 0;
        String previousValues = ""; // string to show previous values
        String operationPerformed = "";
        bool isOperationPerformed = false; // confirms if an operation has changed the result value
        bool isEqualPerformed = false; // this will allow us to confirm if a final result has been produced

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonClick(object sender, EventArgs e)
        {
            if (textBox_Result.Text == "0" || isOperationPerformed || isEqualPerformed)
            {
                // clears the starting 0 and any number after an operator (or equal) has been pressed
                if (isEqualPerformed) // clears the saved result if equal has already been pressed as an operator click will be the next logical choice
                {
                    resultValue = 0;
                }
                textBox_Result.Clear();
                //previousValues = "";
            }
            // sender does not containg the parameter for text so we need to cast sender as Button object
            Button button = (Button)sender;
            if (button.Text == ".")
            {
                // limiting the number of full dots to 1 for a decimal dot
                if (!textBox_Result.Text.Contains(".") && !resultValue.ToString().Contains("."))
                {
                    textBox_Result.Text = resultValue.ToString() + button.Text; // this way the current total can be set to a decimal even if it's 0
                } else
                {
                    textBox_Result.Text = "0.";
                }

            } 
            else
            {
                textBox_Result.Text = textBox_Result.Text + button.Text;
            }
            
            isOperationPerformed = false;
            isEqualPerformed = false;
            
        }

        private void operatorClick(object sender, EventArgs e)
        {
            // for the +, - , * , / Operators

            Button button = (Button)sender;
            if (!isOperationPerformed)
            {
                if (isEqualPerformed)
                {
                    previousValues = ""; // clears the current value to allow the concatenation to work
                    isEqualPerformed = false;
                }
                previousValues += textBox_Result.Text;
                operationPerformed = button.Text;
                textBox_Result.Text = Calculate(operationPerformed); // this will allow us to view the result as we click numbers  
                resultValue = Double.Parse(textBox_Result.Text); // this will turn the text to a double
                labelCurrentValue.Text = previousValues + " " + operationPerformed;
                previousValues = labelCurrentValue.Text + " ";
            }

            isOperationPerformed = true; // having this at the end will ensure we don't add any text to the lable if we choose another operator in quick succesion
        }

        private void buttonClearEntry_Click(object sender, EventArgs e)
        {
            // clearing the most recent entry on the text bar
            textBox_Result.Text = "0";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // clearing the entry and the result (basically clearing all global variables to starting state)
            textBox_Result.Text = "0";
            resultValue = 0;
            labelCurrentValue.Text = "";
            previousValues = ""; // string to show previous values
            operationPerformed = "";
            isOperationPerformed = false; // confirms if an operation has changed the result value
            isEqualPerformed = false;
        }

        private string Calculate(string operation)
        {
            switch (operation)
            {
                case "+":
                    return (resultValue + Double.Parse(textBox_Result.Text)).ToString();
                case "-":
                    return (resultValue - Double.Parse(textBox_Result.Text)).ToString();
                case "*":
                    return (resultValue * Double.Parse(textBox_Result.Text)).ToString();
                case "/":
                    return (resultValue / Double.Parse(textBox_Result.Text)).ToString();
                default:
                    return "";
            }
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            if (labelCurrentValue.Text != "") // this will avoid the program calculating nothing if the equal sign is pressed too early
            {
                textBox_Result.Text = Calculate(operationPerformed);
                labelCurrentValue.Text = ""; // clearing the previous label text so that the current result is displayed
                resultValue = Double.Parse(textBox_Result.Text); // setting the current result Value for the label text
            }
            
            previousValues = textBox_Result.Text; // sets the previous value to the final result, in case of further calculations
            labelCurrentValue.Text = ""; // clearing the previous label text so that the current result is displayed
            isEqualPerformed = true;
        }
    }
}
