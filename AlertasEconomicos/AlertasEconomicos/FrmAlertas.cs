using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using HtmlAgilityPack;
using System.Threading;

namespace AlertasEconomicos
{
    public partial class FrmAlertas : Form
    {
        public enum Site
        {
            Investing = 0,
            MarketWach,
            Sino
        }

        public FrmAlertas()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
    

        }



        private void CarregarCotacoesFrame(string endereco,
            Label lblCotacao,
            Label lblVar,
            Label lblLog)
        {

            var web = new HtmlWeb();
            var html = web.Load(endereco);

            try
            {

                lblCotacao.Tag = lblCotacao.Text;

                // cotação
                lblCotacao.Text = BuscarCotacao(html);

                // variação
                lblVar.Text = BuscarVariacao(html);

                AtualizarCorVariacaoPercentual(lblVar);

            }
            catch (Exception)
            {

            }


            // log atualização         
            AtualizarLog(lblCotacao,lblLog);

        }



        private string BuscarCotacao(HtmlAgilityPack.HtmlDocument html)
        {
            try
            {
                return html.DocumentNode.SelectSingleNode("//*[@id='last_last']").InnerHtml;
            }
            catch (Exception)
            {

                try
                {
                    return html.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div/div/div[2]/main/div/div[1]/div[2]/div[1]/span").InnerHtml;
                }
                catch (Exception)
                {

                    return "0";
                }
            }
           
        }

        private string BuscarVariacao(HtmlAgilityPack.HtmlDocument html)
        {
           return BuscarVariacao(html, Site.Investing);
        }

        private string BuscarVariacao(HtmlAgilityPack.HtmlDocument html, Site site)
        {

            string cotacao = "";

            try
            {                
                List<string> possiveisXPath = new List<string>();

                if (site == Site.Investing)
                {
                    possiveisXPath.Add("/html/body/div[1]/div[2]/div/div/div[2]/main/div/div[1]/div[2]/div[1]/div[2]/span[2]/text()[3]");
                    possiveisXPath.Add("/html/body/div[1]/div[2]/div/div/div[2]/main/div/div[1]/div[2]/div[1]/div[2]/span[2]/text()[2]");
                    possiveisXPath.Add("//*[@id='quotes_summary_current_data']/div[1]/div[1]/div[1]/div[2]/span[4]/text()");
                    possiveisXPath.Add("//*[@id='__next']/div/div/div[2]/main/div/div[1]/div[2]/div[1]/div[2]/span[2]/text()[2]");
                    possiveisXPath.Add("//*[@id='__next']/div/div/div[2]/main/div/div[1]/div[2]/div[1]/div[2]/span[2]/text()[3]");
                    possiveisXPath.Add("//*[@id='__next']/div/div/div[2]/main/div/div[1]/div[2]/div[1]/div[2]/span[1]");                    
                }

                if (site == Site.MarketWach)
                {
                    possiveisXPath.Add("//*[@id='maincontent']/div[2]/div[3]/div/div[2]/bg-quote/span[2]/bg-quote");
                }

                if (site == Site.Sino)
                {
                    possiveisXPath.Add("//*[@id='futures-code-month-I2201']/td[3]/span/font/font");
                    possiveisXPath.Add("/html/body/div[1]/div[2]/div[2]/table/tbody/tr/td[2]/div[1]/div/table/tbody/tr[1]/td[3]/span");
                    possiveisXPath.Add("//*[@id='futures-code-month-I0']/td[3]/span/font/font");
                }

                foreach (var xPath in possiveisXPath)
                {
                    cotacao = BuscarInnerHtml(html, xPath);

                    cotacao = cotacao.Replace("<!-- -->", "");

                    if (!string.IsNullOrEmpty(cotacao) && cotacao.Length < 15 && cotacao != "+" && cotacao != "-" && cotacao != "%)")
                        return (cotacao.Contains("%") ? cotacao : cotacao + "%");
                    else
                    {
                        /*
                        if (site == Site.Sino)
                        {
                            if (string.IsNullOrEmpty(cotacao))
                                lblError.Text += Environment.NewLine + "Retornou Vazio";
                            else
                                lblError.Text += Environment.NewLine + cotacao;
                        }
                        */
                    }
                }

                return "";
            }
            catch (Exception e)
            {
    
                //lblError.Text += Environment.NewLine + e.Message + " " + cotacao;
                throw;
            }
     
        }

        private string BuscarStatus(HtmlAgilityPack.HtmlDocument html)
        {
            string status = "";

            List<string> possiveisXPath = new List<string>();
            possiveisXPath.Add("//*[@id='quotes_summary_current_data']/div[1]/div[1]/div[2]/span[2]");
            possiveisXPath.Add("//*[@id='__next']/div/div/div[2]/main/div/div[1]/div[2]/div[2]/div[2]/time");

            foreach (var xPath in possiveisXPath)
            {
                status = BuscarInnerHtml(html, xPath);

                if (status != "" && status.Length < 15)
                    return status;
            }

            return "";
        }

        private string BuscarAnaliseTecnica(HtmlAgilityPack.HtmlDocument html)
        {
            string analiseTecnica = "";

            List<string> possiveisXPath = new List<string>();
            possiveisXPath.Add("//*[@id='leftColumn']/table[2]/tbody/tr[3]/td[4]");
            possiveisXPath.Add("//*[@id='leftColumn']/table[1]/tbody/tr[3]/td[4]");
            possiveisXPath.Add("//*[@id='__next']/div/div/div[2]/main/div/div[5]/div/div[7]/div/div/div/table/tbody/tr[3]/td[5]");
            possiveisXPath.Add("//*[@id='__next']/div/div/div[2]/main/div/div[5]/div/div[9]/div/div/div/table/tbody/tr[3]/td[5]");


            foreach (var xPath in possiveisXPath)
            {
                analiseTecnica = BuscarInnerHtml(html, xPath);

                if (analiseTecnica != "" && analiseTecnica.Length < 15)
                    return analiseTecnica;                
            }
                
            return "";

        }

        public static string BuscarInnerHtml(HtmlAgilityPack.HtmlDocument html, string xPath)
        {
            try
            {
                return html.DocumentNode.SelectSingleNode(xPath).InnerHtml;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void AtualizarCorVariacaoPercentual(Label label)
        {
            if (string.IsNullOrEmpty(label.Text))
                return;

            if (Convert.ToDecimal(label.Text.Replace("%", "").Replace(" ", "").ToString()) > 0)
                label.ForeColor = System.Drawing.Color.Lime;
            else
                label.ForeColor = System.Drawing.Color.Red;
        }

        private void AtualizarLog(Label lblCotacao, Label lblLog)
        {
            /*
            if (!lblCotacao.Tag.Equals(lblCotacao.Text))
                lblLog.Text = "Últ Atu. " + lblHora.Text;
                */
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (!System.Diagnostics.Debugger.IsAttached)
                    this.TopMost = true;                                                          

               // trmCotacoes_Tick(null, null);
               /*
                lblHora.Text = DateTime.Now.ToString("HH:mm:ss");

                mtbFuturos.Text = ConfigurationManager.AppSettings["HoraBovespaFuturos"];
                mtbHoraBovespa.Text = ConfigurationManager.AppSettings["HoraBovespa"];
                mtbHoraSP500.Text = ConfigurationManager.AppSettings["HoraSP500"];
                */

                var width = ConfigurationManager.AppSettings["FormWidth"];
                var Height = ConfigurationManager.AppSettings["FormHeight"];
                var locationX = ConfigurationManager.AppSettings["LocationX"];
                var locationY = ConfigurationManager.AppSettings["LocationY"];


                this.Size = new Size(Convert.ToInt16(width), Convert.ToInt16(Height));

                var posicao = new System.Drawing.Point();
                posicao.X = Convert.ToInt16(locationX);
                posicao.Y = Convert.ToInt16(locationY);

                this.Location = posicao;

                this.BackColor = System.Drawing.Color.FromArgb(51, 51, 51);

                FrmAlertas_ResizeEnd(null, null);

            }
            catch (Exception)
            {


            }

        }

        private void tmrHorario_Tick(object sender, EventArgs e)
        {

            DateTime timeUtc = DateTime.UtcNow;
            /*
            var kstZoneUS = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            lblHoraUSA.Text = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZoneUS).ToString("HH:mm:ss");

            var kstZoneEuropa = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            lblHoraEuropa.Text = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZoneEuropa).ToString("HH:mm:ss");

            var kstZoneTokyo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            lblHoraTokyo.Text = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZoneTokyo).ToString("HH:mm:ss");


            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            */

            var hora = new DateTime();
            hora = DateTime.Now;

            var emAlerta = false;

            foreach (Control c in this.Controls)
            {
      
                if (c is MaskedTextBox)
                {
                    try
                    {
                        if ((DateTime.Parse(c.Text) >= hora.AddMinutes(-2)) && (DateTime.Parse(c.Text) <= hora.AddMinutes(2)))
                            emAlerta = true;
                    }
                    catch (Exception)
                    { }

                }

            }

            if (emAlerta)
                {
                    var corFundo = this.BackColor;

                    if (corFundo == System.Drawing.Color.FromArgb(51, 51, 51))
                        this.BackColor = System.Drawing.Color.Red;

                    else if (corFundo == System.Drawing.Color.Red)
                        this.BackColor = System.Drawing.Color.FromArgb(51, 51, 51);

                Thread t = new Thread(new ThreadStart(System.Media.SystemSounds.Hand.Play));
                t.Start();

                }
                else
                    this.BackColor = System.Drawing.Color.FromArgb(51,51,51);          

        }

        private void txtHoraBovespa_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void FrmAlertas_FormClosed(object sender, FormClosedEventArgs e)
        {

            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            /*
            settings["HoraBovespa"].Value = mtbHoraBovespa.Text;
            settings["HoraSP500"].Value = mtbHoraSP500.Text;
            settings["HoraBovespaFuturos"].Value = mtbFuturos.Text;
            */
            settings["FormWidth"].Value = this.Width.ToString();
            settings["FormHeight"].Value = this.Height.ToString();

            if (this.Location.X > 0)
                settings["LocationX"].Value = this.Location.X.ToString();
            else
                settings["LocationX"].Value = "0";

            if (this.Location.Y > 0)
                settings["LocationY"].Value = this.Location.Y.ToString();
            else
                settings["LocationY"].Value = "0";

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

        }

        private void FrmAlertas_ResizeEnd(object sender, EventArgs e)
        {


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        /*
       private void AtualizarCotacaoSP500()
       {

           CarregarCotacoesFrame("https://br.investing.com/indices/us-spx-500-futures",
               lblSP500Fut_pts,
               lblSP500Fut_var,
               lblSP500Fut_log);
       }

       private void AtualizarCotacaoDX()
       {
           CarregarCotacoesFrame("https://br.investing.com/currencies/us-dollar-index",
               lblDX_pts,
               lblDX_var,
               lblDX_Log);
       }

       private void AtualizarCotacao10Anos()
       {
           CarregarCotacoesFrame("https://br.investing.com/rates-bonds/u.s.-10-year-bond-yield",
               lbl10Anos_Pts,
               lbl10Anos_Var,
               lbl10Anos_Status,
               lbl10Anos_Analise,
               lbl10Anos_Log);
       }

       private void AtualizarCotacaosp500vix()
       {
           CarregarCotacoesFrame("https://br.investing.com/indices/volatility-s-p-500",
               lblSP500Vix_Pts,
               lblSP500Vix_Var,
               lblSP500Vix_Status,
               lblSP500Vix_Analise,
               lblSP500Vix_Log);
       }
     
        private void AtualizarCotacaoPetroleoWTI()
        {
            CarregarCotacoesFrame("https://br.investing.com/commodities/crude-oil",
                lblPetroleoWTI_Pts,
                lblPetroleoWTI_Var,
                lblPetroleoWTI_Log);
        }
        
        private void AtualizarCotacaoOuro()
        {
            CarregarCotacoesFrame("https://br.investing.com/commodities/gold",
                lblOuro_Pts,
                lblOuro_Var,
                lblOuro_Status,
                lblOuro_Analise,
                lblOuro_Log);
        }
        
        private void AtualizarCotacaoMinerio()
        {
            CarregarCotacoesFrame("https://br.investing.com/commodities/iron-ore-62-cfr-futures",
                lblMinerio_Pts,
                lblMinerio_Var,
                lblMinerio_Status,
                lblMinerio_Analise,
                lblMinerio_Log);
        }

        private void AtualizarCotacaoBitcoin()
        {
            CarregarCotacoesFrame("https://br.investing.com/crypto/bitcoin/bitcoin-futures",
                lblBTC_Pts,
                lblBTC_Var,
                lblBTC_Status,
                lblBTC_Analise,
                lblBTC_Log);
        }



        private void AtualizarVariacaoUS()
        {
            var web = new HtmlWeb();

            var html = web.Load("https://www.investing.com/indices/us-30");
            lblVariacaoDowJones.Text = BuscarVariacao(html);
            AtualizarCorVariacaoPercentual(lblVariacaoDowJones);

            html = web.Load("https://www.investing.com/indices/us-spx-500");
            lblVariacaoSP500.Text = BuscarVariacao(html);
            AtualizarCorVariacaoPercentual(lblVariacaoSP500);

            html = web.Load("https://www.investing.com/indices/nasdaq-composite");
            lblVariacaoNasdaq.Text = BuscarVariacao(html);
            AtualizarCorVariacaoPercentual(lblVariacaoNasdaq);
        }

        private void AtualizarVariacaoEUR()
        {
            var web = new HtmlWeb();

            var html = web.Load("https://www.investing.com/indices/uk-100");
            lblVariacaoFTSE100.Text = BuscarVariacao(html);
            AtualizarCorVariacaoPercentual(lblVariacaoFTSE100);

            html = web.Load("https://www.investing.com/indices/france-40");
            lblVariacaoCAC40.Text = BuscarVariacao(html);
            AtualizarCorVariacaoPercentual(lblVariacaoCAC40);

            html = web.Load("https://www.investing.com/indices/germany-30");
            lblVariacaoDAX.Text = BuscarVariacao(html);
            AtualizarCorVariacaoPercentual(lblVariacaoDAX);
        }

        private void AtualizarVariacaoASIA()
        {
            var web = new HtmlWeb();

            var html = web.Load("https://www.investing.com/indices/japan-ni225");
            lblVariacaoNIKKEI.Text = BuscarVariacao(html);
            AtualizarCorVariacaoPercentual(lblVariacaoNIKKEI);

            html = web.Load("https://www.investing.com/indices/kospi");
            lblVariacaoKOSPI.Text = BuscarVariacao(html);
            AtualizarCorVariacaoPercentual(lblVariacaoKOSPI);

            html = web.Load("https://www.investing.com/indices/hang-sen-40");
            lblVariacaoHangSeng.Text = BuscarVariacao(html);
            AtualizarCorVariacaoPercentual(lblVariacaoHangSeng);
        }

        private void AtualizarVariacaoOutros()
        {
            var web = new HtmlWeb();
          var html = web.Load("https://br.investing.com/indices/bloomberg-commodity");
            lblVariacaoCommodityBloomberg.Text = BuscarVariacao(html);
            AtualizarCorVariacaoPercentual(lblVariacaoCommodityBloomberg);

            
            html = web.Load("https://www.marketwatch.com/investing/fund/ewz");
            lblVariacaoEWZ.Text = BuscarVariacao(html,Site.MarketWach);
            AtualizarCorVariacaoPercentual(lblVariacaoEWZ);
            /*
            try
            {
                html = web.Load("https://finance.sina.com.cn/futures/quotes/I0.shtml");
                lblVariacaoMinerioDalian.Text = BuscarVariacao(html, Site.Sino);
                AtualizarCorVariacaoPercentual(lblVariacaoMinerioDalian);
            }
            catch (Exception e)
            {
                lblError.Text += Environment.NewLine + e.Message;
            }
            
            
        }
   */

        private void trmCotacoes_Tick(object sender, EventArgs e)
        {/*
            Thread threadSP500FUT = new Thread(new ThreadStart(AtualizarCotacaoSP500));
            threadSP500FUT.Start();
            
            Thread threadDX = new Thread(new ThreadStart(AtualizarCotacaoDX));
            threadDX.Start();

            Thread threadPetroleoWTI = new Thread(new ThreadStart(AtualizarCotacaoPetroleoWTI));
            threadPetroleoWTI.Start();

            

            Thread thread = new Thread(new ThreadStart(AtualizarCotacao10Anos));
            thread.Start();
            
            Thread threadSP500vix = new Thread(new ThreadStart(AtualizarCotacaosp500vix));
            threadSP500vix.Start();
            


            Thread threadOuro = new Thread(new ThreadStart(AtualizarCotacaoOuro));
            threadOuro.Start();

            Thread threadMinerio = new Thread(new ThreadStart(AtualizarCotacaoMinerio));
            threadMinerio.Start();

            Thread threadBitcoin = new Thread(new ThreadStart(AtualizarCotacaoBitcoin));
            threadBitcoin.Start();
            
 
            Thread threadVariacaoUS = new Thread(new ThreadStart(AtualizarVariacaoUS));
            threadVariacaoUS.Start();

            Thread threadVariacaoEUR = new Thread(new ThreadStart(AtualizarVariacaoEUR));
            threadVariacaoEUR.Start();

            Thread threadVaricaoASIA = new Thread(new ThreadStart(AtualizarVariacaoASIA));
            threadVaricaoASIA.Start();

            Thread threadVariacaoOutros = new Thread(new ThreadStart(AtualizarVariacaoOutros));
            threadVariacaoOutros.Start();
            */
        }


        private void FrmAlertas_LocationChanged(object sender, EventArgs e)
        {
            FrmAlertas_ResizeEnd(null, null);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        /*
        private void cmdStatusInvest_Click(object sender, EventArgs e)
        {         

            try
            {
                var web = new HtmlWeb();
                var html = web.Load(txtEnderecoStatusInvest.Text);

                var dados = "";

                //"Dividend Yield 
                dados = html.DocumentNode.SelectSingleNode("/html/body/main/div[2]/div/div[1]/div/div[4]/div/div[1]/strong").InnerHtml;
                dados += "	";
                //P / L
                dados += html.DocumentNode.SelectSingleNode("/html/body/main/div[2]/div/div[6]/div[2]/div/div[1]/div/div[2]/div/div/strong").InnerHtml;
                dados += "	";
                //LPA
                dados += html.DocumentNode.SelectSingleNode("/html/body/main/div[2]/div/div[6]/div[2]/div/div[1]/div/div[11]/div/div/strong").InnerHtml;
                dados += "	";
                //Margem Líquida
                dados += html.DocumentNode.SelectSingleNode("/html/body/main/div[2]/div/div[6]/div[2]/div/div[3]/div/div[4]/div/div/strong").InnerHtml;
                dados += "	";
                //ROE
                dados += html.DocumentNode.SelectSingleNode("/html/body/main/div[2]/div/div[6]/div[2]/div/div[4]/div/div[1]/div/div/strong").InnerHtml;
                dados += "	";
                //Liquidez Corrente
                dados += html.DocumentNode.SelectSingleNode("/html/body/main/div[2]/div/div[6]/div[2]/div/div[2]/div/div[6]/div/div/strong").InnerHtml;
                dados += "	";

                txtdados.Text = dados;

            }
            catch (Exception msgm)
            {
                MessageBox.Show(msgm.Message);
                txtdados.Text = "";


            }       
        }
        */
        private void button1_Click(object sender, EventArgs e)
        {
            var texto = "6,49	10,22	3,56	20,53%	7,42%	1,95";

     
            foreach (char c in texto)
            {
                MessageBox.Show(c + ": " + (int)c);
            }
        }

        private void txtEnderecoStatusInvest_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEnderecoStatusInvest_MouseClick(object sender, MouseEventArgs e)
        {
            txtEnderecoStatusInvest.Text = "";
        }

        private void lblHoraTokyo_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            txtResultado.Text = "";

            List<string> Lista = new List<string>();
            
            var linhas = txtLista.Text.Split((char)13);

            foreach (string linha in linhas)
            {
                var web = new HtmlWeb();
                var html = web.Load(linha);

                var retorno = BuscarInnerHtml(html, txtNode.Text);

                if (txtResultado.Text.Length > 0)
                    txtResultado.Text += Environment.NewLine;

                txtResultado.Text += retorno;
            }

            this.Cursor = Cursors.Default;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtNode.Text = "/html/body/main/div[2]/div/div[7]/div[2]/div/div[1]/div/div[1]/div/div/strong";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtNode.Text = "/html/body/main/div[2]/div/div[7]/div[2]/div/div[1]/div/div[2]/div/div/strong";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtNode.Text = "/html/body/main/div[2]/div/div[7]/div[2]/div/div[1]/div/div[11]/div/div/strong";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtNode.Text = "/html/body/main/div[2]/div/div[7]/div[2]/div/div[3]/div/div[4]/div/div/strong";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtNode.Text = "/html/body/main/div[2]/div/div[7]/div[2]/div/div[4]/div/div[1]/div/div/strong";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtNode.Text = "/html/body/main/div[2]/div/div[7]/div[2]/div/div[2]/div/div[6]/div/div/strong";
        }
    }
}
