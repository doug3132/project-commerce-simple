using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamadaNegocio;

namespace CamadaApresentacao
{
    public partial class FrmApresentacao : Form
    {
        private bool eNovo = false;
        private bool eEditar = false;

        public FrmApresentacao()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.txtNomeProduto, "Insira o nome da Apresentação");
        }

        //Mostrar mensagem de confirmação
        private void MensagemOk(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //Mostrar mensagem de erro
        private void MensagemErro(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        //Limpar Campos
        private void Limpar()
        {
            this.txtNomeProduto.Text = string.Empty;
            this.txtIdApresentacao.Text = string.Empty;
            this.txtDescricao.Text = string.Empty;
        }


        //Habilitar os text box
        private void Habilitar(bool valor)
        {
            this.txtNomeProduto.ReadOnly = !valor;
            this.txtDescricao.ReadOnly = !valor;
            this.txtIdApresentacao.ReadOnly = !valor;
        }


        //Habilitar os botoes
        private void botoes()
        {
            if (this.eNovo || this.eEditar)
            {
                this.Habilitar(true);
                this.btnNovo.Enabled = false;
                this.btnSalvar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;

            }
            else
            {
                this.Habilitar(false);
                this.btnNovo.Enabled = true;
                this.btnSalvar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }

        }



        //Ocultar as Colunas do Grid
        private void OcultarColunas()
        {
            this.dataLista.Columns[0].Visible = false;
            // this.dataLista.Columns[1].Visible = false;
        }


        //Mostrar no Data Grid
        private void Mostrar()
        {
            this.dataLista.DataSource = Napresentacao.Exibir();
            this.OcultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }



        //Buscar pelo Nome
        private void BuscarNome()
        {
            this.dataLista.DataSource = Napresentacao.BuscarNome(this.txtBuscar.Text);

            this.OcultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }
        private void FrmApresentacao_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Habilitar(false);
            this.botoes();
            this.Mostrar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.eEditar = false;
            this.botoes();
            this.Limpar();
            this.Habilitar(true);
            this.txtNomeProduto.Focus();
            this.txtIdApresentacao.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string resp = "";
            try
            {

                if (this.txtNomeProduto.Text == string.Empty)
                {
                    MensagemErro("Preencha todos os campos");
                    errorIcone.SetError(txtNomeProduto, "Insira o nome");

                }
                else
                {
                    if (this.eNovo)
                    {
                        resp = Napresentacao.Inserir(this.txtNomeProduto.Text.Trim().ToLowerInvariant(), this.txtDescricao.Text.Trim().ToLowerInvariant());
                    }
                    else
                    {
                        resp = Napresentacao.Editar(Convert.ToInt32(this.txtIdApresentacao.Text), this.txtNomeProduto.Text.Trim().ToLowerInvariant(), this.txtDescricao.Text.Trim().ToLowerInvariant());
                    }

                    if (resp.Equals("ok"))
                    {
                        if (this.eNovo)
                        {
                            this.MensagemOk("Registro salvo com Sucesso");
                        }
                        else
                        {
                            this.MensagemOk("Registro editado com sucesso");
                        }
                    }
                    else
                    {
                        this.MensagemErro(resp);
                    }
                }
                this.eNovo = false;
                this.eEditar = false;
                this.botoes();
                this.Limpar();
                this.Mostrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtIdApresentacao.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["idApresentacao"].Value);
            this.txtNomeProduto.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["nome"].Value);
            this.txtDescricao.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["Descricao"].Value);
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtIdApresentacao.Text.Equals(""))
            {
                this.MensagemErro("selecione um registro para inserir!!");
            }
            else
            {
                this.eEditar = true;
                this.botoes();
                this.Habilitar(true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.eNovo = false;
            this.eEditar = false;
            this.botoes();
            this.Habilitar(false);
            this.Limpar();
        }

        private void chkDeletar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeletar.Checked)
            {
                this.dataLista.Columns[0].Visible = true;
            }
            else
            {
                this.dataLista.Columns[0].Visible = false;
            }
        }

        private void dataLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataLista.Columns["Deletar"].Index)
            {
                DataGridViewCheckBoxCell chkDeleter = (DataGridViewCheckBoxCell)dataLista.Rows[e.RowIndex].Cells["Deletar"];
                chkDeleter.Value = !Convert.ToBoolean(chkDeleter.Value);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opc;
                opc = MessageBox.Show(" Realmente deseja apara esse/s registros", "Sitema comercio", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opc == DialogResult.OK)
                {
                    string Codigo;
                    string resp = "";

                    foreach (DataGridViewRow row in dataLista.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            resp = Napresentacao.Deletar(Convert.ToInt32(Codigo));

                            if (resp.Equals("ok"))
                            {
                                this.MensagemOk("Registro Excluido");
                            }
                            else
                            {
                                this.MensagemErro(resp);
                            }
                        }
                    }
                    this.Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            finally
            {
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtNomeProduto_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
