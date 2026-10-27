namespace DiffEngine
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnHesapla;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private DataGridView dgvSonuclar;
        private Label lblSonuclar;
        private Button btnTamDiferansiyel;
        private Label lblTamDiferansiyel;
        private Button btnBernoulli;
        private Label lblBernoulli;
        private Button btnHomojen;
        private ComboBox cmbYontem;
        private TextBox txtDenklem;
        private Button btnDenklemCoz;
        private Label lblDenklem;
        private RichTextBox rtbHesaplamaSonuclari;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnHesapla = new Button();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            dgvSonuclar = new DataGridView();
            lblSonuclar = new Label();
            btnTamDiferansiyel = new Button();
            lblTamDiferansiyel = new Label();
            btnBernoulli = new Button();
            lblBernoulli = new Label();
            btnHomojen = new Button();
            cmbYontem = new ComboBox();
            txtDenklem = new TextBox();
            btnDenklemCoz = new Button();
            lblDenklem = new Label();
            rtbHesaplamaSonuclari = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)dgvSonuclar).BeginInit();
            SuspendLayout();

            lblDenklem.AutoSize = true;
            lblDenklem.Location = new Point(175, 20);
            lblDenklem.Text = "Yöntem:";

            cmbYontem.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYontem.Items.AddRange(new object[] { "Euler", "Tam diferansiyel", "Bernoulli", "Homojen", "Cauchy-Euler" });
            cmbYontem.Location = new Point(230, 16);
            cmbYontem.Size = new Size(150, 23);

            txtDenklem.Location = new Point(390, 16);
            txtDenklem.Size = new Size(390, 23);

            btnDenklemCoz.Location = new Point(12, 50);
            btnDenklemCoz.Size = new Size(140, 32);
            btnDenklemCoz.Text = "Denklemi Çöz";
            btnDenklemCoz.UseVisualStyleBackColor = true;
            btnDenklemCoz.Click += btnDenklemCoz_Click;

            btnHesapla.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnHesapla.Location = new Point(12, 12);
            btnHesapla.Name = "btnHesapla";
            btnHesapla.Size = new Size(140, 32);
            btnHesapla.Text = "Hesapla";
            btnHesapla.UseVisualStyleBackColor = true;
            btnHesapla.Click += btnHesapla_Click;

            formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot1.Location = new Point(12, 92);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(560, 370);

            lblSonuclar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSonuclar.AutoSize = true;
            lblSonuclar.Location = new Point(590, 20);
            lblSonuclar.Name = "lblSonuclar";
            lblSonuclar.Text = "Hesaplama sonuçları";

            rtbHesaplamaSonuclari.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            rtbHesaplamaSonuclari.BackColor = SystemColors.Window;
            rtbHesaplamaSonuclari.Font = new Font("Segoe UI", 9F);
            rtbHesaplamaSonuclari.Location = new Point(590, 56);
            rtbHesaplamaSonuclari.Name = "rtbHesaplamaSonuclari";
            rtbHesaplamaSonuclari.ReadOnly = true;
            rtbHesaplamaSonuclari.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbHesaplamaSonuclari.Size = new Size(370, 370);
            rtbHesaplamaSonuclari.Text = "Bir çözüm türü seçin.";
            rtbHesaplamaSonuclari.Visible = true;

            dgvSonuclar.AllowUserToAddRows = false;
            dgvSonuclar.AllowUserToDeleteRows = false;
            dgvSonuclar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dgvSonuclar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSonuclar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSonuclar.Columns.Add("X", "x");
            dgvSonuclar.Columns.Add("Y", "y");
            dgvSonuclar.Location = new Point(590, 56);
            dgvSonuclar.Name = "dgvSonuclar";
            dgvSonuclar.ReadOnly = true;
            dgvSonuclar.RowHeadersVisible = false;
            dgvSonuclar.Size = new Size(370, 370);
            dgvSonuclar.Visible = false;

            btnTamDiferansiyel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnTamDiferansiyel.Location = new Point(12, 432);
            btnTamDiferansiyel.Name = "btnTamDiferansiyel";
            btnTamDiferansiyel.Size = new Size(190, 32);
            btnTamDiferansiyel.Text = "Tam Diferansiyeli Çöz";
            btnTamDiferansiyel.UseVisualStyleBackColor = true;
            btnTamDiferansiyel.Click += btnTamDiferansiyel_Click;

            lblTamDiferansiyel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTamDiferansiyel.AutoSize = false;
            lblTamDiferansiyel.Location = new Point(220, 430);
            lblTamDiferansiyel.Name = "lblTamDiferansiyel";
            lblTamDiferansiyel.Size = new Size(360, 82);
            lblTamDiferansiyel.Text = "Tam diferansiyel sonucu burada gösterilir.";

            btnBernoulli.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBernoulli.Location = new Point(12, 525);
            btnBernoulli.Name = "btnBernoulli";
            btnBernoulli.Size = new Size(190, 32);
            btnBernoulli.Text = "Bernoulli Çöz";
            btnBernoulli.UseVisualStyleBackColor = true;
            btnBernoulli.Click += btnBernoulli_Click;

            btnHomojen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnHomojen.Location = new Point(12, 563);
            btnHomojen.Name = "btnHomojen";
            btnHomojen.Size = new Size(190, 32);
            btnHomojen.Text = "Homojen Çöz";
            btnHomojen.UseVisualStyleBackColor = true;
            btnHomojen.Click += btnHomojen_Click;

            lblBernoulli.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblBernoulli.AutoSize = false;
            lblBernoulli.Location = new Point(220, 520);
            lblBernoulli.Name = "lblBernoulli";
            lblBernoulli.Size = new Size(360, 100);
            lblBernoulli.Text = "Bernoulli sonucu burada gösterilir.";

            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 670);
            MinimumSize = new Size(700, 620);
            Text = "Euler Yöntemi - DiffEngine";
            Controls.Add(formsPlot1);
            Controls.Add(lblDenklem);
            Controls.Add(cmbYontem);
            Controls.Add(txtDenklem);
            Controls.Add(btnDenklemCoz);
            Controls.Add(dgvSonuclar);
            Controls.Add(rtbHesaplamaSonuclari);
            Controls.Add(lblSonuclar);
            Controls.Add(btnHesapla);
            Controls.Add(btnTamDiferansiyel);
            Controls.Add(lblTamDiferansiyel);
            Controls.Add(btnBernoulli);
            Controls.Add(lblBernoulli);
            Controls.Add(btnHomojen);
            ((System.ComponentModel.ISupportInitialize)dgvSonuclar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
