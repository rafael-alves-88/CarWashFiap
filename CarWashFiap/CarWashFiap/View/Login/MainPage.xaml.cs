using CarWashFiap.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CarWashFiap
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            // Habilitar ao implementar API
            /*UsuarioModel userAuth = App.UsuarioVM.Usuarios.Where(
                a => a.NickName.ToLower() == txtUsuario.Text.ToLower() &&
                a.Senha == pwdSenha.Password).FirstOrDefault();

            if ((userAuth != null) && (!userAuth.Ativo))
            {
                var messageDialog = new MessageDialog(
                    "Usuário inativo", "Atenção");
                await messageDialog.ShowAsync();
            }
            else if (userAuth != null)
            {
                this.Frame.Navigate(typeof(HomeView), userAuth);
            }
            else
            {
                var messageDialog = new MessageDialog(
                    "Usuário inválido", "Atenção");
                await messageDialog.ShowAsync();
            }*/

            if (txtUsuario.Text == "fiap" && pwdSenha.Password == "123")
            {
                this.lblMensagem.Visibility = Visibility.Collapsed;
                this.Frame.Navigate(typeof(View.HomePage));
            } else
            {
                this.lblMensagem.Visibility = Visibility.Visible;
                this.lblMensagem.Text = "Usuário Inválido";
            }
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(View.Login.CadastrarPage));
        }
    }
}
