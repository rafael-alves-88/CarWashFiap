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

namespace CarWashFiap.View.Agendamento
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AgendamentoPage : Page
    {
        public AgendamentoPage()
        {
            this.InitializeComponent();
        }

        private void btnAgendar_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtVeiculo.Text.Length > 0 && this.txtPlaca.Text.Length > 0 && this.txtData.Text.Length > 0)
            {
                this.lblMensagem.Visibility = Visibility.Collapsed;
                if (this.Frame.CanGoBack) this.Frame.GoBack();
            } else
            {
                this.lblMensagem.Visibility = Visibility.Visible;
                this.lblMensagem.Text = "Por favor, preencha todos os campos";
            }
        }
    }
}
