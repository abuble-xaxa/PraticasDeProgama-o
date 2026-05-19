using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace TP2
{
    public partial class Form1 : Form
    {
        List<Endereco> enderecos = new List<Endereco>();

        public Form1()
        {
            InitializeComponent();

         
        }
        public class Endereco
        {
            public string Tipo { get; set; }
            public string Rua { get; set; }
            public int Numero { get; set; }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipo.Text))
            {
                MessageBox.Show("O Tipo não pode estar vazio!");
            }
            if (string.IsNullOrWhiteSpace(txtRua.Text))
            {
                MessageBox.Show("Rua não pode estar vazio!");
            }
            if (!int.TryParse(txtNumero.Text, out int numero) || numero <= 0)
            {
                MessageBox.Show("Numero deve ser valido");
            }


            Endereco endereco = new Endereco
            {
                Tipo = txtTipo.Text,
                Rua = txtRua.Text,
                Numero = numero

            };

            enderecos.Add(endereco);

            txtTipo.Clear();
            txtRua.Clear();
            txtNumero.Clear();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(enderecos, options);

            lblVisualizacao.Text = json;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            lblVisualizacao.Text = " - ";
        }
    }
}
