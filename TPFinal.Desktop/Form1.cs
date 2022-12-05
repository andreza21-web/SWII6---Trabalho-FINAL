using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPFinal.Desktop.Models;
using TPFinal.Desktop.Service;

namespace TPFinal.Desktop
{
    public partial class Form1 : Form
    {
        private UsuarioService _usuariosService;
        public Form1()
        {
            InitializeComponent();

            _usuariosService = new UsuarioService();

            carregarUsuarios();
        }

        private async void carregarUsuarios(string id = "")
        {
            try
            {
                List<Usuario> usuarios = new List<Usuario>();

                if (!string.IsNullOrEmpty(id))
                {
                    var usuario = await _usuariosService.GetUsuario(id);
                    usuarios.Add(usuario);
                }
                else
                {
                    usuarios = await _usuariosService.GetUsuarios();
                }

                carregarUsuariosList(usuarios);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void carregarUsuariosList(List<Usuario> usuarios)
        {

            var bindingList = new BindingList<Usuario>(usuarios);
            var source = new BindingSource(bindingList, null);
            dgUsuarios.DataSource = source;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            var idUsuario = txtIdUsuario.Text;

            carregarUsuarios(idUsuario);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
           UsuarioForm usuarioForm = new UsuarioForm(carregarUsuarios);
           usuarioForm.Show();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dgUsuarios.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var idUsuario = dgUsuarios.SelectedRows[0].Cells[0].Value.ToString();

                var usuario = await _usuariosService.GetUsuario(idUsuario);

                UsuarioForm usuarioForm = new UsuarioForm(carregarUsuarios, usuario);
                usuarioForm.Show();
            }
            else
            {
                MessageBox.Show("Selecione o usuario que deseja editar");
            }
        }

        private async void btnExcluir_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dgUsuarios.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var idUsuario = dgUsuarios.SelectedRows[0].Cells[0].Value.ToString();
                await _usuariosService.DeleteUsuario(idUsuario);

                carregarUsuarios();
            }
            else
            {
                MessageBox.Show("Selecione o usuario que deseja editar");
            }
        }
    }
}
