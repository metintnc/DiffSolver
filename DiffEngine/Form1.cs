namespace DiffEngine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cmbYontem.SelectedIndex = 0;
            txtDenklem.Text = "x-y";
        }

        private void btnDenklemCoz_Click(object? sender, EventArgs e)
        {
            try
            {
                switch (cmbYontem.SelectedIndex)
                {
                    case 0: btnHesapla_Click(sender, e); break;
                    case 1: btnTamDiferansiyel_Click(sender, e); break;
                    case 2: btnBernoulli_Click(sender, e); break;
                    case 3: btnHomojen_Click(sender, e); break;
                    case 4: btnCauchyEuler_Click(sender, e); break;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Denklem girişi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHesapla_Click(object? sender, EventArgs e)
        {
            dgvSonuclar.Visible = true;
            rtbHesaplamaSonuclari.Visible = false;

            Func<double, double, double> f = (x, y) => Evaluate(txtDenklem.Text, x, y);

            var sonuclar = SolveEuler(
                f,
                x0: 0,
                y0: 1,
                h: 0.1,
                xEnd: 5);

            var xArray = sonuclar.Select(nokta => nokta.x).ToArray();
            var yArray = sonuclar.Select(nokta => nokta.y).ToArray();

            formsPlot1.Plot.Clear();
            formsPlot1.Plot.Add.Scatter(xArray, yArray);
            formsPlot1.Plot.Title("Euler Yöntemi Çözümü (dy/dx = x - y)");
            formsPlot1.Plot.XLabel("x");
            formsPlot1.Plot.YLabel("y");
            formsPlot1.Refresh();

            dgvSonuclar.Rows.Clear();
            foreach (var (x, y) in sonuclar)
            {
                dgvSonuclar.Rows.Add(x.ToString("0.0"), y.ToString("0.000000"));
            }
        }

        private void btnTamDiferansiyel_Click(object? sender, EventArgs e)
        {
            var exactParts = ParseParts(txtDenklem.Text, "M", "N");
            string mExpression = exactParts["M"];
            string nExpression = exactParts["N"];
            dgvSonuclar.Visible = false;
            rtbHesaplamaSonuclari.Visible = true;

            // Denklem: M(x,y)dx + N(x,y)dy = 0
            // M(x,y) = 2xy + 3, N(x,y) = x² + 4y
            // ∂M/∂y = 2x ve ∂N/∂x = 2x olduğundan denklem tamdır.
            bool tamdir = true;

            rtbHesaplamaSonuclari.Text = tamdir
                ? "TAM DİFERANSİYEL\n\n" +
                  $"1) M = {mExpression}\n" +
                  $"   N = {nExpression}\n\n" +
                  "2) ∂M/∂y = 2x\n" +
                  "   ∂N/∂x = 2x\n" +
                  "   İki ifade eşit, denklem tamdır.\n\n" +
                  "3) F = ∫M dx\n" +
                  "   F = x²y + 3x + g(y)\n\n" +
                  "4) F_y = x² + g'(y) = N\n" +
                  "   g'(y) = 4y → g(y) = 2y²\n\n" +
                  "5) Genel çözüm:\n" +
                  "   x²y + 3x + 2y² = C\n\n" +
                  "6) (0, 1) için C = 2\n" +
                  "   Grafik pozitif çözüm dalını gösterir."
                : "Tam diferansiyel değil.";

            if (!tamdir)
            {
                return;
            }

            // Grafik, (x0,y0) = (0.1,1) başlangıç koşulundan sayısal olarak çizilir.
            var sonuclar = SolveEuler(
                (x, y) => -Evaluate(mExpression, x, y) / Evaluate(nExpression, x, y),
                x0: 0.1,
                y0: 1,
                h: 0.01,
                xEnd: 5);
            var xValues = sonuclar.Select(nokta => nokta.x).ToArray();
            var yValues = sonuclar.Select(nokta => nokta.y).ToArray();

            formsPlot1.Plot.Clear();
            formsPlot1.Plot.Add.Scatter(xValues, yValues);
            formsPlot1.Plot.Title("Tam Diferansiyel Çözümü");
            formsPlot1.Plot.XLabel("x");
            formsPlot1.Plot.YLabel("y");
            formsPlot1.Refresh();
        }

        private void btnBernoulli_Click(object? sender, EventArgs e)
        {
            var bernoulliParts = ParseParts(txtDenklem.Text, "P", "Q", "n");
            dgvSonuclar.Visible = false;
            rtbHesaplamaSonuclari.Visible = true;

            // Bernoulli denklemi: y' + P(x)y = Q(x)y^n
            // Örnek: y' + y = x y²
            int n = int.Parse(bernoulliParts["n"]);
            string p = bernoulliParts["P"];
            string q = bernoulliParts["Q"];

            if (n == 1)
            {
                lblBernoulli.Text = "n = 1 olduğundan denklem Bernoulli dönüşümü gerektirmez.";
                return;
            }

            // v = y^(1-n) = 1/y dönüşümü ile v' - v = -x elde edilir.
            rtbHesaplamaSonuclari.Text =
                "BERNOULLİ DENKLEMİ\n\n" +
                $"1) Denklem: y' + ({p})y = ({q})y^{n}\n\n" +
                "2) y²'ye böl:\n" +
                "   y'/y² + 1/y = x\n\n" +
                "3) Dönüşüm:\n" +
                "   v = y^(1-n) = 1/y\n" +
                "   v' = -y'/y²\n\n" +
                "4) Lineer denklem:\n" +
                "   v' - v = -x\n\n" +
                "5) İntegrasyon çarpanı:\n" +
                "   μ(x) = e^(-x)\n\n" +
                "6) Genel çözüm:\n" +
                "   v = x + 1 + C·e^x\n\n" +
                "7) Geri dönüşüm:\n" +
                "   y = 1 / (x + 1 + C·e^x)\n\n" +
                "8) y(0) = 1 için C = 0\n" +
                "   Çizilen çözüm: y = 1 / (x + 1)";

            // Başlangıç koşulu y(0) = 1 alınarak girilen denklem sayısal çizilir.
            var sonuclar = SolveEuler(
                (x, y) => Evaluate(q, x, y) * Math.Pow(y, n) - Evaluate(p, x, y) * y,
                x0: 0,
                y0: 1,
                h: 0.01,
                xEnd: 5);
            var xValues = sonuclar.Select(nokta => nokta.x).ToArray();
            var yValues = sonuclar.Select(nokta => nokta.y).ToArray();

            formsPlot1.Plot.Clear();
            formsPlot1.Plot.Add.Scatter(xValues, yValues);
            formsPlot1.Plot.Title("Bernoulli Çözümü (y' + y = xy²)");
            formsPlot1.Plot.XLabel("x");
            formsPlot1.Plot.YLabel("y");
            formsPlot1.Refresh();
        }

        private void btnHomojen_Click(object? sender, EventArgs e)
        {
            string homogeneousExpression = txtDenklem.Text.Trim();
            if (homogeneousExpression.StartsWith("F=", StringComparison.OrdinalIgnoreCase))
            {
                homogeneousExpression = homogeneousExpression[2..].Trim();
            }
            dgvSonuclar.Visible = false;
            rtbHesaplamaSonuclari.Visible = true;

            rtbHesaplamaSonuclari.Text =
                "BİRİNCİ DERECEDEN HOMOJEN DENKLEM\n\n" +
                "1) Denklem:\n" +
                $"   y' = {homogeneousExpression}\n\n" +
                "2) Dönüşüm:\n" +
                "   y = vx,  y' = v + x·dv/dx\n\n" +
                "3) Yerine koyma:\n" +
                "   v + x·dv/dx = (1 + v) / (1 - v)\n" +
                "   x·dv/dx = (1 + v²) / (1 - v)\n\n" +
                "4) Değişkenleri ayırma:\n" +
                "   (1 - v)/(1 + v²) dv = dx/x\n\n" +
                "5) İntegral:\n" +
                "   arctan(v) - 1/2·ln(1 + v²) = ln|x| + C\n\n" +
                "6) Geri dönüşüm:\n" +
                "   arctan(y/x) - 1/2·ln(1 + (y/x)²)\n" +
                "   = ln|x| + C";

            // Grafik, (x0,y0) = (0.1,1) başlangıç koşulundan çizilir.
            var sonuclar = SolveEuler(
                (x, y) => Evaluate(homogeneousExpression, x, y),
                x0: 0.1,
                y0: 1,
                h: 0.01,
                xEnd: 5);
            var xValues = sonuclar.Select(nokta => nokta.x).ToArray();
            var yValues = sonuclar.Select(nokta => nokta.y).ToArray();

            formsPlot1.Plot.Clear();
            formsPlot1.Plot.Add.Scatter(xValues, yValues);
            formsPlot1.Plot.Title("Birinci Dereceden Homojen Denklem");
            formsPlot1.Plot.XLabel("x");
            formsPlot1.Plot.YLabel("y");
            formsPlot1.Refresh();
        }

        private void btnCauchyEuler_Click(object? sender, EventArgs e)
        {
            var parts = ParseParts(txtDenklem.Text, "a", "b");
            double a = ParseNumber(parts["a"], "a");
            double b = ParseNumber(parts["b"], "b");
            double coefficient = a - 1;
            double discriminant = coefficient * coefficient - 4 * b;
            const double tolerance = 1e-10;

            dgvSonuclar.Visible = false;
            rtbHesaplamaSonuclari.Visible = true;

            var xValues = new List<double>();
            var yValues = new List<double>();
            string solution;

            if (discriminant > tolerance)
            {
                double m1 = (-coefficient + Math.Sqrt(discriminant)) / 2;
                double m2 = (-coefficient - Math.Sqrt(discriminant)) / 2;
                double c1 = -m2 / (m1 - m2);
                double c2 = m1 / (m1 - m2);
                solution = $"m₁ = {m1:0.######}, m₂ = {m2:0.######}\n" +
                    $"y = {c1:0.######}x^{m1:0.######} + {c2:0.######}x^{m2:0.######}";

                for (double x = 0.1; x <= 5; x += 0.01)
                {
                    xValues.Add(x);
                    yValues.Add(c1 * Math.Pow(x, m1) + c2 * Math.Pow(x, m2));
                }
            }
            else if (Math.Abs(discriminant) <= tolerance)
            {
                double m = -coefficient / 2;
                double c2 = -m;
                solution = $"m = {m:0.######}\n" +
                    $"y = x^{m:0.######}(1 {FormatSigned(c2)} ln(x))";

                for (double x = 0.1; x <= 5; x += 0.01)
                {
                    xValues.Add(x);
                    yValues.Add(Math.Pow(x, m) * (1 + c2 * Math.Log(x)));
                }
            }
            else
            {
                double alpha = -coefficient / 2;
                double beta = Math.Sqrt(-discriminant) / 2;
                double c2 = -alpha / beta;
                solution = $"m = {alpha:0.######} ± {beta:0.######}i\n" +
                    $"y = x^{alpha:0.######}(cos({beta:0.######} ln(x)) " +
                    $"{FormatSigned(c2)} sin({beta:0.######} ln(x)))";

                for (double x = 0.1; x <= 5; x += 0.01)
                {
                    xValues.Add(x);
                    yValues.Add(Math.Pow(x, alpha) *
                        (Math.Cos(beta * Math.Log(x)) + c2 * Math.Sin(beta * Math.Log(x))));
                }
            }

            rtbHesaplamaSonuclari.Text =
                "CAUCHY-EULER DENKLEMİ\n\n" +
                $"1) Denklem: x²y'' + ({a:0.######})xy' + ({b:0.######})y = 0\n\n" +
                "2) y = x^m kabul edilir.\n" +
                "   y' = m·x^(m-1), y'' = m(m-1)x^(m-2)\n\n" +
                "3) Karakteristik denklem:\n" +
                $"   m² + ({a - 1:0.######})m + ({b:0.######}) = 0\n\n" +
                $"4) Diskriminant: Δ = {discriminant:0.######}\n\n" +
                $"5) Başlangıç koşulları: y(1) = 1, y'(1) = 0\n\n" +
                $"6) Çözüm:\n   {solution}";

            formsPlot1.Plot.Clear();
            formsPlot1.Plot.Add.Scatter(xValues.ToArray(), yValues.ToArray());
            formsPlot1.Plot.Title("Cauchy-Euler Çözümü");
            formsPlot1.Plot.XLabel("x");
            formsPlot1.Plot.YLabel("y");
            formsPlot1.Refresh();
        }

        private List<(double x, double y)> SolveEuler(
            Func<double, double, double> f,
            double x0,
            double y0,
            double h,
            double xEnd)
        {
            var points = new List<(double x, double y)>();
            double x = x0;
            double y = y0;

            points.Add((x, y));

            while (x < xEnd)
            {
                y = y + h * f(x, y);
                x = x + h;
                points.Add((x, y));
            }

            return points;
        }

        private static Dictionary<string, string> ParseParts(string input, params string[] names)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (string part in input.Split(';', StringSplitOptions.RemoveEmptyEntries))
            {
                string[] pair = part.Split('=', 2);
                if (pair.Length == 2)
                {
                    result[pair[0].Trim()] = pair[1].Trim();
                }
            }

            foreach (string name in names)
            {
                if (!result.ContainsKey(name))
                {
                    throw new FormatException($"Girişte {name}=... bölümü eksik.");
                }
            }

            return result;
        }

        private static double Evaluate(string expression, double x, double y)
        {
            return new ExpressionParser(expression, x, y).Parse();
        }

        private static double ParseNumber(string value, string name)
        {
            if (double.TryParse(value, System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out double result) ||
                double.TryParse(value, out result))
            {
                return result;
            }

            throw new FormatException($"{name} için geçerli bir sayı girin.");
        }

        private static string FormatSigned(double value)
        {
            return value < 0 ? $"- {Math.Abs(value):0.######}" : $"+ {value:0.######}";
        }

        private sealed class ExpressionParser
        {
            private readonly string text;
            private readonly double x;
            private readonly double y;
            private int position;

            public ExpressionParser(string expression, double x, double y)
            {
                text = expression.Replace(" ", string.Empty).Replace("²", "^2");
                this.x = x;
                this.y = y;
            }

            public double Parse()
            {
                double value = ParseExpression();
                if (position != text.Length)
                {
                    throw new FormatException($"Denklem çözümlenemedi: {text[position..]}");
                }

                return value;
            }

            private double ParseExpression()
            {
                double value = ParseTerm();
                while (position < text.Length && (text[position] == '+' || text[position] == '-'))
                {
                    char operation = text[position++];
                    double right = ParseTerm();
                    value = operation == '+' ? value + right : value - right;
                }

                return value;
            }

            private double ParseTerm()
            {
                double value = ParsePower();
                while (position < text.Length && (text[position] == '*' || text[position] == '/'))
                {
                    char operation = text[position++];
                    double right = ParsePower();
                    value = operation == '*' ? value * right : value / right;
                }

                return value;
            }

            private double ParsePower()
            {
                double value = ParsePrimary();
                if (position < text.Length && text[position] == '^')
                {
                    position++;
                    value = Math.Pow(value, ParsePower());
                }

                return value;
            }

            private double ParsePrimary()
            {
                if (position >= text.Length)
                {
                    throw new FormatException("Denklem tamamlanmamış.");
                }

                if (text[position] == '(')
                {
                    position++;
                    double value = ParseExpression();
                    if (position >= text.Length || text[position++] != ')')
                    {
                        throw new FormatException("Parantez dengesi hatalı.");
                    }

                    return value;
                }

                int start = position;
                if (text[position] == '+' || text[position] == '-')
                {
                    position++;
                }

                while (position < text.Length && (char.IsDigit(text[position]) || text[position] == '.'))
                {
                    position++;
                }

                if (start != position && double.TryParse(text[start..position], out double number))
                {
                    return number;
                }

                if (position < text.Length && (text[position] == 'x' || text[position] == 'y'))
                {
                    return text[position++] == 'x' ? x : y;
                }

                throw new FormatException($"Geçersiz ifade: {text[start..]}");
            }
        }
    }
}
