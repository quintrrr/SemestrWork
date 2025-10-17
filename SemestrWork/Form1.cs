using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SemestrWork
{
    public partial class Form1 : Form
    {
        private AppContext _context;

        public Form1()
        {
            InitializeComponent();
            treeView.BeforeExpand += TreeView_BeforeExpand;
            FormClosing += Form1_FormClosing;
            _context = new AppContext("AppConnection");
            InitRootGroup();
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

            var id = Convert.ToInt64(expandedNode.Name.Split("|")[0]);
            var type = expandedNode.Name.Split("|")[1];
            if (type == "Property")
            {
                return;
            }

            var relationsGroups = ReadTRelationByParentId(id);
            var childGroups = new List<TGroup>();
            foreach (var relationsGroup in relationsGroups)
            {
                var childGroup = ReadTGroupById(relationsGroup.ChildId);
                if (childGroup is not null) childGroups.Add(childGroup);
            }
            foreach (var childGroup in childGroups)
            {
                var child = new TreeNode()
                {
                    Text = childGroup.Name,
                    Name = $"{childGroup.Id}|Group"
                };

                var techNode = new TreeNode
                {
                    Text = "TechnicalGroup",
                    Name = "TechnicalGroup"
                };

                child.Nodes.Add(techNode);
                expandedNode.Nodes.Add(child);
            }

            var groupProperties = ReadTPropertyByGroupId(id);
            foreach (var groupProperty in groupProperties)
            {
                var property = new TreeNode()
                {
                    Text = groupProperty.Name,
                    Name = $"{groupProperty.Id}|Property"
                };

                var techNode = new TreeNode
                {
                    Text = "TechnicalGroup",
                    Name = "TechnicalGroup"
                };

                property.Nodes.Add(techNode);
                expandedNode.Nodes.Add(property);
            }
        }

        public void InitRootGroup()
        {
            var firstGroup = ReadTGroupById(1);
            if (firstGroup is null)
            {
                MessageBox.Show("Корневая группа отсутствует в базе данных");
                return;
            }

            var rootNode = new TreeNode
            {
                Text = firstGroup.Name,
                Name = $"{firstGroup.Id}|Group"
            };
            var techNode = new TreeNode
            {
                Text = "TechnicalGroup",
                Name = "TechnicalGroup"
            };

            rootNode.Nodes.Add(techNode);
            treeView.Nodes.Add(rootNode);
        }


        public List<TGroup> ReadTGroup()
        {
            return _context.Groups
                .AsNoTracking()
                .ToList();
        }

        public TGroup? ReadTGroupById(long id)
        {
            return _context.Groups
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }

        private void CreateTGroup(string name)
        {
            if (_context == null) return;

            var maxId = _context.Groups.Any() ? _context.Groups.Max(x => x.Id) + 1 : 1;

            var newGroup = new TGroup()
            {
                Id = maxId,
                Name = name
            };

            _context.Groups.Add(newGroup);
        }

        private void DeleteTGroup(long id)
        {
            if (_context == null) return;

            var groupForDelete = _context.Groups.SingleOrDefault(x => x.Id == id);

            if (groupForDelete == null)
            {
                MessageBox.Show($@"Группа с id = {id} не найдена!");
                return;
            }

            _context.Groups.Remove(groupForDelete);
        }

        private void UpdateTGroup(long id, string name)
        {
            if (_context == null) return;

            var groupForUpdate = _context.Groups.FirstOrDefault(x => x.Id == id);

            if (groupForUpdate == null)
            {
                MessageBox.Show(@"Группа, предназначенная для обновления, не найдена");
                return;
            }

            groupForUpdate.Name = name;
        }


        public List<TRelation> ReadTRelation()
        {
            return _context.Relations
                .AsNoTracking()
                .ToList();
        }

        public List<TRelation> ReadTRelationByParentId(long id)
        {
            return _context.Relations.Where(x => x.ParentId == id).ToList();
        }

        public List<TRelation> ReadTRelationByChildId(long id)
        {
            return _context.Relations
                .AsNoTracking()
                .Where(x => x.ChildId == id)
                .ToList();
        }

        private void CreateTRelation(long parentId, long childId)
        {
            if (_context == null) return;

            if (_context.Relations.Any(x => x.ParentId == parentId && x.ChildId == childId))
            {
                MessageBox.Show("Такая связь уже существует");
                return;
            }

            var newRelation = new TRelation()
            {
                ParentId = parentId,
                ChildId = childId
            };

            _context.Relations.Add(newRelation);
        }

        private void DeleteTRelation(long parentId, long childId)
        {
            if (_context == null) return;

            var relationForDelete = _context.Relations
                .FirstOrDefault(x => x.ParentId == parentId && x.ChildId == childId);

            if (relationForDelete == null)
            {
                MessageBox.Show("Такой связи не существует");
                return;
            }

            _context.Relations.Remove(relationForDelete);
        }

        public List<TProperty> ReadTProperty()
        {
            return _context.Properties.AsNoTracking().ToList();
        }

        public TProperty? ReadTPropertyById(long id)
        {
            return _context.Properties.AsNoTracking().SingleOrDefault(g => g.Id == id);
        }

        public List<TProperty> ReadTPropertyByGroupId(long groupId)
        {
            return _context.Properties.AsNoTracking().Where(p => p.GroupId == groupId).ToList();
        }

        private void CreateTProperty(string name, string value, long groupId)
        {
            if (_context == null) return;

            var maxId = _context.Properties.Any() ? _context.Properties.Max(x => x.Id) + 1 : 1;

            var newProperty = new TProperty()
            {
                Id = maxId,
                Name = name,
                Value = value,
                GroupId = groupId
            };

            _context.Properties.Add(newProperty);
        }

        private void DeleteTProperty(long id)
        {
            if (_context == null) return;

            var propertyForDelete = _context.Properties.SingleOrDefault(x => x.Id == id);

            if (propertyForDelete == null)
            {
                MessageBox.Show($@"Свойство с id = {id} не найдено!");
                return;
            }

            _context.Properties.Remove(propertyForDelete);
        }

        private void UpdateTProperty(long id, string name, string value)
        {
            if (_context == null) return;

            var propertyForUpdate = _context.Properties.FirstOrDefault(x => x.Id == id);

            if (propertyForUpdate == null)
            {
                MessageBox.Show(@"Группа, предназначенная для обновления, не найдена");
                return;
            }

            propertyForUpdate.Name = name;
            propertyForUpdate.Value = value;
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
            tbGroupId.Text = (_context.Groups.Any() ? _context.Groups.Max(x => x.Id) + 1 : 1)
                .ToString();


        }

        private void miAddProperty_Click(object sender, EventArgs e)
        {
            gbEditGroup.Visible = false;

            var selectedNode = treeView.SelectedNode;

            if (selectedNode == null) return;

            var id = Convert.ToInt64(selectedNode.Name.Split("|")[0]);
            var type = selectedNode.Name.Split("|")[1];
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

            var id = selectedNode.Name.Split("|")[0];
            var type = selectedNode.Name.Split("|")[1];

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

                var property = ReadTPropertyById(Convert.ToInt64(id));

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

            var id = Convert.ToInt64(selectedNode.Name.Split("|")[0]);
            var type = selectedNode.Name.Split("|")[1];

            if (type == "Group")
            {
                var parentRelations = ReadTRelationByParentId(id);
                var childRelations = ReadTRelationByChildId(id);
                var properties = ReadTPropertyByGroupId(id);

                foreach (var parentRelation in parentRelations)
                {
                    DeleteTRelation(id, parentRelation.ChildId);
                }

                foreach (var childRelation in childRelations)
                {
                    DeleteTRelation(childRelation.ParentId, id);
                }

                foreach (var property in properties)
                {
                    DeleteTProperty(property.Id);
                }
                _context.SaveChanges();

                DeleteTGroup(id);
            }
            else if (type == "Property")
            {
                DeleteTProperty(id);
            }

            _context.SaveChanges();

            selectedNode.Remove();
        }

        private void btnGroupSave_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt64(tbGroupId.Text);
            var name = tbGroupName.Text;

            var selectedNode = treeView.SelectedNode;
            var parentId = Convert.ToInt64(selectedNode.Name.Split("|")[0]);
            var type = selectedNode.Name.Split("|")[1];

            if (_context.Groups.Any(g => g.Id == id))
            {
                UpdateTGroup(id, name);

                _context.SaveChanges();

                MessageBox.Show("Группа изменена");

                selectedNode.Text = name;
            }
            else
            {
                CreateTGroup(name);

                CreateTRelation(parentId, id);

                _context.SaveChanges();

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
            var id = Convert.ToInt64(selectedNode.Name.Split("|")[0]);
            var type = selectedNode.Name.Split("|")[1];

            if (type == "Group")
            {
                CreateTProperty(name, value, groupId);

                _context.SaveChanges();

                MessageBox.Show("Свойство добавлено");

                selectedNode.Collapse();
                selectedNode.Expand();
            }
            else if (type == "Property")
            {
                UpdateTProperty(id, name, value);

                _context.SaveChanges();

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
    }

    [Table("tgroup")]
    public class TGroup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }

    [Table("trelation")]
    [PrimaryKey(nameof(ParentId), nameof(ChildId))]
    public class TRelation
    {
        [Column("id_parent")]
        public long ParentId { get; set; }
        [Column("id_child")]
        public long ChildId { get; set; }
    }

    [Table("tproperty")]
    public class TProperty
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("value")]
        public string Value { get; set; }
        [Column("id_group")]
        public long GroupId { get; set; }
    }

    public class AppContext : DbContext 
    {
        public DbSet<TGroup> Groups { get; set; }
        public DbSet<TRelation> Relations { get; set; }
        public DbSet<TProperty> Properties { get; set; }

        public AppContext(string connectionName)
        : base(new DbContextOptionsBuilder<AppContext>()
            .UseNpgsql(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString)
            .Options)
        { }

    }

   
}
