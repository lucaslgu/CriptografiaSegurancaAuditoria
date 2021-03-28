using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CriptografiaSegurancaAuditoria.CriptografiaDescriptografia;

namespace CriptografiaSegurancaAuditoria
{
    public partial class Form_Main : Form
    {
        string chromeRoute = "C:/Program Files/Google/Chrome/Application/chrome.exe";
        bool ehCriptografia = true;
        Algoritmo algoritmoCriptografia = new Algoritmo();
        string result = "";

        public Form_Main()
        {
            InitializeComponent();
        }

        private void pictureBox_Face_Click(object sender, EventArgs e)
        {
            Process.Start(chromeRoute, "https://www.facebook.com/lusca.fsociety");
        }

        private void pictureBox_Insta_Click(object sender, EventArgs e)
        {
            Process.Start(chromeRoute, "https://www.instagram.com/lusca.py");
        }

        private void pictureBox_Git_Click(object sender, EventArgs e)
        {
            Process.Start(chromeRoute, "https://github.com/lucaslgu");
        }

        private void pictureBox_Link_Click(object sender, EventArgs e)
        {
            Process.Start(chromeRoute, "https://www.linkedin.com/in/lucas-ribeiro-py");
        }

        private void pictureBox_Gmail_Click(object sender, EventArgs e)
        {
            Process.Start(chromeRoute, "https://mail.google.com/mail/u/0/#inbox?compose=GTvVlcSBpgMwTnfJChCxLmtmKcFrPcPxMLSLxggQzHfKmQGFKmRfvcZvLxnLDktmHDMBNGVQsMQjz");
        }

        private void pictureBox_Wpp_Click(object sender, EventArgs e)
        {
            Process.Start(chromeRoute, "https://api.whatsapp.com/send?phone=5581993094325&text=Ol%C3%A1%20Lucas%2C%20vim%20atrav%C3%A9s%20deste%20link%20conversar%20contigo%20sobre%20sua%20aplica%C3%A7%C3%A3o%20de%20criptografia");
        }

        private void pictureBox_Help_Click(object sender, EventArgs e)
        {
            Process.Start(chromeRoute, "https://docs.google.com/presentation/d/1rbpJz59EPaXmtUfRb4oFRanQGy3FqiG6nA3qpUTlsgo/edit?usp=sharing");
        }

        private void pictureBox_Invert_Click(object sender, EventArgs e)
        {
            ehCriptografia = !ehCriptografia;
            groupBox_sendToBack.Text = ehCriptografia ? "Digite abaixo seu texto a ser criptografado" : "Digite abaixo seu texto a ser descriptografado";
            txt_Result.PlaceholderText = ehCriptografia ? "Aqui ficará o resultado de sua criptografia" : "Aqui ficará o resultado de sua descriptografia";
            btn_CriptAndDescript.Text = ehCriptografia ? "&Criptografar!" : "&Descriptografar!";
            txt_Result.Clear();
            txt_sendToBackend.Clear();
            txt_sendToBackend.PlaceholderText = ehCriptografia ? "Digite aqui sua criptografia" : "Digite aqui sua descriptografia";
            txt_Result.PlaceholderText = ehCriptografia ? "Aqui ficará o resultado de sua criptografia" : "Aqui ficará o resultado de sua descriptografia";
        }

        private void btn_CriptAndDescript_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_sendToBackend.Text))
            {
                if (ehCriptografia)
                {
                    result = algoritmoCriptografia.Criptografia(txt_sendToBackend.Text);

                    if (result.Length > txt_sendToBackend.Text.Length * 5 * 4 + 4)
                    {
                        result = algoritmoCriptografia.Criptografia(txt_sendToBackend.Text);
                    }
                }
                else
                {
                    result = algoritmoCriptografia.Descriptografia(txt_sendToBackend.Text);
                }

                txt_Result.Text = result;
                txt_sendToBackend.Clear();
            }
            else
            {
                MessageBox.Show("Necessário preencher algum texto!", "Erro ao criptografar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
