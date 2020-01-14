using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Agenda
{
    public static class cl_geral
    {
        public static string versao = "v 1.0.0";

        //lista de contactos
        public static List<cl_contacto> LISTA_CONTACTOS;

        //=====================================================
        public static void ConstroiListaContactos()
        {
            //método para carregamento da lista de contactos

            //verificar se o ficheiro existe
            string pasta_documentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string nome_ficheiro = pasta_documentos + @"\ficheiro_contactos.txt";

            //cria a lista vazia
            LISTA_CONTACTOS = new List<cl_contacto>();

            if (File.Exists(nome_ficheiro))
            {
                StreamReader ficheiro = new StreamReader(nome_ficheiro, Encoding.Default);                
                while (!ficheiro.EndOfStream)
                {
                    //carrega nome
                    string nome = ficheiro.ReadLine();
                    //carrega numero
                    string numero = ficheiro.ReadLine();

                    //adicionar à lista de contactos o contacto carregado
                    cl_contacto novo_contacto = new cl_contacto();
                    novo_contacto.nome = nome;
                    novo_contacto.numero = numero;
                    LISTA_CONTACTOS.Add(novo_contacto);
                }
                ficheiro.Dispose();
            }
        }

        //=====================================================
        public static void GravarNovoRegisto(string _nome, string _numero)
        {
            //gravar um novo registo (na lista e no ficheiro)

            //cl_contacto novo = new cl_contacto();
            //novo.nome = _nome;
            //novo.numero = _numero;
            //LISTA_CONTACTOS.Add(novo);


            //gravar na lista
            LISTA_CONTACTOS.Add(new cl_contacto() { nome = _nome, numero = _numero });

            //atualiza o ficheiro
            GravarFicheiro();
        }

        //=====================================================
        public static void EditarRegisto(int indice, string _nome, string _numero)
        {
            //editar um registo da lista
            LISTA_CONTACTOS[indice].nome = _nome;
            LISTA_CONTACTOS[indice].numero = _numero;
            GravarFicheiro();
        }

        //=====================================================
        public static void GravarFicheiro()
        {
            string pasta_documentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string nome_ficheiro = pasta_documentos + @"\ficheiro_contactos.txt";

            StreamWriter ficheiro = new StreamWriter(nome_ficheiro, false, Encoding.Default);
            foreach (cl_contacto contacto in LISTA_CONTACTOS)
            {
                ficheiro.WriteLine(contacto.nome);
                ficheiro.WriteLine(contacto.numero);
            }
            ficheiro.Dispose();
        }
    }    
}
