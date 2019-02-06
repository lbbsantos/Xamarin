using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            //TODO - Validações. 
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (String.IsNullOrEmpty(end.cep))
                    {
                        RESULTADO.Text = string.Format("O endereço não foi encontrado para o CEP informado: " + cep);
                    }
                    else
                    {
                        RESULTADO.Text = string.Format("CEP........: {0} \n" +
                                                       "Endereço...: {1} \n" +
                                                       "Complemento: {2} \n" +
                                                       "Bairro.....: {3} \n" +
                                                       "Cidade.....: {4} \n" +
                                                       "UF.........: {5} \n" +
                                                       "Código IBGE: {6}", end.cep, end.logradouro, end.complemento, end.bairro, end.localidade, end.uf, end.ibge);
                    }
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres", "OK");
                BOTAO.Focus();
                valido= false;
            }

            int NovoCEP = 0;
            if (!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve ser composto apenas por números", "OK");
                BOTAO.Focus();
                valido = false;
            }
            return valido;
        }
    }
}
