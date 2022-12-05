using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TPFinal.Desktop.Models;
using TPFinal.Desktop.Service;

namespace TPFinal.Desktop
{
    public partial class UsuarioForm : Form
    {
        private bool isEdicao;
        private UsuarioService _usuariosService;
        private Action<string> reloadUsuarios;

        public UsuarioForm(Action<string> carregarUsuarios,
                           Usuario usuario = null)
        {
            InitializeComponent();

            this.reloadUsuarios = carregarUsuarios;
            this.isEdicao = usuario != null;
            this._usuariosService = new UsuarioService();

            if (isEdicao) carregarCamposUsuario(usuario);
            else esconderCampoId();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                var user = getUsuario();

                if (isEdicao)
                    await _usuariosService.PutUsuario(user);
                else
                    await _usuariosService.PostUsuario(user);


                reloadUsuarios.Invoke("");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void esconderCampoId() {
            lblId.Visible = false;
            txtId.Visible = false;
        }
        private void carregarCamposUsuario(Usuario usuario)
        {
            txtId.Text = usuario.Id.ToString();
            txtNome.Text = usuario.Nome;
            txtSenha.Text = usuario.Senha;

            if (usuario.Status)
                rdAtivo.Checked = true;
            else
                rdInativo.Checked = true;
        }
        private Usuario getUsuario()
        {
            var user = new Usuario();

            if (string.IsNullOrEmpty(txtNome.Text))
                throw new Exception("Preencha o campo Nome");

            if (string.IsNullOrEmpty(txtSenha.Text))
                throw new Exception("Preencha o campo Senha");

            if (!rdAtivo.Checked && !rdInativo.Checked)
                throw new Exception("Escolha o status do usuario ");

            user.Nome = txtNome.Text;
            user.Senha = txtSenha.Text;
            user.Status = rdAtivo.Checked;

            if (isEdicao) user.Id = int.Parse(txtId.Text);


            return user;
        }
    }
}
