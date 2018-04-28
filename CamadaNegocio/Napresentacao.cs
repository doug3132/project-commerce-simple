using System;
using System.Data;
using CamadaDados;

namespace CamadaNegocio
{
    public class Napresentacao
    {
        //metodo inserir
        public static String Inserir(String nome, String descricao)
        {
             Dapresentacao obj = new CamadaDados.Dapresentacao();
            obj.Nome = nome;
            obj.Descricao = descricao;
            return obj.InserirAP(obj);
        }
        //metodo editar
        public static String Editar(int idApresentacao, String nome, String descricao)
        {
            Dapresentacao obj = new CamadaDados.Dapresentacao();
            obj.IdApresentacao = idApresentacao;
            obj.Nome = nome;
            obj.Descricao = descricao;
            return obj.EditApresentacao(obj);
        }
        //metodo deletar
        public static String Deletar(int idApresentacao)
        {
            Dapresentacao obj = new CamadaDados.Dapresentacao();
            obj.IdApresentacao = idApresentacao;
            return obj.DeletarApresentacao(obj);
        }
        // metodo exibir
        public static DataTable Exibir()
        {
            return new Dapresentacao().ExibeDados();
        }
        //metodo buscar nome
        public static DataTable BuscarNome(string nome)
        {
            Dapresentacao obj = new Dapresentacao();
            obj.Nome = nome;
            return obj.Exibnome(obj);
        }
    }
}
