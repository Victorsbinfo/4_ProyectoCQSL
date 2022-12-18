using Entidades.Usuarios;
using LogicaNegocio.Usuarios;
using ProyectoCSQL.Utilidades;
using System;
using System.Windows.Forms;

namespace ProyectoCSQL.Principal
{
    public partial class FrmUsuario : Form
    {
        private ClsUsuario ObjUsuario = null;
        private readonly ClsUsuarioLn ObjUsuarioLn= new ClsUsuarioLn();
        private ClsUtilidades ObjUtilidades = new ClsUtilidades(); 

        public FrmUsuario()
        {
            InitializeComponent();
            CargarListaUsuarios();
            LimpiarCampos();
        }

        private void ValidarCampos()
        {
            ObjUtilidades = new ClsUtilidades()
            {
                LstTxtBox = new System.Collections.Generic.List<TextBox>()
            };
            ObjUtilidades.LstTxtBox.Add(TxtNombre);
            ObjUtilidades.LstTxtBox.Add(TxtApellido1);

            ObjUtilidades.ValidarTextBox(ObjUtilidades.LstTxtBox);

        }

        private void LimpiarCampos()
        {
            TxtNombre.Text = string.Empty;
            TxtApellido1.Text = string.Empty;
            TxtApellido2.Text = string.Empty;
            DtpFechaNacimiento.Value = DateTime.Now;
            ChkEstado.Checked = true;
            LblIdUsuario.Text = string.Empty;

            BtnActualizar.Enabled = false;
            BtnEliminar.Enabled = false;
        }

        private void CargarListaUsuarios()
        {
            ObjUsuario = new ClsUsuario();
            ObjUsuarioLn.Index(ref ObjUsuario);
            if (ObjUsuario.MensajeError == null)
            {
                DgvUsuarios.DataSource = ObjUsuario.DtResultados;
                ObjUtilidades.FormatoDataGridView(ref DgvUsuarios);
                DgvUsuarios.Columns[0].DisplayIndex = DgvUsuarios.ColumnCount - 1;
            }
            else
            {
                MessageBox.Show(ObjUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAgregar_Click(object sender, System.EventArgs e)
        {
            ValidarCampos();

            if (ObjUtilidades.MensajeError == null)
            {
                ObjUsuario = new ClsUsuario()
                {
                    Nombre = TxtNombre.Text,
                    Apellido1 = TxtApellido1.Text,
                    Apellido2 = TxtApellido2.Text,
                    FechaNacimiento = DtpFechaNacimiento.Value,
                    Estado = ChkEstado.Checked
                };

                ObjUsuarioLn.Create(ref ObjUsuario);

                if (ObjUsuario.MensajeError == null)
                {
                    MessageBox.Show("El ID: " + ObjUsuario.ValorScalar + ", fue agregado correctamente");
                    CargarListaUsuarios();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(ObjUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show(ObjUtilidades.MensajeError.ToString(), "Mensaje Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
        }

        private void DgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DgvUsuarios.Columns[e.ColumnIndex].Name == "Editar")
                {
                    ObjUsuario = new ClsUsuario()
                    {
                        IdUsuario = Convert.ToByte(DgvUsuarios.Rows[e.RowIndex].Cells["IdUsuario"].Value.ToString())
                    };

                    LblIdUsuario.Text = ObjUsuario.IdUsuario.ToString();

                    ObjUsuarioLn.Read(ref ObjUsuario);
                    TxtNombre.Text = ObjUsuario.Nombre;
                    TxtApellido1.Text = ObjUsuario.Apellido1;
                    TxtApellido2.Text = ObjUsuario.Apellido2;
                    DtpFechaNacimiento.Value = ObjUsuario.FechaNacimiento;
                    ChkEstado.Checked = ObjUsuario.Estado;

                    BtnActualizar.Enabled = true;
                    BtnEliminar.Enabled = true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro que desea actualizar el registro: " + LblIdUsuario.Text + "?", "Mensaje del sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (respuesta == DialogResult.OK)
            {
                ObjUsuario = new ClsUsuario()
                {
                    IdUsuario = Convert.ToByte(LblIdUsuario.Text),
                    Nombre = TxtNombre.Text,
                    Apellido1 = TxtApellido1.Text,
                    Apellido2 = TxtApellido2.Text,
                    FechaNacimiento = DtpFechaNacimiento.Value,
                    Estado = ChkEstado.Checked
                };

                ObjUsuarioLn.Update(ref ObjUsuario);
                if (ObjUsuario.MensajeError == null)
                {
                    MessageBox.Show("El usuario fue actualizado correctamente");
                    CargarListaUsuarios();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(ObjUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar el registro: " + LblIdUsuario.Text+ "?","Mensaje del sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (respuesta == DialogResult.OK)
            {
                ObjUsuario = new ClsUsuario()
                {
                    IdUsuario = Convert.ToByte(LblIdUsuario.Text)
                };

                ObjUsuarioLn.Delete(ref ObjUsuario);

                CargarListaUsuarios();
                LimpiarCampos();
            }

        }


    }
}
