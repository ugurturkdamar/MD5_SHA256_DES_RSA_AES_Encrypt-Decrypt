﻿using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace odev2
{
    public partial class Form1 : Form
    {
        //G -> ALICE'S PUBLIC KEY
        //N -> BOB'S PUBLIC KEY, BOB'S PRIVATE KEY
        //D -> ALICE'S PRIVATE KEY
        public static string unHashed = "Merhaba Bob ben Alice"; 
        private static int kalicip = 0;
        private static int kaliciq = 0;
        public static double kalicig = 0; 
        public static double kalicin = 0; 
        public static string kalicisifrelimesaj = "";
        public Form1()
        {
            InitializeComponent();        
            richTextBox4.Text = unHashed;         
        }       
   
        private void button1_Click(object sender, EventArgs e)
        {       
                byte[] data = UTF8Encoding.UTF8.GetBytes(richTextBox4.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(unHashed));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        richTextBox6.Text = Convert.ToBase64String(results, 0, results.Length);
                        kalicisifrelimesaj = richTextBox6.Text;
                    }
                }
            richTextBox4.Clear();
        }
        private void button2_Click(object sender, EventArgs e)
        {      
                byte[] data = Convert.FromBase64String(richTextBox6.Text); // decrypt the incrypted text
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(unHashed));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        Console.WriteLine(transform);
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        richTextBox4.Text = UTF8Encoding.UTF8.GetString(results);
                    }
                }         
        }
    
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int p = Convert.ToInt32(richTextBox10.Text);            
                int q = Convert.ToInt32(richTextBox11.Text);
                int n = p * q;
                int z = (p - 1) * (q - 1);
                richTextBox7.Text = n.ToString();
                richTextBox8.Text = z.ToString();
                label7.Text = "--> 1 ile " + richTextBox8.Text + " arasında, " + richTextBox8.Text + " ile aralarında asal tamsayı giriniz.";           
            }
            catch(ArgumentException)
            {
                MessageBox.Show("Asal girmediniz..");
            }         
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int p = Convert.ToInt32(richTextBox10.Text);
                kalicip = p;
                int q = Convert.ToInt32(richTextBox11.Text);
                kaliciq = q;
                int n = p * q;
                kalicin = n;
                int z = (p - 1) * (q - 1);
                richTextBox7.Text = n.ToString();
                richTextBox8.Text = z.ToString();            
                double g = Convert.ToInt32(richTextBox9.Text);
                kalicig = g;
                double d = 0;
                int i = 0;
                while (true)
                {
                    d = (1 + i * z) / g;
                    bool is_integer = unchecked(d == (int)d);
                    if (is_integer) { break; }
                    i++;
                }          
                richTextBox5.Text = n.ToString();
                richTextBox2.Text = n.ToString();
                richTextBox1.Text = g.ToString();
                richTextBox3.Text = d.ToString();              
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Asal girmediniz.." + ex);
            }          
        }      
        protected void button5_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            
        }
        public double dyibul()
        {      
            int p = kalicip;
            int q = kaliciq;
            int n = p * q;
            int z = (p - 1) * (q - 1);          
            double g = kalicig;
            double d = 0;
            int i = 0;
            while(true)
            {
                d = (1 + i * z) / g;
                bool is_integer = unchecked(d == (int)d);
                if (is_integer) { break; }
                i++;
            }                   
            return d;
        }  
    }
}