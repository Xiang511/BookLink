namespace Online_Ordering_System
{
    partial class MarketPanel
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearch = new MaterialSkin.Controls.MaterialTextBox2();
            this.btnSearch = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.materialCard1 = new MaterialSkin.Controls.MaterialCard();
            this.btnCategroy1 = new System.Windows.Forms.Button();
            this.btnCategroy2 = new System.Windows.Forms.Button();
            this.materialCard2 = new MaterialSkin.Controls.MaterialCard();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.materialCard1.SuspendLayout();
            this.materialCard2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(24, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "瀏覽和搜尋所有書籍";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(22, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "書籍市場";
            // 
            // txtSearch
            // 
            this.txtSearch.AnimateReadOnly = false;
            this.txtSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtSearch.Depth = 0;
            this.txtSearch.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.HideSelection = true;
            this.txtSearch.Hint = "搜尋書名、作者 、ISBN";
            this.txtSearch.LeadingIcon = null;
            this.txtSearch.Location = new System.Drawing.Point(28, 126);
            this.txtSearch.MaxLength = 32767;
            this.txtSearch.MouseState = MaterialSkin.MouseState.OUT;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PrefixSuffixText = null;
            this.txtSearch.ReadOnly = false;
            this.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSearch.SelectedText = "";
            this.txtSearch.SelectionLength = 0;
            this.txtSearch.SelectionStart = 0;
            this.txtSearch.ShortcutsEnabled = true;
            this.txtSearch.Size = new System.Drawing.Size(532, 48);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TabStop = false;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSearch.TrailingIcon = null;
            this.txtSearch.UseSystemPasswordChar = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(764, 126);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(104, 48);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "搜尋";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(719, 415);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // materialCard1
            // 
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.Controls.Add(this.listView1);
            this.materialCard1.Depth = 0;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(28, 191);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(721, 415);
            this.materialCard1.TabIndex = 8;
            // 
            // btnCategroy1
            // 
            this.btnCategroy1.BackColor = System.Drawing.Color.Orange;
            this.btnCategroy1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategroy1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCategroy1.ForeColor = System.Drawing.Color.White;
            this.btnCategroy1.Location = new System.Drawing.Point(17, 55);
            this.btnCategroy1.Name = "btnCategroy1";
            this.btnCategroy1.Size = new System.Drawing.Size(70, 41);
            this.btnCategroy1.TabIndex = 9;
            this.btnCategroy1.Text = "科技";
            this.btnCategroy1.UseVisualStyleBackColor = false;
            this.btnCategroy1.Click += new System.EventHandler(this.btnCategroy1_Click);
            // 
            // btnCategroy2
            // 
            this.btnCategroy2.BackColor = System.Drawing.Color.LimeGreen;
            this.btnCategroy2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategroy2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCategroy2.ForeColor = System.Drawing.Color.White;
            this.btnCategroy2.Location = new System.Drawing.Point(17, 102);
            this.btnCategroy2.Name = "btnCategroy2";
            this.btnCategroy2.Size = new System.Drawing.Size(70, 41);
            this.btnCategroy2.TabIndex = 10;
            this.btnCategroy2.Text = "運動";
            this.btnCategroy2.UseVisualStyleBackColor = false;
            this.btnCategroy2.Click += new System.EventHandler(this.btnCategroy2_Click);
            // 
            // materialCard2
            // 
            this.materialCard2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard2.Controls.Add(this.button2);
            this.materialCard2.Controls.Add(this.btnCategroy1);
            this.materialCard2.Controls.Add(this.btnCategroy2);
            this.materialCard2.Depth = 0;
            this.materialCard2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard2.Location = new System.Drawing.Point(764, 191);
            this.materialCard2.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard2.Name = "materialCard2";
            this.materialCard2.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard2.Size = new System.Drawing.Size(104, 415);
            this.materialCard2.TabIndex = 11;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Tan;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(17, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 41);
            this.button2.TabIndex = 11;
            this.button2.Text = "全部";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(566, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 48);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton1.Location = new System.Drawing.Point(6, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(49, 20);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "書名";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton2.Location = new System.Drawing.Point(60, 22);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(61, 20);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "出版商";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton3.Location = new System.Drawing.Point(127, 22);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(54, 20);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "ISBN";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // MarketPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.materialCard2);
            this.Controls.Add(this.materialCard1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Name = "MarketPanel";
            this.Size = new System.Drawing.Size(953, 641);
            this.Load += new System.EventHandler(this.MarketPanel_Load);
            this.materialCard1.ResumeLayout(false);
            this.materialCard2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialTextBox2 txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListView listView1;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private System.Windows.Forms.Button btnCategroy1;
        private System.Windows.Forms.Button btnCategroy2;
        private MaterialSkin.Controls.MaterialCard materialCard2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}
