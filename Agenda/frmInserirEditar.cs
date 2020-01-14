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
    public partial class frmInserirEditar : Form
    {
        int indice;
        bool editar = false;

        //=====================================================
        public frmInserirEditar()
        {
            InitializeComponent();
            ConstroiLista();
        }

        //=====================================================
        private void cmd_fechar_Click(object sender, EventArgs e)
        {
            //fechar quadro
            this.Close();
        }

        //=====================================================
        private void frmInserirEditar_Load(object sender, EventArgs e)
        {

        }

        //=====================================================
        private void ConstroiLista()
        {
            //adiciona à lista de contactos os contactos registados
            lista_contactos.Items.Clear();

            foreach (cl_contacto contacto in cl_geral.LISTA_CONTACTOS)
            {
                lista_contactos.Items.Add(contacto.nome + " (" + contacto.numero + ")");
            }

            //atualizar o número de registos
            label_numero_registos.Text = "Registos: " + lista_contactos.Items.Count;

            //impedir edição e eliminação do registo
            cmd_apagar.Enabled = false;
            cmd_editar.Enabled = false;
            editar = false;
        }

        //=====================================================
        private void cmd_gravar_Click(object sender, EventArgs e)
        {
            //insere um novo registo na lista

            //verifica se todos os campos estão preenchidos
            if (text_nome.Text == "" || text_numero.Text == "")
            {
                MessageBox.Show("Os campos não estão todos preenchidos.");
                return;
            }

            //---------------------------------------
            #region NOVO REGISTO
            if (!editar)
            {
                //verifica se existe registo igual na lista
                foreach (cl_contacto contacto in cl_geral.LISTA_CONTACTOS)
                {
                    if (contacto.nome == text_nome.Text &&
                        contacto.numero == text_numero.Text)
                    {
                        MessageBox.Show("ERRO! Esse registo já existe.");
                        return;
                    }
                }

                //gravar novo registo
                cl_geral.GravarNovoRegisto(text_nome.Text, text_numero.Text);
            }
            #endregion

            //---------------------------------------
            #region EDITAR REGISTO
            else
            {
                //verifica se existe um registo igual
                for (int m = 0; m < cl_geral.LISTA_CONTACTOS.Count; m++)
                {
                    if (cl_geral.LISTA_CONTACTOS[m].nome == text_nome.Text &&
                        cl_geral.LISTA_CONTACTOS[m].numero == text_numero.Text &&
                        m != indice)
                    {
                        MessageBox.Show("ERRO! Já existe outro registo com os mesmos dados.");
                        return;
                    }
                }

                //editar o registo
                cl_geral.EditarRegisto(indice, text_nome.Text, text_numero.Text);
            }
            #endregion  

            //atualizar a lista de contactos
            ConstroiLista();

            //prepara o quadro para novo registo
            text_nome.Text = "";
            text_numero.Text = "";
            text_nome.Focus();
        }

        //=====================================================
        private void lista_contactos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selecionar um contacto.

            //verificar se indice = -1
            if (lista_contactos.SelectedIndex == -1) return;

            //seleciona um indice da lista
            indice = lista_contactos.SelectedIndex;
            cmd_editar.Enabled = true;
            cmd_apagar.Enabled = true;
        }

        //=====================================================
        private void cmd_apagar_Click(object sender, EventArgs e)
        {
            //apaga o registo selecionado

            //1.º eliminar o registo da lista
            cl_geral.LISTA_CONTACTOS.RemoveAt(indice);

            //2.º renovar o ficheiro
            cl_geral.GravarFicheiro();

            //3.º reconstruir a lista de contactos
            ConstroiLista();
        }

        //=====================================================
        private void cmd_editar_Click(object sender, EventArgs e)
        {
            //editar um registo de contacto
            editar = true;
            text_nome.Text = cl_geral.LISTA_CONTACTOS[indice].nome;
            text_numero.Text = cl_geral.LISTA_CONTACTOS[indice].numero;
            text_nome.Focus();
        }

        //=====================================================
        private void cmd_novo_Click(object sender, EventArgs e)
        {
            //novo registo
            editar = false;
            text_nome.Text = "";
            text_numero.Text = "";
            text_nome.Focus();
        }

        //=====================================================
        private void lista_contactos_DoubleClick(object sender, EventArgs e)
        {
            //acionar o comando editar
            if (indice < 0) return;

            cmd_editar_Click(cmd_editar, EventArgs.Empty);
        }
    }
}
