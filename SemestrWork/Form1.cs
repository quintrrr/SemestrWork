using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SemestrWork
{
    public partial class Form1 : Form
    {
        private readonly IGroupService _groupService;
        private readonly IPropertyService _propertyService;
        private readonly IRelationService _relationService;
        
        public Form1(
            IGroupService groupService,
            IPropertyService propertyService,
            IRelationService relationService)
        {
            InitializeComponent();
            treeView.BeforeExpand += TreeView_BeforeExpand;
            FormClosing += Form1_FormClosing;
            InitRootGroup();
            
            _groupService = groupService;
            _propertyService = propertyService;
            _relationService = relationService; 
            
            gbEditGroup.Visible = false;
            gbEditProperty.Visible = false;

        }

        public void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _context.Database.CloseConnection();
        }

        private void TreeView_BeforeExpand(object? sender, TreeViewCancelEventArgs e)
        {
            var expandedNode = e.Node;
            if (expandedNode is null)
            {
                MessageBox.Show("Пустые узлы");
                return;
            }

            expandedNode.Nodes.Clear();

            var (id, type) = ParseNodeKey(expandedNode.Name);
            
            if (type == "Property")
            {
                return;
            }
            
            var childGroups = await _groupService.GetChildGroupsAsync(id);
            foreach (var childGroup in childGroups)
            {
                var childNode = CreateTreeNode(childGroup);
                expandedNode.Nodes.Add(childNode);
            }
            
            var properties = await _propertyService.GetGroupPropertiesAsync(id);
            foreach (var property in properties)
            {
                var propertyNode = CreateTreeNode(property);
                expandedNode.Nodes.Add(propertyNode);
            }
        }

        public void InitRootGroup()
        {
            var firstGroup = await _groupService.GetGroupAsync(1);
            if (firstGroup is null)
            {
                MessageBox.Show("Корневая группа отсутствует в базе данных");
                return;
            }

            var rootNode = CreateTreeNode(firstGroup);
            
            treeView.Nodes.Add(rootNode);
        }

        private void miAddGroup_Click(object sender, EventArgs e)
        {
            var selectedNode = treeView.SelectedNode;
            var type = selectedNode.Name.Split("|")[1];
            if (type == "Property")
            {
                MessageBox.Show("Выберите группу, а не свойство");
                return;
            }

            gbEditProperty.Visible = false;
            gbEditGroup.Visible = true;
            tbGroupName.Text = string.Empty;
            tbGroupId.Text = await _groupService.GetNextGroupIdAsync().ToString();
        }

        private void miAddProperty_Click(object sender, EventArgs e)
        {
            gbEditGroup.Visible = false;

            var selectedNode = treeView.SelectedNode;

            if (selectedNode == null) return;

            var (id, type) = ParseNodeKey(selectedNode.Name);
            
            if (type == "Property")
            {
                MessageBox.Show("Выберите группу, в которую хотите добавить свойство");
                gbEditProperty.Visible = false;
                return;
            }

            gbEditProperty.Visible = true;

            tbPropertyName.Text = string.Empty;
            tbPropertyValue.Text = string.Empty;
            tbPropertyGroupId.Text = id.ToString();
        }

        private void miEdit_Click(object sender, EventArgs e)
        {
            var selectedNode = treeView.SelectedNode;

            if (selectedNode is null) return;

            var (id, type) = ParseNodeKey(selectedNode.Name);

            if (type == "Group")
            {
                gbEditGroup.Visible = true;
                gbEditProperty.Visible = false;

                tbGroupName.Text = selectedNode.Text;
                tbGroupId.Text = id;
            }
            else if (type == "Property")
            {
                gbEditGroup.Visible = false;
                gbEditProperty.Visible = true;

                var property = await _propertyService.GetPropertyAsync(id);

                if (property is null) return;
                tbPropertyName.Text = selectedNode.Text;
                tbPropertyValue.Text = property.Value;
                tbPropertyGroupId.Text = property.GroupId.ToString();
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            var selectedNode = treeView.SelectedNode;

            if (selectedNode is null)
            {
                MessageBox.Show("Выберите узел");
                return;
            }

            var (id, type) = ParseNodeKey(selectedNode.Name);

            if (type == "Group")
            {
                var parentRelations = await _relationService.GetParentRelationsAsync(id);
                var childRelations = await _relationService.GetChildRelationsAsync(id);
                var properties = await _propertyService.GetGroupPropertiesAsync(id);

                foreach (var parentRelation in parentRelations)
                {
                    await _relationService.DeleteRelationAsync(id, parentRelation.ChildId);
                }

                foreach (var childRelation in childRelations)
                {
                    await _relationService.DeleteRelationAsync(childRelation.ParentId, id);
                }

                foreach (var property in properties)
                {
                    await _propertyService.DeletePropertyAsync(property.Id);
                }

                await _groupService.DeleteGroupAsync(id);
            }
            else if (type == "Property")
            {
                await _propertyService.DeletePropertyAsync(id);
            }

            selectedNode.Remove();
        }

        private void btnGroupSave_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt64(tbGroupId.Text);
            var name = tbGroupName.Text;

            var selectedNode = treeView.SelectedNode;
            
            var (parentId, type) = ParseNodeKey(selectedNode.Name);

            if (await _groupService.IsGroupExistsAsync(id))
            {
                await _groupService.UpdateGroupAsync(id, name);

                MessageBox.Show("Группа изменена");

                selectedNode.Text = name;
            }
            else
            {
                await _groupService.CreateGroupAsync(name);

                await _relationService.CreateRelationAsync(parentId, id);

                MessageBox.Show("Группа добавлена");

                selectedNode.Collapse();
                selectedNode.Expand();
            }



            tbGroupName.Text = string.Empty;
            tbGroupId.Text = string.Empty;
            gbEditGroup.Visible = false;
        }

        private void btnGroupCancel_Click(object sender, EventArgs e)
        {
            tbGroupName.Text = string.Empty;
            tbGroupId.Text = string.Empty;
            gbEditGroup.Visible = false;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            gbEditGroup.Visible = false;
            gbEditProperty.Visible = false;
        }

        private void btnPropertySave_Click(object sender, EventArgs e)
        {
            var name = tbPropertyName.Text;
            var value = tbPropertyValue.Text;
            var groupId = Convert.ToInt64(tbPropertyGroupId.Text);

            var selectedNode = treeView.SelectedNode;
            var (id, type) = ParseNodeKey(selectedNode.Name);

            if (type == "Group")
            {
                await _propertyService.CreatePropertyAsync(name, value, groupId);

                MessageBox.Show("Свойство добавлено");

                selectedNode.Collapse();
                selectedNode.Expand();
            }
            else if (type == "Property")
            {
                await _propertyService.UpdatePropertyAsync(id, name, value);
                
                MessageBox.Show("Свойство изменено");

                selectedNode.Text = name;
            }

            tbPropertyName.Text = string.Empty;
            tbPropertyValue.Text = string.Empty;
            tbPropertyGroupId.Text = string.Empty;
            gbEditProperty.Visible = false;
        }

        private void btnPropertyCancel_Click(object sender, EventArgs e)
        {
            tbPropertyName.Text = string.Empty;
            tbPropertyValue.Text = string.Empty;
            tbPropertyGroupId.Text = string.Empty;
            gbEditProperty.Visible = false;
        }

        private (long id, string type) ParseNodeKey(string nodeKey)
        {
            var parts = nodeKey.Split('|');
            if (parts.Length != 2)
                throw new InvalidOperationException($"Неверный формат Name: {nodeKey}");

            var id = Convert.ToInt64(parts[0]);
            var type = parts[1];

            return (id, type);
        }
        
        private TreeNode CreateTreeNode(TGroup group)
        {
            var node = new TreeNode
            {
                Text = group.Name,
                Name = $"{group.Id}|Group"
            };
            
            node.Nodes.Add(new TreeNode
            {
                Text = "TechnicalGroup",
                Name = "TechnicalGroup"
            });

            return node;
        }
        
        private TreeNode CreateTreeNode(TProperty property)
        {
            var node = new TreeNode
            {
                Text = property.Name,
                Name = $"{property.Id}|Property"
            };

            node.Nodes.Add(new TreeNode
            {
                Text = "TechnicalGroup",
                Name = "TechnicalGroup"
            });

            return node;
        }
        
        
    }   
}
