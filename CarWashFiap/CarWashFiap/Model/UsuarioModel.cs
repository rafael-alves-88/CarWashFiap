using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace CarWashFiap.Model
{
    public class UsuarioModel
    {
        public static readonly string USER_PATH = "Usuarios";

        public UsuarioModel() { }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public string NickName { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public string Situacao
        {
            get
            {
                if (Ativo) return "Ativo";
                else return "Inativo";
            }
        }
    }

    public static class UsuarioRepository
    {
        private static List<UsuarioModel> usuarioSqlAzure;

        public static async Task<List<UsuarioModel>> GetUsuarioSqlAzureAsync()
        {
            if (usuarioSqlAzure != null) return usuarioSqlAzure;

            var httpRequest = new HttpClient();
            var stream = await httpRequest.GetStreamAsync(
                Utils.AppConfig.BASE_API_PATH + UsuarioModel.USER_PATH);

            var usuarioSerializer = new DataContractJsonSerializer(typeof(List<UsuarioModel>));
            usuarioSqlAzure = (List<UsuarioModel>)usuarioSerializer.ReadObject(stream);

            return usuarioSqlAzure;
        }

        public static async Task<bool> DeleteUsuarioSqlAzureAsync(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id)) return false;

            var httpRequest = new HttpClient();
            httpRequest.BaseAddress = new Uri(Utils.AppConfig.BASE_API_PATH);
            httpRequest.DefaultRequestHeaders.Accept.Clear();
            httpRequest.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(Utils.AppConfig.API_TYPE_JSON));

            var response = await httpRequest.DeleteAsync(
                string.Format("api/" + UsuarioModel.USER_PATH + "/{0}", Id));

            if (response.IsSuccessStatusCode) return true;

            return false;
        }

        public static async Task<bool> PostUsuarioSqlAzureAsync(UsuarioModel userAdd)
        {
            if (userAdd == null) return false;

            var httpRequest = new HttpClient();
            httpRequest.BaseAddress = new Uri(Utils.AppConfig.BASE_API_PATH);
            httpRequest.DefaultRequestHeaders.Accept.Clear();
            httpRequest.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(Utils.AppConfig.API_TYPE_JSON));

            string usuarioJson = Newtonsoft.Json.JsonConvert.SerializeObject(userAdd);
            var response = await httpRequest.PostAsync("api/" + UsuarioModel.USER_PATH,
                new StringContent(usuarioJson, Encoding.UTF8, Utils.AppConfig.API_TYPE_JSON));

            if (response.IsSuccessStatusCode) return true;

            return false;
        }
    }
}
