using System;
using System.Data;
using System.Data.SqlClient;

namespace CamadaDados
{
    public class Dapresentacao
    {
        private int _idApresentacao;
        private string _nome;
        private string _descricao;
        private string _TextoBuscar;

        //construtores 
        public Dapresentacao()
        {

        }
        public Dapresentacao(int _idApresentacao, string _nome, string _descricao, string _TextoBuscar)
        {
            this._idApresentacao = _idApresentacao;
            this._nome = _nome;
            this._descricao = _descricao;
            this._TextoBuscar = _TextoBuscar;

        }

        // metodos gets e sets 
        public int IdApresentacao
        {
            get => _idApresentacao;
            set => _idApresentacao = value;
        }
        public string Nome
        {
            get => _nome;
            set => _nome = value;
        }
        public string Descricao
        {
            get => _descricao;
            set => _descricao = value;
        }
        public string TextoBuscar
        {
            get => _TextoBuscar;
            set => _TextoBuscar = value;
        }

        // metodo de inserçao
        public  string InserirAP(Dapresentacao apresentacao)
        {
            string resp = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conection.cn;
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "inserir_Apresentacao";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdapresentacao = new SqlParameter() { ParameterName = "@id_apresentacao" };
                parIdapresentacao.SqlDbType = SqlDbType.Int;
                parIdapresentacao.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(parIdapresentacao);

                SqlParameter parNome = new SqlParameter() { ParameterName = "@nome" };
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50;
                parNome.Value = apresentacao._nome;
                sqlcmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter() { ParameterName = "@descricao" };
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 250;
                parDescricao.Value = apresentacao._descricao;
                sqlcmd.Parameters.Add(parDescricao);

                //teste e execucao do comando
                resp = sqlcmd.ExecuteNonQuery() == 1 ? "ok" : "O resgitro não foi inserido";
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
        // metodo para para editar Apresentaçao
        public string EditApresentacao(Dapresentacao apresentacao)
        {
            string resp = "";
            SqlConnection sqlcon = new SqlConnection();
            
            try
            {
                sqlcon.ConnectionString = Conection.cn;
                sqlcon.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "edit_apresentacao";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdapresentacao = new SqlParameter() { ParameterName = "@id_apresentacao" };
                parIdapresentacao.SqlDbType = SqlDbType.Int;
                parIdapresentacao.Value = apresentacao._idApresentacao;
                sqlcmd.Parameters.Add(parIdapresentacao);

                SqlParameter parNome = new SqlParameter() { ParameterName = "@nome" };
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50;
                parNome.Value = apresentacao._nome;
                sqlcmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter() { ParameterName = "@descricao" };
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 250;
                parDescricao.Value = apresentacao._descricao;
                sqlcmd.Parameters.Add(parDescricao);

                // comando para execuçao 
                resp = sqlcmd.ExecuteNonQuery() == 1 ? "ok" : "Registro não pode ser editado!!";
            }
            catch(Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open) sqlcon.Close();
            }
                 return resp;
        }
        //metodo para deleçao
        public String DeletarApresentacao(Dapresentacao apresentacao)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "delet_apresentacao";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdApesentacao = new SqlParameter() { ParameterName = "@idapresentacao" };
                parIdApesentacao.SqlDbType = SqlDbType.Int;
                parIdApesentacao.Value = apresentacao._idApresentacao;
                SqlCmd.Parameters.Add(parIdApesentacao);
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
            catch(Exception)
            {
                resultDT = null;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open) sqlcon.Close();
            }
            return resultDT;
        }
        //metodo exibe nome
        public DataTable Exibnome(Dapresentacao apresentacao)
        {
            DataTable dtResultados = new DataTable("apresentacao");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conection.cn;

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "buscar_nomeApresentacao";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter() { ParameterName = "@textobuscar" };
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 50;
                parTextoBuscar.Value = apresentacao._nome;
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
