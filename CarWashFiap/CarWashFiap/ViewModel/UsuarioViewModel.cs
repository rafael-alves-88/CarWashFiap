using CarWashFiap.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace CarWashFiap.ViewModel
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<UsuarioModel> Usuarios { get; set; }
            = new ObservableCollection<UsuarioModel>();
        public List<UsuarioModel> FiltroUsuarios { get; set; } = new List<UsuarioModel>();
        private UsuarioModel usuarioSelecionado;
        public UsuarioModel UsuarioSelecionado
        {
            get { return usuarioSelecionado; }
            set
            {
                usuarioSelecionado = value;
                DeleteUsuarioCommando.DeleteCanExecuteChanged();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UsuarioSelecionado)));
            }
        }

        private string pesquisaNome;
        public string PesquisaNome
        {
            get { return pesquisaNome; }
            set
            {
                pesquisaNome = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PesquisaNome)));
                ExecutarFiltro();
            }
        }
        public DeleteUsuarioCommand DeleteUsuarioCommando { get; }

        public UsuarioViewModel()
        {
            DeleteUsuarioCommando = new DeleteUsuarioCommand(this);

            this.LoadedUsuario();
        }

        public async void LoadedUsuario()
        {
            FiltroUsuarios = await UsuarioRepository.GetUsuarioSqlAzureAsync();
            ExecutarFiltro();
        }

        public void AddUsuario(UsuarioModel param)
        {
            App.UsuarioVM.Usuarios.Add(param);
        }

        public void ExecutarFiltro()
        {
            if (pesquisaNome == null) pesquisaNome = "";
            var resultado = FiltroUsuarios.Where(n => n.Nome.ToLowerInvariant()
                                   .Contains(PesquisaNome.ToLowerInvariant().Trim())).ToList();

            var removerDaLista = Usuarios.Except(resultado).ToList();
            foreach (var item in removerDaLista)
            {
                Usuarios.Remove(item);
            }

            for (int index = 0; index < resultado.Count; index++)
            {
                var item = resultado[index];
                if (index + 1 > Usuarios.Count || !Usuarios[index].Equals(item))
                    Usuarios.Insert(index, item);
            }
        }

        public async void RemoverUsuarioAsync()
        {
            if (UsuarioSelecionado == null) return;

            var messageDialog = new MessageDialog(
                string.Format("Tem certeza que deseja remover o {0}?",
                UsuarioSelecionado.Nome),
                "Atenção");

            messageDialog.Commands.Add(new UICommand("Sim"));
            messageDialog.Commands.Add(new UICommand("Não"));

            var result = await messageDialog.ShowAsync();
            if (result.Label == "Sim")
            {
                if (await UsuarioRepository.DeleteUsuarioSqlAzureAsync(
                    UsuarioSelecionado.Id.ToString()))
                {
                    App.UsuarioVM.FiltroUsuarios.Remove(UsuarioSelecionado);
                    App.UsuarioVM.ExecutarFiltro();
                }
            }
        }
    }

    public class DeleteUsuarioCommand : ICommand
    {
        private UsuarioViewModel usuarioVM;
        public DeleteUsuarioCommand(UsuarioViewModel paramVM)
        {
            usuarioVM = paramVM;
        }

        public event EventHandler CanExecuteChanged;

        public void DeleteCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) => usuarioVM.UsuarioSelecionado != null;

        public void Execute(object parameter)
        {
            usuarioVM.RemoverUsuarioAsync();
        }
    }
}
