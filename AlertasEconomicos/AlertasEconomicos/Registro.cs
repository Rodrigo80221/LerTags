using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlertasEconomicos
{
    /// <summary>
    /// Esta classe possui funções para manipulação do registro do Windows.
    /// </summary>
    public static class Registro
    {

        /// <summary>
        /// Lê um valor do registro do Windows.
        /// </summary>
        /// <param name="local">H_Key de onde o valor será lido.</param>
        /// <param name="sistema">Chave de onde o valor será lido.</param>
        /// <param name="subChave">SubChave de onde o valor será lido.</param>
        /// <param name="campo">Campo de onde o valor será lido.</param>
        /// <param name="valorPadrao">Valor padrão caso o campo não exista.</param>
        /// <returns>Retorna uma string com o valor lido ou o valor padrão caso não encontre o campo.</returns>
        public static string Ler(RegistryKey local, string sistema, string subChave, string campo, string valorPadrao)
        {

            // cria uma referêcnia para a chave de registro Software
            var rk = local.OpenSubKey(sistema, true);
            if (rk == null)
            {
                return valorPadrao;
            }

            rk.CreateSubKey(subChave);

            // realiza a leitura do registro
            var lido = rk.OpenSubKey(subChave, true).GetValue(campo);

            var retorno = valorPadrao;

            if (lido != null)
            {
                if (!String.IsNullOrEmpty(lido.ToString()))
                    retorno = lido.ToString();
            }
            return retorno;
        }

        /// <summary>
        /// Grava um valor do registro do Windows.
        /// </summary>
        /// <param name="local">H_Key de onde o valor será gravado.</param>
        /// <param name="sistema">Chave de onde o valor será gravado.</param>
        /// <param name="subChave">SubChave de onde o valor será gravado.</param>
        /// <param name="campo">Campo de onde o valor será gravado.</param>
        /// <param name="valor">Valor que será gravado.</param>
        /// <returns>Retorna True caso consiga gravar ou False caso ocorra algum erro.</returns>
        public static bool Gravar(RegistryKey local, string sistema, string subChave, string campo, string valor)
        {
            //try
            //{
            // cria uma referêcnia para a chave de registro Software
            var rk = local.OpenSubKey(sistema, true);
            if (rk == null)
            {
                local.CreateSubKey(sistema);
                rk = local.OpenSubKey(sistema, true);
            }

            // cria um Subchave como o nome GravaRegistro
            rk = rk.CreateSubKey(subChave);

            // grava o caminho na SubChave GravaRegistro
            rk.SetValue(campo, valor);

            // fecha a Chave de Restistro registro
            rk.Close();

            return true;

            //}
            //catch (Exception ex)
            //{
            //    Outros.Universal.UltimoErro = ex.Message;
            //    return false;
            //}
        }

        public static string LerVB6(string grupo, string projeto, string chave, string valorPadrao)
        {
            return Ler(Registry.CurrentUser, "Software\\VB and VBA Program Settings", grupo + "\\" + projeto, chave, valorPadrao);
        }

        public static void GravarVB6(string grupo, string projeto, string chave, string valor)
        {
            Gravar(Registry.CurrentUser, "Software\\VB and VBA Program Settings", grupo + "\\" + projeto, chave, valor);
        }

    }

    }
