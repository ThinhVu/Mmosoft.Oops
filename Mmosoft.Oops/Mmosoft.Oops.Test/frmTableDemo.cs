using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mmosoft.Oops;
using Mmosoft.Oops.Controls.Table;
using System.Drawing;

namespace Mmosoft.OopsTest
{
    public partial class frmTableDemo : Form
    {
        public frmTableDemo()
        {
            InitializeComponent();         
        }

        private void frmTableDemo_Load(object sender, EventArgs e)
        {
            InitTitleBar();
            InitTable();
        }

        private void InitTitleBar()
        {
            titleBar1.MaximizeEnable = false;
            titleBar1.OnCloseClicked += (s, e) => this.Close();
            titleBar1.OnMinimizeClicked += (s, e) => this.WindowState = FormWindowState.Minimized;
            titleBar1.OnMouseDragging += (s, e) => { this.Left += e.OffsetX; this.Top += e.OffsetY; };
        }

        private void InitTable()
        {
            Table<Song> table1 = new Table<Song>();
            table1.Font = new Font("Segoe UI", 8f, FontStyle.Regular);
            table1.HeaderFont = new Font("Segoe UI", 10f, FontStyle.Regular);
            table1.BackgroundImage = Oops.Test.Properties.Resources._525625bb4317fb9;
            table1.BackColor = Color.Transparent;
            table1.Location = new Point(1, 40);
            table1.Size = new Size(540, 490);
            //
            table1.AddColumns(new List<Column>
            {
                new Column() { Title = "Id", Width = 40, MappingProperty = "Id" },
                new Column() { Title = "Song Title", Width = 200, MappingProperty = "Name" },
                new Column() { Title = "Duration", Width = 150, MappingProperty = "Length" },
                new Column() { Title = "Singer", Width = 150, MappingProperty = "Singer" },
            });
            table1.AddModels(new List<Song>
            {
                new Song{ Id = 0, Name = "7 Rings", Length = 3.52f, Singer = "Ariana Grande" },
                new Song{ Id = 1, Name = "Without Me", Length = 3.52f, Singer = "Halsey" },
                new Song{ Id = 2, Name = "Comethru", Length = 3.52f, Singer = "Jeremy Zucker" },
                new Song{ Id = 3, Name = "Thank U, Next", Length = 3.52f, Singer = "Ariana Grande" },
                new Song{ Id = 4, Name = "One Call Away", Length = 3.52f, Singer = "Charlie Puth" },
                new Song{ Id = 5, Name = "That Girl", Length = 3.52f, Singer = "Olly Murs" },
                new Song{ Id = 6, Name = "Shape Of You", Length = 3.52f, Singer = "Ed Sheeran" },
                new Song{ Id = 7, Name = "What Do You Mean?", Length = 3.52f, Singer = "Justin Bieber" },
                new Song{ Id = 8, Name = "Girls Like You", Length = 3.52f, Singer = "Maroon 5, Cardi B" },
                new Song{ Id = 9, Name = "Solo", Length = 3.52f, Singer = "Clean Bandit" },     
                //
                new Song{ Id = 10, Name = "7 Rings", Length = 3.52f, Singer = "Ariana Grande" },
                new Song{ Id = 11, Name = "Without Me", Length = 3.52f, Singer = "Halsey" },
                new Song{ Id = 12, Name = "Comethru", Length = 3.52f, Singer = "Jeremy Zucker" },
                new Song{ Id = 13, Name = "Thank U, Next", Length = 3.52f, Singer = "Ariana Grande" },
                new Song{ Id = 14, Name = "One Call Away", Length = 3.52f, Singer = "Charlie Puth" },
                new Song{ Id = 15, Name = "That Girl", Length = 3.52f, Singer = "Olly Murs" },
                new Song{ Id = 16, Name = "Shape Of You", Length = 3.52f, Singer = "Ed Sheeran" },
                new Song{ Id = 17, Name = "What Do You Mean?", Length = 3.52f, Singer = "Justin Bieber" },
                new Song{ Id = 18, Name = "Girls Like You", Length = 3.52f, Singer = "Maroon 5, Cardi B" },
                new Song{ Id = 19, Name = "Solo", Length = 3.52f, Singer = "Clean Bandit" },     
                //
                new Song{ Id = 20, Name = "7 Rings", Length = 3.52f, Singer = "Ariana Grande" },
                new Song{ Id = 21, Name = "Without Me", Length = 3.52f, Singer = "Halsey" },
                new Song{ Id = 22, Name = "Comethru", Length = 3.52f, Singer = "Jeremy Zucker" },
                new Song{ Id = 23, Name = "Thank U, Next", Length = 3.52f, Singer = "Ariana Grande" },
                new Song{ Id = 24, Name = "One Call Away", Length = 3.52f, Singer = "Charlie Puth" },
                new Song{ Id = 25, Name = "That Girl", Length = 3.52f, Singer = "Olly Murs" },
                new Song{ Id = 26, Name = "Shape Of You", Length = 3.52f, Singer = "Ed Sheeran" },
                new Song{ Id = 27, Name = "What Do You Mean?", Length = 3.52f, Singer = "Justin Bieber" },
                new Song{ Id = 28, Name = "Girls Like You", Length = 3.52f, Singer = "Maroon 5, Cardi B" },
                new Song{ Id = 29, Name = "Solo", Length = 3.52f, Singer = "Clean Bandit" },     
                //
            });
            //
            table1.RenderTable();

            this.Controls.Add(table1);
        }
    }

    public class Song // model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Length { get; set; }
        public string Singer { get; set; } // format for date time
    }
}
