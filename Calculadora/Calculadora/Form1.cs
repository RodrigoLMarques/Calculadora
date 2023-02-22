/* *******************************************************************
 * Colegio Técnico Antônio Teixeira Fernandes (Univap)
 * Curso Técnico em Informática - Data de Entrega: 27/05/2022
 * Autores do Projeto: Rodrigo Lopes Marques
 *                     Gustavo Otacílio
 * Turma: 2F
 * Atividade Proposta em aula
 * Observação: <colocar se houver>
 * 
 * calculadora.cs
 * ************************************************************/ 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Calculadora
{
    public partial class Form1 : Form
    {
        // Váriaveis Globais
        double valor1 = 0, valor2 = 0; //Valores digitado pelo usuário
        string operacao = "";          //Tipo da operação digitado pelo usuário
        bool btnTrancado = true;       //Guarda se os buttons foram desabilitado por algum caso
        bool control = false;          //As váriaveis control 1, 2 adimistra o reset de alguns casos
        bool control2 = false;         
        bool control3 = false;         //Control3 para se está usando grau ou radiano para cálculos trigonométricos

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            txtResultado.Text = "0";
        }

        // Configuração
        private void C_Click(object sender, EventArgs e)
        {
            VericarBtn(sender);
            txtResultado.Text = "0";
            txtSaida.Text = "";
            txtGrauRad.Text = "grau";
            operacao = "";
            valor1 = 0;
            valor2  = 0;
            control = false;
            control2 = false;
            control3 = false;    
        }

        private void CE_Click(object sender, EventArgs e)
        {
            txtResultado.Text = "0";
            control2 = false;
        }

        private void Excluir_Click(object sender, EventArgs e)
        {
            VericarBtn(sender);
            string texto = txtResultado.Text;
            int tamanho = texto.Length;
            if (tamanho > 1)
            {
                txtResultado.Text = texto.Remove(tamanho - 1);
            }
            else
            {
                txtResultado.Text = "0";
            }
        }

        //Função para que desabilite ou habilite alguns botões
        private void TrancarBotoes(object sender, bool opcao)
        {
            Mais.Enabled     = opcao;
            Menos.Enabled    = opcao;
            Vezes.Enabled    = opcao;
            Divisao.Enabled  = opcao;
            Decimal.Enabled  = opcao;
            CE.Enabled       = opcao;
            Pi.Enabled       = opcao;
            Seno.Enabled     = opcao;
            Cosseno.Enabled  = opcao;
            Tangente.Enabled = opcao;
            UmPorX.Enabled   = opcao;
            RaizQuad.Enabled = opcao;
            RaizCubo.Enabled = opcao;
            RaizY.Enabled    = opcao;
            Mod.Enabled      = opcao;
            Log10.Enabled    = opcao;
            Fatorial.Enabled = opcao;
            Porcentagem.Enabled  = opcao;
            InverteSinal.Enabled = opcao;
            ElevadoQuad.Enabled  = opcao;
            ElevadoCubo.Enabled  = opcao;
            ElevadoY.Enabled     = opcao;
            DezElevadoX.Enabled  = opcao;
            grauAndRad.Enabled   = opcao;
            if (opcao == true)
            {
                C_Click(sender, new EventArgs());
            }
        }

        //Funçao para verificar se o botão está desabilitado
        private void VericarBtn(object sender)
        {
            if (btnTrancado == false)
            {
                btnTrancado = true;
                TrancarBotoes(sender, btnTrancado);
            }
        }

        // Operadores Básicos
        private void Mais_Click(object sender, EventArgs e)
        {
            if (valor1 != 0 && control2 == false)
            {
                Igual_Click(sender, new EventArgs());
            }

            valor1 = double.Parse(txtResultado.Text);
            txtSaida.Text = valor1.ToString() + "+";
            txtResultado.Text = "0";
            operacao = "soma";
            control = false;
            control2 = true;
        }

        private void Menos_Click(object sender, EventArgs e)
        {
            if (valor1 != 0 && control2 == false)
            {
                Igual_Click(sender, new EventArgs());
            }

            valor1 = double.Parse(txtResultado.Text);
            txtSaida.Text = valor1.ToString() + "-";
            txtResultado.Text = "0";
            operacao = "sub";
            control = false;
            control2 = true; 
        }

        private void Vezes_Click(object sender, EventArgs e)
        { 
            if (valor1 != 0 && control2 == false)
            {
                Igual_Click(sender, new EventArgs());
            }

            valor1 = double.Parse(txtResultado.Text);
            txtSaida.Text = valor1.ToString() + "*";
            txtResultado.Text = "0";
            operacao = "mult";
            control = false;
            control2 = true;
        }

        private void Divisao_Click(object sender, EventArgs e)
        {

            if (valor1 != 0 && control2 == false)
            {
                Igual_Click(sender, new EventArgs());
            }

            valor1 = double.Parse(txtResultado.Text);
            txtSaida.Text = valor1.ToString() + "/";
            txtResultado.Text = "0";
            operacao = "divi";
            control = false;
            control2 = true;

        }

        // Formata a saida nas casas de decimais corretas, e de milhar
        private string FormataNumero(object sender, double num)
        {
            if (double.IsInfinity(num))
            {
                btnTrancado = false;
                TrancarBotoes(sender, btnTrancado);
                return "Estouro";
            }
            else
            {  
                string texto = num.ToString();
                int tamanho = texto.Length;
                int x = 0;
                if ((num % 1 != 0) && texto[tamanho - 1] != ',')
                {
                    x = (texto.Split(','))[1].Length;
                }
                    
                string formato = "";
                for (int i = 0; i < x; i++)
                {
                    formato += "#";
                }
                formato = "{0:#,#." + formato + "}";
                formato = string.Format(formato, num);
                formato = Math.Truncate(num) == 0 ? "0" + formato : formato;
                return formato;
            }
        }

        //Função "Igual", realiza operações que necessita de 2 valores informados pelo usuário
        private void Igual_Click(object sender, EventArgs e)
        {
            VericarBtn(sender);
            if (operacao != "")
            {
                double res = 0;
                valor2 = double.Parse(txtResultado.Text);
                
                if (control == false)
                {
                    txtSaida.Text += valor2.ToString();
                }
                txtSaida.Text += "=";

                if (operacao == "soma")
                {
                    res = valor1 + valor2;
                }
                else if (operacao == "sub")
                {
                    res = valor1 - valor2;
                }
                else if (operacao == "mult")
                {
                    res = valor1 * valor2;
                }
                else if (operacao == "divi")
                {
                    if (valor2 == 0)
                    {
                        txtResultado.Text = "Não é possível dividir por zero";
                        btnTrancado = false;
                        TrancarBotoes(sender, btnTrancado);
                    }
                    else
                    {
                        res = valor1 / valor2;
                    }
                }
                else if (operacao == "mod")
                {
                    res = valor1 % valor2;
                }
                else if (operacao == "elevadoY")
                {
                    res = Math.Pow(valor1, valor2);
                }
                else if (operacao == "raizY")
                {
                    res = Math.Pow(valor1, 1 / valor2);
                }
                else if (operacao == "porcent")
                {
                    res = (valor1 / 100) * valor2;
                }

                if (btnTrancado == true)
                {
                    txtResultado.Text = FormataNumero(sender, res);
                }

                valor2 = 0;
                control = true;
                operacao = "";
            }
        }

        // Potência ao Quadrado
        private void ElevadoQuad_Click(object sender, EventArgs e)
        {
            double aux, res;
            aux = double.Parse(txtResultado.Text);
            res = Math.Pow(aux, 2);
            if (control == false)
            {
                txtSaida.Text += aux + "²";
            }
            else
            {
                txtSaida.Text = aux + "²";
            }
            txtResultado.Text = FormataNumero(sender, res);
            control = true;
        }

        // Potência ao Cubo
        private void ElevadoCubo_Click(object sender, EventArgs e)
        {
            double aux, res;
            aux = double.Parse(txtResultado.Text);
            res = Math.Pow(aux, 3);
            if (control == false)
            {
                txtSaida.Text += aux + "³";
            }
            else
            {
                txtSaida.Text = aux + "³";
            }
            txtResultado.Text = FormataNumero(sender, res);
            control = true;
        }

        // Potência ao Y
        private void ElevadoY_Click(object sender, EventArgs e)
        {
            if (valor1 != 0 && control2 == false)
            {
                Igual_Click(sender, new EventArgs());
            }

            valor1 = double.Parse(txtResultado.Text);  
            txtSaida.Text = txtResultado.Text + "^";   
            operacao = "elevadoY";
            txtResultado.Text = "0";
            control = false;
            control2 = true;
        }

        //Raiz Quadrada
        private void RaizQuad_Click(object sender, EventArgs e)
        {
            double aux, res;
            aux = double.Parse(txtResultado.Text);
            if (control == false)
            {
                txtSaida.Text += "√(" + aux + ")";
            }
            else
            {
                txtSaida.Text = "√(" + aux + ")";
            }
            res = Math.Sqrt(aux);
            txtResultado.Text = FormataNumero(sender, res);
            control = true;
        }

        //Raiz Cubica
        private void RaizCubo_Click(object sender, EventArgs e)
        {
            double aux, res;
            aux = double.Parse(txtResultado.Text);
            if (control == false)
            {
                txtSaida.Text += "³√(" + aux + ")";
            }
            else
            {
                txtSaida.Text = "³√(" + aux + ")";
            }
            res = Math.Cbrt(aux);
            txtResultado.Text = FormataNumero(sender, res);
            control = true;
        }

        //Raiz Y
        private void RaizY_Click(object sender, EventArgs e)
        {
            if (valor1 != 0 && control2 == false)
            {
                Igual_Click(sender, new EventArgs());
            }

            valor1 = double.Parse(txtResultado.Text);
            txtSaida.Text = "ʸ√(" + valor1 + ") y -> ";
            txtResultado.Text = "0";
            operacao = "raizY";
            control = false;
            control2 = true;
        }

        // Fatorial
        private void Fatorial_Click(object sender, EventArgs e)
        {
            double aux, res;
            aux = double.Parse(txtResultado.Text);

            res = 1;
            for (int i = 2; i <= aux; i++)
            {
                res *= i;
            }

            if (control == false)
            {
                txtSaida.Text += aux + "!";
            }
            else
            {
                txtSaida.Text = aux + "!";
            }
            txtResultado.Text = FormataNumero(sender, res);
            control = true;
        }
        
        //Resto de uma divisão
        private void Mod_Click(object sender, EventArgs e)
        {
            if (valor1 != 0 && control2 == false)
            {
                Igual_Click(sender, new EventArgs());
            }
            valor1 = double.Parse(txtResultado.Text);
            txtSaida.Text = valor1 + " mod ";
            txtResultado.Text = "0";
            operacao = "mod";
            control = false;
            control2 = true;
        }

        // Porcentagem
        private void Porcentagem_Click(object sender, EventArgs e) // Terminar de configurar
        {
            if (valor1 != 0 && control2 == false)
            {
                Igual_Click(sender, new EventArgs());
            }

            valor1 = double.Parse(txtResultado.Text);
            txtSaida.Text = valor1.ToString() + "% de ";
            txtResultado.Text = "0";
            operacao = "porcent";
            control = false;
            control2 = true;
        }

        // Um dividido por X
        private void UmPorX_Click(object sender, EventArgs e)
        {
            double aux, res;
            aux = double.Parse(txtResultado.Text);
            if (aux != 0)
            {
                if (control == false)
                {
                    txtSaida.Text += "1/(" + aux + ")";
                }
                else
                {
                    txtSaida.Text = "1/(" + aux + ")";
                }
                res = 1 / aux;
                txtResultado.Text = FormataNumero(sender, res);
                control = true;
            }
            else
            {
                txtSaida.Text = "1/(0)";
                txtResultado.Text = "Não é possível dividir por zero";
                btnTrancado = false;
                TrancarBotoes(sender, btnTrancado);
            }
        }

        // PI
        private void Pi_Click(object sender, EventArgs e)
        {
            txtResultado.Text = (Math.PI).ToString();
        }

        private void InverteSinal_Click(object sender, EventArgs e)
        {
            double aux, res;
            if (txtResultado.Text != "0")
            {
                aux = double.Parse(txtResultado.Text);
                res = aux * (-1);
                txtResultado.Text = FormataNumero(sender, res);
            }
        }

        private void DezElevadoX_Click(object sender, EventArgs e)
        {
            double aux, res;
            aux = double.Parse(txtResultado.Text);
            res = Math.Pow(10, aux);
            if (control == false)
            {
                txtSaida.Text += "10^(" + aux + ")";
            }
            else
            {
                txtSaida.Text = "10^(" + aux + ")";
            }
            txtResultado.Text = FormataNumero(sender, res);
            control = true;
        }

        private void Log10_Click(object sender, EventArgs e)
        {
            double aux, res;
            aux = double.Parse(txtResultado.Text);
            if (control == false)
            {
                txtSaida.Text += "log10(" + aux + ")";
            }
            else
            {
                txtSaida.Text = "log10(" + aux + ")";
            }

            if (aux < 1)
            {
                txtResultado.Text = "Entrada Inválida";
                btnTrancado = false;
                TrancarBotoes(sender, btnTrancado);
            }
            else
            {
                res = Math.Log10(aux);
                txtResultado.Text = FormataNumero(sender, res);
                control = true;
            }
        }

        // Faz a troca do uso de grau ou radiano em operações trigonométricas
        private void grauAndRad_Click(object sender, EventArgs e)
        {
            if (control3 == false)
            {
                txtGrauRad.Text = "rad";
                control3 = true;
            }
            else
            {
                txtGrauRad.Text = "grau";
                control3 = false;
            }
        }

        private void Seno_Click(object sender, EventArgs e)
        {
            double aux, res;
            aux = double.Parse(txtResultado.Text);
            if (control3 == false)
            {
                res = Math.Sin(aux * (Math.PI / 180));
            }
            else
            {
                res = Math.Sin(aux);
            }
            if (control == false)
            {
                txtSaida.Text += "sin(" + aux + ")";
            }
            else
            {
                txtSaida.Text = "sin(" + aux + ")";
            }
            txtResultado.Text = FormataNumero(sender, res);
            control = true;
        }

        private void Cosseno_Click(object sender, EventArgs e)
        {
            double aux, res;
            aux = double.Parse(txtResultado.Text);
            if (control3 == false)
            {
                res = Math.Cos(aux * (Math.PI / 180));
            }
            else
            {
                res = Math.Cos(aux);
            }
            if (control == false)
            {
                txtSaida.Text += "cos(" + aux + ")";
            }
            else
            {
                txtSaida.Text = "cos(" + aux + ")";
            }
            txtResultado.Text = FormataNumero(sender, res);
            control = true;
        }

        private void Tangente_Click(object sender, EventArgs e)
        {
            double aux, res;
            aux = double.Parse(txtResultado.Text);
            if (control == false)
            {
                txtSaida.Text += "tan(" + aux + ")";
            }
            else
            {
                txtSaida.Text = "tan(" + aux + ")";
            }

            if (aux == 90 || aux == -90)
            {
                txtResultado.Text = "Entrada Inválida";
                btnTrancado = false;
                TrancarBotoes(sender, btnTrancado);
            }
            else
            {
                if (control3 == false)
                {
                    res = Math.Tan(aux * (Math.PI / 180));
                }
                else
                {
                    res = Math.Tan(aux);
                }
                txtResultado.Text = FormataNumero(sender, res);
                control = true;
            }
        }

        // Decimal
        private void Decimal_Click(object sender, EventArgs e)
        {
            VericarBtn(sender);
            if (control2 == true)
            {
                CE_Click(sender, new EventArgs());
            }
            else if (control == true)
            {
                C_Click(sender, new EventArgs());
            }

            double aux;
            string texto = txtResultado.Text;
            aux = double.Parse(texto);

            int tamanho = txtResultado.Text.Length;
            if ((aux % 2 == 0 || aux % 2 == 1) && texto[tamanho-1] != ',')
            {
                txtResultado.Text += ",";
            }
        }
        
        // Números de 0 até 9
        private void ImprimirNum(object sender, string numero)
        {
            VericarBtn(sender);
            if (control2 == true)
            {
                CE_Click(sender, new EventArgs());
            }
            else if (control == true)
            {
                C_Click(sender, new EventArgs());
            }
            double aux = double.Parse(txtResultado.Text + numero);
            txtResultado.Text = FormataNumero(sender, aux);
            control = false;
        }

        private void n0_Click(object sender, EventArgs e)
        {
            ImprimirNum(sender, "0");
        }

        private void n1_Click(object sender, EventArgs e)
        {
            ImprimirNum(sender, "1");
        }

        private void n2_Click(object sender, EventArgs e)
        {
            ImprimirNum(sender, "2");
        }

        private void n3_Click(object sender, EventArgs e)
        {
            ImprimirNum(sender, "3");
        }

        private void n4_Click(object sender, EventArgs e)
        {
            ImprimirNum(sender, "4");
        }

        private void n5_Click(object sender, EventArgs e)
        {
            ImprimirNum(sender, "5");
        }

        private void n6_Click(object sender, EventArgs e)
        {
            ImprimirNum(sender, "6");
        }

        private void n7_Click(object sender, EventArgs e)
        {
            ImprimirNum(sender, "7");
        }

        private void n8_Click(object sender, EventArgs e)
        {
            ImprimirNum(sender, "8");
        }

        private void n9_Click(object sender, EventArgs e)
        {
            ImprimirNum(sender, "9");
        }
    }
}
