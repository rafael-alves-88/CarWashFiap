using CarWashFiap.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CarWashFiap.View.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CadastrarPage : Page
    {
        public CadastrarPage()
        {
            this.InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtUsuario.Text.Length > 0 && this.pwdSenha.Password.Length > 0 && txtNickname.Text.Length > 0)
            {
                this.lblMensagem.Visibility = Visibility.Collapsed;

                //Implementar API
                /*UsuarioModel userAuth = App.UsuarioVM.Usuarios.Where(
                a => a.NickName.ToLower() == txtUsuario.Text.ToLower()).FirstOrDefault();*/

                UsuarioModel userAuth = null;

                if (userAuth == null)
                {
                    UsuarioModel userAdd = new UsuarioModel();
                    userAdd.Nome = txtUsuario.Text;
                    userAdd.NickName = txtNickname.Text;
                    userAdd.Senha = pwdSenha.Password;
                    userAdd.Ativo = true;

                    //Implementar API
                    //App.UsuarioVM.AddUsuario(userAdd);
                    //Atualizar User Logado
                    if (this.Frame.CanGoBack) this.Frame.GoBack();
                    this.Frame.Navigate(typeof(HomePage));
                } else
                {
                    this.lblMensagem.Visibility = Visibility.Visible;
                    this.lblMensagem.Text = "Este usuário já existe, por favor, escolha um Nickname diferente.";
                }
            } else
            {
                this.lblMensagem.Visibility = Visibility.Visible;
                this.lblMensagem.Text = "Por favor, preencha todos os campos";
            }
        }
    }
}
