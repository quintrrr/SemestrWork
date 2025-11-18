using Core.DTO;
using SemestrWork.ApiClients;
using System.Net;

namespace SemestrWork
{
    public partial class Form1 : Form
    {
        private readonly GroupsApiClient _groupsApiClient;
        private readonly PropertiesApiClient _propertiesApiClient;
        private readonly RelationsApiClient _relationsApiClient;

        public Form1(
            GroupsApiClient groupsApiClient,
            PropertiesApiClient propertiesApiClient,
            RelationsApiClient relationsApiClient)
        {
            InitializeComponent();
            treeView.AfterExpand += TreeView_AfterExpand;

            _groupsApiClient = groupsApiClient;
            _propertiesApiClient = propertiesApiClient; 
            _relationsApiClient = relationsApiClient;

            InitRootGroup();
            
            gbEditGroup.Visible = false;
            gbEditProperty.Visible = false;

        }

        private async void TreeView_AfterExpand(object? sender, TreeViewEventArgs e)
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

            var childGroups = await _groupsApiClient.GetChildGroupsAsync(id);
            foreach (var childGroup in childGroups)
            {
                var childNode = CreateTreeNode(childGroup);
                expandedNode.Nodes.Add(childNode);
            }

            var properties = await _propertiesApiClient.GetGroupPropertiesAsync(id);
            foreach (var property in properties)
            {
                var propertyNode = CreateTreeNode(property);
                expandedNode.Nodes.Add(propertyNode);
            }

        }

        public async void InitRootGroup()
        {
            var firstGroup = await _groupsApiClient.GetGroupAsync(1);
            if (firstGroup is null)
            {
                MessageBox.Show("Корневая группа отсутствует в базе данных");
                return;
            }

            var rootNode = CreateTreeNode(firstGroup);
            
            treeView.Nodes.Add(rootNode);
        }

        private async void miAddGroup_Click(object sender, EventArgs e)
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
            tbGroupId.Text = (await _groupsApiClient.GetNextGroupIdAsync()).ToString();
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

        private async void miEdit_Click(object sender, EventArgs e)
        {
            var selectedNode = treeView.SelectedNode;

            if (selectedNode is null) return;

            var (id, type) = ParseNodeKey(selectedNode.Name);

            if (type == "Group")
            {
                gbEditGroup.Visible = true;
                gbEditProperty.Visible = false;

                tbGroupName.Text = selectedNode.Text;
                tbGroupId.Text = id.ToString();
            }
            else if (type == "Property")
            {
                gbEditGroup.Visible = false;
                gbEditProperty.Visible = true;

                var property = await _propertiesApiClient.GetPropertyAsync(id);

                if (property is null) return;
                tbPropertyName.Text = selectedNode.Text;
                tbPropertyValue.Text = property.Value;
                tbPropertyGroupId.Text = property.GroupId.ToString();
            }
        }

        private async void miDelete_Click(object sender, EventArgs e)
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
                var code = await _groupsApiClient.DeleteGroupAsync(id);

                MessageBox.Show(code == HttpStatusCode.NotFound ? "Группа не найдена" : "Группа успешно удалена");
            }
            else if (type == "Property")
            {
                var code = await _propertiesApiClient.DeletePropertyAsync(id);

                MessageBox.Show(code == HttpStatusCode.NotFound ? "Свойство не найдено" : "Свойство успешно удалено");
            }
           
            selectedNode.Remove();
        }

        private async void btnGroupSave_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt64(tbGroupId.Text);
            var name = tbGroupName.Text;

            var selectedNode = treeView.SelectedNode;
            
            var (parentId, type) = ParseNodeKey(selectedNode.Name);

            var dto = new SaveGroupDTO { GroupId = id, Name = name, ParentId = parentId };

            var code = await _groupsApiClient.SaveGroupAsync(dto);

            if (code == HttpStatusCode.NoContent)
            {
                MessageBox.Show("Группа изменена");

                selectedNode.Text = name;
            }
            else if (code == HttpStatusCode.Created)
            {
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

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            gbEditGroup.Visible = false;
            gbEditProperty.Visible = false;
        }

        private async void btnPropertySave_Click(object sender, EventArgs e)
        {
            var name = tbPropertyName.Text;
            var value = tbPropertyValue.Text;
            var groupId = Convert.ToInt64(tbPropertyGroupId.Text);

            var selectedNode = treeView.SelectedNode;
            var (id, type) = ParseNodeKey(selectedNode.Name);

            var dto = new SavePropertyDTO { GroupId = groupId, Name = name, Value = value, PropertyId = type == "Property" ? id : null };
            
            var code = await _propertiesApiClient.SavePropertyAsync(dto);
            
            if (code == HttpStatusCode.Created)
            {
                MessageBox.Show("Свойство добавлено");

                selectedNode.Collapse();
                selectedNode.Expand();
            }
            else if (code == HttpStatusCode.NoContent)
            {
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

        private static (long id, string type) ParseNodeKey(string nodeKey)
        {
            var parts = nodeKey.Split('|');
            if (parts.Length != 2)
                throw new InvalidOperationException($"Неверный формат Name: {nodeKey}");

            var id = Convert.ToInt64(parts[0]);
            var type = parts[1];

            return (id, type);
        }
        
        private static TreeNode CreateTreeNode(TGroupDTO group)
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
        
        private static TreeNode CreateTreeNode(TPropertyDTO property)
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
