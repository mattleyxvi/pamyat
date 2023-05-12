namespace pamyat
{
    public partial class Form1 : Form
    {
        private int r1, r2;
        private int _height = 1000;
        private int _width = 1700;
        private int _size = 100;
        private PictureBox rememberBox = new PictureBox();
        private List<Button> _buttons = new List<Button>();
        private List<Point> button_coords = new List<Point>();
        private int order_ind = 0,choose_ind = 0;
        private Button _reset = new Button();
        private bool newbox;
        public Form1()
        {
            InitializeComponent();
            this.Width = _width; this.Height = _height;
            rememberBox.BackColor = Color.Brown;
            rememberBox.Size = new Size(_size, _size);

            timer.Tick += new EventHandler(_spawn);
            timer.Interval = 1000;
            timer1.Interval = 1;
            timer1.Tick += new EventHandler(_spawn_button);

            _reset.Location = new Point(800, 850);
            _reset.Text = "Начать новую игру";
            _reset.Click += new EventHandler(_start);
            _reset.Size = new Size(200,40);
            this.Controls.Add(_reset);
        }

        private void _spawn(object sender, EventArgs e)
        {
            if (button_coords.Count < 6)
            {
                Random r = new Random();
                r1 = r.Next(100, _width - _size);
                r2 = r.Next(100, _height - 3*_size);
                Point point = new Point(r1, r2);
                
                foreach(Point p in button_coords)
                {
                    if (Math.Abs(p.X-point.X)<100 && Math.Abs(p.Y - point.Y) < 100)
                    {
                        newbox = false;
                    }
                         
                }
                
                if (newbox)
                {
                    rememberBox.Location = point;
                    button_coords.Add(point);
                    this.Controls.Add(rememberBox);
                }
                newbox = true;
            }
            else
            {
                timer.Stop();
                this.Controls.Remove(rememberBox);
                timer1.Start();

            }
        }
        private void _spawn_button(object sender, EventArgs e)
        {
            if (order_ind < 6)
            {
                Button b = new Button();
                b.BackColor = Color.White;
                b.Size = new Size(_size, _size);
                b.Click += new EventHandler(_checkOrder);
                b.Location = button_coords[order_ind];
                this.Controls.Add(b);
                _buttons.Add(b);
                order_ind++;

            }
            else
            {
                timer1.Stop();

            }
        }
        private void _checkOrder(object sender, EventArgs e)
        {
            Button button = sender as Button;
            foreach (Point p in  button_coords)
            {
                if (button.Location == p)
                {
                    _decider(button_coords.IndexOf(p) == choose_ind,button,p); 
                }
                
            }
            button.Click -= new EventHandler(_checkOrder);

        }
        private void _start(object sender, EventArgs e)
        {
            this.Controls.Clear();
            button_coords = new List<Point>();
            _buttons = new List<Button>();
            order_ind = 0;
            choose_ind = 0;
            timer.Start();
        }
        private void _decider(bool istrue, Button button,Point p)
        {
            if (istrue && choose_ind == button_coords.Count() - 1)
            {
                button.Text = (button_coords.IndexOf(p) + 1).ToString();
                foreach (Button b in _buttons)
                {
                    b.BackColor = Color.Green;
                }
                this.Controls.Add(_reset);
            }
            else if (istrue)
            {
                button.Text = (button_coords.IndexOf(p) + 1).ToString();
                choose_ind++;
            }
            else
            {
                foreach (Button b in _buttons)
                {
                    b.Text = (_buttons.IndexOf(b) + 1).ToString();
                    b.Click -= new EventHandler(_checkOrder);
                }
                button.BackColor = Color.Red;
                this.Controls.Add(_reset);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}