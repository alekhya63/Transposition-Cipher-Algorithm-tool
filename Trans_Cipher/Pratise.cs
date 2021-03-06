﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trans_Cipher
{
    public partial class Pratise : UserControl
    {
        public Pratise()
        {
            InitializeComponent();
        }

        //Code for inserting the pad character while doing encryption
        private static int[] IndexesPadCharacter(string key)
        {
            int LengthofKey = key.Length;
            int[] countOfIndexes = new int[LengthofKey];
            List<KeyValuePair<int, char>> setKey = new List<KeyValuePair<int, char>>();
            int position;

            for (position = 0; position < LengthofKey; ++position)
                setKey.Add(new KeyValuePair<int, char>(position, key[position]));

            setKey.Sort(
                delegate (KeyValuePair<int, char> pair1, KeyValuePair<int, char> pair2) {
                    return pair1.Value.CompareTo(pair2.Value);
                }
            );

            for (position = 0; position < LengthofKey; ++position)
                countOfIndexes[setKey[position].Key] = position;

            return countOfIndexes;
        }

        //logic for performing transposition cipher encryption
        public static string TranspositionEncrypt(string plainText, string key, char[] padChar)
        {
            plainText = (plainText.Length % key.Length == 0) ? plainText : plainText.PadRight(plainText.Length - (plainText.Length % key.Length) + key.Length, padChar[0]);
            StringBuilder cipherText = new StringBuilder();
            int countOfChars = plainText.Length;
            int matrixColumns = key.Length;
            int matrixRows = (int)Math.Ceiling((double)countOfChars / matrixColumns);
            char[,] charactersOfRows = new char[matrixRows, matrixColumns];
            char[,] charactersOfColumns = new char[matrixColumns, matrixRows];
            char[,] sortedColumnCharacters = new char[matrixColumns, matrixRows];
            int currentRowOfMatrix, currentColumnOfMatrix, i, j;
            int[] Indexes = IndexesPadCharacter(key);

            for (i = 0; i < countOfChars; ++i)
            {
                currentRowOfMatrix = i / matrixColumns;
                currentColumnOfMatrix = i % matrixColumns;
                charactersOfRows[currentRowOfMatrix, currentColumnOfMatrix] = plainText[i];
            }

            for (i = 0; i < matrixRows; ++i)
                for (j = 0; j < matrixColumns; ++j)
                    charactersOfColumns[j, i] = charactersOfRows[i, j];

            for (i = 0; i < matrixColumns; ++i)
                for (j = 0; j < matrixRows; ++j)
                    sortedColumnCharacters[Indexes[i], j] = charactersOfColumns[i, j];

            for (i = 0; i < countOfChars; ++i)
            {
                currentRowOfMatrix = i / matrixRows;
                currentColumnOfMatrix = i % matrixRows;
                cipherText.Append(sortedColumnCharacters[currentRowOfMatrix, currentColumnOfMatrix]);
            }

            return cipherText.ToString();
        }


        //logic for performing transposition cipher decryption
        public static string TranspositionDecrypt(string cipherText, string key)
        {
            StringBuilder plainText = new StringBuilder();
            int countOfChars = cipherText.Length;
            int matrixColumns = (int)Math.Ceiling((double)countOfChars / key.Length);
            int matrixRows = key.Length;
            char[,] charactersOfRows = new char[matrixRows, matrixColumns];
            char[,] charactersOfColumns = new char[matrixColumns, matrixRows];
            char[,] columnCharsUnsorted = new char[matrixColumns, matrixRows];
            int currentRowOfMatrix, currentColumnOfMatrix, i, j;
            int[] Indexes = IndexesPadCharacter(key);

            for (i = 0; i < countOfChars; ++i)
            {
                currentRowOfMatrix = i / matrixColumns;
                currentColumnOfMatrix = i % matrixColumns;
                charactersOfRows[currentRowOfMatrix, currentColumnOfMatrix] = cipherText[i];
            }

            for (i = 0; i < matrixRows; ++i)
                for (j = 0; j < matrixColumns; ++j)
                    charactersOfColumns[j, i] = charactersOfRows[i, j];

            for (i = 0; i < matrixColumns; ++i)
                for (j = 0; j < matrixRows; ++j)
                    columnCharsUnsorted[i, j] = charactersOfColumns[i, Indexes[j]];

            for (i = 0; i < countOfChars; ++i)
            {
                currentRowOfMatrix = i / matrixRows;
                currentColumnOfMatrix = i % matrixRows;
                plainText.Append(columnCharsUnsorted[currentRowOfMatrix, currentColumnOfMatrix]);
            }
            //convert plaintext to string
            return plainText.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string plainText = textBox1.Text;
            string key = textBox2.Text;
            string padCharacter = textBox3.Text;
            char[] padChar = padCharacter.ToCharArray();
            textBox4.Text = TranspositionEncrypt(plainText, key, padChar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cipherText = textBox6.Text;
            string key = textBox7.Text;
            textBox9.Text = TranspositionDecrypt(cipherText, key);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CleartextBoxesPractiseEncrypt();
        }

        public void CleartextBoxesPractiseEncrypt()

        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            CleartextBoxesPractiseDecrypt();
        }

        public void CleartextBoxesPractiseDecrypt()

        {
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string encryptString = textBox4.Text;
            string userString = textBox5.Text;

            if(string.Equals(encryptString, userString, StringComparison.Ordinal))
            {
                MessageBox.Show("Good Job! Your encrypted message is correct!!");
            }
            else
            {
                MessageBox.Show("Incorrect Answer! Please Try Again!!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string cipherString = textBox9.Text;
            string userString = textBox10.Text;

            if (string.Equals(cipherString, userString, StringComparison.Ordinal))
            {
                MessageBox.Show("Good Job! Your plain text is correct!!");
            }
            else
            {
                MessageBox.Show("Incorrect Answer! Please Try Again!!");
            }
        }
    }
}
