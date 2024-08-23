using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Math_Quiz
{
    public partial class Form1 : Form
    {
        Random Randomizer = new Random();
        int addend1;
        int addend2;

        int subend1;
        int subend2;

        int mulend1;
        int mulend2;

        int divend1;
        int divend2;

        int timeLeft;

        public void StarttheQuiz()
        { 
            addend1 = Randomizer.Next(51);
            addend2 = Randomizer.Next(51);
            addLeft.Text = addend1.ToString();
            addRight.Text = addend2.ToString();
            sum.Value = 0;


            subend1 = Randomizer.Next(1, 101);
            subend2 = Randomizer.Next(1, subend1);
            subLeft.Text = subend1.ToString();
            subRight.Text = subend2.ToString();
            difference.Value = 0;

            mulend1 = Randomizer.Next(2, 11);
            mulend2 = Randomizer.Next(2, 11);
            mulLeft.Text = mulend1.ToString();
            mulRight.Text = mulend2.ToString();
            product.Value = 0;

            divend2 = Randomizer.Next(2, 11);
            int temporaryQuotient = Randomizer.Next(2, 11);
            divend1 = divend2 * temporaryQuotient;
            divLeft.Text = divend1.ToString();
            divRight.Text = divend2.ToString();
            quotient.Value = 0;

            timeLeft = 10;
            timeLabel.Text = "10 seconds";
            timer1.Start();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StartQuiz_Click(object sender, EventArgs e)
        {
            StarttheQuiz();
            StartQuiz.Enabled = false;
        }

        private bool CheckTheAnswer()
        {
            if(addend1+addend2==sum.Value && subend1-subend2==difference.Value && mulend1*mulend2==product.Value && divend1/divend2==quotient.Value)
            {
                return true;
            }
            else
                return false;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("Correct.");
                StartQuiz.Enabled = true;
                timeLabel.BackColor = Color.White;
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
                if (timeLeft < 5)
                {
                    timeLabel.BackColor = Color.Red;
                }
            }
            else
            {
                timer1.Stop();
                timeLabel.BackColor = Color.White;
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = subend1 - subend2;
                product.Value = mulend1 * mulend2;
                quotient.Value = divend1 / divend2;
                StartQuiz.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerbox = sender as NumericUpDown;
            if (answerbox != null)
            {
                int lenghtofAnswer = answerbox.Value.ToString().Length;
                answerbox.Value = lenghtofAnswer;
            }
        }
    }
}
