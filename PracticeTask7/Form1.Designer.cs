namespace PracticeTask7
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.вводФункцииИзФайлаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вводФункцииВручнуюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Input_Label = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вводФункцииИзФайлаToolStripMenuItem,
            this.вводФункцииВручнуюToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(503, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // вводФункцииИзФайлаToolStripMenuItem
            // 
            this.вводФункцииИзФайлаToolStripMenuItem.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.вводФункцииИзФайлаToolStripMenuItem.Name = "вводФункцииИзФайлаToolStripMenuItem";
            this.вводФункцииИзФайлаToolStripMenuItem.Size = new System.Drawing.Size(219, 32);
            this.вводФункцииИзФайлаToolStripMenuItem.Text = "Ввод функции из файла";
            this.вводФункцииИзФайлаToolStripMenuItem.Click += new System.EventHandler(this.вводФункцииИзФайлаToolStripMenuItem_Click);
            // 
            // вводФункцииВручнуюToolStripMenuItem
            // 
            this.вводФункцииВручнуюToolStripMenuItem.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.вводФункцииВручнуюToolStripMenuItem.Name = "вводФункцииВручнуюToolStripMenuItem";
            this.вводФункцииВручнуюToolStripMenuItem.Size = new System.Drawing.Size(211, 32);
            this.вводФункцииВручнуюToolStripMenuItem.Text = "Ввод функции вручную";
            this.вводФункцииВручнуюToolStripMenuItem.Click += new System.EventHandler(this.вводФункцииВручнуюToolStripMenuItem_Click);
            // 
            // Input_Label
            // 
            this.Input_Label.AutoSize = true;
            this.Input_Label.BackColor = System.Drawing.Color.Transparent;
            this.Input_Label.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Input_Label.Location = new System.Drawing.Point(12, 52);
            this.Input_Label.Name = "Input_Label";
            this.Input_Label.Size = new System.Drawing.Size(0, 33);
            this.Input_Label.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = global::PracticeTask7.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(503, 241);
            this.Controls.Add(this.Input_Label);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Классы булевой функции";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem вводФункцииИзФайлаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вводФункцииВручнуюToolStripMenuItem;
        private System.Windows.Forms.Label Input_Label;
    }
}

