namespace PingClient;

partial class Form1
{
    private Button pingButton;
    private Label requestLabel;
    private Label responseLabel;
    private TextBox resultTextBox;

    private void InitializeComponent()
    {
        pingButton = new Button();
        requestLabel = new Label();
        responseLabel = new Label();
        resultTextBox = new TextBox();
        SuspendLayout();

        pingButton.Location = new Point(16, 16);
        pingButton.Name = "pingButton";
        pingButton.Size = new Size(96, 34);
        pingButton.TabIndex = 0;
        pingButton.Text = "Ping";
        pingButton.UseVisualStyleBackColor = true;
        pingButton.Click += pingButton_Click;

        requestLabel.AutoSize = true;
        requestLabel.Location = new Point(126, 25);
        requestLabel.Name = "requestLabel";
        requestLabel.Text = "GET /ping";

        responseLabel.AutoSize = true;
        responseLabel.Location = new Point(16, 65);
        responseLabel.Name = "responseLabel";
        responseLabel.Text = "Ответ сервера";

        resultTextBox.AccessibleName = "Ответ сервера";
        resultTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        resultTextBox.BackColor = SystemColors.Window;
        resultTextBox.Location = new Point(16, 88);
        resultTextBox.Multiline = true;
        resultTextBox.Name = "resultTextBox";
        resultTextBox.ReadOnly = true;
        resultTextBox.ScrollBars = ScrollBars.Vertical;
        resultTextBox.Size = new Size(468, 116);
        resultTextBox.TabIndex = 1;
        resultTextBox.Text = "Нажмите Ping, чтобы отправить запрос.";

        AcceptButton = pingButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(500, 220);
        Controls.Add(pingButton);
        Controls.Add(requestLabel);
        Controls.Add(responseLabel);
        Controls.Add(resultTextBox);
        MinimumSize = new Size(400, 235);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "PingPong — ASP.NET Core";
        ResumeLayout(false);
        PerformLayout();
    }
}
