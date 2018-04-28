using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CamadaDados
{
    public class DProdutos
    {
        private string _codigo;
        private int _idProduto;
        private string _Descricao;
        private int _idApresentacao;
        private int _idCategoria;
        

        public DProdutos()
        {
        }

        public DProdutos(String _codigo, int _idProduto, String _Descricao, int _idApresentacao, int _idCategoria)
        {
            this.Codigo = _codigo;
            this._idProduto = _idProduto;
            this._Descricao = _Descricao;
            this._idApresentacao = _idApresentacao;
            this._idCategoria = _idCategoria;
        }

        public string Codigo { get => _codigo; set => _codigo = value; }
        public int IdProduto { get => _idProduto; set => _idProduto = value; }
        public string Descricao { get => _Descricao; set => _Descricao = value; }
        public int IdApresentacao { get => _idApresentacao; set => _idApresentacao = value; }
        public int IdCategoria { get => _idCategoria; set => _idCategoria = value; }

        // metodo para inserir dados
        public string inserir_Produto(DProdutos produtos) 
        {
            string resp = "";
            SqlConnection Sqlcon = new SqlConnection();
            try
            {
                Sqlcon.ConnectionString = Conection.cn;
                Sqlcon.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = Sqlcon;
                sqlcmd.CommandText = "inserir_produto";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdProduto = new SqlParameter { ParameterName = "@idProduto" };
                parIdProduto.SqlDbType = SqlDbType.Int;
                parIdProduto.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(parIdProduto);

                SqlParameter parNome = new SqlParameter { ParameterName = "@codigo" };
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50;
                parNome.Value = produtos._codigo;
                sqlcmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter { ParameterName = "@descricao" };
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 200;
                parDescricao.Value = produtos._Descricao;
                sqlcmd.Parameters.Add(parDescricao);

                SqlParameter parIdCategoria = new SqlParameter { ParameterName = "@idCategoria" };
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Value = produtos._idCategoria;
                sqlcmd.Parameters.Add(parIdCategoria);

                SqlParameter parIdapresentacao = new SqlParameter() { ParameterName = "@idApresentacao" };
                parIdapresentacao.SqlDbType = SqlDbType.Int;
                parIdapresentacao.Value = produtos._idApresentacao;
                sqlcmd.Parameters.Add(parIdapresentacao);

                SqlParameter parImagem = new SqlParameter() { ParameterName = "@imagem" };
                parImagem.SqlDbType = SqlDbType.Image;
                


                //executar o comando
                resp = sqlcmd.ExecuteNonQuery() == 1 ? "ok" : " O resgistro não foi inserido.";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();
            }
            return resp;
        }
        //editar produtos
        public string edit_Produto(DProdutos produtos)
        {
            string resp = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conection.cn;
                sqlcon.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "edit_produto";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdProduto = new SqlParameter() { ParameterName = "@idProduto" };
                parIdProduto.SqlDbType = SqlDbType.Int;
                parIdProduto.Value = produtos._idProduto;
                sqlcmd.Parameters.Add(parIdProduto);

                SqlParameter parNome = new SqlParameter() { ParameterName = "@codigo" };
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50;
                parNome.Value = produtos._codigo;
                sqlcmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter() { ParameterName = "@descricao" };
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 250;
                parDescricao.Value = produtos._Descricao;
                sqlcmd.Parameters.Add(parDescricao);
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open) sqlcon.Close();
            }
            return resp;
        }
        // metodo para deletar
        public String DeletarProduto(DProdutos produtos)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "delet_produto";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdProduto = new SqlParameter() { ParameterName = "@idProduto" };
                parIdProduto.SqlDbType = SqlDbType.Int;
                 parIdProduto.Value = produtos._idProduto;
                SqlCmd.Parameters.Add(parIdProduto);
                //executar o comando
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "ok" : "não foi possivel deletar";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return resp;
        }
        // metodo para exibiçao 
        public DataTable ExibeDados()
        {
            DataTable resultDT = new DataTable();
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conection.cn;
                sqlcon.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "mostrar_apresentaçao";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlData = new SqlDataAdapter(sqlcmd);
                sqlData.Fill(resultDT);
            }
            catch (Exception)
            {
                resultDT = null;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open) sqlcon.Close();
            }
            return resultDT;
        }
    }
}
