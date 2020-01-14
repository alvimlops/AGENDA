using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();

            //carrega os contactos
            cl_geral.ConstroiListaContactos();

            //apresenta versão do programa
            label_versao.Text = cl_geral.versao;
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }

        private void cmd_sair_Click(object sender, EventArgs e)
        {
            //sair da aplicação

            //pergunta se pretende mesmo sair da aplicação
            if (MessageBox.Show("Deseja sair da aplicação?", "SAIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            //sair da aplicação definitivamente
            Application.Exit();
        }

        private void cmd_inserir_editar_Click(object sender, EventArgs e)
        {
            //abre o quadro para gestão dos contactos
            frmInserirEditar formulario = new frmInserirEditar();
            formulario.ShowDialog();
        }

        private void cmd_pesquisar_Click(object sender, EventArgs e)
        {
            //abrir o quadro de pesquisa
            frmTexto f = new frmTexto();
            f.ShowDialog();

            //quando fecha o quadro, verifica se foi cancelado
            if (f.cancelado) return;

            //abrir o quadro com resultados da pesquisa
            frmResultados ff = new frmResultados(f.texto);
            ff.ShowDialog();
        }
    }
}
