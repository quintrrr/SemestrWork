namespace SemestrWork
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            miAdd = new ToolStripMenuItem();
            miAddGroup = new ToolStripMenuItem();
            miAddProperty = new ToolStripMenuItem();
            miEdit = new ToolStripMenuItem();
            miDelete = new ToolStripMenuItem();
            treeView = new TreeView();
            gbEditGroup = new GroupBox();
            lbGroupName = new Label();
            btnGroupCancel = new Button();
            btnGroupSave = new Button();
            lbGroupId = new Label();
            tbGroupId = new TextBox();
            tbGroupName = new TextBox();
            gbEditProperty = new GroupBox();
            btnPropertyCancel = new Button();
            btnPropertySave = new Button();
            lbPropertyGroupId = new Label();
            lbPropertyValue = new Label();
            lbPropertyName = new Label();
            tbPropertyGroupId = new TextBox();
            tbPropertyValue = new TextBox();
            tbPropertyName = new TextBox();
            menuStrip.SuspendLayout();
            gbEditGroup.SuspendLayout();
            gbEditProperty.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { miAdd, miEdit, miDelete });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // miAdd
            // 
            miAdd.DropDownItems.AddRange(new ToolStripItem[] { miAddGroup, miAddProperty });
            miAdd.Name = "miAdd";
            miAdd.Size = new Size(71, 20);
            miAdd.Text = "Добавить";
            // 
            // miAddGroup
            // 
            miAddGroup.Name = "miAddGroup";
            miAddGroup.Size = new Size(126, 22);
            miAddGroup.Text = "Группу";
            miAddGroup.Click += miAddGroup_Click;
            // 
            // miAddProperty
            // 
            miAddProperty.Name = "miAddProperty";
            miAddProperty.Size = new Size(126, 22);
            miAddProperty.Text = "Свойство";
            miAddProperty.Click += miAddProperty_Click;
            // 
            // miEdit
            // 
            miEdit.Name = "miEdit";
            miEdit.Size = new Size(99, 20);
            miEdit.Text = "Редактировать";
            miEdit.Click += miEdit_Click;
            // 
            // miDelete
            // 
            miDelete.Name = "miDelete";
            miDelete.Size = new Size(63, 20);
            miDelete.Text = "Удалить";
            miDelete.Click += miDelete_Click;
            // 
            // treeView
            // 
            treeView.Location = new Point(21, 44);
            treeView.Name = "treeView";
            treeView.Size = new Size(210, 394);
            treeView.TabIndex = 1;
            treeView.AfterSelect += treeView_AfterSelect;
            // 
            // gbEditGroup
            // 
            gbEditGroup.Controls.Add(lbGroupName);
            gbEditGroup.Controls.Add(btnGroupCancel);
            gbEditGroup.Controls.Add(btnGroupSave);
            gbEditGroup.Controls.Add(lbGroupId);
            gbEditGroup.Controls.Add(tbGroupId);
            gbEditGroup.Controls.Add(tbGroupName);
            gbEditGroup.Location = new Point(262, 44);
            gbEditGroup.Name = "gbEditGroup";
            gbEditGroup.Size = new Size(527, 164);
            gbEditGroup.TabIndex = 2;
            gbEditGroup.TabStop = false;
            gbEditGroup.Text = "Форма редактирования группы";
            // 
            // lbGroupName
            // 
            lbGroupName.AutoSize = true;
            lbGroupName.Location = new Point(17, 38);
            lbGroupName.Name = "lbGroupName";
            lbGroupName.Size = new Size(90, 15);
            lbGroupName.TabIndex = 4;
            lbGroupName.Text = "Наименование";
            // 
            // btnGroupCancel
            // 
            btnGroupCancel.Location = new Point(404, 126);
            btnGroupCancel.Name = "btnGroupCancel";
            btnGroupCancel.Size = new Size(106, 28);
            btnGroupCancel.TabIndex = 4;
            btnGroupCancel.Text = "Отмена";
            btnGroupCancel.UseVisualStyleBackColor = true;
            btnGroupCancel.Click += btnGroupCancel_Click;
            // 
            // btnGroupSave
            // 
            btnGroupSave.Location = new Point(281, 126);
            btnGroupSave.Name = "btnGroupSave";
            btnGroupSave.Size = new Size(106, 28);
            btnGroupSave.TabIndex = 3;
            btnGroupSave.Text = "Сохранить";
            btnGroupSave.UseVisualStyleBackColor = true;
            btnGroupSave.Click += btnGroupSave_Click;
            // 
            // lbGroupId
            // 
            lbGroupId.AutoSize = true;
            lbGroupId.Location = new Point(17, 86);
            lbGroupId.Name = "lbGroupId";
            lbGroupId.Size = new Size(17, 15);
            lbGroupId.TabIndex = 2;
            lbGroupId.Text = "Id";
            // 
            // tbGroupId
            // 
            tbGroupId.Enabled = false;
            tbGroupId.Location = new Point(113, 83);
            tbGroupId.Name = "tbGroupId";
            tbGroupId.Size = new Size(397, 23);
            tbGroupId.TabIndex = 1;
            // 
            // tbGroupName
            // 
            tbGroupName.Location = new Point(113, 35);
            tbGroupName.Name = "tbGroupName";
            tbGroupName.Size = new Size(397, 23);
            tbGroupName.TabIndex = 0;
            // 
            // gbEditProperty
            // 
            gbEditProperty.Controls.Add(btnPropertyCancel);
            gbEditProperty.Controls.Add(btnPropertySave);
            gbEditProperty.Controls.Add(lbPropertyGroupId);
            gbEditProperty.Controls.Add(lbPropertyValue);
            gbEditProperty.Controls.Add(lbPropertyName);
            gbEditProperty.Controls.Add(tbPropertyGroupId);
            gbEditProperty.Controls.Add(tbPropertyValue);
            gbEditProperty.Controls.Add(tbPropertyName);
            gbEditProperty.Location = new Point(259, 229);
            gbEditProperty.Name = "gbEditProperty";
            gbEditProperty.Size = new Size(530, 183);
            gbEditProperty.TabIndex = 3;
            gbEditProperty.TabStop = false;
            gbEditProperty.Text = "Форма редактирования свойства";
            // 
            // btnPropertyCancel
            // 
            btnPropertyCancel.Location = new Point(404, 140);
            btnPropertyCancel.Name = "btnPropertyCancel";
            btnPropertyCancel.Size = new Size(106, 28);
            btnPropertyCancel.TabIndex = 8;
            btnPropertyCancel.Text = "Отменить";
            btnPropertyCancel.UseVisualStyleBackColor = true;
            btnPropertyCancel.Click += btnPropertyCancel_Click;
            // 
            // btnPropertySave
            // 
            btnPropertySave.Location = new Point(281, 140);
            btnPropertySave.Name = "btnPropertySave";
            btnPropertySave.Size = new Size(106, 28);
            btnPropertySave.TabIndex = 5;
            btnPropertySave.Text = "Сохранить";
            btnPropertySave.UseVisualStyleBackColor = true;
            btnPropertySave.Click += btnPropertySave_Click;
            // 
            // lbPropertyGroupId
            // 
            lbPropertyGroupId.AutoSize = true;
            lbPropertyGroupId.Location = new Point(17, 114);
            lbPropertyGroupId.Name = "lbPropertyGroupId";
            lbPropertyGroupId.Size = new Size(61, 15);
            lbPropertyGroupId.TabIndex = 7;
            lbPropertyGroupId.Text = "Id группы";
            // 
            // lbPropertyValue
            // 
            lbPropertyValue.AutoSize = true;
            lbPropertyValue.Location = new Point(17, 74);
            lbPropertyValue.Name = "lbPropertyValue";
            lbPropertyValue.Size = new Size(60, 15);
            lbPropertyValue.TabIndex = 6;
            lbPropertyValue.Text = "Значение";
            // 
            // lbPropertyName
            // 
            lbPropertyName.AutoSize = true;
            lbPropertyName.Location = new Point(17, 34);
            lbPropertyName.Name = "lbPropertyName";
            lbPropertyName.Size = new Size(90, 15);
            lbPropertyName.TabIndex = 5;
            lbPropertyName.Text = "Наименование";
            // 
            // tbPropertyGroupId
            // 
            tbPropertyGroupId.Enabled = false;
            tbPropertyGroupId.Location = new Point(113, 111);
            tbPropertyGroupId.Name = "tbPropertyGroupId";
            tbPropertyGroupId.Size = new Size(397, 23);
            tbPropertyGroupId.TabIndex = 4;
            // 
            // tbPropertyValue
            // 
            tbPropertyValue.Location = new Point(113, 71);
            tbPropertyValue.Name = "tbPropertyValue";
            tbPropertyValue.Size = new Size(397, 23);
            tbPropertyValue.TabIndex = 3;
            // 
            // tbPropertyName
            // 
            tbPropertyName.Location = new Point(113, 31);
            tbPropertyName.Name = "tbPropertyName";
            tbPropertyName.Size = new Size(397, 23);
            tbPropertyName.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(gbEditProperty);
            Controls.Add(gbEditGroup);
            Controls.Add(treeView);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "Form1";
            Text = "Form1";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            gbEditGroup.ResumeLayout(false);
            gbEditGroup.PerformLayout();
            gbEditProperty.ResumeLayout(false);
            gbEditProperty.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem miAdd;
        private ToolStripMenuItem miEdit;
        private ToolStripMenuItem miDelete;
        private TreeView treeView;
        private GroupBox gbEditGroup;
        private GroupBox gbEditProperty;
        private TextBox tbGroupId;
        private TextBox tbGroupName;
        private TextBox tbPropertyGroupId;
        private TextBox tbPropertyValue;
        private TextBox tbPropertyName;
        private Label lbGroupName;
        private Label lbGroupId;
        private Label lbPropertyValue;
        private Label lbPropertyName;
        private Label lbPropertyGroupId;
        private Button btnGroupCancel;
        private Button btnGroupSave;
        private Button btnPropertyCancel;
        private Button btnPropertySave;
        private ToolStripMenuItem miAddGroup;
        private ToolStripMenuItem miAddProperty;
    }
}
