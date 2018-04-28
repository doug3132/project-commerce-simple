using System;
using CamadaDados;
using System.Data;

namespace CamadaNegocios
{
    public class Ncategoria
    {
        //metodo inserir
        public static String Inserir(String nome, String descricao)
        {
            Dcategoria obj = new CamadaDados.Dcategoria();
            obj.NomeProduto = nome;
            obj.Descricao = descricao;
            return obj.Inserir(obj);
        }
        //metodo editar
        public static String Editar(int idCategoria, String nome, String descricao)
        {
            Dcategoria obj = new CamadaDados.Dcategoria();
            obj.IdCategoria = idCategoria;
            obj.NomeProduto = nome;
            obj.Descricao = descricao;
            return obj.Editar(obj);
        }
        //metodo deletar
        public static String Deletar(int idCategoria)
        {
            Dcategoria obj = new CamadaDados.Dcategoria();
            obj.IdCategoria = idCategoria;
            return obj.DeletarCategoria(obj);
        }
        // metodo exibir
        public static DataTable Exibir()
        {
            return new Dcategoria().ExibeDados();
        }
        //metodo buscar nome
        public static DataTable BuscarNome(string nome)
        {
            Dcategoria obj = new Dcategoria();
            obj.NomeProduto = nome;
            return obj.Exibnome(obj);
        }
    }
}

