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
    public partial class frmResultados : Form
    {
        string texto;

        //=====================================================
        public frmResultados(string texto)
        {
            InitializeComponent();

            //definir o texto de pesquisa
            this.texto = texto.ToUpper();
        }

        //=====================================================
        private void frmResultados_Load(object sender, EventArgs e)
        {
            //executa a pesquisa e constroi lista
            ExecutaPesquisa();
        }

        //=====================================================
        private void ExecutaPesquisa()
        {
            //realiza a pesquisa e apresenta dados
            List<cl_contacto> lista_resultados = new List<cl_contacto>();

            foreach (cl_contacto contacto in cl_geral.LISTA_CONTACTOS)
            {
                if (contacto.nome.ToUpper().Contains(texto) ||
                    contacto.numero.ToUpper().Contains(texto))
                {
                    lista_resultados.Add(contacto);
                }
            }

            //apresentar os resultados na lista
            lista_final.Items.Clear();
            foreach (cl_contacto contacto in lista_resultados)
                lista_final.Items.Add(contacto.nome + " (" + contacto.numero + ")");

            label_numero_registos.Text = "Registos: " + lista_final.Items.Count;
        }

        //=====================================================
        private void cmd_fechar_Click(object sender, EventArgs e)
        {
            //fecha o quadro
            this.Close();
        }

        //=====================================================
        private void cmd_nova_pesquisa_Click(object sender, EventArgs e)
        {
            //pedir novo texto
            frmTexto f = new frmTexto();
            f.ShowDialog();

            //quando o quadro é fechado...
            if (f.cancelado) return;
            texto = f.texto.ToUpper();
            ExecutaPesquisa();
        }
    }
}
