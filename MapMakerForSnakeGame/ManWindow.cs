using Newtonsoft.Json;

using Snake;

using System.Diagnostics;
using System.Text;

namespace MapMakerForSnakeGame
{
    public partial class ManWindow : Form
    {
        public ManWindow()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            var strOfTextBox = textBox1.Text;
            var strs = strOfTextBox.Split(" ");
            var row = Convert.ToInt32(strs[0]);
            var coulmn = Convert.ToInt32(strs[strs.Length-1]);

            MapSize mapSize = new MapSize();
            mapSize.row = row;
            mapSize.coulmn = coulmn;
            GameMap gameMap = new GameMap(mapSize);

            var firstRow = gameMap._Map[0];
            var LastRow = gameMap._Map[gameMap._Map.Count -1];

            List<Cell> firstCoulmn = new List<Cell>();
            foreach (var cellRow in gameMap._Map)
            {
                firstCoulmn.Add(cellRow[0]);
            }

            List<Cell> lastCoulmn= new List<Cell>();
            foreach (var cellRow in gameMap._Map)
            {
                lastCoulmn.Add(cellRow[cellRow.Count-1]);
            }

            MakeItWall(firstRow);
            MakeItWall(LastRow);
            MakeItWall(firstCoulmn);
            MakeItWall(lastCoulmn);

            textBox1.Text = JsonConvert.SerializeObject(gameMap._Map);

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Set the default file extension
            saveFileDialog.DefaultExt = "txt";

            // Set the default filename (optional)
            saveFileDialog.FileName = "myFile";

            // Set the filter for file extension and description
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            // Display the SaveFileDialog by calling ShowDialog method
            DialogResult result = saveFileDialog.ShowDialog();

            // Check if the user clicked the OK button
            Debug.WriteLine(saveFileDialog.FileName);

            var s=File.Create(saveFileDialog.FileName);
            s.Write(Encoding.UTF8.GetBytes(textBox1.Text));
            s.Close();

        }
        private void MakeItWall(List<Cell> cellList)
        {
            foreach (var cell in cellList)
            {
                cell.IsWall = true;
            }
        }
    }
}
