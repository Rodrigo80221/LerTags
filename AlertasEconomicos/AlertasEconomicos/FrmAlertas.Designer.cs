namespace AlertasEconomicos
{
    partial class FrmAlertas
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlertas));
            this.tmrHorario = new System.Windows.Forms.Timer(this.components);
            this.trmCotacoes = new System.Windows.Forms.Timer(this.components);
            this.cmdStatusInvest = new System.Windows.Forms.Button();
            this.txtEnderecoStatusInvest = new System.Windows.Forms.TextBox();
            this.txtdados = new System.Windows.Forms.TextBox();
            this.txtLista = new System.Windows.Forms.TextBox();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.txtNode = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tmrHorario
            // 
            this.tmrHorario.Enabled = true;
            this.tmrHorario.Interval = 1000;
            this.tmrHorario.Tick += new System.EventHandler(this.tmrHorario_Tick);
            // 
            // trmCotacoes
            // 
            this.trmCotacoes.Enabled = true;
            this.trmCotacoes.Interval = 60000;
            this.trmCotacoes.Tick += new System.EventHandler(this.trmCotacoes_Tick);
            // 
            // cmdStatusInvest
            // 
            this.cmdStatusInvest.Location = new System.Drawing.Point(16, 813);
            this.cmdStatusInvest.Name = "cmdStatusInvest";
            this.cmdStatusInvest.Size = new System.Drawing.Size(234, 39);
            this.cmdStatusInvest.TabIndex = 80;
            this.cmdStatusInvest.Text = "Ler Status Invest";
            this.cmdStatusInvest.UseVisualStyleBackColor = true;
            // 
            // txtEnderecoStatusInvest
            // 
            this.txtEnderecoStatusInvest.Location = new System.Drawing.Point(16, 787);
            this.txtEnderecoStatusInvest.Name = "txtEnderecoStatusInvest";
            this.txtEnderecoStatusInvest.Size = new System.Drawing.Size(386, 20);
            this.txtEnderecoStatusInvest.TabIndex = 81;
            this.txtEnderecoStatusInvest.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtEnderecoStatusInvest_MouseClick);
            this.txtEnderecoStatusInvest.TextChanged += new System.EventHandler(this.txtEnderecoStatusInvest_TextChanged);
            // 
            // txtdados
            // 
            this.txtdados.Location = new System.Drawing.Point(16, 858);
            this.txtdados.Multiline = true;
            this.txtdados.Name = "txtdados";
            this.txtdados.Size = new System.Drawing.Size(386, 285);
            this.txtdados.TabIndex = 82;
            // 
            // txtLista
            // 
            this.txtLista.Location = new System.Drawing.Point(1, 22);
            this.txtLista.Multiline = true;
            this.txtLista.Name = "txtLista";
            this.txtLista.Size = new System.Drawing.Size(342, 413);
            this.txtLista.TabIndex = 83;
            this.txtLista.WordWrap = false;
            // 
            // txtResultado
            // 
            this.txtResultado.Location = new System.Drawing.Point(1004, 22);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.Size = new System.Drawing.Size(342, 413);
            this.txtResultado.TabIndex = 84;
            this.txtResultado.WordWrap = false;
            // 
            // txtNode
            // 
            this.txtNode.Location = new System.Drawing.Point(403, 281);
            this.txtNode.Name = "txtNode";
            this.txtNode.Size = new System.Drawing.Size(346, 20);
            this.txtNode.TabIndex = 85;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(772, 281);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(154, 23);
            this.btnBuscar.TabIndex = 86;
            this.btnBuscar.Text = "Buscar Html";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(403, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 23);
            this.button1.TabIndex = 87;
            this.button1.Text = "Investing DY";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(403, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 23);
            this.button2.TabIndex = 88;
            this.button2.Text = "Investing P/L";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(403, 80);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(155, 23);
            this.button3.TabIndex = 89;
            this.button3.Text = "Investing LPA";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(403, 109);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(155, 23);
            this.button4.TabIndex = 90;
            this.button4.Text = "Investing Margem Líquida";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(403, 138);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(155, 23);
            this.button5.TabIndex = 91;
            this.button5.Text = "Investing ROE";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(403, 167);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(155, 23);
            this.button6.TabIndex = 92;
            this.button6.Text = "Investing Liquidez Corrente";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // FrmAlertas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1383, 447);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtNode);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.txtLista);
            this.Controls.Add(this.txtdados);
            this.Controls.Add(this.txtEnderecoStatusInvest);
            this.Controls.Add(this.cmdStatusInvest);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAlertas";
            this.Text = "Ler Tags";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAlertas_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.FrmAlertas_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.FrmAlertas_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrHorario;
        private System.Windows.Forms.Timer trmCotacoes;
        private System.Windows.Forms.Button cmdStatusInvest;
        private System.Windows.Forms.TextBox txtEnderecoStatusInvest;
        private System.Windows.Forms.TextBox txtdados;
        private System.Windows.Forms.TextBox txtLista;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.TextBox txtNode;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}

