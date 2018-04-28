using System;
using System.Data;
using System.Data.SqlClient;
namespace CamadaDados
{
    public class Dcategoria
    {
        private int _idCategoria;
        private string _nome_Produto;
        private string _descricao;
        private string _textoBuscar;

        //constructor null
        public Dcategoria()
        {

        }
        // construtor/ constructor  com parametros
        public Dcategoria(int _idCategoria, string _nome_Produto, string _descricao, string _textoBuscar)
        {
            this._idCategoria = _idCategoria;
            this._nome_Produto = _nome_Produto;
            this._descricao = _descricao;
            this._textoBuscar = _textoBuscar;

        }
        // methodos gets and sets
        public int IdCategoria
        {
            get
            {
                return _idCategoria;
            }
            set
            {
                _idCategoria = value;
            }
        }
        public string NomeProduto
        {
            get
            {
                return _nome_Produto;
            }
            set
            {
                _nome_Produto = value;
            }
        }
        public string Descricao
        {
            get
            {
                return _descricao;
            }
            set
            {
                _descricao = value;
            }
        }
        public string TextoBuscar
        {
            get
            {
                return _textoBuscar;
            }
            set
            {
                _textoBuscar = value;
            }
        }
        // metodo de inserir 
        public String Inserir(Dcategoria categoria)
        {
            string resp = "";
            SqlConnection Sqlcon = new SqlConnection();
            try
            {
                Sqlcon.ConnectionString = Conection.cn;
                Sqlcon.Open();

                SqlCommand Sqlcmd = new SqlCommand();
                Sqlcmd.Connection = Sqlcon;
                Sqlcmd.CommandText = "inserir_categoria";
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdCategoria = new SqlParameter { ParameterName = "@idCategoria" };
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Direction = ParameterDirection.Output;
                Sqlcmd.Parameters.Add(parIdCategoria);

                SqlParameter parNome = new SqlParameter { ParameterName = "@nome" };
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50;
                parNome.Value = categoria._nome_Produto;
                Sqlcmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter { ParameterName = "@descricao" };
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 200;
                parDescricao.Value = categoria._descricao;
                Sqlcmd.Parameters.Add(parDescricao);
                //executar o comando
                resp = Sqlcmd.ExecuteNonQuery() == 1 ? "ok" : " O resgistro não foi inserido.";
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
        //metodo de editar categoria
        public String Editar(Dcategoria categoria)
        {

            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "edit_actegoria";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdcategoria = new SqlParameter() { ParameterName = "@idCategoria" };
                parIdcategoria.SqlDbType = SqlDbType.Int;
                parIdcategoria.Value = categoria._idCategoria;
                SqlCmd.Parameters.Add(parIdcategoria);

                SqlParameter parNome = new SqlParameter() { ParameterName = "@nome" };
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50;
                parNome.Value = categoria._nome_Produto;
                SqlCmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter() { ParameterName = "@descricao" };
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 200;
                parDescricao.Value = categoria._descricao;
                SqlCmd.Parameters.Add(parDescricao);
                //executar o comando
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "ok" : " Deu erro";

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
        // metodo para deletar categoria
        public String DeletarCategoria(Dcategoria categoria)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "delet_categoria";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdcategoria = new SqlParameter() { ParameterName = "@idcategoria" };
                parIdcategoria.SqlDbType = SqlDbType.Int;
                parIdcategoria.Value = categoria._idCategoria;
                SqlCmd.Parameters.Add(parIdcategoria);
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
        //metodo de exibir
        public DataTable ExibeDados()
        {
            DataTable dtResultados = new DataTable("categoria");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conection.cn;

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "mostrar_categoria";
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqldata = new SqlDataAdapter(sqlcmd);
                sqldata.Fill(dtResultados);

            }
            catch (Exception)
            {
                dtResultados = null;
            }
            finally
            {

            }
            return dtResultados;
        }
        //metodo exibe nome
        public DataTable Exibnome(Dcategoria categoria)
        {
            DataTable dtResultados = new DataTable("categoria");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conection.cn;

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "pegar_nome";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter() { ParameterName = "@textobuscar" };
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 50;
                parTextoBuscar.Value = categoria._nome_Produto;
                sqlcmd.Parameters.Add(parTextoBuscar);

                SqlDataAdapter sqldata = new SqlDataAdapter(sqlcmd);
                sqldata.Fill(dtResultados);

            }
            catch (Exception)
            {
                dtResultados = null;
            }
            finally
            {

            }
            return dtResultados;
        }

    }
}
