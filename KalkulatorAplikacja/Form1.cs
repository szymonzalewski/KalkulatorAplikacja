using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KalkulatorAplikacja.Form1;

namespace KalkulatorAplikacja
{
    public enum Operation
    {
        Nic,
        Dodawanie,
        Odejmowanie,
        Mnożenie,
        Dzielenie,
        Potęgowanie,
        Procenty,
        Pierwiastkowanie,
    }
    public partial class Form1 : Form
    {

        private string _firstValue;
        private string _secondValue;
        private Operation _currentOperation = Operation.Nic;
        private bool _isTheResultOnTheScreen;
        
       
        
        public Form1()
        {
            InitializeComponent();
            poleTekstowe.Text = "0";
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void OnTbNumberClick(object sender, EventArgs e)

        {

            var clickedValue = (sender as Button).Text;
            if (poleTekstowe.Text == "0")
            poleTekstowe.Text = string.Empty;
            if (_isTheResultOnTheScreen)
            {
                _isTheResultOnTheScreen = false;
                poleTekstowe.Text = string.Empty;

                if (clickedValue == "'")
                    clickedValue = "O,";
            }
            poleTekstowe.Text += clickedValue;
            SetResultTbState(true);

            if (_currentOperation != Operation.Nic)
                _secondValue += clickedValue;
            else
                SetOperationTbState(true);
        }

        private TextBox GetPoleTekstowe()
        {
            return poleTekstowe;
        }

        private void OnTbOperationClick(object sender, EventArgs e)
        {
            _firstValue = poleTekstowe.Text;
           var operation = (sender as Button).Text;
            _currentOperation = operation switch
            {
                "+" => Operation.Dodawanie,
                "-" => Operation.Odejmowanie,
                "^" => Operation.Potęgowanie,
                "*" => Operation.Mnożenie,
                "/" => Operation.Dzielenie,
                "%" => Operation.Procenty,
                "sqrt" => Operation.Pierwiastkowanie,

            };
            poleTekstowe += $"{operation} ";

            if (_isTheResultOnTheScreen)
                _isTheResultOnTheScreen= false;

            SetOperationTbState(false);
            SetResultTbState(false);
          

        }

        private void OnTbResultClick(object sender, EventArgs e)
        {
            if (_currentOperation == Operation.Nic)
                return;
            var firstNumber = double.Parse(_firstValue);
            var secondNumber = double.Parse(_secondValue);

            var result = Calculate(firstNumber, secondNumber);

            poleTekstowe = result.ToString();
            _secondValue = string.Empty;
            _currentOperation= Operation.Nic;
            _isTheResultOnTheScreen = true;
            SetOperationTbState(true);
            SetResultTbState(true);
        }
        private double Calculate(double firstNumber, double secondNumber)
        { 
            switch (_currentOperation)
            {
                case Operation.Nic:
                    return firstNumber;
                case Operation.Dodawanie:
                    return firstNumber + secondNumber;
                case Operation.Odejmowanie:
                    return firstNumber - secondNumber;
                case Operation.Mnożenie: 
                    return firstNumber * secondNumber;
                case Operation.Procenty: 
                    return firstNumber % secondNumber;
                case Operation.Potęgowanie: 
                    return Math.Pow(firstNumber, secondNumber);
                case Operation.Pierwiastkowanie:
                    return Math.Sqrt(firstNumber * 1/ secondNumber);
                case Operation.Dzielenie:
                    if (secondNumber == 0)
                    {
                        MessageBox.Show("Nie można dzielić przez zero");
                        return 0;
                    }
                    return firstNumber / secondNumber;
            }
            return 0;
        }
        private void OnTbClearClick(object sender, EventArgs e)
        {
            poleTekstowe.Text = "0";
            _firstValue = string.Empty;
            _secondValue = string.Empty;
            _currentOperation = Operation.Nic;
        }
        private void SetOperationTbState(bool value)
        {
            plus.Enabled= value;
            minus.Enabled= value;
            mnozenie.Enabled= value;
            dzielenie.Enabled= value;
            potega.Enabled= value;
            pierwiastek.Enabled= value;
            potega.Enabled= value;
        }
        private void SetResultTbState(bool value)
        {
            wynik.Enabled= value;
        }
    }
}