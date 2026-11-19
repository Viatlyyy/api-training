namespace PingClient;

partial class Form1
{
    private System.ComponentModel.IContainer components;
    private Label requestLabel;
    private Label productNameLabel;
    private TextBox productNameTextBox;
    private Label priceLabel;
    private NumericUpDown priceNumericUpDown;
    private Label quantityLabel;
    private NumericUpDown quantityNumericUpDown;
    private Button sendButton;
    private Label responseLabel;
    private TextBox resultTextBox;
    private ErrorProvider inputErrors;
    private Label saveStatusLabel;
    private TextBox logPathTextBox;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        requestLabel = new Label();
        productNameLabel = new Label();
        productNameTextBox = new TextBox();
        priceLabel = new Label();
        priceNumericUpDown = new NumericUpDown();
        quantityLabel = new Label();
        quantityNumericUpDown = new NumericUpDown();
        sendButton = new Button();
        responseLabel = new Label();
        resultTextBox = new TextBox();
        inputErrors = new ErrorProvider(components);
        saveStatusLabel = new Label();
        logPathTextBox = new TextBox();
        ((System.ComponentModel.ISupportInitialize)priceNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)quantityNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)inputErrors).BeginInit();
        SuspendLayout();

        requestLabel.AutoSize = true;
        requestLabel.Location = new Point(20, 20);
        requestLabel.Name = "requestLabel";
        requestLabel.Text = "POST http://127.0.0.1:8001/products";

        productNameLabel.AutoSize = true;
        productNameLabel.Location = new Point(20, 64);
        productNameLabel.Name = "productNameLabel";
        productNameLabel.Text = "Название товара";

        productNameTextBox.AccessibleName = "Название товара";
        productNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        productNameTextBox.Location = new Point(20, 88);
        productNameTextBox.MaxLength = 100;
        productNameTextBox.Name = "productNameTextBox";
        productNameTextBox.Size = new Size(540, 23);
        productNameTextBox.TabIndex = 0;

        priceLabel.AutoSize = true;
        priceLabel.Location = new Point(20, 132);
        priceLabel.Name = "priceLabel";
        priceLabel.Text = "Цена, руб.";

        priceNumericUpDown.AccessibleName = "Цена товара";
        priceNumericUpDown.DecimalPlaces = 2;
        priceNumericUpDown.Location = new Point(20, 156);
        priceNumericUpDown.Maximum = 1000000000m;
        priceNumericUpDown.Name = "priceNumericUpDown";
        priceNumericUpDown.Size = new Size(240, 23);
        priceNumericUpDown.TabIndex = 1;
        priceNumericUpDown.ThousandsSeparator = true;

        quantityLabel.AutoSize = true;
        quantityLabel.Location = new Point(320, 132);
        quantityLabel.Name = "quantityLabel";
        quantityLabel.Text = "Количество, шт.";

        quantityNumericUpDown.AccessibleName = "Количество товара";
        quantityNumericUpDown.Location = new Point(320, 156);
        quantityNumericUpDown.Maximum = 1000000m;
        quantityNumericUpDown.Name = "quantityNumericUpDown";
        quantityNumericUpDown.Size = new Size(240, 23);
        quantityNumericUpDown.TabIndex = 2;
        quantityNumericUpDown.ThousandsSeparator = true;
        quantityNumericUpDown.Value = 1m;

        sendButton.Location = new Point(20, 204);
        sendButton.Name = "sendButton";
        sendButton.Size = new Size(190, 36);
        sendButton.TabIndex = 3;
        sendButton.Text = "Отправить товар";
        sendButton.UseVisualStyleBackColor = true;
        sendButton.Click += sendButton_Click;

        responseLabel.AutoSize = true;
        responseLabel.Location = new Point(20, 260);
        responseLabel.Name = "responseLabel";
        responseLabel.Text = "Ответ сервера";

        resultTextBox.AccessibleName = "Ответ сервера";
        resultTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        resultTextBox.BackColor = SystemColors.Window;
        resultTextBox.Location = new Point(20, 284);
        resultTextBox.Multiline = true;
        resultTextBox.Name = "resultTextBox";
        resultTextBox.ReadOnly = true;
        resultTextBox.ScrollBars = ScrollBars.Vertical;
        resultTextBox.Size = new Size(560, 196);
        resultTextBox.TabIndex = 4;
        resultTextBox.Text = "Заполните данные и нажмите «Отправить товар».";

        inputErrors.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        inputErrors.ContainerControl = this;

        saveStatusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        saveStatusLabel.AutoEllipsis = true;
        saveStatusLabel.Location = new Point(20, 496);
        saveStatusLabel.Name = "saveStatusLabel";
        saveStatusLabel.Size = new Size(560, 18);
        saveStatusLabel.Text = "Ответы будут дописываться в файл:";

        logPathTextBox.AccessibleName = "Путь к файлу ответов";
        logPathTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        logPathTextBox.BackColor = SystemColors.Window;
        logPathTextBox.Location = new Point(20, 520);
        logPathTextBox.Name = "logPathTextBox";
        logPathTextBox.ReadOnly = true;
        logPathTextBox.Size = new Size(560, 23);
        logPathTextBox.TabIndex = 5;

        AcceptButton = sendButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(600, 560);
        Controls.Add(requestLabel);
        Controls.Add(productNameLabel);
        Controls.Add(productNameTextBox);
        Controls.Add(priceLabel);
        Controls.Add(priceNumericUpDown);
        Controls.Add(quantityLabel);
        Controls.Add(quantityNumericUpDown);
        Controls.Add(sendButton);
        Controls.Add(responseLabel);
        Controls.Add(resultTextBox);
        Controls.Add(saveStatusLabel);
        Controls.Add(logPathTextBox);
        MinimumSize = new Size(616, 510);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Отправка товара — ASP.NET Core";
        ((System.ComponentModel.ISupportInitialize)priceNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)quantityNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)inputErrors).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
